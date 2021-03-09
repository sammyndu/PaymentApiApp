using PaymentApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentAppDbContext _db;
        public UnitOfWork(PaymentAppDbContext db)
        {
            _db = db;
        }
        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
