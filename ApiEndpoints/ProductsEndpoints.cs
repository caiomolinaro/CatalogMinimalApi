using CatalogMinimalApi.Context;
using CatalogMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogMinimalApi.ApiEndpoints
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndpoints(this WebApplication app)
        {
            app.MapPost("/products", async (ProductModel product, AppDbContext db) =>
            {
                db.Products?.Add(product);
                await db.SaveChangesAsync();

                return Results.Created($"/products/{product.CategoryId}", product);
            });

            app.MapGet("/products", async (AppDbContext db) => await db.Products.ToListAsync()).RequireAuthorization();

            app.MapPut("/products/{id:int}", async (int id, ProductModel product, AppDbContext db) =>
            {
                if (product.ProductId != id)
                {
                    return Results.BadRequest();
                }

                var productDB = await db.Products.FindAsync(id);

                if (product is null) return Results.NotFound();

                productDB.Name = product.Name;
                productDB.Description = product.Description;

                await db.SaveChangesAsync();
                return Results.Ok(productDB);
            });

            app.MapDelete("/product/{id:int}", async (int id, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);

                if (product is null) return Results.NotFound();

                db.Products.Remove(product);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapGet("/products/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Products.FindAsync(id)
                is ProductModel product ?
                Results.Ok(product) :
                Results.NotFound();

            });
        }
    }
}
