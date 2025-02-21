using AssesmentByNimap.Models;
using System.Data.SqlClient;
namespace AssesmentByNimap.Models
{
    public class ProductCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public ProductCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
        //get all product list

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            cmd = new SqlCommand("select p.ProductId,p.ProductName, c.CategoryId, c.CategoryName from Product p join Category c on p.CategoryId=c.CategoryId", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.ProductId = Convert.ToInt32(dr["productid"]);
                    product.ProductName = dr["productname"].ToString();
                    product.CategoryId = Convert.ToInt32(dr["categoryid"]);
                    product.CategoryName = dr["categoryname"].ToString();
                    products.Add(product);

                }
            }
            con.Close();
            return products;
        }
        public Product GetProductById(int productid)
        {
            Product product = new Product();
            cmd = new SqlCommand("select*from Product where productid=@productid", con);
            cmd.Parameters.AddWithValue("@productid", productid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    product.ProductId = Convert.ToInt32(dr["productid"]);
                    product.ProductName = dr["productname"].ToString();
                }
            }
            con.Close();
            return product;
        }

        public int AddProduct(Product product)
        {
            int result = 0;
            string qry = "insert into product(productname, categoryid) values(@productname,@categoryid)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@productname", product.ProductName);
            cmd.Parameters.AddWithValue("@categoryid", product.CategoryId);
            con.Open();
            result = cmd.ExecuteNonQuery();

            con.Close();
            return result;
        }
        public int UpdateProduct(Product product)
        {
            int result = 0;
            string qry = "update Product set productname=@productname where productid=@productid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@productname", product.ProductName);
            cmd.Parameters.AddWithValue("@categoryid", product.CategoryId);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from Product where productid=@productid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@productid", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
    }
}

