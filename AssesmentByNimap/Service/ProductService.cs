using AssesmentByNimap.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AssesmentByNimap.Service
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration configuration;

        private readonly string connectionString;

        public ProductService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int AddProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductName) VALUES (@productname)", conn);
                cmd.Parameters.AddWithValue("@productname", product.ProductName);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductId = @ProductId", con);
                cmd.Parameters.AddWithValue("@id", id); conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM product", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Product
                    {
                     ProductId = Convert.ToInt32(dr["productid"]),
                        ProductName = dr["productname"].ToString()
                    });
                }
                conn.Close();
            }
            return list;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE productid = @productid", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    product.ProductId = Convert.ToInt32(dr["productid"]);
                    product.ProductName = dr["ProductName"].ToString();
                }
            }
            return product;
        }

        public int UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductName = @ProductName WHERE ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
