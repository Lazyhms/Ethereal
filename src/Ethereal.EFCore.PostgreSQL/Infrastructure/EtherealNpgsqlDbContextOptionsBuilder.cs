﻿// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Ethereal.EntityFrameworkCore.Infrastructure;

/// <summary>
/// EtherealDbContextOptionsBuilder
/// </summary>
public class EtherealNpgsqlDbContextOptionsBuilder : EtherealNpgsqlDbContextOptionsBuilder<EtherealNpgsqlDbContextOptionsBuilder, EtherealNpgsqlOptionsExtension>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EtherealNpgsqlDbContextOptionsBuilder"/> class.
    /// </summary>
    public EtherealNpgsqlDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
    {
    }

    /// <summary>
    /// override Equals method
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals(object? obj)
        => base.Equals(obj);

    /// <summary>
    /// override GetHashCode method
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode()
        => base.GetHashCode();

    /// <summary>
    /// override ToString method
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string? ToString()
        => base.ToString();
}