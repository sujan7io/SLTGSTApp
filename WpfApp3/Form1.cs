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



namespace WpfApp3
{
    public partial class Form1: Form
    {
        DataTable table1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "SELECT Name, CodeHSN_SAC, OpeningStock, OpeningStockRatePerUnit FROM Items";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            table1 = new DataTable();
            adapter.Fill(table1);

            DataColumn theCol = new DataColumn("Sales", typeof(double));
            table1.Columns.Add(theCol);
            foreach (DataRow theRow in table1.Rows)
            {
                double var1 = double.Parse(theRow.ItemArray[2].ToString());
                double var2 = double.Parse(theRow.ItemArray[3].ToString());
                double value = var1 * var2;
                theRow[4] = value;
                table1.AcceptChanges();
            }


            foreach (DataRow theRow in table1.Rows)
            {
                this.chart1.Series["StockValueReport"].Points.AddXY(theRow.ItemArray[0].ToString(), theRow.ItemArray[4]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "SELECT Name, CodeHSN_SAC, OpeningStock, OpeningStockRatePerUnit FROM Items";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            table1 = new DataTable();
            adapter.Fill(table1);

            DataColumn theCol = new DataColumn("Sales", typeof(double));
            table1.Columns.Add(theCol);
            foreach (DataRow theRow in table1.Rows)
            {
                double var1 = double.Parse(theRow.ItemArray[2].ToString());
                double var2 = double.Parse(theRow.ItemArray[3].ToString());
                double value = var1 * var2;
                theRow[4] = value;
                table1.AcceptChanges();
            }


            foreach (DataRow theRow in table1.Rows)
            {
                this.chart2.Series["StockReport"].Points.AddXY(theRow.ItemArray[0].ToString(), theRow.ItemArray[2]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string query2 = "SELECT CustID,Name,Amount FROM SaleItems";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, conn);
            DataTable table3 = new DataTable();
            adapter2.Fill(table3);

            foreach (DataRow theRow in table3.Rows)
            {
                this.chart3.Series["SalesReport"].Points.AddXY(theRow.ItemArray[0].ToString(), double.Parse(theRow.ItemArray[2].ToString()));
                this.chart3.Series["SalesReport"].Points.AddXY(theRow.ItemArray[1].ToString(), double.Parse(theRow.ItemArray[2].ToString()));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query2 = "SELECT CustID,Name,Discount,Tax,Amount FROM SaleItems";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, conn);
            adapter2.SelectCommand.ExecuteNonQuery();
            DataTable table3 = new DataTable();
            adapter2.Fill(table3);


            foreach (DataRow theRow in table3.Rows)
            {
                string itemName = theRow.ItemArray[1].ToString();
                string query3 = "SELECT PurchaseRate FROM Items WHERE Name='" + itemName + "'";
                SqlDataAdapter adapter3 = new SqlDataAdapter(query3, conn);
                adapter3.SelectCommand.ExecuteNonQuery();
                DataTable table4 = new DataTable();
                adapter3.Fill(table4);

                foreach (DataRow theRow2 in table4.Rows)
                {
                    double saleRate = double.Parse(theRow.ItemArray[4].ToString()) - double.Parse(theRow.ItemArray[3].ToString()) - double.Parse(theRow.ItemArray[2].ToString());
                    double profitLoss = saleRate - double.Parse(theRow2.ItemArray[0].ToString());
                    this.chart4.Series["ProfitLoss"].Points.AddXY(theRow.ItemArray[1].ToString(), profitLoss);
                    this.chart4.Series["ProfitLoss"].Points.AddXY(theRow.ItemArray[0].ToString(), profitLoss );
                }
            }
        }
    }
}
