using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDomainDrivenDesign.Infrastructure.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDomainDrivenDesignContext _context;

        public ProductRepository(EcommerceDomainDrivenDesignContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);            
        }

        public async Task AddRange(List<Product> products, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddRangeAsync(products, cancellationToken);
        }

        public async Task<Product> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Product>> GetByIds(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> ListAll(CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}
