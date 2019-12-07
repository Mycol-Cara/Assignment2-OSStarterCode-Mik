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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for RentalWindow.xaml
    /// </summary>

    public partial class RentalWindow : Window
    {
        private Vehicle v;
        private List<Journey> allJournies; //All the current Journies in the database
        private List<Service> allServices; //All the current Services in the database
        private List<Journey> unpaidJournies; //All the current unpaid Journies
        private Journey temporaryJourney; //A temporary journey to pay
        public ArrayList displayedJournies { get; set; }
        public RentalWindow(Vehicle v, List<Journey> allJournies, List<Service> allServices)
        {
            InitializeComponent();
            this.v = v; //Current vehicle
            unpaidJournies = Journey.FindUnpaidJournies(allJournies, v.getVehicleID()); //The unpaid journies for the vehicle being displayed in the window
            this.allJournies = allJournies; //All the journey data to be updated!
            this.allServices = allServices;
            this.DataContext = this;
            performRefresh(); //Update binding information
        }

        //Add new journey
        private void NewJourneyBtn_Click(object sender, RoutedEventArgs e)
        {

            AddJourneyWindow addJourneyWin = new AddJourneyWindow(v.getVehicleID(), false);
            addJourneyWin.ShowDialog();
            if (addJourneyWin.getSaveState() && addJourneyWin.getValidity())
            {
                Journey J = addJourneyWin.getJourney(); // Get the new journey
                Boolean rentable = true; //Can rent
                if (Journey.JourneyDateExists(allJournies, J) == true) { rentable = false; MessageBox.Show("Date Taken"); } //Check if journey of same date exists in database!
                else if (Service.DistanceSinceServiceKm(Service.FindServices(allServices, v.getVehicleID()), v) > Service.SERVICE_KILOMETER_LIMIT) { rentable = false; MessageBox.Show("Service Required"); } //Needs service
                else if (J.getjourneyAt().Year - v.getMakeYear() < 0) { rentable = false; MessageBox.Show("Journey date older than car"); } //Car doesn't exist yet!
                else if (J.getdistanceTravelled() > Journey.JOURNEY_RENT_KM_LIMIT) { rentable = false; MessageBox.Show("Journey too long for rental trip"); } //Journey too long!

                //Add to the list of unpaid journies
                if (rentable)
                {
                    temporaryJourney = J; //add to list as not paid waiting payment (or remove from list)
                    unpaidJournies.Add(temporaryJourney);   //  Add to the list
                    allJournies.Add(temporaryJourney);      // Add to all journies
                                                            //Refresh display
                    performRefresh();
                    //Disable button
                    NewJourneyBtn.IsHitTestVisible = false;
                    NewJourneyBtn.Opacity = 0.5;
                }
            } else { MessageBox.Show("Invalid Data Inputs"); }


        }

        //Method to compute cost
        private void JourniesDisplayLvw_Compute(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Computing Cost");
            double cost = 0; //initial cost
            IList iL = JourniesDisplayLvw.SelectedItems; //selected items (journies)
            for (int i = 0; i < iL.Count; i++) //Loop through selected objects (Journies)
            {
                cost = cost + ((Journey)iL[i]).JourneyCost(); //Add this cost
            }
            String str = "$" + cost + " will be automatically deducted from your user account";
            CostLbl.Content = str;
        }

        //Process payment
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            //Note verify customer details!!! - just Admin1 for now, Password1
            IList iL = JourniesDisplayLvw.SelectedItems; //Get the selected items
            if (iL.Count == 0)
            {
                MessageBox.Show("Select journies to pay for!...");
            }
            else if (UserNameTxt.Text == "Admin1" && PasswordTxt.Password == "Password1")
            {
                MessageBox.Show("Payment process underway!...");
                //Update status to paid
                Boolean tempJourneySelected = false; //Status for whether the added journey is paid for!
                for (int i = 0; i < iL.Count; i++) //Loop through selected objects (Journies)
                {
                    if ((Journey)iL[i] == temporaryJourney) { tempJourneySelected = true; } //user added journey is logged
                    allJournies[allJournies.IndexOf((Journey)iL[i])].setPaid(true); //Find the journey in all journies and pay it (to be replaced by mySQL layer)
                }
                if (tempJourneySelected == false) { allJournies.Remove(temporaryJourney); } //Remove if not paid for
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter correct user details! - in this software version 1 (beta) this is Admin1, Password1");
            }
        }

        //Close window
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Getters
        public List<Journey> getAllJournies()
        {
            return this.allJournies;
        }

        //Helpers

        private void performRefresh()
        {
            displayedJournies = copyToArrayList(unpaidJournies);
            //set binding
            JourniesDisplayLvw.SetBinding(ListView.ItemsSourceProperty,
               new Binding
               {
                   Path = new PropertyPath("displayedJournies"),
                   NotifyOnTargetUpdated = true
               });
            JourniesDisplayLvw.Items.Refresh();

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



    }
}
