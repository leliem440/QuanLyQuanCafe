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
    public partial class Form1 : Form

    {
        public static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nitro\Documents\QuanLyQuanCaFe\Project\QuanLyQuanCafe-master\QuanLyQuanCafe-master\cafe.mdf;Integrated Security=True;";
        SqlConnection connect = new  SqlConnection(conn);

        public Form1()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_registerBtn_Click(object sender, EventArgs e)
        {
           RegisterForm frm = new RegisterForm();   
            frm.Show();
            this.Hide();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
           
               
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fileds are required to be filedd", "Error Message", MessageBoxButtons.OK);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string selectAccount = "SELECT COUNT(*) FROM users WHERE username = @usern AND password = @pass AND status = @status";
                        using (SqlCommand cmd = new SqlCommand(selectAccount, connect))
                        {
                            cmd.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", "Active");

                            int rowCount = (int)cmd.ExecuteScalar();
                            if (rowCount > 0)
                            {
                                string selectRole = "SELECT role FROM users WHERE username = @usern AND password = @pass";
                                using (SqlCommand getRole = new SqlCommand(selectRole, connect))
                                {
                                    getRole.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                                    getRole.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                                    string userRole = getRole.ExecuteScalar() as string;
                                    MessageBox.Show("Login Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (userRole == "Admin")
                                    {
                                        AdminMainForm adminForm = new AdminMainForm();
                                        adminForm.Show();

                                        this.Hide();
                                    }
                                    else if (userRole == "Cashier")
                                    {
                                        CashierMainForm cashierForm = new CashierMainForm();
                                        cashierForm.Show();
                                        this.Hide();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Password or there's no Admin's approval", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';  
        }
        public bool emptyFields()
        {
            if (login_username.Text == "" || login_password.Text == "")  
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
