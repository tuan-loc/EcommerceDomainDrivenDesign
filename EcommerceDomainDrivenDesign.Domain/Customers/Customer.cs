using EcommerceDomainDrivenDesign.Domain.Core.Base;
using EcommerceDomainDrivenDesign.Domain.CurrencyExchange;
using EcommerceDomainDrivenDesign.Domain.Customers.Events;
using EcommerceDomainDrivenDesign.Domain.Customers.Orders;
using EcommerceDomainDrivenDesign.Domain.Customers.Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDomainDrivenDesign.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public bool WelcomeEmailWasSent { get; private set; }
        public readonly List<Order> Orders = new List<Order>();

        public static Customer CreateCustomer(string email, string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            if (!customerUniquenessChecker.IsUserUnique(email))
                throw new BusinessRuleException("This e-mail is already in use.");

            return new Customer(email, name);
        }

        public Guid PlaceOrder(Basket basket, ICurrencyConverter currencyConverter)
        {
            if (!basket.Products.Any())
                throw new BusinessRuleException("An order should have at least one product.");

            var order = new Order(basket, currencyConverter);
            Orders.Add(order);

            AddDomainEvent(new OrderPlacedEvent(Id, order.Id));
            return order.Id;
        }

        public Guid ChangeOrder(Basket cart, Guid orderId, ICurrencyConverter currencyConverter)
        {
            if (!cart.Products.Any())
                throw new BusinessRuleException("An order should have at least one product.");

            var orderToChange = Orders.Single(o => o.Id == orderId);
            orderToChange.Change(cart, currencyConverter);

            AddDomainEvent(new OrderChangedEvent(orderToChange.Id));
            return orderToChange.Id;
        }

        public void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
            Name = value;
        }

        public void SetWelcomeEmailSent(bool value)
        {
            WelcomeEmailWasSent = value;
        }

        private Customer(string email, string name)
        {
            Email = email;
            Name = name;
            WelcomeEmailWasSent = false;
            AddDomainEvent(new CustomerRegisteredEvent(Id, name, email));
        }

        // Empty constructor for EF
        private Customer() { }
    }
}
