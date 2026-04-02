using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Application.Base;
using EcommerceDomainDrivenDesign.Application.Customers.ViewModels;
using EcommerceDomainDrivenDesign.Application.Products.ListProducts;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Services;
using EcommerceDomainDrivenDesign.Domain.Shared;
using BuildingBlocks.CQRS.QueryHandling;

namespace EcommerceDomainDrivenDesign.Application.Customers.ListCustomerEventHistory
{
    public class ListProductsQueryHandler : QueryHandler<ListProductsQuery, IList<ProductViewModel>> 
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;
        private readonly ICurrencyConverter _currencyConverter;

        public ListProductsQueryHandler(
            IEcommerceUnitOfWork unitOfWork,
            ICurrencyConverter currencyConverter)
        {
            _unitOfWork = unitOfWork;
            _currencyConverter = currencyConverter;
        }

        public override async Task<IList<ProductViewModel>> ExecuteQuery(ListProductsQuery query, CancellationToken cancellationToken)
        {
            IList<ProductViewModel> productsViewMiodel = new List<ProductViewModel>();
            var products = await _unitOfWork.ProductRepository.ListAll(cancellationToken);

            if (string.IsNullOrEmpty(query.Currency))
                throw new InvalidDataException("Currency code cannot be empty.");

            var currency = Currency.FromCode(query.Currency);
            foreach (var product in products)
            {
                var convertedPrice = _currencyConverter.Convert(currency, product.Price);

                productsViewMiodel.Add(new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = convertedPrice.Value.ToString(),
                    CurrencySymbol = currency.Symbol
                });
            }

            return productsViewMiodel;
        }
    }
}
