using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public class PaymentCreateEvent
    {
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class OrderReceiveUpdateEvent
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string? PaymentId { get; set; }
        public string? ShippingId { get; set; }
    }
}