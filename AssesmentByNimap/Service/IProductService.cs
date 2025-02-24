using AssesmentByNimap.Models;

namespace AssesmentByNimap.Service
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        int AddProduct(Product product);
        Product GetProductById(int id);
        int UpdateProduct(Product product);
        int DeleteProduct(int id);
    }
}
