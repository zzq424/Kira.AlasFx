﻿using Rye.DependencyInjection;

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface,AllowMultiple = true, Inherited = true)]
    public class InjectionAttribute: ScanAttribute
    {
        public InjectionAttribute(params Type[] serviceTypes)
        {
            ServiceTypes = serviceTypes;
        }

        public InjectionAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public InjectionAttribute(InjectionPolicy policy)
        {
            Policy = policy;
        }

        public InjectionAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped, 
            InjectionPolicy policy = InjectionPolicy.Append,
            params Type[] serviceTypes)
        {
            ServiceTypes = serviceTypes;
            Lifetime = lifetime;
            Policy = policy;
        }

        /// <summary>
        /// 服务类型, 如果宿主为类,则默认为当前类型, 如果宿主为接口或抽象类, 则查找所有实现类或继承类进行注入
        /// </summary>
        public Type[] ServiceTypes { get; set; }

        /// <summary>
        /// 注入生命周期, 默认为Scoped
        /// </summary>
        public ServiceLifetime Lifetime { get; } = ServiceLifetime.Scoped;

        /// <summary>
        /// 在服务类型已存在时的注入策略, 默认为追加
        /// </summary>
        public InjectionPolicy Policy { get; set; } = InjectionPolicy.Append;
    }
}
