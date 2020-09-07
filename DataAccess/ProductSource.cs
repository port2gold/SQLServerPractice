using ClassLibrary;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;




namespace DataSource

{
    public class ProductSource
    {
        /// <summary>
        /// It handles connection to Product Database
        /// </summary>
         static SqlConnection connection;
        
        
         static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Database=C:\\Users\\hp\\source\\repos\\ShoppingCart\\ShoppingCartUI\\ShoppingCartDB.mdf;Integrated Security = True";
        
        /// <summary>
        /// It Add Entities to Product.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="costprice"></param>
        public static void AddProduct(string productName, decimal costprice)
        {
           try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO Product(Product_Name, Cost_Price) VALUES('" + productName + "'," + costprice+"  )";

                //Product product = new Product { CostPrice = costprice, DateAdded = dateAdded, ProductName = productName };
                SqlCommand command = new SqlCommand(query, connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
           {
                Console.WriteLine("Error", ex);
            }
            
        }
        
        /// <summary>
        /// It removes entitities from product
        /// </summary>
        /// <param name="productName"></param>
        public static void DeleteProduct(string productName)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"DELETE from Product WHERE Product_Name = '" + productName + "'";

                
                SqlCommand command = new SqlCommand(query, connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex);
            }
        }
        

        public static void UpdateProduct(string productName, decimal price)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "UPDATE Product SET Cost_Price = "+price+" WHERE Product_Name = '"+ productName+ "'";
                
                SqlCommand command = new SqlCommand(query, connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                 Console.WriteLine("Error", ex);
            }

        }

        public static SqlDataReader SearchProduct(string productName)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var query = "SELECT * FROM Product WHERE Product_Name= '" + productName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();

        }

        
        
    }
}
