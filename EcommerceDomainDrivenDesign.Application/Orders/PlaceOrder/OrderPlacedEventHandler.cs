using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Application.Base;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Orders.Events;
using EcommerceDomainDrivenDesign.Domain.Payments;
using System;

namespace EcommerceDomainDrivenDesign.Application.Orders.PlaceOrder
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderPlacedEventHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Handle(OrderPlacedEvent orderPlacedEvent, CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IEcommerceUnitOfWork>();
                var order = await unitOfWork.OrderRepository.GetById(orderPlacedEvent.OrderId);

                if (order == null)
                    throw new InvalidDataException("Order not found.");

                // Creating a payment
                var payment = new Payment(Guid.NewGuid(), order);
                await unitOfWork.PaymentRepository.Add(payment);
                await unitOfWork.CommitAsync();
            }
        }
    }
}
