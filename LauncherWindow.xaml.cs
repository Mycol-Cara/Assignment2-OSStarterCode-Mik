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

        private Boolean adminMode;
        public LauncherWindow()
        {
            InitializeComponent();
            vehicleData = new List<Vehicle>(); //Need an option to load in the data
            
            LaunchVehiclesBtn.IsHitTestVisible = false; //can't use until database loaded
            LaunchVehiclesBtn.Opacity = 0.5;
            setAdminMode(false); //set usability for admin button (save database)
        }



        private void LaunchVehiclesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();                // giving opacity to the main window

            VehicleLauncher vl = new VehicleLauncher(copyToArrayList(vehicleData), adminMode);
            vl.ShowDialog();
            vehicleData = new List<Vehicle>();
            vehicleData = vl.getVehicleList(); //update data

            this.adminMode = vl.getAdminMode(); //get the admin mode, incase has disabled...
            setAdminMode(this.adminMode);

            afterEffects();

        }

        private void LoadDatabaseBtn_Click(object sender, RoutedEventArgs e)
        {
            //loading data 
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "saveFile.txt"; //Path
            if (File.Exists(path))
            {                     // reading only if exists
                string readText = File.ReadAllText(path); //Reading the text from file
                //Console.WriteLine(readText);
                vehicleData = new List<Vehicle>();
                vehicleData = JsonConvert.DeserializeObject<List<Vehicle>>(readText);
            }
            else { MessageBox.Show("Could not find saved file!"); }
            LaunchVehiclesBtn.IsHitTestVisible = true; //now vehicle view is usable
            LaunchVehiclesBtn.Opacity = 1.0;
        }

        private void SaveDatabaseBtn_Click(object sender, RoutedEventArgs e)
        {
            //Convert companies to JSON using JSON .NET package
            string jsonVehicles = JsonConvert.SerializeObject(vehicleData);
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "saveFile.txt"; //Path
            File.WriteAllText(path, jsonVehicles); //Write text to file
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
