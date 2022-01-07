// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel;

namespace Ethereal.EntityFrameworkCore.Infrastructure.Internal
{
    /// <summary>
    /// abstract EtherealDbContextOptionsBuilder
    /// </summary>
    public abstract class EtherealDbContextOptionsBuilder<TBuilder, TExtension> : IRelationalDbContextOptionsBuilderInfrastructure
                                                                       where TBuilder : EtherealDbContextOptionsBuilder<TBuilder, TExtension>
                                                                       where TExtension : class, IDbContextOptionsExtension, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealDbContextOptionsBuilder{TBuilder, TExtension}"/> class.
        /// </summary>
        /// <param name="optionsBuilder">The core options builder.</param>
        public EtherealDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) => OptionsBuilder = optionsBuilder;

        /// <inheritdoc />
        DbContextOptionsBuilder IRelationalDbContextOptionsBuilderInfrastructure.OptionsBuilder => OptionsBuilder;

        /// <summary>
        /// Gets the core options builder.
        /// </summary>
        protected virtual DbContextOptionsBuilder OptionsBuilder { get; }

        /// <summary>
        ///     Sets an option by cloning the extension used to store the settings. This ensures the builder
        ///     does not modify options that are already in use elsewhere.
        /// </summary>
        /// <param name="setAction">An action to set the option.</param>
        /// <returns>The same builder instance so that multiple calls can be chained.</returns>
        protected virtual TBuilder WithOption(Func<TExtension, TExtension> setAction)
        {
            ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(
                setAction(OptionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension()));
            return (TBuilder)this;
        }

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
    }
}