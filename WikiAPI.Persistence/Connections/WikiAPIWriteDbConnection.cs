using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;

namespace WikiAPI.Persistence.Connections;

public class WikiAPIWriteDbConnection : IApplicationWriteDbConnection
{
    private readonly IApplicationDbContext context;

    public WikiAPIWriteDbConnection(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await context.Connection.ExecuteAsync(sql, param, transaction);
    }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return (await context.Connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }

    public async Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return (await context.Connection.QueryAsync<TFirst, TSecond, TReturn>(sql, map, param, transaction)).AsList();
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await context.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await context.Connection.QuerySingleAsync<T>(sql, param, transaction);
    }
}
