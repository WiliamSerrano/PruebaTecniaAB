using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Interfaces
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> GetProductToDelete(int id);
        Task<Product> DeleteProduct(Product product);

    }
}
