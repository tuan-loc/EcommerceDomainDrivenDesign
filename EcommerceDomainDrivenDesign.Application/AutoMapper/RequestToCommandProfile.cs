using AutoMapper;
using EcommerceDomainDrivenDesign.Application.Customers.RegisterCustomer;
using EcommerceDomainDrivenDesign.Application.Customers.UpdateCustomer;
using EcommerceDomainDrivenDesign.Application.Orders.PlaceOrder;

namespace EcommerceDomainDrivenDesign.Application.AutoMapper
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<RegisterCustomerRequest, RegisterCustomerCommand>()
            .ConstructUsing(c => new RegisterCustomerCommand(c.Email, c.Name, c.Password, c.PasswordConfirm));
        }
    }
}
