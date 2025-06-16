using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public class Payment
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Completed { get; set; }
    }
}
