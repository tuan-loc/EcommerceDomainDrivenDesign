using System;
using EcommerceDomainDrivenDesign.Application.Carts;

namespace EcommerceDomainDrivenDesign.Application.Carts.CreateCart
{
    public class SaveCartRequest
    {
        public Guid CustomerId { get; set; }
        public ProductDto Product { get; set; }
        public string Currency { get; set; }
    }
}
