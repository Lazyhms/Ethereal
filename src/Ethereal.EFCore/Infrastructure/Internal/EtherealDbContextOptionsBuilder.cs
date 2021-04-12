﻿// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel;

namespace Ethereal.EntityFrameworkCore.Infrastructure.Internal
{
    /// <summary>
    /// abstract EtherealDbContextOptionsBuilder
    /// </summary>
    public abstract class EtherealDbContextOptionsBuilder<TExtension> : IRelationalDbContextOptionsBuilderInfrastructure
                                                                       where TExtension : class, IDbContextOptionsExtension, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="EtherealDbContextOptionsBuilder{TExtension}"/> class.
        /// </summary>
        public EtherealDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) => OptionsBuilder = optionsBuilder;

        DbContextOptionsBuilder IRelationalDbContextOptionsBuilderInfrastructure.OptionsBuilder => OptionsBuilder;

        /// <summary>
        /// OptionsBuilder
        /// </summary>
        protected virtual DbContextOptionsBuilder OptionsBuilder { get; }

        /// <summary>
        /// override Equals method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// override GetHashCode method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// override ToString method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string? ToString() => base.ToString();

        /// <summary>
        /// virtual WithOption method
        /// </summary>
        protected virtual EtherealDbContextOptionsBuilder<TExtension> WithOption(Func<TExtension, TExtension> setAction)
        {
            ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(
                setAction(OptionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension()));
            return this;
        }
    }
}