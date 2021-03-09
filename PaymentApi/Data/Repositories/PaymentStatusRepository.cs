using Microsoft.EntityFrameworkCore;
using PaymentApi.Core.Repositories;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Data.Repositories
{
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        private readonly PaymentAppDbContext _db;
        public PaymentStatusRepository(PaymentAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PaymentStatus>> GetPayments()
        {
            return await _db.PaymentStatus.Include(x => x.Payment).ToListAsync();
        }

        public async Task<PaymentStatus> GetPaymentStatus(Guid id)
        {
            return await _db.PaymentStatus.Where(x => x.Id == id).Include(x => x.Payment).FirstOrDefaultAsync();
        }

        public async Task<PaymentStatus> GetPayment(Guid paymentId)
        {
            return await _db.PaymentStatus.Where(x => x.PaymentId == paymentId).Include(x => x.Payment).FirstOrDefaultAsync();
        }

        public void CreatePaymentStatus(PaymentStatus payment)
        {
            _db.PaymentStatus.Add(payment);
        }

        public void UpdatePaymentStatus(PaymentStatus payment)
        {
            _db.PaymentStatus.Update(payment);
        }
    }
}
