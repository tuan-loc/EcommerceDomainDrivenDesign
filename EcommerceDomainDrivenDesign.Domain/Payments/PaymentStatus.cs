using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceDomainDrivenDesign.Domain.Payments
{
    public enum PaymentStatus
    {
        ToPay = 1,
        Paid = 2,
        PaymentNotAuthorized = 3
    }
}
