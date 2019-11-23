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

        private Boolean adminMode;
        public LauncherWindow()
        {
            InitializeComponent();
            vehicleData = new List<Vehicle>(); //declare/initialise the variables
            serviceData = new List<Service>();
            journeyData = new List<Journey>();
            
            LaunchVehiclesBtn.IsHitTestVisible = false; //can't use until database loaded
            LaunchVehiclesBtn.Opacity = 0.5;
            setAdminMode(false); //set usability for admin button (save database)
        }



        private void LaunchVehiclesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();                // giving opacity to the main window

            VehicleLauncher vl = new VehicleLauncher(copyToArrayList(vehicleData), adminMode); //create new vehicles windows with existing vehicle data/list andd same admin mode
            vl.ShowDialog();
            vehicleData = new List<Vehicle>();
            vehicleData = vl.getVehicleList(); //update data

            this.adminMode = vl.getAdminMode(); //get the admin mode, incase has disabled...
            setAdminMode(this.adminMode);

            afterEffects();

        }

        private void LoadDatabaseBtn_Click(object sender, RoutedEventArgs e)
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

            //TODO Fuel Purchase



            //initialiseData
            initialiseDataServices(); //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid
            initialiseDataJourney(); //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid

            //Change button
            LaunchVehiclesBtn.IsHitTestVisible = true; //now vehicle view is usable
            LaunchVehiclesBtn.Opacity = 1.0;
        }

        private void SaveDatabaseBtn_Click(object sender, RoutedEventArgs e)
        {
            //VEHICLES Convert  to JSON using JSON .NET package
            String jsonVehicles = JsonConvert.SerializeObject(vehicleData);
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "saveVehiclesFile.txt"; //Path
            File.WriteAllText(path, jsonVehicles); //Write text to file

            //BUILD SERVICE DATA 
            ArrayList services; serviceData.Clear();
            for (int i = 0; i < vehicleData.Count; i++)
            {
                services = vehicleData[i].getServices(); //Services for a vehicle!
                for (int j = 0; j < services.Count; j++) //Get each service and add to big list
                {
                    serviceData.Add((Service)services[j]);
                }
            }

            //SERVICES Convert to JSON using JSON .NET package
            String jsonServices = JsonConvert.SerializeObject(serviceData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveServicesFile.txt"; //Path
            File.WriteAllText(path, jsonServices); //Write text to file

            // Build Journey Data
            ArrayList journies; journeyData.Clear();
            for ( int i = 0; i< vehicleData.Count; i++)
            {
                journies = vehicleData[i].getJournies(); // Journies for a vehicle
                for (int j = 0; j < journies.Count; j++) //Get each journey and add to big list
                {
                    journeyData.Add((Journey)journies[j]);
                }
            }
            // Journiesconvert to JSON save file
            String jsonJourney = JsonConvert.SerializeObject(journeyData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveJourniesFile.txt";
            File.WriteAllText(path, jsonJourney);


            MessageBox.Show("Database saved locally"); //Let the user know the file was saved...
        }

        private void AdminLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!adminMode) //current inactive
            {
                LoginForm form = new LoginForm();
                form.ShowDialog();
                if (form.getResult()) //Password worked
                {
                    setAdminMode(!adminMode); //turn on if user and password correct
                }
            }
            else //currently active
            {
                setAdminMode(!adminMode); //turn off, no need for password
            }
        }

        private void initialiseDataServices()
        {
            ArrayList servicesCar; //service data for a vehicle 
            int id; Vehicle car;
            for (int v = 0; v < vehicleData.Count; v++) //Vehicles
            { 
                car = vehicleData[v]; //get the car and it's services
                id = car.getVehicleID();
                servicesCar = car.getServices();
                servicesCar.Clear(); //reset
                for (int i = 0; i < serviceData.Count; i++) //Go through service data and remake the services as shouldnt be any yet
                {
                    if (serviceData[i].getVehicleID() == id) //Add to vehicle services as Id the same!
                    {
                        servicesCar.Add(serviceData[i]);
                    }
                }
                vehicleData[v].setServices(servicesCar); //save back to vehicle data
            }
        }

        private void initialiseDataJourney()
        {
            ArrayList journiesCar; //journey data for a vehicle 
            int id; Vehicle car;
            for (int v = 0; v < vehicleData.Count; v++) //Vehicles
            {
                car = vehicleData[v]; //get the car and it's journies
                id = car.getVehicleID();
                journiesCar = car.getJournies();
                journiesCar.Clear(); //reset
                for (int i = 0; i < journeyData.Count; i++) //Go through journey data and remake the journies as shouldnt be any yet
                {
                    if (journeyData[i].getVehicleID() == id) //Add to vehicle journies as Id the same!
                    {
                        journiesCar.Add(journeyData[i]);
                    }
                }
                vehicleData[v].setJournies(journiesCar); //save back to vehicle data
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

        private ArrayList copyToArrayList(List<Vehicle> L)
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
            SaveDatabaseBtn.IsHitTestVisible = mode; //set admin button activity

            double opac = 1.0;
            if (!mode) { opac = 0.5; } //admin button opacities
            SaveDatabaseBtn.Opacity = opac;

        }
    }
}
