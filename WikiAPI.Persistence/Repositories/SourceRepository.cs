using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Persistence;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Persistence.Repositories;

public class SourceRepository : BaseRepository<Source>, ISourceRepository
{
    private IApplicationReadDbConnection _readDbConnection;
    private IApplicationWriteDbConnection _writeDbConnection;

    public SourceRepository(IApplicationDbContext dbContext, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection) : base(dbContext)
    {
        _readDbConnection = readDbConnection;
        _writeDbConnection = writeDbConnection;
    }
}
