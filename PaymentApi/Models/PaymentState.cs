using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public enum PaymentState
    {
        Processed,
        Pending,
        Failed
    }
}
