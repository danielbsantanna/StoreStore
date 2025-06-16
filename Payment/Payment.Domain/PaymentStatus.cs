using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain
{
    public enum PaymentStatus
    {
        Pending = 1,       // Payment is initiated but not completed
        Completed = 2,     // Payment was successfully processed
        Failed = 3,        // Payment attempt was unsuccessful
        Refunded = 4,      // Amount was returned to the customer
        Cancelled = 5,     // Payment was cancelled before processing
        Processing = 6     // Payment is currently being handled
    }
}
