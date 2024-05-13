using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class CashierMainForm : Form
    {
        public CashierMainForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to sign out?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void cashierOrderForm1_Load(object sender, EventArgs e)
        {
            
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            aminDashboardForm1.Visible = true;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            AminDashboardForm adFrom = aminDashboardForm1 as AminDashboardForm;
            if (adFrom != null)
            {
                adFrom.refreshData();
            }

        }

        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            aminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = true;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            AdminAddProducts aaProd = adminAddProducts1 as AdminAddProducts;
            if (aaProd != null)
            {
                aaProd.refreshData();
            }
        }

        private void order_btn_Click(object sender, EventArgs e)
        {
            aminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = true;
            cashierCustomersForm1.Visible = false;

            CashierOrderForm coForm = cashierOrderForm1 as CashierOrderForm;
            if (coForm != null)
            {
                coForm.refreshData();
            }
        }

        private void customer_btn_Click(object sender, EventArgs e)
        {
            aminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = true;

            CashierCustomersForm ccForm = cashierCustomersForm1 as CashierCustomersForm;
            if (ccForm != null)
            {
                ccForm.refreshData();
            }
        }
    }
}
