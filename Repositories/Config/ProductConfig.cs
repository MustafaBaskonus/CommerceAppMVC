using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();

            builder.HasData(
                new Product() { ProductId = 1, CategoryId=2,ImageUrl="/images/Iphone.jpeg", ProductName = "Computer", Price = 17_000 ,ShowCase=true },
                new Product() { ProductId = 2, CategoryId=2,ImageUrl="/images/default.jpg", ProductName = "Keyboard", Price = 1_000  ,ShowCase=true},
                new Product() { ProductId = 3, CategoryId=2,ImageUrl="/images/indir.jpeg", ProductName = "Mouse", Price = 500 ,ShowCase=true },
                new Product() { ProductId = 4, CategoryId=2,ImageUrl="/images/kırmızıkitap.jpeg", ProductName = "Monitor", Price = 7_000 ,ShowCase=true },
                new Product() { ProductId = 5, CategoryId=2,ImageUrl="/images/telefon.jpeg", ProductName = "Deck", Price = 1_500 ,ShowCase= false },
                new Product() { ProductId = 6, CategoryId=1, ProductName = "History", Price = 25  ,ShowCase=false},
                new Product() { ProductId = 7, CategoryId=1, ProductName = "Hamlet", Price = 45 ,ShowCase=false }
            );
        }
    }
}