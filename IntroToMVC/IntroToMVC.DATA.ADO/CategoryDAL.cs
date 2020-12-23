using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroToMVC.DATA.DOMAIN;
using System.Configuration; //for connectionString
using System.Data.SqlClient; //Access to connected sql classes
using System.Web.Configuration;//Added for the WebConfigManager

namespace IntroToMVC.DATA.ADO
{
    public class CategoryDAL
    {

        //Global string for access to the web.config connection string
        string cs = WebConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;

        //Show all categories
        public List<CategoryDomainModel> GetCategories()
        {
            //Create a holding variable for our results
            List<CategoryDomainModel> categories = new List<CategoryDomainModel>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName, Description FROM Categories", conn);

                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                //Process the results - now take each row from the reader and cast it from a DB category into a C# CategroyDomainModel object. After casting, we ill add the object to our list<>

                while (rdr.Read())
                {
                    CategoryDomainModel c = new CategoryDomainModel()
                    {
                        CategoryID = (int)rdr["CategoryID"],
                        CategoryName = (string)rdr["CategoryName"],
                        //nullable fields need to be converted from DBNull to a c# null value
                        Description = rdr["Description"] is DBNull ? null : (string)rdr["Description"]
                    };

                    //Add the CategoryDomainModel object to our list<>
                    categories.Add(c);
                }

                rdr.Close();
                conn.Close();
            }
            return categories;
        }

        //Create a Category
        public void AddCategory(CategoryDomainModel category)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Insert Into Categories (CategoryName,Description) Values (@CategoryName, @Description)", conn);

                //Sql Parameters should be used to fulfill any dynamic content to the query @CategoryName
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                //Handle any possible null values and translate from C# null to SQL DBNull
                if(category.Description == null)
                {
                    cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                } else
                {
                    cmd.Parameters.AddWithValue("@Description", category.Description);
                }
                //All we have to do is open the connection
                conn.Open();
                //And execute the command
                cmd.ExecuteNonQuery();
                //Normally this returns an int that represents the rowsAffeted, but we are not using a return value, so there is no reason to store it. If you did it would look like this:
                //int rowsAffected = cmd.ExecuteNonQuery();
            

            }//Close and destroy the connection
        }

        //Delete a Category
        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Delete FROM Categories Where CategoryID = @CategoryID", conn);

                conn.Open();

                cmd.Parameters.AddWithValue("@CategoryID", id);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
