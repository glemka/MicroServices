using Catalog.Api.Data;
using Catalog.Api.Entites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            this.catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task CreateProduct(Product product)
        {
            await this.catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await this.catalogContext.Products.DeleteOneAsync(p => p.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await this.catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await this.catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await this.catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await this.catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await this.catalogContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
