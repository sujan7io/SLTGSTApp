using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for UserControlInventoryReport.xaml
    /// </summary>
    public partial class UserControlInventoryReport : UserControl
    {
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommandBuilder scb;
        private Form1 form1;
        DataTable table1;
        public UserControlInventoryReport()
        {
            InitializeComponent();
        }
        //OpeningStock * OpeningStockRatePerUnit = Sales
        //Name-Code
        private void showReport_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            String query = "SELECT Type,Name,Unit,CodeHSN_SAC,TaxPreference,SalesRate,SalesDescription,PurchaseRate,PurchaseDescription,IGST,CGST,SGST,OpeningStock,OpeningStockRatePerUnit,CurrentStock FROM Items";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            dt = new DataTable();
            sda.Fill(dt);
            ReportView.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void showReportSales_Click(object sender, RoutedEventArgs e)
        {
            //replace server name and instance with name that appears in SQL Server Management Studio login window
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "SELECT Name, CodeHSN_SAC, OpeningStock, OpeningStockRatePerUnit, CurrentStock FROM Items";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.ExecuteNonQuery();
            table1 = new DataTable();
            adapter.Fill(table1);

            DataColumn theCol = new DataColumn("Sales (INR)", typeof(double));
            table1.Columns.Add(theCol);
            foreach (DataRow theRow in table1.Rows)
            {
                double var1 = double.Parse(theRow.ItemArray[2].ToString());
                double var2 = double.Parse(theRow.ItemArray[3].ToString());
                double var3 = double.Parse(theRow.ItemArray[4].ToString());
                double value = (var1 - var3) * var2;
                theRow[5] = value;
                table1.AcceptChanges();
            }
            if (chartGrid.Children.Count >= 2)
                chartGrid.Children.RemoveAt(1);
            ReportViewSales.ItemsSource = table1.DefaultView;
            conn.Close();
        }

        private void ChartViewSales_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Integration.WindowsFormsHost host =
               new System.Windows.Forms.Integration.WindowsFormsHost();
            form1 = new Form1();
            form1.TopLevel = false;
            host.Child = form1;
            Grid res = this.chartGrid as Grid;
            form1.Width = (int)res.Width;
            form1.Height = (int)res.Height;
            res.Children.Add(host);
        }
    }
}
