using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Common;

namespace WikiAPI.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : AuditEntity
{
    public IApplicationDbContext _dbContext { get; }

    public BaseRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbContext.Connection.GetAsync<T>(id);
    }

    public virtual async Task<IEnumerable<T>> ListAllAsync()
    {
        return await _dbContext.Connection.GetAllAsync<T>();
    }

    public virtual async Task<int> AddAsync(T entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.LastModifiedDate = DateTime.UtcNow;
        var id = await _dbContext.Connection.InsertAsync(entity);
        return (int)id;
    }

    public virtual async Task<int> AddRangeAsync(IEnumerable<T> entities)
    {
        var updatedEntities = entities.Select(e => { e.CreatedDate = DateTime.UtcNow; e.LastModifiedDate = DateTime.UtcNow; return e; }).ToList();
        var rowsInserted = await _dbContext.Connection.InsertAsync(updatedEntities);
        return (int)rowsInserted;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        entity.LastModifiedDate = DateTime.UtcNow;
        await _dbContext.Connection.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(T entity)
    {
        await _dbContext.Connection.DeleteAsync(entity);
    }
}
