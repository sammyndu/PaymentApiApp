using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class PaymentRequest
    {
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Amount { get; set; }
    }
}
