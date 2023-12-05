using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Repository;

public class Repository<TEntity>(
    AppDbContext dbContext,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IDisposable, IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet;
    }

    public IQueryable<TEntity> AsQueryable(Specification<TEntity> specification)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return queryResult;
    }

    public async Task<List<TEntity>> ToListAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListProjectedAsync<TMapperDestination>(CancellationToken cancellationToken)
    {
        return await _dbSet
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListProjectedDistinctAsync<TMapperDestination>(CancellationToken cancellationToken)
    {
        return await _dbSet
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListProjectedAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TMapperDestination>> ToListProjectedDistinctAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TSpecResult>> ToListProjectedAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TSpecResult>> ToListProjectedDistinctAsync<TSpecResult>(Specification<TEntity, TSpecResult> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult.AnyAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TOut?> FirstOrDefaultProjectedAsync<TOut>(Specification<TEntity, TOut> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TMapperDestination?> FirstOrDefaultProjectedAsync<TMapperDestination>(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult
            .ProjectTo<TMapperDestination>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> FirstAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet,
            specification: specification
        );

        return await queryResult.FirstAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity itemToAdd, CancellationToken cancellationToken, bool saveChanges = true)
    {
        await _dbSet.AddAsync(itemToAdd, cancellationToken);

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void Add(TEntity itemToAdd, bool saveChanges = true)
    {
        _dbSet.Add(itemToAdd);

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd, CancellationToken cancellationToken, bool saveChanges = true)
    {
        await _dbSet.AddRangeAsync(entitiesToAdd, cancellationToken);

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void AddRange(IEnumerable<TEntity> entitiesToAdd, bool saveChanges = true)
    {
        _dbSet.AddRange(entitiesToAdd);

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    public async Task UpdateAsync(TEntity itemToUpdate, CancellationToken cancellationToken, bool saveChanges = true)
    {
        _dbSet.Update(itemToUpdate);

        if (saveChanges)
            await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void Update(TEntity itemToUpdate, bool saveChanges = true)
    {
        _dbSet.Update(itemToUpdate);

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    public async Task DeleteAsync(TEntity itemToDelete, CancellationToken cancellationToken, bool saveChanges = true)
    {
        if (itemToDelete is IArchivableEntity archivableEntity)
        {
            archivableEntity.IsArchived = true;
            await UpdateAsync(itemToDelete, cancellationToken, saveChanges);
            _dbSet.Entry(itemToDelete).State = EntityState.Detached;
        }
        else
        {
            _dbSet.Remove(itemToDelete);
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
            _dbSet.Entry(itemToDelete).State = EntityState.Detached;
        }
        else
        {
            _dbSet.Remove(itemToDelete);
        }

        if (saveChanges)
            unitOfWork.SaveChange();
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}