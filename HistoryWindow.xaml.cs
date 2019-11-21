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

        protected Vehicle car;
        protected ArrayList services;
        public ArrayList displayedServices { get; set; }
        public HistoryWindow(Vehicle car)
        {
            InitializeComponent();
            this.car = car;
            this.Title = "History of Vehicle ID: " + car.vehicleID;
            //Link the journeys

            // services
            this.services = car.getServices();
            this.displayedServices = car.getServices();

            //Link the fuel purches

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

        private void performRefresh()
        {

            displayedServices = copyArrayList(services); //Initialise displayed vehicles (before filter)

            //Manually reset binding and refresh -works! (note XAML binding only seemed to work on window creation!)
            ServicesDisplayLvw.SetBinding(ListView.ItemsSourceProperty,
                new Binding
                {
                    Path = new PropertyPath("displayedServices"),
                    NotifyOnTargetUpdated = true
                });
            ServicesDisplayLvw.Items.Refresh();

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
            performRefresh(); //perform the filter on the displayed vehicles!
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
