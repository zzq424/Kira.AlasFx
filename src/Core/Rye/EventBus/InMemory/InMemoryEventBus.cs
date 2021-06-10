﻿using Microsoft.Extensions.DependencyInjection;

using Rye.EventBus.Abstractions;
using Rye.EventBus.InMemory.Internal;
using Rye.EventBus.InMemory.Options;
using Rye.Util;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rye.EventBus.InMemory
{
    public class InMemoryEventBus : IMemoryEventBus
    {
        private readonly Disruptor.Dsl.Disruptor<EventWrapper> _disruptor;
        private readonly Disruptor.RingBuffer<EventWrapper> _ringBuffer;
        private readonly InternalDisruptorHandler _handler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public event Func<IEvent, InMemoryEventContext, Task> OnProducing;

        public event Func<IEvent, InMemoryEventContext, Task> OnProduced;

        public event Func<IEvent, InMemoryEventErrorContext, Task> OnProductError;

        public event Func<IEvent, InMemoryEventContext, Task> OnConsuming;

        public event Func<IEvent, InMemoryEventContext, Task> OnConsumed;

        public event Func<IEvent, InMemoryEventErrorContext, Task> OnConsumeError;

        public InMemoryEventBus(InMemoryEventBusOptions options, IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;
            _handler = new InternalDisruptorHandler();
            _handler.OnConsumeEvent += OnConsumeEvent;

            if (options.OnProducing != null)
                OnProducing = options.OnProducing;
            if (options.OnProduced != null)
                OnProduced = options.OnProduced;
            if (options.OnProductError != null)
                OnProductError = options.OnProductError;
            if (options.OnConsuming != null)
                OnConsuming = options.OnConsuming;
            if (options.OnConsumed != null)
                OnConsumed = options.OnConsumed;
            if (options.OnConsumeError != null)
                OnConsumeError = options.OnConsumeError;

            _disruptor = new Disruptor.Dsl.Disruptor<EventWrapper>(
                eventFactory: () => new EventWrapper(),
                ringBufferSize: options.BufferSize,
                taskScheduler: TaskScheduler.Default,
                producerType: Disruptor.Dsl.ProducerType.Single,
                waitStrategy: new Disruptor.YieldingWaitStrategy());
            _disruptor.HandleEventsWith(_handler);
            _ringBuffer = _disruptor.Start();
        }

        private async Task OnConsumeEvent(EventWrapper wrapper, List<IEventHandler> handlers)
        {
            var @event = wrapper.Event;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var context = new InMemoryEventContext()
                    {
                        EventBus = this,
                        ServiceProvider = scope.ServiceProvider,
                        RouteKey = wrapper.Route,
                        RetryCount = wrapper.RetryCount
                    };

                    if (OnConsuming != null)
                        await OnConsuming(@event, context);

                    foreach (var handle in handlers)
                    {
                        await handle.OnEvent(wrapper.Event, context);
                    }

                    if (OnConsumed != null)
                        await OnConsumed(@event, context);
                }
                catch (Exception ex)
                {
                    if (OnConsumeError == null)
                        throw;

                    var context = new InMemoryEventErrorContext
                    {
                        EventBus = this,
                        ServiceProvider = scope.ServiceProvider,
                        RouteKey = wrapper.Route,
                        RetryCount = wrapper.RetryCount,
                        Exception = ex
                    };
                    await OnConsumeError(@event, context);
                }
            }
        }

        public async Task PublishAsync(string eventRoute, IEvent @event)
        {
            Check.NotNull(eventRoute, nameof(eventRoute));
            Check.NotNull(@event, nameof(@event));

            if (@event.EventId == 0)
            {
                @event.EventId = IdGenerator.Instance.NextId();
            }

            await PublishAsync(eventRoute, @event, 0);
        }

        private async Task PublishAsync(string eventRoute, IEvent @event, int retryCount = 0)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var context = new InMemoryEventContext
                    {
                        ServiceProvider = scope.ServiceProvider,
                        EventBus = this,
                        RouteKey = eventRoute,
                        RetryCount = retryCount
                    };

                    if (OnProducing != null)
                        await OnProducing(@event, context);

                    long sequence = _ringBuffer.Next();
                    var wapper = _ringBuffer[sequence];
                    wapper.Route = eventRoute;
                    wapper.Event = @event;
                    wapper.RetryCount = retryCount;
                    _ringBuffer.Publish(sequence);

                    if (OnProduced != null)
                        await OnProduced(@event, context);
                }
                catch (Exception ex)
                {
                    if (OnProductError == null)
                        throw;

                    var context = new InMemoryEventErrorContext
                    {
                        ServiceProvider = scope.ServiceProvider,
                        EventBus = this,
                        RouteKey = eventRoute,
                        RetryCount = retryCount,
                        Exception = ex
                    };
                    await OnProductError(@event, context);
                }
            }
        }

        public void Subscribe(string eventRoute, IEnumerable<IEventHandler> handlers)
        {
            Check.NotNull(eventRoute, nameof(eventRoute));
            Check.NotNull(handlers, nameof(handlers));

            _handler.AddHandlers(eventRoute, handlers);
        }

        public Task RetryEvent(IEvent @event, EventContext context)
        {
            return PublishAsync(context.RouteKey, @event, context.RetryCount + 1);
        }

        #region IDisposable Support
        private bool disposedValue = false;
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _disruptor?.Shutdown();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}