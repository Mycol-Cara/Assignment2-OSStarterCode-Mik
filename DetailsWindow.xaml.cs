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

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window

    {
        private Details D; //The service to be created

        public DetailsWindow (Details D) 
        {
            InitializeComponent();
            this.D = D;
            displayDetails();
            
        }
        
        private void displayDetails() //set the details data into the text fields!
        {
            modelTxt.Text = D.getModel();
            manufacturerTxt.Text = D.getManufacturer();
            serviceTxt.Text = D.getServiceNum().ToString();
            distanceTxt.Text = D.getTotDistance().ToString();
            revenueTxt.Text = D.getRevenueInfo();
            distanceSinceServiceTxt.Text = D.getDistanceSinceLastService().ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
