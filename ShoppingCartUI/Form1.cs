using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DataSource;
namespace ShoppingCartUI
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Form Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            scrollValue = 0;
        }
        int scrollValue;
        DataSet set;
        SqlDataAdapter adapter;
        int ID;
        int CartID;

        /// <summary>
        /// The event Handler for Adding into Product 
        /// It calls the Add Product method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            if(textBoxProductName.Text != "" && textBoxCostPrice.Text != "")
            {
                string productName = textBoxProductName.Text;
                decimal cp = Convert.ToDecimal(textBoxCostPrice.Text);
                DataSource.ProductSource.AddProduct(productName, cp);
            }
            textBoxProductName.Text = "";
            textBoxCostPrice.Text = "";
        }


        /// <summary>
        /// The event handler for Remove Product.
        /// It calls the Method Remove Product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveProduct_Click(object sender, EventArgs e)
        {
            if (textBoxProductName.Text !="")
            {
                string productName = textBoxProductName.Text;
                DataSource.ProductSource.DeleteProduct(productName);
            }
            textBoxProductName.Text = "";
        }
        /// <summary>
        /// The event handler for edit Product
        /// It calls the the update Product method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (textBoxProductName.Text != "" && textBoxCostPrice.Text != "")
            {
                string productName = textBoxProductName.Text;
                decimal cp = Convert.ToDecimal(textBoxCostPrice.Text);
                DataSource.ProductSource.UpdateProduct(productName, cp);
            }
            textBoxProductName.Text = "";
            textBoxCostPrice.Text = "";
        }
        /// <summary>
        /// On form Load it loads the product in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            string str = "Data Source=(LocalDB)\\MSSQLLocalDB;Database=C:\\Users\\hp\\source\\repos\\ShoppingCart\\ShoppingCartUI\\ShoppingCartDB.mdf;Integrated Security = True";
            string query = "SELECT * FROM Product";
            SqlConnection con = new SqlConnection(str);
            adapter = new SqlDataAdapter(query, con);
            set = new DataSet();
            con.Open();
            adapter.Fill(set, "Product");
            con.Close();
            dataGridView1.DataSource = set.Tables["Product"];
            label5.Text = dataGridView1.Rows.Count.ToString();


            

        }
        /// <summary>
        /// this is the event handler for the button Next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                scrollValue = scrollValue + 5;

                if (scrollValue >= int.Parse(label5.Text))
                {
                    scrollValue = int.Parse(label5.Text);
                }
                set.Clear();
                adapter.Fill(set, scrollValue, 5, "Product");
                label7.Text = "Item  " + (scrollValue + 1) + "  Of";
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error!!!", ex);
            }
        }
        /// <summary>
        /// This is the event Handler for the Previous
        /// It shows the previous product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            try
            {
                scrollValue = scrollValue - 5;
                if (scrollValue <= 0)
                {
                    scrollValue = 0;
                }
                set.Clear();
                adapter.Fill(set, scrollValue, 5, "Product");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error!!!", ex);
            }
        }

        /// <summary>
        /// This is the Event handler for the search Product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            string proName = textBoxProductName.Text;
            var result = DataSource.ProductSource.SearchProduct(proName);
  
            while (result.Read())
            {
                dataGridView3.Rows.Add(result.GetInt32(0), result.GetString(1), result.GetDecimal(2), result.GetDateTime(3));

            }

            result.Close();
        }


        /// <summary>
        /// This gets the ID from the row or column that was selected based on the datagrid view selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong Field Data Inputted or Clicked");
            }
        }

        /// <summary>
        /// This is add to the database
        /// and shows the item on the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                int id = ID;
                int quantity = int.Parse(textBox2.Text);
                DataSource.CartSource.AddCart(id, quantity);
                textBox2.Text = "";
                
                string str = "Data Source=(LocalDB)\\MSSQLLocalDB;Database=C:\\Users\\hp\\source\\repos\\ShoppingCart\\ShoppingCartUI\\ShoppingCartDB.mdf;Integrated Security = True";
                string query = " SELECT p.Id, p.Product_Name, p.Cost_Price, c.Quantity FROM Cart c INNER JOIN Product p on c.ProductId = p.Id";
                SqlConnection con = new SqlConnection(str);
                SqlDataAdapter adapter = new SqlDataAdapter(query,con);
                
                DataSet da = new DataSet();
                con.Open();
                adapter.Fill(da);
                con.Close();
                dataGridView2.DataSource = set.Tables["Product"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveCart_Click(object sender, EventArgs e)
        {
            try
            {
                DataSource.CartSource.RemoveCart(CartID);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// it gest the item Id that was selected from the Data grid view 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView2.CurrentRow.Selected = true;
                    CartID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong Field Data Inputted or Clicked");
            }
        }

        private void buttonshowCart(object sender, EventArgs e)
        {
            string str = "Data Source=(LocalDB)\\MSSQLLocalDB;Database=C:\\Users\\hp\\source\\repos\\ShoppingCart\\ShoppingCartUI\\ShoppingCartDB.mdf;Integrated Security = True";
            string query = " SELECT p.Id, p.Product_Name, p.Cost_Price, c.Quantity FROM Cart c INNER JOIN Product p on c.ProductId = p.Id";
            SqlConnection con = new SqlConnection(str);
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);

            DataSet da = new DataSet();
            con.Open();
            adapter.Fill(da);
            con.Close();
            dataGridView2.DataSource = set.Tables["Product"];
        }
    }
}
