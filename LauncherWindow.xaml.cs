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

            VehicleLauncher vl = new VehicleLauncher(convertListToArrayList(vehicleData), adminMode); //create new vehicles windows with existing vehicle data/list andd same admin mode
            vl.ShowDialog();
            vehicleData = new List<Vehicle>();
            vehicleData = vl.getVehicleList(); //update data

            this.adminMode = vl.getAdminMode(); //get the admin mode, incase has disabled...
            setAdminMode(this.adminMode);

            afterEffects();

        }

        private void LoadSQLBtn_Click(object sender, RoutedEventArgs e)
        {

            Object[] data = UpdateSQL.LoadAll();
            if (data != null)
            {
                vehicleData = (List<Vehicle>) data[0]; //put list from object in here
                serviceData = (List<Service>) data[1];
                journeyData = (List<Journey>) data[2];
                fuelData = (List<FuelPurchase>)data[3];
                //initialiseData
                initialiseDataServices(); //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid from the loaded serviceData
                initialiseDataJourney(); //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid from loaded journeyData
                initialiseDataFuelPurchase();  //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid from loaded fuelData
                //Change button
                LaunchVehiclesBtn.IsHitTestVisible = true; //now vehicle view is usable
                LaunchVehiclesBtn.Opacity = 1.0;
            }
  
        }

        private void SaveSQLBtn_Click(object sender, RoutedEventArgs e)
        {
            buildServiceData(); //Compile service data from vehicles into one list serviceData
            buildJourneyData(); //Compiling journey data from vehicle into one list journeyData
            buildFuelPurchaseData(); //Compiling fuel data from vehicle into one list fuelData
            UpdateSQL.ReplaceAll(vehicleData, serviceData, journeyData, fuelData);
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


            //initialiseData
            initialiseDataServices(); //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid from the loaded serviceData
            initialiseDataJourney(); //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid from loaded journeyData
            initialiseDataFuelPurchase();  //puts the relevant journies and services and purchase in their vehicles acccording to vehicleid from loaded fuelData
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

            //BUILD SERVICE DATA 
            buildServiceData();

            //SERVICES Convert to JSON using JSON .NET package
            String jsonServices = JsonConvert.SerializeObject(serviceData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveServicesFile.txt"; //Path
            File.WriteAllText(path, jsonServices); //Write text to file

            // Build Journey Data
            buildJourneyData();
           
            // Journiesconvert to JSON save file
            String jsonJourney = JsonConvert.SerializeObject(journeyData);
            path = System.AppDomain.CurrentDomain.BaseDirectory + "saveJourniesFile.txt";
            File.WriteAllText(path, jsonJourney);


            // Build Fuel purchases Data
            buildFuelPurchaseData();
           
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

        private void buildServiceData()
        { //Puts all the services of each vehicle in serviceData
            ArrayList services; serviceData.Clear();
            foreach (Vehicle v in vehicleData)
            {
                services = v.getServices(); //Services for a vehicle!
                foreach (Service s in services) //Get each service and add to big list
                {
                    serviceData.Add(s);
                }
            }
        }
        private void buildJourneyData()
        { //Put all the journies of all vehicles in journeyData
            ArrayList journies; journeyData.Clear();
            foreach (Vehicle v in vehicleData)
            {
                journies = v.getJournies(); // Journies for a vehicle
                foreach (Journey j in journies) //Get each journey and add to big list
                {
                    journeyData.Add(j);
                }
            }
        }
        private void buildFuelPurchaseData()
        { //Put all the fuel purchases of all the vehicles in fuelData
            ArrayList fpurchases; fuelData.Clear();
            foreach (Vehicle v in vehicleData)
            {
                fpurchases = v.getFPurchases(); // Fuel for a vehicle
                foreach (FuelPurchase f in fpurchases) //Get each fuel purchases and add to big list
                {
                    fuelData.Add(f);
                }
            }
        }

        private void initialiseDataServices()
        { //Go through serviceData and match services to the vehicles, add them to vehicleData
            ArrayList services; //intialise service data for a vehicle 
            foreach (Vehicle v in vehicleData) //Vehicles
            { 
                services = new ArrayList(); //new empty list
                foreach (Service s in serviceData) //Go through service data and remake the services as shouldnt be any yet
                {
                    if (s.getVehicleID() == v.getVehicleID()) //Add to vehicle services as Id the same!
                    {
                        services.Add(s);
                    }
                }
                v.setServices(services); //save back to vehicle data
            }
        }
        private void initialiseDataJourney()
        { //Go through the journeyData and match journies to the vehicles, add them to vehicleData
            ArrayList journies; //journey data for a vehicle 
            foreach (Vehicle v in vehicleData) //Vehicles
            {
                journies = new ArrayList(); //empty
                foreach (Journey j in journeyData) //Go through journey data and remake the journies as shouldnt be any yet
                {
                    if (j.getVehicleID() == v.getVehicleID()) //Add to vehicle journies as Id the same!
                    {
                        journies.Add(j);
                    }
                }
                v.setJournies(journies); //save back to vehicle data
            }
        }
        private void initialiseDataFuelPurchase()
        {//going through the fuelData and match fpurchases to the vehicles and then adding them to the vehicleData
            ArrayList fpurchases; //fuel purchase data for a vehicle 
            foreach (Vehicle v in vehicleData) //Vehicles
            {
                fpurchases = new ArrayList();  // empty
                
                foreach (FuelPurchase fp in fuelData) //Go through fuel data and remake the fuelpurchases as shouldnt be any yet
                {
                    if (fp.getVehicleID() == v.getVehicleID() ) //Add to vehicle fuel purchases as Id the same!
                    {
                        fpurchases.Add(fp);
                    }
                }
                v.setFPurchases(fpurchases); //save back to vehicle data
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

  
    }
}
