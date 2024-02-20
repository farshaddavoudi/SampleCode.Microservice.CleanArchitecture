using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    IQueryable<TEntity> AsQueryable();

    IQueryable<TEntity> AsQueryable(Specification<TEntity> specification);

    /// <summary>
    /// Filter using specification expression without projection
    /// </summary>
    /// <param name="specification"> Specification Expression </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Entity Collection </returns>
    Task<List<TEntity>> ToListAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// A simple ToList without any filtering and projection
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns> Entity Collection </returns>
    Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo queryable extension method without any filtering
    /// </summary>
    /// <typeparam name="TAutoMapperDestination"> AutoMapper Destination Type </typeparam>
    /// <param name="cancellationToken"></param>
    /// <returns> AutoMapper Destination Type Collection </returns>
    Task<List<TAutoMapperDestination>> ToListAsync<TAutoMapperDestination>(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo queryable extension method without any filtering along using .Distinct method
    /// </summary>
    /// <typeparam name="TAutoMapperDestination"> AutoMapper Destination Type </typeparam>
    /// <param name="cancellationToken"></param>
    /// <returns> AutoMapper Destination Type Distinct Collection </returns>
    Task<List<TAutoMapperDestination>> ToListDistinctAsync<TAutoMapperDestination>(CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo queryable extension method filter by specification expression
    /// </summary>
    /// <typeparam name="TAutoMapperDestination"> AutoMapper Destination Type </typeparam>
    /// <param name="specification"> Specification Expression </param>
    /// <param name="cancellationToken"></param>
    /// <returns> AutoMapper Destination Type Collection </returns>
    Task<List<TAutoMapperDestination>> ToListAsync<TAutoMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo queryable extension method filter by specification expression along using .Distinct method
    /// </summary>
    /// <typeparam name="TAutoMapperDestination"> AutoMapper Destination Type </typeparam>
    /// <param name="specification"> Specification Expression </param>
    /// <param name="cancellationToken"></param>
    /// <returns> AutoMapper Destination Type Distinct Collection </returns>
    Task<List<TAutoMapperDestination>> ToListDistinctAsync<TAutoMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    ///  Filter along with projection using specification expression
    /// </summary>
    /// <typeparam name="TSpecResult"> Specification Result Type </typeparam>
    /// <param name="specification"> Specification Pattern with Result </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Specification Result Type Collection </returns>
    Task<List<TSpecResult>> ToListAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    /// <summary>
    ///  Filter along with projection using specification expression along using .Distinct method
    /// </summary>
    /// <typeparam name="TSpecResult"> Specification Result Type </typeparam>
    /// <param name="specification"> Specification Pattern with Result </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Specification Result Type Distinct Collection </returns>
    Task<List<TSpecResult>> ToListDistinctAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using Spec output
    /// </summary>
    Task<TSpecResult?> FirstOrDefaultAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Projection using AutoMapper .ProjectTo extension method
    /// </summary>
    Task<TAutoMapperDestination?> FirstOrDefaultAsync<TAutoMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken);

    Task<TEntity> FirstAsync(Specification<TEntity> specification, CancellationToken cancellationToken);
}