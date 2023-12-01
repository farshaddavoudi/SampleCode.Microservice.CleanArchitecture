namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IUserRolesCacheService
{
    Task SetUserRolesAsync(int userId, string appKey, List<string> roleKeys, CancellationToken cancellationToken);

    Task<List<string>> GetUserRolesAsync(int userId, List<string> appKeys, CancellationToken cancellationToken);
}