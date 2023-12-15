using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Repository;

internal class EfRepository<TEntity>(
    AppDbContext dbContext,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : ReadOnlyEfRepository<TEntity>(dbContext, mapper), IDisposable, IRepository<TEntity> where TEntity : class, IBaseEntity
{
    public async Task AddAsync(TEntity itemToAdd, CancellationToken cancellationToken, bool saveChanges = true)
    {
        await DbSet.AddAsync(itemToAdd, cancellationToken);

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void Add(TEntity itemToAdd, bool saveChanges = true)
    {
        DbSet.Add(itemToAdd);

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd, CancellationToken cancellationToken, bool saveChanges = true)
    {
        await DbSet.AddRangeAsync(entitiesToAdd, cancellationToken);

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void AddRange(IEnumerable<TEntity> entitiesToAdd, bool saveChanges = true)
    {
        DbSet.AddRange(entitiesToAdd);

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    public async Task UpdateAsync(TEntity itemToUpdate, CancellationToken cancellationToken, bool saveChanges = true)
    {
        DbSet.Update(itemToUpdate);

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void Update(TEntity itemToUpdate, bool saveChanges = true)
    {
        DbSet.Update(itemToUpdate);

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    public async Task DeleteAsync(TEntity itemToDelete, CancellationToken cancellationToken, bool saveChanges = true)
    {
        if (itemToDelete is IArchivableEntity archivableEntity)
        {
            archivableEntity.IsArchived = true;
            await UpdateAsync(itemToDelete, cancellationToken, saveChanges);
            DbSet.Entry(itemToDelete).State = EntityState.Detached;
        }
        else
        {
            DbSet.Remove(itemToDelete);
        }

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void Delete(TEntity itemToDelete, bool saveChanges = true)
    {
        if (itemToDelete is IArchivableEntity archivableEntity)
        {
            archivableEntity.IsArchived = true;
            Update(itemToDelete, saveChanges);
            DbSet.Entry(itemToDelete).State = EntityState.Detached;
        }
        else
        {
            DbSet.Remove(itemToDelete);
        }

        if (saveChanges)
            unitOfWork.SaveChange();
    }
}