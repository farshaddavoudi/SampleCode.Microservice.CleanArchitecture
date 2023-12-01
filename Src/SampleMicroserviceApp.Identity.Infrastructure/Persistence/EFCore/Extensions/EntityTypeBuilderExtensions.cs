using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static IndexBuilder HasUniqueIndexArchivable<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> indexExpression
    )
        where TEntity : class, IArchivableEntity
    {
        return builder
            .HasIndex(indexExpression)
            .HasFilter($"{nameof(IArchivableEntity.IsArchived)} = 0")
            .IsUnique();
    }
}