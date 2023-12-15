using Microsoft.EntityFrameworkCore.Storage;

namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IAppDbContext
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}