using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }

        public void CraeteProduct(ProductDtoForInsertion productDtoForInsertion)
        {
            var product=_mapper.Map<Product>(productDtoForInsertion) ;
            _manager.Product.CreateProduct(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            var prd = GetOneProduct(id, false);
            _manager.Product.DeleteOneProduct(prd);
            _manager.Save();
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p, bool trackChanges)
        {
            return _manager.Product.GetAllProductsWithDetails(p, trackChanges);
        }

        public IEnumerable<Product> GetLastestProducts(int n ,bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges).OrderByDescending(p => p.ProductId).Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id,trackChanges);
            if(product is null)
                throw new Exception("Product not found!");
            return product;
        }

        public ProductDtoForUpdate? GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product = GetOneProduct(id, trackChanges);
            var productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }

        public IEnumerable<Product> GetShowCaseProducts(bool trackChanges)
        {
            return _manager.Product.GetShowCaseProducts(trackChanges);
        }

        public void UpdateProduct(Product product)
        {
            var entity = _manager.Product.GetOneProduct(product.ProductId, true);

            entity.ProductName = product.ProductName;
            entity.Price = product.Price;
            _manager.Save();
        }

        public void UpdateProductDtoForUpdate(ProductDtoForUpdate productDto)
        {
            //var entity = _manager.Product.GetOneProduct(productDto.ProductId, false);
            var entity =_mapper.Map<Product>( productDto);
            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();

        }
    }
}