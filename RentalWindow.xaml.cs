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
    /// Interaction logic for RentalWindow.xaml
    /// </summary>
    public partial class RentalWindow : Window
    {
        public RentalWindow(int numberOfVehicles)
        {
            InitializeComponent();
            this.Title = "Rental Request for " + numberOfVehicles + " Vehicle(s)";
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO verify customer details!!!
            MessageBox.Show("We will contact you to confirm your request shortly!");
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
