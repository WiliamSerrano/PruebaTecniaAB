using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBVENTASContext _dbVentasContext;

        public ProductRepository(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbVentasContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int idProduct)
        {
            Product oProduct = new Product();

            if (idProduct != 0)
            {

                oProduct = await _dbVentasContext.Products.FindAsync(idProduct);

            }

            return oProduct;
        }

        public async Task<Product> AddProduct(Product oProduct)
        {
            await _dbVentasContext.Products.AddAsync(oProduct);

            await _dbVentasContext.SaveChangesAsync();

            return oProduct;
        }

        public async Task<Product> UpdateProduct(Product oProduct)
        {
            var existingProduct = await _dbVentasContext.Products.FindAsync(oProduct.IdProduct);

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            existingProduct.ProductName = oProduct.ProductName;
            existingProduct.UnitPrice = oProduct.UnitPrice;
            existingProduct.Quantity = oProduct.Quantity;
            existingProduct.Active = oProduct.Active;

            _dbVentasContext.Products.Update(existingProduct);
            _dbVentasContext.Entry(existingProduct).State = EntityState.Modified;
            await _dbVentasContext.SaveChangesAsync();
            return existingProduct;
        }
        
        public async Task<Product> GetProductToDelete(int idProduct)
        {
            Product oProduct = await _dbVentasContext.Products.Where(e => e.IdProduct == idProduct).FirstOrDefaultAsync();

            return oProduct;
        }

        public async Task<bool> CanDeleteProduct(int idProduct)
        {

            bool hasDependencies = await _dbVentasContext.SalesProducts.AnyAsync(o => o.IdProduct == idProduct);

            return !hasDependencies;
        }

        [ValidateAntiForgeryToken]
        public async Task<Product> DeleteProduct(Product product)
        {

            Product productExistence = await _dbVentasContext.Products.FirstOrDefaultAsync(p => p.IdProduct == product.IdProduct);

            if (productExistence != null)
            {
                bool canDelete = await CanDeleteProduct(product.IdProduct);

                if (canDelete)
                {
                    _dbVentasContext.Remove(productExistence);
                    await _dbVentasContext.SaveChangesAsync();
                }
                else {

                    throw new InvalidOperationException("The product cannot be deleted due to dependencies on other entities");
                }

            }
           
            return product;
        }

    }
}
