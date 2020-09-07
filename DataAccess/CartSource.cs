using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace DataSource
{
    public class CartSource
    {

        /// <summary>
        /// It handles the Cart table
        /// </summary>
        /// <param name="Productid"></param>
        /// <param name="Qty"></param>
        /// 
        //This Method Add to cart.
        public static void  AddCart(int Productid, int Qty)
        {
             SqlConnection connect;
             string connectionString= "Data Source=(LocalDB)\\MSSQLLocalDB;Database=C:\\Users\\hp\\source\\repos\\ShoppingCart\\ShoppingCartUI\\ShoppingCartDB.mdf;Integrated Security = True";
            try
            {
                connect = new SqlConnection(connectionString);
                connect.Open();
                string query = "INSERT INTO Cart(ProductId, Quantity) VALUES('" + Productid + "'," + Qty + "  )";

               
                SqlCommand command = new SqlCommand(query, connect);

                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex);
            }
        }
        /// <summary>
        /// This Method Removes from Cart
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveCart(int id)
        {
            try
            {
                SqlConnection conn;
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Database=C:\\Users\\hp\\source\\repos\\ShoppingCart\\ShoppingCartUI\\ShoppingCartDB.mdf;Integrated Security = True";
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = $"DELETE from Cart WHERE Id = " + id + "";


                SqlCommand command = new SqlCommand(query, conn);

                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex);
            }

        }
    }
}
