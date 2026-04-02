using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain.Products;
using Microsoft.Extensions.DependencyInjection;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Customers;
using EcommerceDomainDrivenDesign.Infrastructure.Messaging;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using EcommerceDomainDrivenDesign.Domain.Payments;
using EcommerceDomainDrivenDesign.Domain.Services;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Carts;
using EcommerceDomainDrivenDesign.Domain.Orders;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Orders;
using EcommerceDomainDrivenDesign.Domain.Carts;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.CurrencyExchange;

namespace EcommerceDomainDrivenDesign.DataSeed
{
    class DataSeeder
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            RegisterServices();

            var unitOfWork = _serviceProvider.GetService<IEcommerceUnitOfWork>();
            var currencyConverter = _serviceProvider.GetService<ICurrencyConverter>();
            await SeedProducts.SeedData(unitOfWork, currencyConverter);

            DisposeServices();
            Console.Read();
        }

        private static void RegisterServices()
        {            
            var services = new ServiceCollection();
            var connString = GetDbConnection();
            services.AddDbContext<EcommerceDomainDrivenDesignContext>(options =>
            options.UseSqlServer(connString));

            services.AddScoped<IEcommerceUnitOfWork, EcommerceUnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IStoredEventRepository, StoredEventRepository>();
            services.AddScoped<IEventSerializer, EventSerializer>();
            services.AddScoped<ICurrencyConverter, CurrencyConverter>();
            services.AddScoped<EcommerceDomainDrivenDesignContext>();
            
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
                return;
            if (_serviceProvider is IDisposable)
                ((IDisposable)_serviceProvider).Dispose();
        }

        private static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("DefaultConnection");

            return strConnection;
        }
    }
}
