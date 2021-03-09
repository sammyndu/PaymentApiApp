using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Core.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentResource>> GetPayments();

        Task<PaymentResource> GetPayment(Guid id);

        Task<PaymentResource> CreatePayment(PaymentRequest paymentRequest);
    }
}
