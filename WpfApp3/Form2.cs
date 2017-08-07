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
using System.Data.SqlTypes;

namespace WpfApp3
{
    public partial class Form2 : Form
    {
        SqlDataAdapter sda;
        SalesOrderUC souccontrol;
        public Form2()
        {
            InitializeComponent();

            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string query = "SELECT Name FROM Items";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.ExecuteNonQuery();
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            foreach (DataRow theRow in table1.Rows)
            {
                comboBox1.Items.Add(theRow.ItemArray[0].ToString());
            }
            conn.Close();
        }

        public void setSalesUC(SalesOrderUC souc)
        {
            souccontrol = souc;
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //Execute NonQuery or ExecuteReader
            con.Open();                  
            String query = "INSERT INTO SaleItems (CustID, CustName, Name, Quantity,Rate,Discount,Tax,Amount) VALUES('" + souccontrol.CustName.Text + "-" + comboBox1.Text + "','" + souccontrol.CustName.Text + "','" + comboBox1.Text + "','" + textBox_QUANTITY.Text + "','" + textBox_RATE.Text + "','" + textBox_DISCOUNT.Text + "','" + textBox_TAX.Text + "','" + textBox_AMOUNT.Text + "')";
            sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED DATA IN TABLE SaleItems !");

            string query2 = "SELECT CustID,CustName,Name,Quantity,Rate,Discount,Tax,Amount FROM SaleItems";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, con);
            adapter2.SelectCommand.ExecuteNonQuery();
            DataTable table3 = new DataTable();
            adapter2.Fill(table3);

            souccontrol.ItemDetails.ItemsSource = table3.DefaultView;
            con.Close();
        }

        private void textBox_QUANTITY_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            string name = comboBox1.Text;
            string quan = textBox_QUANTITY.Text;
            string query = "SELECT SalesRate FROM Items WHERE Name='" + name + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.SelectCommand.ExecuteNonQuery();
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            foreach (DataRow theRow in table1.Rows)
            {
                textBox_RATE.Text = theRow.ItemArray[0].ToString();
            }
            con.Close();
        }

        private void textBox_DISCOUNT_TextChanged(object sender, EventArgs e)
        {
            string name = comboBox1.Text;
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            string query = "SELECT IGST, CGST, SGST FROM Items WHERE Name='" + name + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.SelectCommand.ExecuteNonQuery();
            DataTable table1 = new DataTable();
            adapter.Fill(table1);
            List<double> tax = new List<double>();
            foreach (DataRow theRow in table1.Rows)
            {
                if (theRow.ItemArray[0].ToString().Length >= 1)
                    tax.Add(double.Parse(theRow.ItemArray[0].ToString()));
                else
                    tax.Add(0);
                if (theRow.ItemArray[1].ToString().Length >= 1)
                    tax.Add(double.Parse(theRow.ItemArray[0].ToString()));
                else
                    tax.Add(0);
                if (theRow.ItemArray[2].ToString().Length >= 1)
                    tax.Add(double.Parse(theRow.ItemArray[0].ToString()));
                else
                    tax.Add(0);
            }
            double rate = double.Parse(textBox_RATE.Text);
            double totaltax = (tax[0] / 100 + tax[1] / 100 + tax[2] / 100) * rate;
            double Newamount = rate + totaltax;
            double amount2beDeducted = (rate * double.Parse(textBox_DISCOUNT.Text) / 100);
            double amount = Newamount - amount2beDeducted;
            textBox_TAX.Text = totaltax.ToString();
            textBox_AMOUNT.Text = amount.ToString();
            con.Close();
        }
    }
}
