using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    [Table("Payments")]
    public class Payment
    {
        public Guid Id { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }


    }
}
