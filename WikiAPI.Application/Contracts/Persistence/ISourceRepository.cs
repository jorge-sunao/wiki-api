using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Domain.Entities;

namespace WikiAPI.Application.Contracts.Persistence
{
    public interface ISourceRepository : IAsyncRepository<Source>
    {
    }
}
