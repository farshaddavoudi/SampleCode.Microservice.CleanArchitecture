using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.PipelineBehaviours;

public class DbTransactionBehaviour<TRequest, TResponse>(IAppDbContext dbContext) : IPipelineBehavior<TRequest, TRequest> where TRequest : notnull
{
    public async Task<TRequest> Handle(TRequest request, RequestHandlerDelegate<TRequest> next, CancellationToken cancellationToken)
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
        catch (Exception e)
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