using System.Data.SqlClient;

namespace AssesmentByNimap.Models
{
    public class CategoryCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;

        public CategoryCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }

        //get all Category list
        public List<Category> GetAllCategories()
        {
            List<Category> list = new List<Category>();
            cmd = new SqlCommand("select * from Category", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Category category = new Category();
                    category.CategoryId = Convert.ToInt32(dr["categoryid"]);
                    category.CategoryName = dr["categoryname"].ToString();
                    list.Add(category);
                }
            }
            con.Close();
            return list;
        }

        public Category GetCategoryById(int categoryid)
        {
            Category category = new Category();
            cmd = new SqlCommand("select * from category where categoryid=@categoryid", con);
            cmd.Parameters.AddWithValue("@categoryid", categoryid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    {

                        category.CategoryId = Convert.ToInt32(dr["categoryid"]);
                        category.CategoryName = dr["categoryname"].ToString();
                    }
                }
            }
            con.Close();
            return category;
        }


        //Add category
        public int AddCategory(Category category)
        {
            int result = 0;
            string qry = "insert into Category  values(@categoryname)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@categoryname", category.CategoryName);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int UpdateCategory(Category category)
        {
            int result = 0;
            string qry = "update category set name=@cname where categoryid=@categoryid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@categoryname", category.CategoryName);

            cmd.Parameters.AddWithValue("@categoryid", category.CategoryId);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int DeleteCategory(int catid)
        {
            int result = 0;
            string qry = "delete from category where categoryid=@categoryid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@categoryid", catid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
