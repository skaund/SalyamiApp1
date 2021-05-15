
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SalyamiApp
{
    public partial class Navigation : Form
    {
        [DllImport("Kernel32.dll")]
        static extern Boolean AllocConsole();

        public Navigation()
        {
            InitializeComponent();
        }   

        /// <summary>
        /// Opens the FillorCancel form as a dialog box. 
        /// </summary>
        private void btnGoToFillOrCancel_Click(object sender, EventArgs e)
        {
            Form frm = new FillOrCancel();
            frm.ShowDialog();
        }

        /// <summary>
        /// Closes the application (not just the Navigation form).
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGoToAdd_Click_1(object sender, EventArgs e)
        {
            Form frm = new NewCustomer();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AllocConsole())
                MessageBox.Show("Failed");

            List<Order> orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                // Define a t-SQL query string that has a parameter for orderID.
                const string sql = "SELECT [CustomerID],[OrderID],[OrderDate],[FilledDate],[Status],[Amount] FROM[Sales].[Sales].[Orders]";

                // Create a SqlCommand object.
                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {

                    try
                    {
                        connection.Open();
                        var read = sqlCommand.ExecuteReader();
                        while (read.Read())
                        {
                            orders.Add(

                                new Order()
                                {
                                    CustomerID = read.GetInt32(0),
                                    OrderID = read.GetInt32(1),
                                    OrderDate = read.GetDateTime(2),
                                    FilledDate = read.GetDateTime(3),
                                    Status = read.GetString(4),
                                    Amount = read.GetInt32(5)
                                }
                             );

                        }
                    }
                    catch
                    {
                        MessageBox.Show("The requested order could not be loaded into the form.");
                    }
                    finally
                    {
                        // Close the connection.
                        connection.Close();
                    }
                }
            }
            string s = "CustomerID\t OrderID\t OrderDate\t\t\t FilledData\t\t\t Status\t\t\t Amount";
            Console.WriteLine(s);
            Console.WriteLine(new string('_',s.Length+50));

            foreach (var item in orders)
            {
                Console.WriteLine(item.CustomerID + "\t\t" + item.OrderID + "\t\t" + item.OrderDate + "\t\t" + item.FilledDate + "\t\t" +
                    item.Status + "\t\t" + item.Amount);

            }
        }
    }
}
