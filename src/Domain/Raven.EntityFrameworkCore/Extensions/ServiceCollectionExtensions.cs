﻿using Raven.DataAccess;
using Raven.EntityFrameworkCore.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Raven.EntityFrameworkCore;

namespace Raven
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Raven框架对数据库的支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRavenDatabase(this IServiceCollection services)
        {
            services.RemoveAll<IDbProvider>();
            services.RemoveAll<IUnitOfWorkFactory>();
            services.RemoveAll<IRepositoryFactory>();
            services.TryAddScoped<IDbProvider, DbProvider>();
            services.TryAddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.TryAddSingleton<IRepositoryFactory, RepositoryFactory>();
            return services;
        }

        /// <summary>
        /// 添加DbContextOptionsBuilderOptions配置选项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbBuilderOptions(this IServiceCollection services, string dbName, Action<DbContextOptionsBuilder> builderAction = null)
        {
            var builder = new DbContextOptionsBuilder();
            builderAction(builder);
            services.TryAddSingleton<DbContextOptionsBuilderOptions>(new DbContextOptionsBuilderOptions(builder, dbName));
            return services;
        }

        /// <summary>
        /// 添加DbContextOptionsBuilderOptions配置选项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbName"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbBuilderOptions<TContext>(this IServiceCollection services, string dbName, Action<DbContextOptionsBuilder<TContext>> builderAction = null)
            where TContext: DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builderAction(builder);
            services.TryAddSingleton<DbContextOptionsBuilderOptions>(new DbContextOptionsBuilderOptions(builder, dbName, typeof(TContext)));
            return services;
        }
    }
}