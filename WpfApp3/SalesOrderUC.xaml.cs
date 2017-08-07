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
    public partial class SalesOrderUC : UserControl
    {
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommandBuilder scb;
        Form2 addItem;
        SqlDataAdapter adapter2;
        DataTable table3;
        public SalesOrderUC()
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
            System.Data.DataTable table = new System.Data.DataTable("SalesOrder");

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "CustName",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "SalesOrderNo",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "ReferenceNo",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "SalesOrderDate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "ShipmentDate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "DeliveryMethod",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "SalesPerson",
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
            table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            con.Open();
            bool ifExists = TableExists(con, "(localdb)\\MSSQLLocalDB", "SalesOrder");
            if (!ifExists)
                sqlc1 = CreateTABLEPablo("SalesOrder", table);
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
            String query = "SELECT CustName,SalesOrderNo,ReferenceNo,SalesOrderDate,ShipmentDate,DeliveryMethod,SalesPerson,Subtotal,Adjustment,Total,CustNotes,TermsNCond FROM SalesOrder";
            sda = new SqlDataAdapter(query, con);
            dt = new DataTable();
            sda.Fill(dt);
            DataGridTable.ItemsSource = dt.DefaultView;
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

            String query = "INSERT INTO SalesOrder (CustName,SalesOrderNo,ReferenceNo,SalesOrderDate,ShipmentDate,DeliveryMethod,SalesPerson,Subtotal,Adjustment,Total,CustNotes,TermsNCond) VALUES('" + CustName.Text + "','" + SalesOrderNo.Text + "','" + ReferenceNo.Text + "','" + SalesOrderDate.Text + "','" + ShipmentDate.Text + "','" + DeliveryMethod.Text + "','" + Salesperson.Text + "','" + SubTotal.Text + "','" + Adjustment.Text + "','" + Total.Text + "','" + CustNotes.Text + "','" + TermAndCondns.Text + "')";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED DATA IN TABLE SalesOrder !");

            string custName = CustName.Text;

            String query2 = "SELECT Name,Quantity FROM SaleItems WHERE CustName='" + custName + "'";
            sda = new SqlDataAdapter(query2, con);
            DataTable table2 = new DataTable();
            sda.SelectCommand.ExecuteNonQuery();
            sda.Fill(table2);

            string itemName = "";
            int result2 = 0;
            foreach (DataRow theRow2 in table2.Rows)
            {
                itemName = theRow2.ItemArray[0].ToString();
                result2 = int.Parse(theRow2.ItemArray[1].ToString());
                string query4 = "SELECT CurrentStock FROM Items WHERE Name='" + itemName + "'";
                sda = new SqlDataAdapter(query4, con);
                DataTable table3 = new DataTable();
                sda.SelectCommand.ExecuteNonQuery();
                sda.Fill(table3);

                int curStock = 0;
                int result3 = 0;
                foreach (DataRow theRow3 in table3.Rows)
                {
                    if (int.TryParse(theRow3.ItemArray[0].ToString(), out result3))
                    {
                        curStock = result3 - result2;
                        break;
                    }

                }
                string query3 = "UPDATE Items SET CurrentStock='" + curStock.ToString() + "' WHERE Name='" + itemName + "'";
                adapter2 = new SqlDataAdapter(query3, con);
                adapter2.SelectCommand.ExecuteNonQuery();
                Console.WriteLine("UPDATED DATA IN TABLE Items !");
            }

            con.Close();
        }

        private void SaveAndSend_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();

            String query = "INSERT INTO SalesOrder (CustName,SalesOrderNo,ReferenceNo,SalesOrderDate,ShipmentDate,DeliveryMethod,SalesPerson,Subtotal,Adjustment,Total,CustNotes,TermsNCond) VALUES('" + CustName.Text + "','" + SalesOrderNo.Text + "','" + ReferenceNo.Text + "','" + SalesOrderDate.Text + "','" + ShipmentDate.Text + "','" + DeliveryMethod.Text + "','" + Salesperson.Text + "','" + SubTotal.Text + "','" + Adjustment.Text + "','" + Total.Text + "','" + CustNotes.Text + "','" + TermAndCondns.Text + "')";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED DATA IN TABLE SalesOrder !");

            string custName = CustName.Text;

            String query2 = "SELECT Name,Quantity FROM SaleItems WHERE CustName='" + custName + "'";
            sda = new SqlDataAdapter(query2, con);
            DataTable table2 = new DataTable();
            sda.SelectCommand.ExecuteNonQuery();
            sda.Fill(table2);

            string itemName = "";
            int result2 = 0;
            foreach (DataRow theRow2 in table2.Rows)
            {
                itemName = theRow2.ItemArray[0].ToString();
                result2 = int.Parse(theRow2.ItemArray[1].ToString());
                string query4 = "SELECT CurrentStock FROM Items WHERE Name='" + itemName + "'";
                sda = new SqlDataAdapter(query4, con);
                DataTable table3 = new DataTable();
                sda.SelectCommand.ExecuteNonQuery();
                sda.Fill(table3);

                int curStock = 0;
                int result3 = 0;
                foreach (DataRow theRow3 in table3.Rows)
                {
                    if (int.TryParse(theRow3.ItemArray[0].ToString(), out result3))
                    {
                        curStock = result3 - result2;
                        break;
                    }

                }
                string query3 = "UPDATE Items SET CurrentStock='" + curStock.ToString() + "' WHERE Name='" + itemName + "'";
                adapter2 = new SqlDataAdapter(query3, con);
                adapter2.SelectCommand.ExecuteNonQuery();
                Console.WriteLine("UPDATED DATA IN TABLE Items !");
            }

            con.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CustName.Clear();
            SalesOrderNo.Clear();
            ReferenceNo.Clear();
            SalesOrderDate.Clear();
            ShipmentDate.Clear();
            SubTotal.Clear();
            Adjustment.Clear();
            Total.Clear();
            CustNotes.Clear();
            TermAndCondns.Clear();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            addItem = new Form2();
            addItem.setSalesUC(this);
            addItem.ShowDialog();
        }

        private void ShowItem_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            string query2 = "SELECT CustID,Name,Quantity,Rate,Discount,Tax,Amount FROM SaleItems";
            adapter2 = new SqlDataAdapter(query2, con);
            adapter2.SelectCommand.ExecuteNonQuery();
            table3 = new DataTable();
            adapter2.Fill(table3);
            this.ItemDetails.ItemsSource = table3.DefaultView;
            con.Close();
        }

        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {
            scb = new SqlCommandBuilder(adapter2);
            adapter2.Update(table3);
        }

        private void CreateItemTable_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable table = new System.Data.DataTable("SaleItems");

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "CustID",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "CustName",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Name",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Quantity",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Rate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Discount",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Tax",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Amount",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            string sqlc1;
            bool ifExists = TableExists(con, "master", "SaleItems");
            if (!ifExists)
            {
                sqlc1 = CreateTABLEPablo("SaleItems", table);
                sda = new SqlDataAdapter(sqlc1, con);
                sda.SelectCommand.ExecuteNonQuery();
            }
            else
                return;

            string query3 = "SELECT CustName FROM SalesOrder";
            SqlDataAdapter adapter = new SqlDataAdapter(query3, con);
            adapter = new SqlDataAdapter(query3, con);
            DataTable table2 = new DataTable();
            adapter.Fill(table2);

            ForeignKeyConstraint fk = new ForeignKeyConstraint("ForeignKey", table2.Columns["CustName"], table.Columns["CustID"]);
            table.Constraints.Add(fk);

            Console.WriteLine(sqlc1);
            con.Close();
        }

        private void ItemDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid tblData0 = sender as DataGrid;

            string custID = (tblData0.Columns[0].GetCellContent(tblData0.CurrentItem) as TextBlock).Text;

            int cnt0 = tblData0.Items.Count;
            double sum = 0;
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //Execute NonQuery or ExecuteReader
            con.Open();
            string query2 = "SELECT Amount FROM SaleItems WHERE custID='" + custID + "'";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, con);
            adapter2.SelectCommand.ExecuteNonQuery();
            DataTable table3 = new DataTable();
            adapter2.Fill(table3);
            con.Close();


            foreach (DataRow theRow2 in table3.Rows)
            {
               sum += double.Parse(theRow2.ItemArray[0].ToString());
            }

            this.SubTotal.Text = sum.ToString();
        }

        private void Adjustment_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Total.Text = (double.Parse(this.SubTotal.Text) + double.Parse(this.Adjustment.Text)).ToString();
        }

        private void DataGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid tblData = sender as DataGrid;
            String custName = (tblData.Columns[0].GetCellContent(tblData.CurrentItem) as TextBlock).Text;
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //Execute NonQuery or ExecuteReader
            con.Open();
            string query2 = "SELECT * FROM SaleItems WHERE CustName='" + custName + "'";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, con);
            adapter2.SelectCommand.ExecuteNonQuery();
            DataTable table3 = new DataTable();
            adapter2.Fill(table3);
            ItemDetails.ItemsSource = table3.DefaultView;
            con.Close();
            tblData.SelectedCells.Clear();
        }
    }
}
