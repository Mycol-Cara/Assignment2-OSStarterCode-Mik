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
using MySql.Data.MySqlClient;
using System.Data;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for DatabaseWindow.xaml
    /// </summary>
    public partial class VehicleDatabaseWindow : Window
    {
        protected MySqlConnection con; //Connection to vehicle database 
        protected DataTable dt; //Data table of vehicle database
        public VehicleDatabaseWindow()
        {
            InitializeComponent();
            BindData();
            AddData();
        }

        public void BindData()
        {
            //String conStr = "user id = root; persistsecurityinfo = True; server = localhost; database = cars; password=Password1;";
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
            } catch (MySqlException mse)
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
                    //cmd.CommandText = "INSERT INTO `vehicletable`(`Field`,`Field Name`,`Type`,`Size`,`Primary Key`,`Foreign Key`,`Null`,`Zero Fill`,`Auto Increment`,`Default`,`Unsigned`) VALUES((1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11))";
                    cmd.CommandText = "INSERT INTO `vehicleseed`(`ID`,`NAME`,`MODEL`,`YEAR`,`REGO`,`CAPACITY`,`INITIALS`) VALUES((1),(2),(3),(4),(5),(6),(7))";
                    cmd.ExecuteNonQuery();
                } catch(MySqlException mse)
                {
                    Console.WriteLine("Error binding vehicle database");
                    Console.WriteLine(mse.ToString());
                }
            }
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (con != null)
            {
                con.Close(); //Close the database connection
                Console.WriteLine("Closed database connection for Vehicles");
            }
        }

        private void FillTable()
        {
            if (con != null)
            {
                //MySqlCommand cmd = new MySqlCommand("select * from vehicletable", con);
                MySqlCommand cmd = new MySqlCommand("select * from vehicleseed", con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose(); adp.Dispose();
            }
        }
    }
}
