﻿using Microsoft.EntityFrameworkCore;
using Monica.EntityFrameworkCore.Options;
using System;
using System.Collections.Generic;

namespace Monica.EntityFrameworkCore
{
    public class MonicaDbContextOptionsBuilder
    {
        internal Dictionary<Type, object> Options { get; } = new Dictionary<Type, object>();

        public MonicaDbContextOptionsBuilder AddDbContext<TContext>(string dbName = null, Action<DbContextOptionsBuilder<TContext>> builderAction = null)
            where TContext: DbContext, IDbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builderAction?.Invoke(builder);
            Options[typeof(TContext)] = (new DbContextOptionsBuilderOptions<TContext>(builder, dbName));
            return this;
        }
    }
}
