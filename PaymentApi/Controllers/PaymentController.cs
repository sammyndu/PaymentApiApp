using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Core.Services;
using PaymentApi.Helpers;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        [Route("ProcessPayment")]
        public async Task<ActionResult> ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (paymentRequest.ExpirationDate < DateTime.Now)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "error", Message = "Expiration Date is invalid" });
            }

            try
            {
                await _paymentService.CreatePayment(paymentRequest);
            } 
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "error", Message = "An error occurred" });
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Status = "success", Message = "Payment is being processed" });


        }
    }
}
