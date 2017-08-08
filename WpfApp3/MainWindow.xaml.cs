using System;
using System.Windows.Forms.DataVisualization;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private items item;
        private UserControlSOPOS sopos;
        private SalesOrderInvoiceUC soiuc;
        private PurchaseOrderUC pouc;
        private PurchaseOrderInvoiceUC poiuc;
        private Form1 form1;
        private UserControlInventoryReport reportuc;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            item = new items();
            Grid res = this.center as Grid;
            res.Children.Clear();
            res.Children.Add( item);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ItemsPriceLists !");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ItemsItemsAdjustments !");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Showing Sales Orders UserControl");
            sopos = new UserControlSOPOS();
            Grid res = this.center as Grid;
            res.Children.Clear();
            sopos.Width = res.Width;
            sopos.Height = res.Height;
            res.Children.Add(sopos);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Showing Invoices UserControl");
            soiuc = new SalesOrderInvoiceUC();
            Grid res = this.center as Grid;
            res.Children.Clear();
            soiuc.Width = res.Width;
            soiuc.Height = res.Height;
            res.Children.Add(soiuc);
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Showing Credit Notes/Sales Return UserControl");
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Showing Purchase Orders UserControl");
            pouc = new PurchaseOrderUC();
            Grid res = this.center as Grid;
            res.Children.Clear();
            pouc.Width = res.Width;
            pouc.Height = res.Height;
            res.Children.Add(pouc);
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Showing Bills UserControl");
            poiuc = new PurchaseOrderInvoiceUC();
            Grid res = this.center as Grid;
            res.Children.Clear();
            poiuc.Width = res.Width;
            poiuc.Height = res.Height;
            res.Children.Add(poiuc);
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Showing Debit Notes/Purchase Return User Control");
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Showing Inventory Report !");
            reportuc = new UserControlInventoryReport();
            Grid res = this.center as Grid;
            reportuc.Width = res.Width;
            reportuc.Height = res.Height;
            res.Children.Clear();
            res.Children.Add(reportuc);
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Showing GST Filing !");
        }

        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();
            form1 = new Form1();
            form1.TopLevel = false;
            host.Child = form1;
            Grid res = this.center as Grid;
            form1.Width =  (int)res.Width;
            form1.Height = (int)res.Height;
            res.Children.Clear();
            res.Children.Add(host);
        }

        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
