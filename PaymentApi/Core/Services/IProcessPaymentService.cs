using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Core.Services
{
    public interface IProcessPaymentService
    {
        Task ProcessPayment(PaymentStatus paymentStatus);
    }
}
