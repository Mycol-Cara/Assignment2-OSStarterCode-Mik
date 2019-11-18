using System;
using System.Collections;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public List<Vehicle> vehicleData;
        public LauncherWindow()
        {
            InitializeComponent();
            vehicleData = new List<Vehicle>(); //Need an option to load in the data
        }


        private void LaunchVehiclesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();                // giving opacity to the main window

            VehicleLauncher vl = new VehicleLauncher(copyToArrayList(vehicleData));
            vl.ShowDialog();
           
            afterEffects();

        }

        private void LaunchJourniesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            //JourneyDatabseWindow jdw = new JourneyDatabseWindow();
            //jdw.ShowDialog();

            afterEffects();
        }

        private void LaunchFuelPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            //FuelPurchaseDatabaseWindow fdw = new FuelPurchaseDatabaseWindow();
            //fdw.ShowDialog();

            afterEffects();

        }

        private void LaunchServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            //ServiceDatabaseWindow sdw = new ServiceDatabaseWindow();
            //sdw.ShowDialog();

            afterEffects();

        }

        private void beforeEffects()
        {
            this.VisualOpacity = 0.5;
            this.VisualEffect = new BlurEffect();
        }
        private void afterEffects()
        {
            this.VisualOpacity = 1;
            this.VisualEffect = null;
          
        }
        private ArrayList copyArrayList(ArrayList AL)
        { //This can use to copy the public variable of companies to avoid just pointing to it
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < AL.Count; i++) //Clone all values to the new list!
            {
                newAL.Add((Vehicle)AL[i]);
            }
            return newAL;
        }
        private ArrayList copyToArrayList(List<Vehicle> L)
        { //This can use to copy the public variable of companies to avoid just pointing to it
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < L.Count; i++) //Clone all values to the new list!
            {
                newAL.Add((Vehicle) L[i]);
            }
            return newAL;
        }
    }
}
