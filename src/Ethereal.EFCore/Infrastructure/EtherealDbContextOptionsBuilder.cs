﻿// Copyright (c) Ethereal. All rights reserved.
//

using Ethereal.EntityFrameworkCore.Infrastructure.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Ethereal.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    /// EtherealDbContextOptionsBuilder
    /// </summary>
    public class EtherealDbContextOptionsBuilder : EtherealDbContextOptionsBuilder<EtherealOptionsExtension>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealDbContextOptionsBuilder"/> class.
        /// </summary>
        public EtherealDbContextOptionsBuilder([NotNull] DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
        {

        }

        /// <summary>
        /// override ToString method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string? ToString() => base.ToString();

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
    }
}
