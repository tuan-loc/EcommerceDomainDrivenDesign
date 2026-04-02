using EcommerceDomainDrivenDesign.Domain.Core.Base;
using EcommerceDomainDrivenDesign.Domain.Products;
using System;

namespace EcommerceDomainDrivenDesign.Domain.Carts
{
    public class CartItem : Entity<Guid>
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public CartItem(Guid id, Product product, int quantity)
        {
            Id = id;
            Product = product;
            Quantity = quantity;
        }

        public void ChangeQuantity(int quantity)
        {
            if (quantity == 0)
                throw new BusinessRuleException("The product quantity must be at last 1.");

            Quantity = quantity;
        }

        // Empty constructor for EF
        private CartItem() { }
    }
}
