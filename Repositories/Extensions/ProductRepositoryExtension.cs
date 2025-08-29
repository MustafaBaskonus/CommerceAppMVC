using Entities.Models;

namespace Entities.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredProductByCategoryId(this IQueryable<Product> products
                , int? categoryId)
        {
            if (categoryId == null)
            {
                return products;
            }
            else
            {
                return products.Where(m => m.CategoryId.Equals(categoryId));
            }
        }
        public static IQueryable<Product> FilteredProductBySearchTerm(this IQueryable<Product> products, String? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return products;
            }
            else
            {
                return products.Where(m => m.ProductName.ToLower().Contains(searchTerm.ToLower()));
            }
        }
        public static IQueryable<Product> PriceFilter(this IQueryable<Product> products, int? maxPrice, int? minPrice, bool isValid)
        {
            if (!isValid)
            {
                return products;
            }
            return products.Where(m => m.Price >= minPrice && m.Price <= maxPrice);
        }
        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products, int pageSize, int pageNumber)
        {
            return products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        }
    }
    
}