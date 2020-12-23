using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient; //To access all connected Sql Classes
using System.Web.Configuration; //Added for access to the web.config connection string
using IntroToMVC.DATA.ADO; //Added for DAL access
using IntroToMVC.DATA.DOMAIN;//Added for CategoryDomainModel access

#region Connected Sql (ADO.NET) Rules for data access
/*
 * ConnectionString has 3 basic parts: Data Source (Server), Ininital Catalog (Db Name), Integrated Security (True/False, SSPI). True Indicates Windows (integrated) authentication) False indicates Sql SErver authentication which requires a username and password combo. SSPI basically works the same as false, but is rarely used.
 * 
 * SqlCommand Object. THe command requires a minimum of CommandText (sql query as a string) and a sqlConnection object to initialize the request
 * Availible methods (sqlCommand)
 * ExecuteReader() - Select statments - ReturnTye is SqlDataReader
 * ExecuteNonQuery() - Insert, Update, delete statements -  Return type is an int. Usually to show the rows affected by the statement.
 * ExecuteScalar() - Aggregate functions: SUM, AVG, COUNT, etc. -ReturnType is object.
 * If a parameterized query is written, you can use the Parameters property and the AddWithValue() to specify both its parameter and its value.
 * 
 * SqlDataReader - This object holds the results of the command objects ExecuteReader().
 * The reader will need to be looped through (if more than one result is desired) or branched if a single result is requied.
 * To call the ExecuteReader() the connect objects Open() must have been called. When the reader is done, you should call the Sql.DataRader.Close().
 * 
 */
#endregion

namespace IntroToMVC.UI.MVC.Controllers
{
    public class IntroToADOController : Controller
    {
        //global setting for access to the web.config connection string
        string cs = WebConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
        // GET: IntroToADO
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts()
        {
            //define the connection string
            //string cs = @"Data source =.\sqlexpress;Initial Catalog=Northwind;Integrated Security = true;";

            //Create the connection to the DB (SqlConnection)
            using (SqlConnection conn = new SqlConnection(cs))
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("SELECT ProductName FROM Products", conn);

                //Open the connection
                conn.Open();

                //Execute the query- since it is a select, we will store results in a SqlDataReader
                SqlDataReader rdr = cmd.ExecuteReader();

                //Process the results - Usually with reader, this involves a while loop. Store the data in a string or whatever C# object you may need.
                string productNames = "";

                //Read() advances to the next record and gets a boolean value letting us know if there is another record available.
                while (rdr.Read())
                {
                                        //Field Name of in the table. Not case sensitive
                    productNames += rdr["ProductName"] + "<br />";
                }

                //Close the reader
                rdr.Close();

                //Close the connection
                conn.Close();

                //Pass the data to the view
                ViewBag.Products = productNames;

            }//At the end of the using, the SqlConnection will be close and the connection object will be disposed from memory.

            return View();
        }

        //Create an action that queries the products table and returns dicontinued products to the view
        public ActionResult GetDiscontinuedProducts()
        {
            //Create the Connection
            using (SqlConnection conn = new SqlConnection(cs))
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("SELECT ProductName FROM Products WHERE Discontinued = 1", conn);
                
                //Open the connection
                conn.Open();

                //Execute the command
                SqlDataReader rdr = cmd.ExecuteReader();

                //process the results
                string products = "";

                while (rdr.Read())
                {
                    products += rdr["ProductName"] + "<br />";
                }

                //Close the reader, and Close the connection
                rdr.Close();
                conn.Close();

                ViewBag.Products = products;
            }

            return View();
        }

        //MINI LAB
        //Create an action that queries the products table and returns the products where the unitprice is more than $20. Display the productName, as well as the price.
        public ActionResult DisplayProductPriceTwenty()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductName, Format(UnitPrice, 'c') as [myUnitPrice] FROM Products WHERE UnitPrice > 20", conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                string productAndCost = "";

                //"&nbsp&nbsp" +
                while (rdr.Read())
                {
                    productAndCost += rdr["ProductName"] + "&nbsp&nbsp&nbsp&nbsp" + rdr["myUnitPrice"] + "<br />";
                }

                rdr.Close();
                conn.Close();

                ViewBag.ProductsAndCost = productAndCost;
            }

            return View();
        }


        //Below uses the DAL(Data Access Layer) method we created in the Data.ADO project.
        public ActionResult AllCategories()
        {
            //To call the GetCategories() method, we need an instance of it's class CategoryDAL
            CategoryDAL dal = new CategoryDAL();

            List<CategoryDomainModel> cats = dal.GetCategories();

            return View(cats);
        }

        //Create
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(CategoryDomainModel category)
        {
            //send category to the DAL and return the all categoreis view.
            CategoryDAL dal = new CategoryDAL();
            dal.AddCategory(category);

            return RedirectToAction("AllCategories");
        }

        public ActionResult DeleteCategory(int id)
        {
            CategoryDAL dal = new CategoryDAL();
            dal.DeleteCategory(id);
            return RedirectToAction("AllCategories");
        }
    }
}