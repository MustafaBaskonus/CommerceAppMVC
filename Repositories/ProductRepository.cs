using Entities.Extensions;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateProduct(Product product) => Create(product);

        public void DeleteOneProduct(Product prd) => Delete(prd);


        public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p, bool trackChanges)
        {
            return _context.Products
            .FilteredProductByCategoryId(p.CategoryId)
            .FilteredProductBySearchTerm(p.SearchTerm)
            .PriceFilter(p.MaxPrice, p.MinPrice, p.IsValid)
            .ToPaginate(p.PageSize,p.PageNumber);
        }

        // Interface

        public Product? GetOneProduct(int id, bool trackChanges)
        {
              return FindByCondition(p => p.ProductId.Equals(id),trackChanges);  
        }

        public IQueryable<Product> GetShowCaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges).Where(y=> y.ShowCase==true);
        }


        public void UpdateOneProduct(Product product)=> Update(product);

    }
}