using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class PaymentResource
    {
        public Guid Id { get; set; }
        public string CreditCardNumber { get; set; }

        public string CardHolder { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

    }
}
