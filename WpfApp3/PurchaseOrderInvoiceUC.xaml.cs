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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for SalesOrderUC.xaml
    /// </summary>
    public partial class PurchaseOrderInvoiceUC : UserControl
    {
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommandBuilder scb;
        public PurchaseOrderInvoiceUC()
        {
            InitializeComponent();
        }

        public static string CreateTABLEPablo(string tableName, System.Data.DataTable table)
        {
            string sqlsc;
            //using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            //connection.Open();
            sqlsc = "CREATE TABLE " + tableName + "(";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n" + table.Columns[i].ColumnName;
                if (table.Columns[i].DataType.ToString().Contains("System.Int32"))
                    sqlsc += " int ";
                else if (table.Columns[i].DataType.ToString().Contains("System.DateTime"))
                    sqlsc += " datetime ";
                else if (table.Columns[i].DataType.ToString().Contains("System.String"))
                    sqlsc += " nvarchar(" + table.Columns[i].MaxLength.ToString() + ") ";
                else if (table.Columns[i].DataType.ToString().Contains("System.Single"))
                    sqlsc += " single ";
                else if (table.Columns[i].DataType.ToString().Contains("System.Double"))
                    sqlsc += " double ";
                else
                    sqlsc += " nvarchar(" + table.Columns[i].MaxLength.ToString() + ") ";



                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }

            string pks = "\nCONSTRAINT PK_" + tableName + " PRIMARY KEY (";
            for (int i = 0; i < table.PrimaryKey.Length; i++)
            {
                pks += table.PrimaryKey[i].ColumnName + ",";
            }
            pks = pks.Substring(0, pks.Length - 1) + ")";

            sqlsc += pks;

            return sqlsc + ")";
        }

        private bool TableExists(SqlConnection conn, string database, string name)
        {
            string strCmd = null;
            SqlCommand sqlCmd = null;

            try
            {
                strCmd = "select case when exists((select '['+SCHEMA_NAME(schema_id)+'].['+name+']' As name FROM [" + database + "].sys.tables WHERE name = '" + name + "')) then 1 else 0 end";
                sqlCmd = new SqlCommand(strCmd, conn);

                return (int)sqlCmd.ExecuteScalar() == 1;
            }
            catch { return false; }
        }

        private void CreateTable_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //Execute NonQuery or ExecuteReader
            System.Data.DataTable table = new System.Data.DataTable("PurchaseOrderInvoice");

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "VendorName",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "BillNo",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "OrderNo",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "BillDate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "DueDate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "PaymentTerms",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Subtotal",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Adjustment",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Total",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "CustNotes",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "TermsNCond",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            string sqlc1;
            table.PrimaryKey = new DataColumn[] { table.Columns[1] };
            con.Open();
            bool ifExists = TableExists(con, "master", "SalesInvoice");
            if (!ifExists)
                sqlc1 = CreateTABLEPablo("SalesInvoice", table);
            else
                return;
            sda = new SqlDataAdapter(sqlc1, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine(sqlc1);
            con.Close();
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String query = "SELECT VendorName,BillNo,OrderNo,BillDate,DueDate,PaymentTerms,Subtotal,Adjustment,Total,CustNotes,TermsNCond FROM PurchaseOrderInvoice";
            sda = new SqlDataAdapter(query, con);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridTable.ItemsSource = dt.DefaultView;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void SaveAsDraft_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            String query = "INSERT INTO PurchaseOrderInvoice (VendorName,BillNo,OrderNo,BillDate,DueDate,PaymentTerms,Subtotal,Adjustment,Total,CustNotes,TermsNCond) VALUES('" + VendorName.Text + "','" + BillNo.Text + "','" + OrderNo.Text + "','" + BillDate.Text + "','" + DueDate.Text + "','" + DueDate.Text + "','" + PaymentTerms.Text + "','" + SubTotal.Text + "','" + Adjustment.Text + "','" + Total.Text + "','" + CustNotes.Text + "','" + TermAndCondns.Text + "')";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED DATA IN TABLE PurchaseOrderInvoice !");
            con.Close();
        }

        private void SaveAndSend_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            String query = "INSERT INTO PurchaseOrderInvoice (VendorName,BillNo,OrderNo,BillDate,DueDate,PaymentTerms,Subtotal,Adjustment,Total,CustNotes,TermsNCond) VALUES('" + VendorName.Text + "','" + BillNo.Text + "','" + OrderNo.Text + "','" + BillDate.Text + "','" + DueDate.Text + "','" + DueDate.Text + "','" + PaymentTerms.Text + "','" + SubTotal.Text + "','" + Adjustment.Text + "','" + Total.Text + "','" + CustNotes.Text + "','" + TermAndCondns.Text + "')";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED DATA IN TABLE PurchaseOrderInvoice !");
            con.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            VendorName.Clear();
            BillNo.Clear();
            OrderNo.Clear();
            BillDate.Clear();
            DueDate.Clear();
            SubTotal.Clear();
            Adjustment.Clear();
            Total.Clear();
            CustNotes.Clear();
            TermAndCondns.Clear();
        }
    }
}
