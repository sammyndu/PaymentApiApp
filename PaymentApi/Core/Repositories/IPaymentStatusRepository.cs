using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Core.Repositories
{
    public interface IPaymentStatusRepository
    {
        Task<List<PaymentStatus>> GetPayments();

        Task<PaymentStatus> GetPaymentStatus(Guid id);

        Task<PaymentStatus> GetPayment(Guid paymentId);

        void CreatePaymentStatus(PaymentStatus payment);

        void UpdatePaymentStatus(PaymentStatus payment);
    }
}
