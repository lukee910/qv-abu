using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QvAbu.Api.Data.UnitOfWork
{
    internal interface IUnitOfWork
    {
        Task<int> Complete();
    }
}
