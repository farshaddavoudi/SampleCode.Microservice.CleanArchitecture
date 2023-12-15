
// ReSharper disable InvalidXmlDocComment

using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IBaseEntity
{
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