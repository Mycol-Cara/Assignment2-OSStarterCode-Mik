using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for ServiceDatabaseWindow.xaml
    /// </summary>
    public partial class ServiceDatabaseWindow : Window
    {
        protected MySqlConnection con;
        protected DataTable dt; //Data table of vehicle database

        public ServiceDatabaseWindow()
        {
            InitializeComponent();
            BindData();
            AddData();
        }


        public void BindData()
        {
            String conStr = "user id = root; persistsecurityinfo = True; server = localhost; database = testdata; password=Password1;";
            try
            {
                //Create connection and open it
                con = new MySqlConnection(conStr); //Create the connection
                con.Open();
                //Create data table 
                dt = new DataTable();
                //Fill DataTable with private method
                FillTable();
                //View data table 
                dataGrid.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException mse)
            {
                Console.WriteLine("Error binding vehicle database");
                Console.WriteLine(mse.ToString());
            }

        }

        public void AddData()
        {
            if (con != null)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO `serviceseed`(`ID`,`VehicleID`,`OdomRead`) VALUES((1),(2),(3))";
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException mse)
                {
                    Console.WriteLine("Error binding service database");
                    Console.WriteLine(mse.ToString());
                }
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (con != null)
            {
                con.Close(); //Close the database connection
                Console.WriteLine("Closed database connection for Services");
            }
        }

        private void FillTable()
        {
            if (con != null)
            {
                MySqlCommand cmd = new MySqlCommand("select * from serviceseed", con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose(); adp.Dispose();
            }
        }
    }
}