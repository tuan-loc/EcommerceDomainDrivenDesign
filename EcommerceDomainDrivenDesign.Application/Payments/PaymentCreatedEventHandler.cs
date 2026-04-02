using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain.Payments.Events;
using EcommerceDomainDrivenDesign.Domain.Services;
using EcommerceDomainDrivenDesign.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceDomainDrivenDesign.Application.Payments
{
    public class PaymentCreatedEventHandler : INotificationHandler<PaymentCreatedEvent>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PaymentCreatedEventHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Handle(PaymentCreatedEvent paymentCreatedEvent, CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {                
                var orderStatusWorkflowManager = scope.ServiceProvider.GetRequiredService<IOrderStatusWorkflow>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IEcommerceUnitOfWork>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var payment = await unitOfWork.PaymentRepository.GetById(paymentCreatedEvent.PaymentId);

                if (payment == null)
                    throw new InvalidDataException("Payment not found.");

                if (payment.Order == null)
                    throw new InvalidDataException("Order not found.");

                // Changing order status
                orderStatusWorkflowManager.CalculateOrderStatus(payment.Order, payment);
                await unitOfWork.CommitAsync();

                // Attempting to pay
                MakePaymentCommand command = new MakePaymentCommand(paymentCreatedEvent.PaymentId);
                await mediator.Send(command);            
            }
        }
    }
}
