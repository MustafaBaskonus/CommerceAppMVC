using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        IEnumerable<Product> GetLastestProducts(int n , bool trackChanges);
        IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p, bool trackChanges);
        IEnumerable<Product> GetShowCaseProducts(bool trackChanges);
        Product? GetOneProduct(int id, bool trackChanges);
        void CraeteProduct(ProductDtoForInsertion productDtoForInsertion);
        void UpdateProduct(Product product);
        void DeleteOneProduct(int id);
        ProductDtoForUpdate? GetOneProductForUpdate(int id, bool trackChanges);
        void UpdateProductDtoForUpdate(ProductDtoForUpdate productDto);
    }
}