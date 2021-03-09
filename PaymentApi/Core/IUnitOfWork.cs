using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}
