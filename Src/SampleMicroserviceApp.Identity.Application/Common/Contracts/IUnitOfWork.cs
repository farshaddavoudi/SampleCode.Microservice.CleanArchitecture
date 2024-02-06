using Microsoft.EntityFrameworkCore.Storage;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    void SaveChange();

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    
    void ClearChangeTracker();
}