﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BoundVerse.Domain.Entities;

namespace BoundVerse.Infrastructure.Persistence.Configurations;

public abstract class BaseConfiguration
{
    public virtual void Configure<TEntity>(EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity
    {
        builder
            .Property(pe => pe.Id)
            .IsRequired();

        builder
            .HasKey(pe => pe.Id);

        builder
            .Property(pe => pe.CreatedAtUtc)
            .IsRequired();

        builder
            .Property(pe => pe.CreatedBy)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(pe => pe.LastModifiedBy)
            .HasMaxLength(255);
    }
}
