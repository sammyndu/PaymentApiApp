using Newtonsoft.Json;
using PaymentApi.Core.ExternalServices;
using PaymentApi.Models;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PaymentApi.Helpers;

namespace PaymentApi.Services.ExternalServices
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public HttpResponseMessage ProcessPayment(PaymentRequest payment)
        {
            //var json = JsonConvert.SerializeObject(payment);
            //var url = $"https://www.c-sharpcorner.com/mytestpagefor404";

            //var client = _clientFactory.CreateClient("cheapPayment");
            //var response = await client.PostAsJsonAsync(url, payment);

            //var result = await response.Content.ReadAsStringAsync();

            var response = RandomResponse.GetResponse();
            return response;
        }
    }
}
