﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Rye.EventBus.Abstractions;
using Rye.EventBus.RabbitMQ.Options;

using System;

namespace Rye.EventBus.RabbitMQ
{
    public static class RabbitMQEventBusServiceCollectionExtensions
    {
        /// <summary>
        /// 添加适用于Rabbit MQ的事件总线
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection serviceCollection, Action<RabbitMQEventBusOptions> action)
        {
            var options = new RabbitMQEventBusOptions();
            action(options);

            serviceCollection.AddEventBus<IRabbitMQEventBus>(service => new RabbitMQEventBus(
                options, 
                service.GetRequiredService<IServiceScopeFactory>(),
                service.GetRequiredService<ILogger<RabbitMQEventBus>>()));
            serviceCollection.AddEventPublisher<IRabbitMQEventPublisher>(service => service.GetService<IRabbitMQEventBus>());
            serviceCollection.AddEventSubscriber<IRabbitMQEventSubscriber>(service => service.GetService<IRabbitMQEventBus>());
            serviceCollection.AddEventBus<IEventBus>(sevice => sevice.GetService<IRabbitMQEventBus>());
            serviceCollection.AddEventPublisher<IEventPublisher>(sevice => sevice.GetService<IRabbitMQEventBus>());
            serviceCollection.AddEventSubscriber<IEventSubscriber>(sevice => sevice.GetService<IRabbitMQEventBus>());

            return serviceCollection;
        }

        /// <summary>
        /// 添加适用于Rabbit MQ的事件总线模块
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBusModule(this IServiceCollection serviceCollection, Action<RabbitMQEventBusOptions> action)
        {
            var module = new RabbitMQEventBusModule(action);
            return serviceCollection.AddModule(module);
        }

        /// <summary>
        /// 订阅Rabbit MQ EventBus
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="subscriberAction"></param>
        /// <returns></returns>
        public static IServiceCollection SubscriberRabbitMQEventBus(this IServiceCollection serviceCollection, Action<IServiceProvider, IRabbitMQEventBus> subscriberAction)
        {
            return serviceCollection.Subscriber<IRabbitMQEventBus>(subscriberAction);
        }
    }
}
