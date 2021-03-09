using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentApi.Helpers
{
    public class RandomResponse
    {
        public static HttpResponseMessage GetResponse()
        {
            var response1 = SetStatusCode(HttpStatusCode.OK);
            var response2 = SetStatusCode(HttpStatusCode.InternalServerError);
            var response3 = SetStatusCode(HttpStatusCode.NotFound);
            var response4 = SetStatusCode(HttpStatusCode.BadRequest);

            var responseList = new List<HttpResponseMessage> { response1, response2, response3, response4 };

            var random = new Random();

            var response = responseList[random.Next(0, 4)];

            return response;
        }

        private static HttpResponseMessage SetStatusCode(HttpStatusCode code)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                StatusCode = code,
            };

            return response;
        }
    }
}
