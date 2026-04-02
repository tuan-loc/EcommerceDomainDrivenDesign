using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.Helpers;
using EcommerceDomainDrivenDesign.Application.Products.ListProducts;
using System.Collections.Generic;
using EcommerceDomainDrivenDesign.Application.Customers.ViewModels;
using System.Net;
using EcommerceDomainDrivenDesign.WebApp.Controllers.Base;

namespace EcommerceDomainDrivenDesign.WebApp.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(
            IMediator mediator, 
            IUserProvider userProvider)
            : base(userProvider, mediator)
        {
        }

        [HttpGet, Route("{currency}")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(typeof(IList<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts([FromRoute]string currency)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var query = new ListProductsQuery(currency);
            return Response(await Mediator.Send(query));
        }
    }
}