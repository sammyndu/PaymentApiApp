using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentApi.Core.ExternalServices
{
    public interface IExpensivePaymentGateway
    {
        HttpResponseMessage ProcessPayment(PaymentRequest payment);
    }
}
