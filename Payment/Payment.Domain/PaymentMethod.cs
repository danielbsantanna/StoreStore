using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain
{
    public enum PaymentMethod
    {
        CreditCard = 1,
        DebitCard = 2,
        PayPal = 3,
        ApplePay = 4,
        GooglePay = 5,
        BankTransfer = 6,
        CashOnDelivery = 7,
        Cryptocurrency = 8
    }
}
