using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemiProjectAPI.Data;
using UdemiProjectAPI.Entities;
using UdemiProjectAPI.IRepositories;

namespace UdemiProjectAPI.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context, ILogger logger):base(context,logger)
        {
            _dbSet = context.Products;
        }
        public override async Task<bool> Upsert(Product product)
        {
            var existingProduct = await _dbSet.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
            if (existingProduct == null)
                return await Add(product);
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.QuantityInStock = product.QuantityInStock;
            existingProduct.Type = product.Type;
            return true;

        }
        public override async Task<bool> Delete(int id)
        {
            var existingProduct = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (existingProduct == null)
                return false;
            _dbSet.Remove(existingProduct);
            return true;
        }
    }

}
