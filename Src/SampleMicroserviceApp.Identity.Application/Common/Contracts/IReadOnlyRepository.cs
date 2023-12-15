using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    IQueryable<TEntity> AsQueryable();

    IQueryable<TEntity> AsQueryable(Specification<TEntity> specification);

    Task<List<TEntity>> ToListAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedAsync<TMapperDestination>(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method along using .Distinct method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedDistinctAsync<TMapperDestination>(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method along using .Distinct method
    /// </summary>
    Task<List<TMapperDestination>> ToListProjectedDistinctAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Spec output
    /// </summary>
    Task<List<TSpecResult>> ToListProjectedAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Spec output along using .Distinct method
    /// </summary>
    Task<List<TSpecResult>> ToListProjectedDistinctAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Spec output
    /// </summary>
    Task<TSpecResult?> FirstOrDefaultProjectedAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<TMapperDestination?> FirstOrDefaultProjectedAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity> FirstAsync(Specification<TEntity> specification, CancellationToken cancellationToken);
}