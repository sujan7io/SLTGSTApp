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
    /// Interaction logic for UserControlSOPOS.xaml
    /// </summary>
    public partial class UserControlSOPOS : UserControl
    {
        SqlDataAdapter sda;
        DataTable tblData;
        System.Data.DataTable table;
        public UserControlSOPOS()
        {
            InitializeComponent();

            Date.Text = DateTime.Today.ToShortDateString();
            Time.Text = DateTime.Now.ToShortTimeString();
            table = new System.Data.DataTable("ItemsToBeSold");

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
                ColumnName = "Unit",
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
                ColumnName = "Price",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });
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

        public class SearchStringList
        {
            private List<string> strings = new List<string>();
            private List<StringPosition> positions = new List<StringPosition>();
            private bool dirty = false;
            private readonly bool ignoreCase = true;

            public SearchStringList(bool ignoreCase)
            {
                this.ignoreCase = ignoreCase;
            }

            public void Add(string s)
            {
                if (s.Length > 255) throw new ArgumentOutOfRangeException("string too big.");
                this.strings.Add(s);
                this.dirty = true;
                for (byte i = 0; i < s.Length; i++) this.positions.Add(new StringPosition(strings.Count - 1, i));
            }

            public string this[int index] { get { return this.strings[index]; } }

            public void EnsureSorted()
            {
                if (dirty)
                {
                    this.positions.Sort(Compare);
                    this.dirty = false;
                }
            }

            public IEnumerable<StringPosition> FindAll(string keyword)
            {
                var idx = IndexOf(keyword);
                while ((idx >= 0) && (idx < this.positions.Count)
                    && (Compare(keyword, this.positions[idx]) == 0))
                {
                    yield return this.positions[idx];
                    idx++;
                }
            }

            private int IndexOf(string keyword)
            {
                EnsureSorted();

                // binary search
                // When the keyword appears multiple times, this should
                // point to the first match in positions. The following
                // positions could be examined for additional matches
                int minP = 0;
                int maxP = this.positions.Count - 1;
                while (maxP > minP)
                {
                    int midP = minP + ((maxP - minP) / 2);
                    if (Compare(keyword, this.positions[midP]) > 0)
                    {
                        minP = midP + 1;
                    }
                    else
                    {
                        maxP = midP;
                    }
                }
                if ((maxP == minP) && (Compare(keyword, this.positions[minP]) == 0))
                {
                    return minP;
                }
                else
                {
                    return -1;
                }
            }

            private int Compare(StringPosition pos1, StringPosition pos2)
            {
                int len = Math.Max(this.strings[pos1.ListIndex].Length - pos1.StringIndex, this.strings[pos2.ListIndex].Length - pos2.StringIndex);
                return String.Compare(strings[pos1.ListIndex], pos1.StringIndex, this.strings[pos2.ListIndex], pos2.StringIndex, len, ignoreCase);
            }

            private int Compare(string keyword, StringPosition pos2)
            {
                return String.Compare(keyword, 0, this.strings[pos2.ListIndex], pos2.StringIndex, keyword.Length, this.ignoreCase);
            }

            // Packs index of string, and position within string into a single int. This is
            // set up for strings no greater than 255 bytes. If longer strings are desired,
            // the code for the constructor, and extracting  ListIndex and StringIndex would
            // need to be modified accordingly, taking bits from ListIndex and using them
            // for StringIndex.
            public struct StringPosition
            {
                public static StringPosition NotFound = new StringPosition(-1, 0);
                private readonly int position;
                public StringPosition(int listIndex, byte stringIndex)
                {
                    this.position = (listIndex < 0) ? -1 : this.position = (listIndex << 8) | stringIndex;
                }
                public int ListIndex { get { return (this.position >= 0) ? (this.position >> 8) : -1; } }
                public byte StringIndex { get { return (byte)(this.position & 0xFF); } }
                public override string ToString()
                {
                    return ListIndex.ToString() + ":" + StringIndex;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            string query = "SELECT Name, SalesRate FROM Items";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            tblData = new DataTable();
            sda.Fill(tblData);

            var list = new SearchStringList(true);
            var list2 = new SearchStringList(false);

            foreach(DataRow theRow in tblData.Rows)
            {
                list.Add(theRow.ItemArray[0].ToString());
                list2.Add(theRow.ItemArray[1].ToString());
            }

            string keyword = SearchText.Text;


            if (keyword.Length == 0)
            {
                DisplaySearch.ItemsSource = tblData.DefaultView;

            }
            else
            {
                DataTable tblData2 = new DataTable();

                tblData2.Columns.Add(new DataColumn()
                {
                    ColumnName = "Name",
                    DataType = System.Type.GetType("System.String"),
                    AllowDBNull = true,
                    DefaultValue = String.Empty,
                    MaxLength = 100
                });
                tblData2.Columns.Add(new DataColumn()
                {
                    ColumnName = "Price",
                    DataType = System.Type.GetType("System.String"),
                    AllowDBNull = true,
                    DefaultValue = String.Empty,
                    MaxLength = 100
                });

                foreach (var pos in list.FindAll(keyword))
                {
                    Console.WriteLine(pos.ToString() + " =>" + list[pos.ListIndex]);

                    DataRow dr = tblData2.NewRow();

                    dr[0] = list[pos.ListIndex];
                    dr[1] = list2[pos.ListIndex];
                    tblData2.Rows.Add(dr);
                }
                DisplaySearch.ItemsSource = tblData2.DefaultView;
            }
        }

        private void DisplaySearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
      
            DataGrid tblData0 = sender as DataGrid;
            string query2 = "SELECT IGST, CGST, SGST FROM Items WHERE Name='" + (tblData0.Columns[0].GetCellContent(tblData0.CurrentItem) as TextBlock).Text + "'";
            sda = new SqlDataAdapter(query2, con);
            sda.SelectCommand.ExecuteNonQuery();
            DataTable tbl = new DataTable();
            sda.Fill(tbl);

            double igst = 0, cgst = 0, sgst = 0;
            foreach(DataRow therow in tbl.Rows)
            {
                igst = double.Parse(therow.ItemArray[0].ToString());
                cgst = double.Parse(therow.ItemArray[1].ToString());
                sgst = double.Parse(therow.ItemArray[2].ToString());
                break;
            }

            double price = double.Parse((tblData0.Columns[1].GetCellContent(tblData0.CurrentItem) as TextBlock).Text);
            double adjustment = (price * (igst/100)) + (price * (cgst / 100)) + (price * (sgst / 100));
            int unit = 1;

            DataRow dr = table.NewRow();

            dr[0] = (tblData0.Columns[0].GetCellContent(tblData0.CurrentItem) as TextBlock).Text;
            dr[1] = unit.ToString();
            dr[2] = adjustment.ToString();
            dr[3] = price.ToString();

            table.Rows.Add(dr);//this will add the row at the end of the datatable

            DisplayMainTable.ItemsSource = table.DefaultView;

            con.Close();
        }

        private void DisplayMainTable_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid tblData0 = sender as DataGrid;

            int count = tblData0.Items.Count;
            count--;
            double subtotal = 0, tax = 0;
            for(int i=0; i<count; i++)
            {
                int unit  = int.Parse((tblData0.Columns[1].GetCellContent(tblData0.Items[i]) as TextBlock).Text);
                tax += int.Parse((tblData0.Columns[2].GetCellContent(tblData0.Items[i]) as TextBlock).Text);
                subtotal += (unit * int.Parse((tblData0.Columns[3].GetCellContent(tblData0.Items[i]) as TextBlock).Text));
            }

            Subtotal.Text = subtotal.ToString();
            Tax.Text = tax.ToString();
        }

        private void Discount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Discount.Text!="")
            {
                double subtotal = double.Parse(Subtotal.Text);
                double tax = double.Parse(Tax.Text);
                double disc = double.Parse(Discount.Text);
                disc = subtotal * (disc / 100);
                double total = subtotal + tax - disc;
                Total.Text = total.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            System.Data.DataTable table = new System.Data.DataTable("ItemsToBeSold");

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
                ColumnName = "CustName",
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
                MaxLength = 50
            });

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
                ColumnName = "Date",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Time",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 50
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
                ColumnName = "Adjustment",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            table.Columns.Add(new DataColumn()
            {
                ColumnName = "Price",
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
                ColumnName = "Tax",
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
                ColumnName = "Total",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });

            string sqlc1="";
            table.PrimaryKey = new DataColumn[] { table.Columns[3] };
            con.Open();
            bool ifExists = TableExists(con, "master", "ItemsToBeSold");
            if (!ifExists)
            {
                sqlc1 = CreateTABLEPablo("ItemsToBeSold", table);
                SqlDataAdapter sda2 = new SqlDataAdapter(sqlc1, con);
                sda2.SelectCommand.ExecuteNonQuery();
                Console.WriteLine(sqlc1);
            }
            DataGrid tblData0 = DisplayMainTable as DataGrid;
            int count = tblData0.Items.Count;

            string custName = CustName.Text;
            string billno = BillNumber.Text;
            string dataToday = Date.Text;
            string timeNow = Time.Text;
            count--;
            DataTable table2 = new DataTable();
            table2.Columns.Add(new DataColumn()
            {
                ColumnName = "Name",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });
            table2.Columns.Add(new DataColumn()
            {
                ColumnName = "Unit",
                DataType = System.Type.GetType("System.String"),
                AllowDBNull = true,
                DefaultValue = String.Empty,
                MaxLength = 100
            });
            for (int i=0; i<count; i++)
            {
                string name = (tblData0.Columns[0].GetCellContent(tblData0.Items[i]) as TextBlock).Text;
                string unit = (tblData0.Columns[1].GetCellContent(tblData0.Items[i]) as TextBlock).Text;
                string adjust = (tblData0.Columns[2].GetCellContent(tblData0.Items[i]) as TextBlock).Text;
                string price = (tblData0.Columns[3].GetCellContent(tblData0.Items[i]) as TextBlock).Text;
                string subtotal = Subtotal.Text;
                string tax = Tax.Text;
                string disc = Discount.Text;
                string total = Total.Text;
               
                String query = "INSERT INTO ItemsToBeSold (Name, CustName, BillNo, CustID, Date, Time, Unit, Adjustment, Price, Subtotal, Tax, Discount, Total) VALUES('" + name + "','" + custName + "','" + billno.ToString() + "','" + custName + "-" + name + "','" +  dataToday + "','" + timeNow + "','" +  unit + "','" + adjust + "','" + price + "','" + subtotal + "','" + tax + "','" + disc + "','" + total + "')";
                SqlDataAdapter sda3 = new SqlDataAdapter(query, con);
                sda3.SelectCommand.ExecuteNonQuery();
                Console.WriteLine("INSERTED DATA IN TABLE ItemsToBeSold !");

                DataRow dr = table2.NewRow();

                dr[0] = name;
                dr[1] = unit;
                table2.Rows.Add(dr);
            }

            string itemName = "";
            int result2 = 0;
            foreach (DataRow theRow2 in table2.Rows)
            {
                itemName = theRow2.ItemArray[0].ToString();
                result2 = int.Parse(theRow2.ItemArray[1].ToString());
                string query4 = "SELECT CurrentStock FROM Items WHERE Name='" + itemName + "'";
                SqlDataAdapter sda5 = new SqlDataAdapter(query4, con);
                DataTable table3 = new DataTable();
                sda5.SelectCommand.ExecuteNonQuery();
                sda5.Fill(table3);

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
                SqlDataAdapter adapter2 = new SqlDataAdapter(query3, con);
                adapter2.SelectCommand.ExecuteNonQuery();
                Console.WriteLine("UPDATED DATA IN TABLE Items !");
            }

            con.Close();
        }
    }
}
