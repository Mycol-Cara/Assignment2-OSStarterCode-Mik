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
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        protected int vehicleID;

        protected List<Service> services;
        protected List<Service> allServices;
        public ArrayList displayedServices { get; set; }

        protected List<Journey> journies;
        protected List<Journey> allJournies;
        public ArrayList displayedJournies { get; set; }

        protected List<FuelPurchase> FPurchases;
        protected List<FuelPurchase> allFuelPurchases;
        public ArrayList displayedFPurchases { get; set; }

        public HistoryWindow(int vehicleID, List<Service> allServices, List<Journey> allJournies, List<FuelPurchase> allFuelPurchases)
        {
            InitializeComponent();
            this.vehicleID = vehicleID;
            this.Title = "History of Vehicle ID: " + vehicleID;

            // the journeys of the car
            this.allJournies = allJournies;
            this.journies = Journey.FindJournies(allJournies, vehicleID);
            // services of the car
            this.allServices = allServices;
            this.services = Service.FindServices(allServices, vehicleID);
            //Fuel Purchase of the car
            this.allFuelPurchases = allFuelPurchases;
            this.FPurchases = FuelPurchase.FindFuelPurchases(allFuelPurchases, vehicleID);

            this.DataContext = this; formatDates("{0:dd/MM/yyyy}");  performRefresh(); //Binding
        }
        

        //SERVICES ADD EDIT DELETE
        private void AddServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddServiceWindow addServiceWin = new AddServiceWindow(vehicleID);
            addServiceWin.ShowDialog();
            if (addServiceWin.getSaveState() && addServiceWin.getValidity()) //If a vehicle was saved!
            {
                Service s = addServiceWin.getService(); //Get the new service
                services.Add(s); //Add to the list of services for the car
                allServices.Add(s); //Add to the list of all services
                Console.WriteLine("Service Added");
            }
            afterEffects();
        }
        private void EditServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            
            //Get indicies to edit
            IList iL = ServicesDisplayLvw.SelectedItems; //selected items (services)
            if (iL.Count == 1) //Single item selected
            {
                beforeEffects();
                EditServiceWindow editServiceWin = new EditServiceWindow((Service) iL[0]); //Send service to be edited
                editServiceWin.ShowDialog();
                //Check if saved or not
                if (editServiceWin.getSaveState() && editServiceWin.getValidity()) //If a serice was saved!
                {
                    Service S = editServiceWin.getService(); //Get the saved service
                    services[services.IndexOf((Service) iL[0])] = S; //Overide the service at the editted location!
                    allServices[allServices.IndexOf((Service) iL[0])] = S; //Overide the service in all services!
                    Console.WriteLine("Service Edited");
                }
                afterEffects();
            } else
            {
                MessageBox.Show("Select a single service to edit");
            }

        }
        private void DeleteServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = ServicesDisplayLvw.SelectedItems; //selected items from list view (services)
            if (iL.Count == 1) //Only proceed if theere is a single selection
            {
                beforeEffects();
                DeleteVehiclesWindow deleteVehiclesWin = new DeleteVehiclesWindow("Standard"); //Delete  vehicle verification using standard proceedure (message)
                deleteVehiclesWin.ShowDialog();
                if (deleteVehiclesWin.deletionVerified()) //Check that the user said Yes to the delete action!
                {
                    //Removal of the selected service
                    services.Remove((Service) iL[0]); //Remove from service list for this vehicle
                    allServices.Remove((Service)iL[0]); //Remove for all services data
                }
                afterEffects();
            }
            else
            {
                MessageBox.Show("Select a single item to delete");
            }
        }

        //JOURNEYS ADD EDIT DELETE
        private void AddJourniesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddJourneyWindow addJourneyWin = new AddJourneyWindow(vehicleID,true);
            addJourneyWin.ShowDialog();
            if (addJourneyWin.getSaveState() && addJourneyWin.getValidity())
            {
                Journey J = addJourneyWin.getJourney(); // Get the new journey
                journies.Add(J);   //  Add to the list
                allJournies.Add(J); //Add to the list of all journeys
                Console.WriteLine(" Journey Added");
            }
            afterEffects();
        }
        private void EditJourniesBtn_Click(object sender, RoutedEventArgs e)
        {
            
            IList iL = JourniesDisplayLvw.SelectedItems;  //select items (journies)
            if (iL.Count == 1)
            {
                beforeEffects();
                EditJourneyWindow editJourneyWin = new EditJourneyWindow((Journey) iL[0]);
                editJourneyWin.ShowDialog();
                // Check if saved or not
                if (editJourneyWin.getSaveState() && editJourneyWin.getValidity())   //if a journey was saved
                    {
                        Journey J = editJourneyWin.getJourney();                        // Get the saved journey
                        journies[journies.IndexOf((Journey) iL[0])] = J;              // Overide the service at the editted location
                        allJournies[allJournies.IndexOf((Journey)iL[0])] = J; //Overide the service in all journies!
                        Console.WriteLine(" Journey Edited");
                }
                afterEffects();
            }
            else
            {
                MessageBox.Show(" Select a single journey to  edit");
            }
            

        }
        private void DeleteJourniesBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = JourniesDisplayLvw.SelectedItems; //selected items from list view (journies)
            if (iL.Count == 1) //Only proceed if theere is a single selection
            {
                beforeEffects();
                DeleteVehiclesWindow deleteVehiclesWin = new DeleteVehiclesWindow("Standard"); //Delete  vehicle verification using standard proceedure (message)
                deleteVehiclesWin.ShowDialog();
                if (deleteVehiclesWin.deletionVerified()) //Check that the user said Yes to the delete action!
                {
                    //Removal of the selected service
                    journies.Remove((Journey)iL[0]); //Remove from journey list for this vehicle
                    allJournies.Remove((Journey)iL[0]); //Remove for all journies data
                }
                afterEffects();
            }
            else
            {
                MessageBox.Show("Select a single item to delete");
            }
        }

        //FUEL PURCHASES ADD EDIT DELETE 
        private void AddFuelPurchasesBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            AddFuelRecordsWindow addFuelWin = new AddFuelRecordsWindow(vehicleID);
            addFuelWin.ShowDialog();
            if (addFuelWin.getSaveState() && addFuelWin.getValidity())
            {
                FuelPurchase F = addFuelWin.getFuelPurchase(); // Get the new Fuel
                FPurchases.Add(F);   //  Add to the list
                allFuelPurchases.Add(F); //Add to the list of all fuel purchases
                Console.WriteLine(" Fuel Purchase Added");
            }
            afterEffects();
        }
        private void EditFuelPurchasesBtn_Click(object sender, RoutedEventArgs e)
        {
            
            IList iL = FuelPurchasesDisplayLvw.SelectedItems;  //select items (fuel purchases)
            if (iL.Count == 1)
            {
                beforeEffects();
                EditFuelRecordsWindow editFuelRecordsWin = new EditFuelRecordsWindow((FuelPurchase)iL[0]);
                editFuelRecordsWin.ShowDialog();
                // Check if saved or not
                if (editFuelRecordsWin.getSaveState() && editFuelRecordsWin.getValidity())   //if a purchase was saved
                {
                    FuelPurchase F = editFuelRecordsWin.getFuelPurchase();                        // Get the saved purchase
                    FPurchases[FPurchases.IndexOf((FuelPurchase) iL[0])] = F;                          // Overide the fuel purchase at the editted location
                    allFuelPurchases[allFuelPurchases.IndexOf((FuelPurchase)iL[0])] = F; //Overide the service in all fuel purchases!
                    Console.WriteLine(" Fuel Edited");
                }
                afterEffects();
            }
            else
            {
                MessageBox.Show(" Select a single Fuel Record to edit");
            }
            
        }
        private void DeleteFuelBtn_Click(object sender, RoutedEventArgs e)
        {
            IList iL = FuelPurchasesDisplayLvw.SelectedItems; //selected items from list view (fuel purchase)
            if (iL.Count == 1) //Only proceed if theere is a single selection
            {
                beforeEffects();
                DeleteVehiclesWindow deleteVehiclesWin = new DeleteVehiclesWindow("Standard"); //Delete  vehicle verification using standard proceedure (message)
                deleteVehiclesWin.ShowDialog();
                if (deleteVehiclesWin.deletionVerified()) //Check that the user said Yes to the delete action!
                {
                    //Removal of the selected service
                    FPurchases.Remove((FuelPurchase)iL[0]); //Remove from fpurchase list for this vehicle
                    allFuelPurchases.Remove((FuelPurchase)iL[0]); //Remove for all fpurchase data
                }
                afterEffects();
            }
            else
            {
                MessageBox.Show("Select a single item to delete");
            }
        }


        //GETTERS
        public List<Service> getAllServices()
        {
            return this.allServices;
        }
        public List<Journey> getAllJournies()
        {
            return this.allJournies; 
        }
        public List<FuelPurchase> getAllFuelPurchases()
        {
            return this.allFuelPurchases; 
        }


        //HELPERS
        private void performRefresh()
        {
           

            displayedServices = copyToArrayList(services); //Initialise displayed data 
            displayedJournies = copyToArrayList(journies);
            displayedFPurchases = copyToArrayList(FPurchases);

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

        private ArrayList copyToArrayList(List<Service> L)
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < L.Count; i++) //Clone all values to the new list!
            {
                newAL.Add(L[i]);
            }
            return newAL;
        }
        private ArrayList copyToArrayList(List<Journey> L)
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < L.Count; i++) //Clone all values to the new list!
            {
                newAL.Add(L[i]);
            }
            return newAL;
        }
        private ArrayList copyToArrayList(List<FuelPurchase> L)
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < L.Count; i++) //Clone all values to the new list!
            {
                newAL.Add(L[i]);
            }
            return newAL;
        }

        //Put dates in a format
        private void formatDates(String format)
        {
            JourniesGridView.Columns[1].DisplayMemberBinding.StringFormat = format;
            JourniesGridView.Columns[2].DisplayMemberBinding.StringFormat = format;
            JourniesGridView.Columns[3].DisplayMemberBinding.StringFormat = format;
            ServiceGridView.Columns[2].DisplayMemberBinding.StringFormat = format;
            ServiceGridView.Columns[3].DisplayMemberBinding.StringFormat = format;
            ServiceGridView.Columns[4].DisplayMemberBinding.StringFormat = format;
            FuelPurchasesGridView.Columns[2].DisplayMemberBinding.StringFormat = format;
            FuelPurchasesGridView.Columns[3].DisplayMemberBinding.StringFormat = format;
        }
        
    }
}
