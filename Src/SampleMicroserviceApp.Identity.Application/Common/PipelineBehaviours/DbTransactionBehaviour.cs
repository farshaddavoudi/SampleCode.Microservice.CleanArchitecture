using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.PipelineBehaviours;

public class DbTransactionBehaviour<TRequest, TResponse>(IAppDbContext dbContext) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }

        await using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}