using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPayments();

        Task<Payment> GetPayment(Guid id);

        void CreatePayment(Payment payment);

        void UpdatePayment(Payment payment);
    }
}
