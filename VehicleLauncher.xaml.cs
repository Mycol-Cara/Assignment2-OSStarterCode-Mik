﻿using MySql.Data.MySqlClient;
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

        //protected EditCompany editCompany;

        protected AddVehiclesWindow addVehiclesWin;
        protected ArrayList vehicles;
        public ArrayList displayedVehicles { get; set; }

        private Boolean adminMode;
        public VehicleLauncher(ArrayList existingVehicles)
        {
            InitializeComponent();
            //String iconPath = System.AppDomain.CurrentDomain.BaseDirectory + "icon4.png";
            this.Title = "    Vehicle Launcher";
            //this.Icon = BitmapFrame.Create(new Uri(iconPath));

            //Get the (a copy of) vehicle list 
            vehicles = existingVehicles;
            displayedVehicles = existingVehicles;

            //Data context for displayed list of companies
            this.DataContext = this;

            //Set up admin buttons to be non accesible to customer
            setAdminMode(false);
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
        private void AddVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            addVehiclesWin = new AddVehiclesWindow();
            addVehiclesWin.ShowDialog();
            if (addVehiclesWin.getSaveState() && addVehiclesWin.getValidity()) //If a vehicle was saved!
            {
                Vehicle v = addVehiclesWin.getVehicle(); //Get the saved vehicle
                vehicles.Add(v); //Add the vehicle to the list of vehicles
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
            if (iL.Count > 0) //Only proceed if theere is selection
            {
                beforeEffects();

                int[] selectInd = new int[iL.Count]; //Selected indicies array
                for (int i = 0; i < iL.Count; i++)
                {
                    selectInd[i] = vehicles.IndexOf((Vehicle)iL[i]); //Store all the indices
                }

                DeleteVehiclesWindow deleteVehiclesWin = new DeleteVehiclesWindow("Standard"); //Delete  vehicle verification using standard proceedure (message)
                deleteVehiclesWin.ShowDialog();

                if (deleteVehiclesWin.deletionVerified()) //Check that the user said Yes to the delete action!
                {
                    //Removal of the selected vehicles
                    for (int j = selectInd.Length - 1; j >= 0; j = j - 1) //start at the end (Length-1) and count backwards
                    { //Loop through selected items
                        vehicles.RemoveAt(selectInd[j]);
                    }
                }

                afterEffects();
            }
        }

        //TODO New Buttons!
        private void ViewDetailsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RentBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdminLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!adminMode) //current inactive
            {
                //Open admin window
                //TODO get username & password
                //if username and password correct{
                setAdminMode(!adminMode); //turn on if user and password correct
                //}
            } else //currently active
            {
                setAdminMode(!adminMode); //turn off, no need for password
            }
            
        }

        private void VehicleHistoryBtn_Click(object sender, RoutedEventArgs e)
        {

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

        public ArrayList getVehicleList()
        {
            return copyArrayList(vehicles);
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

       
    }
}