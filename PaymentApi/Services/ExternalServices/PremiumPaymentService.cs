using PaymentApi.Core.ExternalServices;
using PaymentApi.Helpers;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentApi.Services.ExternalServices
{
    public class PremiumPaymentService : IPremiumPaymentService
    {
        public HttpResponseMessage ProcessPayment(PaymentRequest paymentRequest)
        {
            var response = RandomResponse.GetResponse();
            return response;
        }
    }
}
