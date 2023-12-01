
// ReSharper disable InvalidXmlDocComment

using Ardalis.Specification;
using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IRepository<TEntity> where TEntity : class, IEntity
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

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    Task AddAsync(TEntity itemToAdd, CancellationToken cancellationToken, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    void Add(TEntity itemToAdd, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd, CancellationToken cancellationToken, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    void AddRange(IEnumerable<TEntity> entitiesToAdd, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    Task UpdateAsync(TEntity itemToUpdate, CancellationToken cancellationToken, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    void Update(TEntity itemToUpdate, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    Task DeleteAsync(TEntity itemToDelete, CancellationToken cancellationToken, bool saveChanges = true);

    /// <param name="saveChanges"> Call SaveChanges or SaveChangesAsync method of UnitOfWork class according to UoW pattern after operation has been done which actually commits changes to database </param>
    void Delete(TEntity itemToDelete, bool saveChanges = true);
}