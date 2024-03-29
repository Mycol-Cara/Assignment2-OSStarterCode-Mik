﻿//using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for VehicleLauncherWindow.xaml
    /// </summary>
    public partial class VehicleLauncher : Window
    {
        protected ArrayList vehicles; //all vehicle data, displayed
        protected List<Service> allServices; //Not displayed/modified here, displayed in history window
        protected List<Journey> allJournies; //Not displayed/modified here, displayed in history window
        protected List<FuelPurchase> allFuelPurchases; //Not displayed/modified here, displayed in history window
        public ArrayList displayedVehicles { get; set; } //linked to the display allowing filter by this vehicle data
        private Boolean adminMode;
        public VehicleLauncher(ArrayList existingVehicles, List<Service> existingServices, List<Journey> existingJournies, List<FuelPurchase> existingFuelPurchases, Boolean adminMode)
        {
            InitializeComponent();
            this.Title = "Vehicle Launcher";

            //Get the (a copy of) vehicle list 
            vehicles = existingVehicles;
            displayedVehicles = existingVehicles;

            //other data (not displayed)
            allServices = existingServices;
            allJournies = existingJournies;
            allFuelPurchases = existingFuelPurchases;

            //Data context for displayed list of companies
            this.DataContext = this;

            //Set up admin buttons to be non accesible to customer
            this.adminMode = adminMode;
            setAdminMode(adminMode);
        }


        private void AddVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddVehiclesWindow addVehiclesWin = new AddVehiclesWindow(generateID());
            addVehiclesWin.ShowDialog();
            if (addVehiclesWin.getSaveState() && addVehiclesWin.getValidity()) //If a vehicle was saved!
            {
                Vehicle v = addVehiclesWin.getVehicle(); //Get the saved vehicle
                vehicles.Add(v); //Add the vehicle to the list of vehicles
                // update SQL
                Console.WriteLine("Vehicle Added");
            }
            afterEffects();
        }

        private void EditVehicleBtn_Click(object sender, RoutedEventArgs e)
        { 
            beforeEffects();
            //Get indicies to edit
            IList iL = DisplayLvw.SelectedItems; //selected items (vehicles)
            int[] selectInd = new int[iL.Count]; //Selected indicies array
            for (int i = 0; i < iL.Count; i++)
            {
                selectInd[i] = vehicles.IndexOf((Vehicle)iL[i]); //Store all the indices for editing
            }
            //Run the edits
            for (int i = 0; i < selectInd.Length; i++) //Loop through the edits (because we can select more than one vehicle to edit!)
            {
                EditVehiclesWindow editVehiclesWin = new EditVehiclesWindow((Vehicle)vehicles[(selectInd[i])]); //Send vehicle to from list to be edited
                editVehiclesWin.ShowDialog();
                //Check if saved or not
                if (editVehiclesWin.getSaveState() && editVehiclesWin.getValidity()) //If a vehicle was saved!
                {
                    Vehicle V = editVehiclesWin.getVehicle(); //Get the saved vehicle
                    vehicles[(selectInd[i])] = V; //Overide the vehicle at the editted location!
                    Console.WriteLine("Vehicle Edited");
                }
            }

            afterEffects();
        }
        
        private void DeleteVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = DisplayLvw.SelectedItems; //selected items from list view (vehicles)
            if (iL.Count == 1) //Only proceed if theere is a single selection
            {
                beforeEffects();

                DeleteVehiclesWindow deleteVehiclesWin = new DeleteVehiclesWindow("Standard"); //Delete  vehicle verification using standard proceedure (message)
                deleteVehiclesWin.ShowDialog();

                if (deleteVehiclesWin.deletionVerified()) //Check that the user said Yes to the delete action!
                {
                    //Removal of the selected vehicle
                    int vId = ((Vehicle)iL[0]).getVehicleID(); //Get the id
                    vehicles.Remove(iL[0]); //remove the object from vehicles
                    allServices = Service.RemoveServices(allServices, vId);//Remove the services for that vehicle id
                    allJournies = Journey.RemoveJournies(allJournies, vId);//Remove the journies for that vehicle id
                    allFuelPurchases = FuelPurchase.RemoveFuelPurchases(allFuelPurchases, vId);//Remove the fuelPurchases for that vehicle id
                }

                afterEffects();
            } else
            {
                MessageBox.Show("Select a single vehicle to delete");
            }
        }

        private void ViewDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = DisplayLvw.SelectedItems; //selected items from list view (vehicles)
            if (iL.Count == 1) //Only proceed if theere is a  single selection
            {
                beforeEffects();
                //Get vehicle
                Vehicle v = (Vehicle) iL[0];
                //Create details
                Details D = new Details(v, allServices, allJournies);
                //View details
                DetailsWindow detailsWin = new DetailsWindow(D);
                detailsWin.ShowDialog();
                //
                afterEffects();
            } else
            {
                MessageBox.Show("Select a single vehicle to see the details");
            }
        }

        private void RentBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = DisplayLvw.SelectedItems; //selected items from list view (vehicles)
            if (iL.Count == 1) //Only proceed if theere is selection
            {
                RentalWindow rentalWin = new RentalWindow(new Rental((Vehicle)iL[0], allJournies, allServices));   //TODO advanced customer detail and contact with customer
                rentalWin.ShowDialog();
                allJournies = rentalWin.getAllJournies(); //Update journey data
            } else
            {
                MessageBox.Show("Please select a single vehicle to pay rent!");
            }
            
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
                //}
            } else //currently active
            {
                setAdminMode(!adminMode); //turn off, no need for password
            }
            
        }

        private void VehicleHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = DisplayLvw.SelectedItems; //selected items from list view (vehicles)
            if (iL.Count == 1) //Only proceed if theere is selection
            {
                beforeEffects();

                HistoryWindow hw = new HistoryWindow(((Vehicle) iL[0]).vehicleID, allServices, allJournies, allFuelPurchases); //Open the history window! It takes the selected vehicle and then all of the other data!
                hw.ShowDialog();

                //Get data back that may have changed
                allServices = hw.getAllServices();
                allJournies = hw.getAllJournies();
                allFuelPurchases = hw.getAllFuelPurchases();

                afterEffects();
            } else
            {
                MessageBox.Show("Select a single vehicle to see the history");
            }
        }


        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Perform the filter function using the search text
            performFilterAndRefresh(SearchTxt.Text);
        }
        
        

        //Getters
        public List<Vehicle> getVehicleList()
        {
            return arrayListToList(vehicles);
        }
        public List<Service> getServiceList()
        {
            return this.allServices;
        }
        public List<Journey> getJourneyList()
        {
            return this.allJournies;
        }
        public List<FuelPurchase> getFuelPurchaseList()
        {
            return this.allFuelPurchases;
        }


        //Helpers



        private void performFilterAndRefresh(String keyWord)
        {

            displayedVehicles = copyArrayList(vehicles); //Initialise displayed vehicles (before filter)


            if (keyWord.Length > 0) //Only if there is a keyword try to filter!
            {
                Vehicle V; //Company var
                int i = 0; //Index var
                while (i < displayedVehicles.Count) //loop through companies to display
                {
                    V = (Vehicle)displayedVehicles[i]; //get company
                    if (match(keyWord, V.getManufacturer())) //evaluate if there is a match with keyword
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else if (match(keyWord, V.getModel()))
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else if (match(keyWord, V.getMakeYear().ToString()))
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else if (match(keyWord, V.getRegistrationNumber()))
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else if (match(keyWord, V.getOdometerReading().ToString()))
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else if (match(keyWord, V.getTankCapacity().ToString()))
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else
                    {
                        displayedVehicles.RemoveAt(i); //Remove the non match!
                    }
                }
            }

            //Console.WriteLine(JsonConvert.SerializeObject(displayedVehicles)); //Check object in console

            //Manually reset binding and refresh -works! (note XAML binding only seemed to work on window creation!)
            DisplayLvw.SetBinding(ListView.ItemsSourceProperty,
                new Binding
                {
                    Path = new PropertyPath("displayedVehicles"),
                    NotifyOnTargetUpdated = true
                });
            DisplayLvw.Items.Refresh();

        }
        private Boolean match(String child, String parent)
        {
            //Simple search using substring method in lowercase
            child = child.ToLower(); //lower case conversion
            parent = parent.ToLower();
            return parent.Contains(child); //Return contains boolean!
        }

        private int generateID()
        {
            int maxID = 1; //to overwrite to generate a new vehicle ID
            int vID; //vehicle id

            for (int i = 0; i < vehicles.Count; i++)
            {
                vID = ((Vehicle)vehicles[i]).getVehicleID();
                if (vID >= maxID)
                {
                    maxID = vID + 1;
                }
            }

            return maxID;
        }
        private ArrayList copyArrayList(ArrayList AL)
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < AL.Count; i++) //Clone all values to the new list!
            {
                newAL.Add((Vehicle)AL[i]);
            }
            return newAL;
        }

        private List<Vehicle> arrayListToList(ArrayList AL)
        {
            List<Vehicle> newL = new List<Vehicle>();
            for (int i = 0; i < AL.Count; i++) //Clone all values to the new list!
            {
                newL.Add((Vehicle)AL[i]);
            }
            return newL;
        }

        private void setAdminMode(Boolean mode)
        {
            adminMode = mode; //set admin mode
            AddVehicleBtn.IsHitTestVisible = mode; //set admin button activity
            VehicleHistoryBtn.IsHitTestVisible = mode;
            EditVehicleBtn.IsHitTestVisible = mode;
            DeleteVehicleBtn.IsHitTestVisible = mode;
            double opac = 1.0;
            if (!mode) { opac = 0.5; } //admin button opacities
            EditVehicleBtn.Opacity = opac;
            AddVehicleBtn.Opacity = opac;
            VehicleHistoryBtn.Opacity = opac;
            DeleteVehicleBtn.Opacity = opac;
        }
        public Boolean getAdminMode() { return this.adminMode; }

        private void beforeEffects()
        {
            this.VisualOpacity = 0.5;
            this.VisualEffect = new BlurEffect();
        }
        private void afterEffects()
        {
            this.VisualOpacity = 1;
            this.VisualEffect = null;
            performFilterAndRefresh(SearchTxt.Text); //perform the filter on the displayed vehicles!
        }

   
    }
}