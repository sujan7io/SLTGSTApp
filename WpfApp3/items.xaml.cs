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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for items.xaml
    /// </summary> total 14 resources for database server, all string resources
    public partial class items : UserControl
    {
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommandBuilder scb;
        public items()
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

        //Create Table Items on database
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //Execute NonQuery or ExecuteReader
                System.Data.DataTable table = new System.Data.DataTable("Items");

                table.Columns.Add(new DataColumn()
                {
                    ColumnName = "Type",
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
                    MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                    ColumnName = "Unit",
                    DataType = System.Type.GetType("System.String"),
                    AllowDBNull = true,
                    DefaultValue = String.Empty,
                    MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                    ColumnName = "CodeHSN_SAC",
                    DataType = System.Type.GetType("System.String"),
                    AllowDBNull = true,
                    DefaultValue = String.Empty,
                    MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                    ColumnName = "TaxPreference",
                    DataType = System.Type.GetType("System.String"),
                    AllowDBNull = true,
                    DefaultValue = String.Empty,
                    MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "SalesRate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "SalesDescription",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "PurchaseRate",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "PurchaseDescription",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "IGST",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "CGST",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "SGST",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "OpeningStock",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "OpeningStockRatePerUnit",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

                table.Columns.Add(new DataColumn()
                {
                ColumnName = "CurrentStock",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
                });

            string sqlc1;
                table.PrimaryKey = new DataColumn[] { table.Columns[1] };
                con.Open();
                bool ifExists = TableExists(con, "master", "Items");
                if (!ifExists)
                    sqlc1 = CreateTABLEPablo("Items", table);
                else
                    return;
                sda = new SqlDataAdapter(sqlc1, con);
                sda.SelectCommand.ExecuteNonQuery();
                Console.WriteLine(sqlc1);
                con.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            String type, taxpref;
            if (TaxPref_Taxable.IsChecked == true)
                taxpref = "Taxable";
            else
                taxpref = "NonTaxable";

            if (Radio_Goods.IsChecked == true)
                type = "Goods";
            else
                type = "Service";
            String query = "INSERT INTO Items (Type,Name,Unit,CodeHSN_SAC,TaxPreference,SalesRate,SalesDescription,PurchaseRate,PurchaseDescription,IGST,CGST,SGST,OpeningStock,OpeningStockRatePerUnit,CurrentStock) VALUES('" + type + "','" + TEXT_Name.Text + "','" + Combo_Unit.Text + "','" + Edit_HSNSACCode.Text + "','" + taxpref + "','" + Edit_SalesRate.Text + "','" + Edit_SalesDescrip.Text + "','" + Edit_PurchaseRate.Text + "','" + Edit_PurchaseDescrip.Text + "','" + Combo_IGST.Text + "','" + Combo_CGST.Text + "','" + Combo_SGST.Text + "','" + Edit_OpeningSTock.Text + "','" + Edit_OpeningStockRate.Text + "','" + Edit_OpeningSTock.Text + "')";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED DATA IN TABLE Items !");
            con.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String query = "SELECT Type,Name,Unit,CodeHSN_SAC,TaxPreference,SalesRate,SalesDescription,PurchaseRate,PurchaseDescription,IGST,CGST,SGST,OpeningStock,OpeningStockRatePerUnit,CurrentStock FROM Items";
            sda = new SqlDataAdapter(query, con);
            dt = new DataTable();
            sda.Fill(dt);
            dataGrid1.ItemsSource = dt.DefaultView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void Radio_Service_Checked(object sender, RoutedEventArgs e)
        {
            int var = 1;
            ComboCode.SelectedIndex = var;
        }

        private void Radio_Goods_Checked(object sender, RoutedEventArgs e)
        {
            int var = 0;
            ComboCode.SelectedIndex = var;
        }

        private void Edit_OpeningSTock_TextChanged(object sender, TextChangedEventArgs e)
        {
            double salesrate = double.Parse(Edit_SalesRate.Text); 
            double igst = double.Parse(Edit_SalesRate.Text) * (double.Parse(Combo_IGST.Text)/100);
            double cgst = double.Parse(Edit_SalesRate.Text) * (double.Parse(Combo_CGST.Text) / 100);
            double sgst = double.Parse(Edit_SalesRate.Text) * (double.Parse(Combo_SGST.Text) / 100);

            double opstkrate = salesrate + igst + cgst + sgst;

            Edit_OpeningStockRate.Text = opstkrate.ToString();
        }
    }
}
