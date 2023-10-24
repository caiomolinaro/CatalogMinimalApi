using CatalogMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogMinimalApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CategoryModel>? Categories { get; set; }
        public DbSet<ProductModel>? Products { get; set; }

        //usa a fluent api do ef core para não usar as convenções padrões do ef core e sim do mysql
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<CategoryModel>().HasKey(c => c.CategoryId); //define a chave primaria
            mb.Entity<CategoryModel>().Property(c => c.Name).HasMaxLength(100).IsRequired(); //define o tamanho como 100 e como required
            mb.Entity<CategoryModel>().Property(c => c.Description).HasMaxLength(100); //define o tamanho como 100 e como required

            mb.Entity<ProductModel>().HasKey(p => p.ProductId);
            mb.Entity<ProductModel>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            mb.Entity<ProductModel>().Property(p => p.Description).HasMaxLength(150);
            mb.Entity<ProductModel>().Property(p => p.ImageUrl).HasMaxLength(100);
            mb.Entity<ProductModel>().Property(p => p.Price).HasPrecision(14, 2); //define a precisao do decimal como 14.2

            //definindo os relacionamentos
            mb.Entity<ProductModel>().HasOne<CategoryModel>(c => c.Category).WithMany(p => p.Products); //define que tem uma relação de um para muitos
        }
    }
}
