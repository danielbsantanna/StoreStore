using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{

    public enum OrderStatus
    {
        Pending,           // Order has been placed but not yet processed
        PaymentProcessing, // Order is awaiting payment confirmation
        PaymentAuthorized, // Payment has been authorized
        PaymentFailed,     // Payment was unsuccessful
        Processing,        // Order is being prepared
        PickupReady,       // Order is ready to pick up
        Shipped,           // Order has been dispatched to the customer
        Delivered,         // Order has been received by the customer
        Canceled           // Order has been canceled
    }
}
