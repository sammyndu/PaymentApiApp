using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class PaymentStatus
    {
        public Guid Id { get; set; }

        public Guid PaymentId { get; set; }

        public Payment Payment { get; set; }

        public PaymentState paymentState { get; set; }
    }
}
