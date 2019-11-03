﻿using System;
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
    public partial class DatabaseWindow : Window
    {
        public DatabaseWindow()
        {
            InitializeComponent();
            BindData();
        }

        public void BindData()
        {
            String conStr = "user id = root; persistsecurityinfo = True; server = localhost; database = cars; password=Password1;";
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from vehicletable",con);
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
