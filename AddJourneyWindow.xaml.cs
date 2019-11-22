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
    /// Interaction logic for AddJourneyWindow.xaml
    /// </summary>
    public partial class AddJourneyWindow : Window
    {
        private Journey J; //The journey to be created
        protected Boolean saveState; //was it saved on exit

        public AddJourneyWindow(int vehicleID)
        {
            InitializeComponent();
            //this.J = new Journey(0, 0, DateTime.Now, vehicleID);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            saveState = true; //set vehicle to saved before exit!
            //UpdateService(); //Update the vehicle
            this.Close(); //CLOSE WINDOW!
        }
    }
}
