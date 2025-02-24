using AssesmentByNimap.Models;
using System.Data;
using System.Data.SqlClient;

namespace AssesmentByNimap.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly IConfiguration configuration;

        private readonly string connectionString;

        //private readonly ICategoryService categoryService;

        public CategoryService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public int AddCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) {
                SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) VALUES (@categoryname)", conn);
                cmd.Parameters.AddWithValue("@categoryname", category.CategoryName); 
                conn.Open(); 
                return cmd.ExecuteNonQuery(); 
            }
        }

        public int DeleteCategory(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_delete_category", con);
                cmd.CommandType = CommandType.StoredProcedure; cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                con.Open();
                return cmd.ExecuteNonQuery();
            }

        }

        public List<Category> GetAllCategories()
        {
            List<Category> list = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM category", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Category
                    {
                        CategoryId = Convert.ToInt32(dr["categoryid"]),
                        CategoryName = dr["categoryname"].ToString()
                    });
                }
                conn.Close();
            }
            return list;


        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = new Category();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Category WHERE categoryid = @categoryid", conn);
                cmd.Parameters.AddWithValue("@categoryid", categoryId); 
                conn.Open(); 
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    category.CategoryId = Convert.ToInt32(dr["categoryid"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                }
            }
            return category;

        }

        public int UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Category SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId", conn);
                 cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }

        }
    }
}
