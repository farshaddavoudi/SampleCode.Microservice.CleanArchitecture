namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IRoleClaimsCacheService
{
    Task SetRoleClaimsAsync(string roleKey, List<string> claimKeys, CancellationToken cancellationToken);

    Task<List<string>> GetRoleClaimsAsync(string roleKey, CancellationToken cancellationToken);
}