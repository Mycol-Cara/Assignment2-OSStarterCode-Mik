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
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public List<Vehicle> vehicleData;
        public List<Service> serviceData;
        public List<Journey> journeyData;
        public List<FuelPurchase> fuelData;

        private Boolean adminMode;
        public LauncherWindow()
        {
            InitializeComponent();
            vehicleData = new List<Vehicle>(); //declare/initialise the variables
            serviceData = new List<Service>();
            journeyData = new List<Journey>();
            fuelData = new List<FuelPurchase>();

            LaunchVehiclesBtn.IsHitTestVisible = false; //can't use until database loaded
            LaunchVehiclesBtn.Opacity = 0.5;
            setAdminMode(false); //set usability for admin button (save database)
        }



        private void LaunchVehiclesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();                // giving opacity to the main window

            //Create window with data
            VehicleLauncher vl = new VehicleLauncher(convertListToArrayList(vehicleData), serviceData, journeyData, fuelData, adminMode); //create new vehicles windows with existing vehicle data and all of the other data andd same admin mode
            vl.ShowDialog();

            //Get data back that may have changed
            vehicleData = new List<Vehicle>();
            vehicleData = vl.getVehicleList(); //update data 
            serviceData = new List<Service>();
            serviceData = vl.getServiceList();
            journeyData = new List<Journey>();
            journeyData = vl.getJourneyList();
            fuelData = new List<FuelPurchase>();
            fuelData = vl.getFuelPurchaseList();

            this.adminMode = vl.getAdminMode(); //get the admin mode, incase has disabled...
            setAdminMode(this.adminMode);

            afterEffects();

        }

        private void LoadSQLBtn_Click(object sender, RoutedEventArgs e)
        {

            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            if (data != null)
            {
                vehicleData = (List<Vehicle>) data[0]; //put list from object in here
                serviceData = (List<Service>) data[1];
                journeyData = (List<Journey>) data[2];
                fuelData = (List<FuelPurchase>)data[3];

                //Change button
                LaunchVehiclesBtn.IsHitTestVisible = true; //now vehicle view is usable
                LaunchVehiclesBtn.Opacity = 1.0;
            }
  
        }

        private void SaveSQLBtn_Click(object sender, RoutedEventArgs e)
        {
            SQLConnector.ReplaceAll(vehicleData, serviceData, journeyData, fuelData, "user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
        }

        private void LoadTxtBtn_Click(object sender, RoutedEventArgs e)
        {
            //Vehicles
            string readText;
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "saveVehiclesFile.txt"; //Path
            if (File.Exists(path))
            {    // reading only if exists
                readText = File.ReadAllText(path); //Reading the text from file
                vehicleData = new List<Vehicle>();
                vehicleData = JsonConvert.DeserializeObject<List<Vehicle>>(readText);
            }
            else { MessageBox.Show("Could not find saves"); }
            
            //Services 
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveServicesFile.txt"; //Path
            if (File.Exists(path))
            {
                readText = File.ReadAllText(path); //Reading the text from file
                serviceData = new List<Service>();
                serviceData = JsonConvert.DeserializeObject<List<Service>>(readText);
            }

            //Journies
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveJourniesFile.txt";   //Path
            if (File.Exists(path))
            {
                readText = File.ReadAllText(path);   //Reading text from the file
                journeyData = new List<Journey>();
                journeyData = JsonConvert.DeserializeObject<List<Journey>>(readText);
            }

            // Fuel Purchase
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveFuelPurchaseFile.txt";   //Path
            if (File.Exists(path))
            {
                readText = File.ReadAllText(path);   //Reading text from the file
                fuelData = new List<FuelPurchase>();
                fuelData = JsonConvert.DeserializeObject<List<FuelPurchase>>(readText);
            }

            //Change button
            LaunchVehiclesBtn.IsHitTestVisible = true; //now vehicle view is usable
            LaunchVehiclesBtn.Opacity = 1.0;
        }

        private void SaveTxtBtn_Click(object sender, RoutedEventArgs e)
        {
            //VEHICLES Convert  to JSON using JSON .NET package
            String jsonVehicles = JsonConvert.SerializeObject(vehicleData);
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "saveVehiclesFile.txt"; //Path
            File.WriteAllText(path, jsonVehicles); //Write text to file

            //SERVICES Convert to JSON using JSON .NET package
            String jsonServices = JsonConvert.SerializeObject(serviceData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveServicesFile.txt"; //Path
            File.WriteAllText(path, jsonServices); //Write text to file

            // Journiesconvert to JSON save file
            String jsonJourney = JsonConvert.SerializeObject(journeyData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveJourniesFile.txt";
            File.WriteAllText(path, jsonJourney);

            //Fuel Purchase Convert to JSON using JSON .NET package
            String jsonFuel = JsonConvert.SerializeObject(fuelData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveFuelPurchaseFile.txt"; //Path
            File.WriteAllText(path, jsonFuel); //Write text to file

            MessageBox.Show("Database saved locally"); //Let the user know the file was saved...
        }

        private void AdminLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (adminMode == false) //current inactive, not an admin
            {
                LoginForm form = new LoginForm();
                form.ShowDialog();
                if (form.getResult()) //Password worked
                {
                    setAdminMode(true); //turn on if user and password correct
                }
            }
            else //currently active
            {
                setAdminMode(false); //turn off, no need for password
            }
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

        private ArrayList convertListToArrayList(List<Vehicle> L)
        { //This can use to copy the public variable of companies to avoid just pointing to it
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < L.Count; i++) //Clone all values to the new list!
            {
                newAL.Add((Vehicle) L[i]);
            }
            return newAL; 
        }

        private void setAdminMode(Boolean mode)
        {
            adminMode = mode; //set admin mode
            SaveTxtBtn.IsHitTestVisible = mode; //set admin button activity
            SaveSQLBtn.IsHitTestVisible = mode;
            double opac = 1.0;
            if (!mode) { opac = 0.5; } //admin button opacities
            SaveTxtBtn.Opacity = opac;
            SaveSQLBtn.Opacity = opac;

        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Car Rental System v1.0; Copyright Michela Carandente 2019. Note: the following packages are used by this program: JSON .NET (Newtonsoft.Json v12.0.2; www.newtonsoft.com/json)");
        }
    }
}
