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
        public ServiceDatabaseWindow()
        {
            InitializeComponent();
            BindData();
        }
        public void BindData()
        {
            String conStr = "user id = root; persistsecurityinfo = True; server = localhost; database = cars; password=Password1;";
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from servicestable", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //Add data to view! 
            dataGrid.ItemsSource = dt.DefaultView;
            cmd.Dispose();
            con.Close();

        }
    }
}
