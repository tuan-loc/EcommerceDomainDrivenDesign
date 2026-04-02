using EcommerceDomainDrivenDesign.Domain.Orders;
using EcommerceDomainDrivenDesign.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Domain.Services
{
    public interface IOrderStatusWorkflow
    {
        void CalculateOrderStatus(Order order, Payment payment);
    }
}
