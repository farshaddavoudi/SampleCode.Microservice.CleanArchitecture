using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Interceptors;

public class FixSchemaInterceptor : DbCommandInterceptor
{
    private readonly List<KeyValuePair<string, string>>? _dbSchemaPairs;

    /// <summary>
    /// Add Replacing Pairs Except than ATAWorkflow and PardisWebDb Dbs
    /// </summary>
    /// <param name="dbSchemaPairs"> example: {"[ATAWorkflow.dbo].", "[ATAWorkflow].[dbo]."} </param>
    public FixSchemaInterceptor(List<KeyValuePair<string, string>>? dbSchemaPairs)
    {
        _dbSchemaPairs = dbSchemaPairs;
    }

    public FixSchemaInterceptor()
    {

    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // https://dejanstojanovic.net/aspnet/2020/november/accessing-multiple-databases-from-the-same-dbcontext-in-ef-core/

        command = FixCommandQuery(command);

        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        command = FixCommandQuery(command);

        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }

    private DbCommand FixCommandQuery(DbCommand command)
    {
        // Just as an example
        command.CommandText = command.CommandText.Replace(
            "[MyDB.dbo].",
            "[MyDB].[dbo].");

        if (_dbSchemaPairs != null)
        {
            foreach (var dbSchemaPair in _dbSchemaPairs)
            {
                command.CommandText = command.CommandText.Replace(
                    dbSchemaPair.Key,
                    dbSchemaPair.Value);
            }
        }

        return command;
    }
}