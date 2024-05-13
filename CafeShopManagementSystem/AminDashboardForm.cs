using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    public partial class AminDashboardForm : UserControl
    {
        SqlConnection connect = new SqlConnection(Form1.conn);
        public AminDashboardForm()
        {
            InitializeComponent();
            displayTotalCashier();
            displayTotalCustomers();
            displayTotalIncome();
            displayTodaysIncome();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayTotalCashier();
            displayTotalCustomers();
            displayTotalIncome();
            displayTodaysIncome();
        }

        public void displayTotalCashier()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM users WHERE role = @role AND status = @status";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@role", "Cashier");
                        cmd.Parameters.AddWithValue("@status", "Active");

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TC.Text = count.ToString();
                        }
                        reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        public void displayTotalCustomers()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                       

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TCust.Text = count.ToString();
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }

        }
        public void displayTotalIncome()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT SUM(total_price) FROM customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TI.Text = "$" +  count.ToString("0.00");
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTodaysIncome()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT sum(total_price) FROM customers WHERE DATE = @date";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        DateTime today = DateTime.Today;
                        string getToday = today.ToString("yyyy-MM-dd");
                        cmd.Parameters.AddWithValue("@date", getToday);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);

                            dashboard_TIn.Text = "$" + count.ToString("0.00");
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

    }


}
