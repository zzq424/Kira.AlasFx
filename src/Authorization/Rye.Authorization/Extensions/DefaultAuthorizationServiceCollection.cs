﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Rye.Authorization.Abstraction;
using Rye.Authorization.Abstraction.Builder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rye
{
    public static class DefaultAuthorizationServiceCollection
    {
        /// <summary>
        /// 添加Rye 默认授权服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddRyeAuthorization<TPermissionKey>(this IServiceCollection serviceCollection)
            where TPermissionKey: IEquatable<TPermissionKey>
        {
            return serviceCollection.AddRyeAuthorization(ConfigureBuilder<TPermissionKey>);
        }

        /// <summary>
        /// 增加默认授权模块
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorizationModule<TPermissionKey>(this IServiceCollection serviceCollection)
            where TPermissionKey: IEquatable<TPermissionKey>
        {
            return serviceCollection.AddAuthorizationModule(ConfigureBuilder<TPermissionKey>);
        }

        private static void ConfigureBuilder<TPermissionKey>(IModuleAuthorizationBuilder builder)
            where TPermissionKey: IEquatable<TPermissionKey>
        {
            builder.ConfigureOptions = options =>
            {
                options.InvokeHandlersAfterFailure = false;
                options.AddPolicy("RyePermission", policy => policy.Requirements.Add(new RyeRequirement()));
            };
            builder.UseHandle<RyeDefaultPolicyAuthorizationHandler<TPermissionKey>>();
        }
    }
}
