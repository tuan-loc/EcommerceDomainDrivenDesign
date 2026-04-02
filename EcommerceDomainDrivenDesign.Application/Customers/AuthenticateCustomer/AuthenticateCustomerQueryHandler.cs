using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain.Customers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.Services;
using EcommerceDomainDrivenDesign.Application.Customers.ViewModels;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser;
using EcommerceDomainDrivenDesign.Application.Base;
using BuildingBlocks.CQRS.QueryHandling;

namespace EcommerceDomainDrivenDesign.Application.Customers.AuthenticateCustomer
{
    public class AuthenticateCustomerQueryHandler : QueryHandler<AuthenticateCustomerQuery, CustomerViewModel> 
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtService _jwtService;

        public AuthenticateCustomerQueryHandler(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ICustomerRepository customerRepository,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _customerRepository = customerRepository;
            _jwtService = jwtService;
        }

        public async override Task<CustomerViewModel> ExecuteQuery(AuthenticateCustomerQuery request, CancellationToken cancellationToken)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();

            var signIn = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);
            if (signIn.Succeeded)            
            {
                var token = await _jwtService.GenerateJwt(request.Email);
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new InvalidDataException("User not found.");

                //Customer data
                var customer = await _customerRepository.GetByEmail(user.Email, cancellationToken);
                customerViewModel.Id = customer.Id;
                customerViewModel.Name = customer.Name;
                customerViewModel.Email = user.Email;
                customerViewModel.Token = token;
                customerViewModel.LoginSucceeded = signIn.Succeeded;
            }
            else
                customerViewModel.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, 
                    "Usename or password invalid"));

            return customerViewModel;
        }
    }
}
