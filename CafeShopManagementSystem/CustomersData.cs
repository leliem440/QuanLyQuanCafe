using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    internal class CustomersData
    {
        SqlConnection connect = new SqlConnection(Form1.conn);
        public int CustomnerID { get; set; }
        public String TotalPrice { get; set; }
        public String Amount { get; set; }
        public String Change { get; set; }
        public String Date { get; set; }

        public List<CustomersData> allCustomersData()
        {
            List<CustomersData> listData = new List<CustomersData>();
            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT * FROM customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomersData cData = new CustomersData();
                            cData.CustomnerID = (int)reader["customer_id"];
                            cData.TotalPrice = reader["total_price"].ToString();
                            cData.Amount = reader["amount"].ToString();
                            cData.Change = reader["change"].ToString();
                            cData.Date = reader["date"].ToString();


                            listData.Add(cData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed connection: "+ ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }
    }
}
