using Microsoft.EntityFrameworkCore;
using PaymentApi.Core.Repositories;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentAppDbContext _db; 
        public PaymentRepository(PaymentAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _db.Payments.ToListAsync();
        }

        public async Task<Payment> GetPayment(Guid id)
        {
            try
            {
                var result = await _db.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
                return result;
            }catch(Exception e)
            {
                var mes = e;
                return null;
            }
        }

        public void CreatePayment(Payment payment)
        {
            _db.Payments.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            _db.Payments.Update(payment);
        }
    }
}
