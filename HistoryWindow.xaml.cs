﻿using System;
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
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {

        protected Vehicle car;

        protected ArrayList services;
        public ArrayList displayedServices { get; set; }

        protected ArrayList journies;

        public ArrayList displayedJournies { get; set; }

        protected ArrayList FPurchases { get; set; }
        public ArrayList displayedFPurchases { get; set; }

        public HistoryWindow(Vehicle car)
        {
            InitializeComponent();
            this.car = car;
            this.Title = "History of Vehicle ID: " + car.vehicleID;

            //Link the journeys
            this.journies = car.getJournies();
            this.displayedJournies = car.getJournies();
            // services
            this.services = car.getServices();
            this.displayedServices = car.getServices();
            // Link Fuel Purchase
            this.FPurchases = car.getFPurchases();
            this.displayedFPurchases = car.getFPurchases();
            

            this.DataContext = this;
        }
        

        private void AddServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddServiceWindow addServiceWin = new AddServiceWindow(car.getVehicleID());
            addServiceWin.ShowDialog();
            if (addServiceWin.getSaveState() && addServiceWin.getValidity()) //If a vehicle was saved!
            {
                Service s = addServiceWin.getService(); //Get the new service
                services.Add(s); //Add to the list
                Console.WriteLine("Service Added");
            }
            afterEffects();
        }

        private void EditServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            //Get indicies to edit
            IList iL = ServicesDisplayLvw.SelectedItems; //selected items (vehicles)
            if (iL.Count == 1)
            {
                EditServiceWindow editServiceWin = new EditServiceWindow((Service) iL[0]); //Send service to be edited
                editServiceWin.ShowDialog();
                //Check if saved or not
                if (editServiceWin.getSaveState() && editServiceWin.getValidity()) //If a serice was saved!
                {
                    Service S = editServiceWin.getService(); //Get the saved service
                    services[services.IndexOf(iL[0])] = S; //Overide the service at the editted location!
                    Console.WriteLine("Service Edited");
                }
            } else
            {
                MessageBox.Show("Select a single service to edit");
            }

            afterEffects();
        }



        private void AddJourniesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddJourneyWindow addJourneyWin = new AddJourneyWindow(car.getVehicleID());
            addJourneyWin.ShowDialog();
            if (addJourneyWin.getSaveState() && addJourneyWin.getValidity())
            {
                Journey J = addJourneyWin.getJourney(); // Get the new journey
                journies.Add(J);   //  Add to the list
                Console.WriteLine(" Journey Added");
            }
            afterEffects();
        }




        private void EditJourniesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            IList iL = JourniesDisplayLvw.SelectedItems;  //select items (vehicles)
            if (iL.Count == 1)
            {
                EditJourneyWindow editJourneyWin = new EditJourneyWindow((Journey) iL[0]);
                editJourneyWin.ShowDialog();
                // Check if saved or not
                if (editJourneyWin.getSaveState() && editJourneyWin.getValidity())   //if a journey was saved
                    {
                        Journey J = editJourneyWin.getJourney();                        // Get the saved journey
                        journies[journies.IndexOf(iL[0])] = J;                          // Overide the service at the editted location
                        Console.WriteLine(" Journey Edited");
                    } else
                {
                     MessageBox.Show(" Select a single journey to  edit");
                } 
            }
            afterEffects();

        }

        private void AddFuelPurchasesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddFuelRecordsWindow addFuelWin = new AddFuelRecordsWindow(car.getVehicleID());
            addFuelWin.ShowDialog();
            if (addFuelWin.getSaveState() && addFuelWin.getValidity())
            {
                FuelPurchase F = addFuelWin.getFuelPurchase(); // Get the new Fuel
                FPurchases.Add(F);   //  Add to the list
                Console.WriteLine(" Fuel Purchase Added");
            }
            afterEffects();
        }

        private void EditFuelPurchasesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            IList iL = FuelPurchasesDisplayLvw.SelectedItems;  //select items (vehicles)
            if (iL.Count == 1)
            {
                EditFuelRecordsWindow editFuelRecordsWin = new EditFuelRecordsWindow((FuelPurchase)iL[0]);
                editFuelRecordsWin.ShowDialog();
                // Check if saved or not
                if (editFuelRecordsWin.getSaveState() && editFuelRecordsWin.getValidity())   //if a journey was saved
                {
                    FuelPurchase F = editFuelRecordsWin.getFuelPurchase();                        // Get the saved journey
                    FPurchases[FPurchases.IndexOf(iL[0])] = F;                          // Overide the service at the editted location
                    Console.WriteLine(" Fuel Edited");
                }
                else
                {
                    MessageBox.Show(" Select a single Fuel Record to edit");
                }
            }
            afterEffects();
        }

        private void performRefresh()
        {

            displayedServices = copyArrayList(services); //Initialise displayed services (before filter)
            displayedJournies = copyArrayList(journies);
            displayedFPurchases = copyArrayList(FPurchases);
            //Manually reset binding and refresh -works! (note XAML binding only seemed to work on window creation!)
            ServicesDisplayLvw.SetBinding(ListView.ItemsSourceProperty,
                new Binding
                {
                    Path = new PropertyPath("displayedServices"),
                    NotifyOnTargetUpdated = true
                });
            ServicesDisplayLvw.Items.Refresh();

            JourniesDisplayLvw.SetBinding(ListView.ItemsSourceProperty,
               new Binding
               {
                   Path = new PropertyPath("displayedJournies"),
                   NotifyOnTargetUpdated = true
               });
            JourniesDisplayLvw.Items.Refresh();

            FuelPurchasesDisplayLvw.SetBinding(ListView.ItemsSourceProperty,
                new Binding
                {
                    Path = new PropertyPath("displayedFPurchases"),
                    NotifyOnTargetUpdated = true
                });
            FuelPurchasesDisplayLvw.Items.Refresh();
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
            performRefresh(); //perform the filter on the displayed data!
        }

        public Vehicle getVehicle()
        {
            return this.car;
        }

        private ArrayList copyArrayList(ArrayList AL)
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < AL.Count; i++) //Clone all values to the new list!
            {
                newAL.Add(AL[i]);
            }
            return newAL;
        }

      
    }
}
