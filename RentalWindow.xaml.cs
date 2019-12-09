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
        private Rental R;
        public ArrayList displayedJournies { get; set; }
        public RentalWindow(Rental R)
        {
            InitializeComponent();
            this.R = R;
            this.DataContext = this;
            formatDates("{0:dd/MM/yyyy}");  performRefresh(); //Update binding information
        }

        //Add new journey
        private void NewJourneyBtn_Click(object sender, RoutedEventArgs e)
        {

            AddJourneyWindow addJourneyWin = new AddJourneyWindow(R.getVehicle().getVehicleID(), false);
            addJourneyWin.ShowDialog();
            if (addJourneyWin.getSaveState() && addJourneyWin.getValidity())
            {
                Journey J = addJourneyWin.getJourney(); // Get the new journey
                Boolean[] rentable = R.isJourneyRentable(J); //use rental of vehicle to check if vehicle can be rented for this journey

                //Add to the list of unpaid journies
                if (rentable[0] && rentable[1] && rentable[2] && rentable[3]) //Rentable as met all conditions
                {
                    R.addTemporaryRental(J); //set rental awaiting payment                                    
                    performRefresh(); //Refresh display
                    //Disable button, one sucessful rent for window
                    NewJourneyBtn.IsHitTestVisible = false;
                    NewJourneyBtn.Opacity = 0.5;
                }
                else
                { //not rentable
                    String errorMsg = "Not Rentable:";
                    if (!rentable[0]) { errorMsg = errorMsg + " Date Taken!"; } //Check if journey of same date exists in database!
                    if (!rentable[1]) { errorMsg = errorMsg + " Service Required!"; } //Needs service
                    if (!rentable[2]) { errorMsg = errorMsg + " Journey date older than car!"; } //Car doesn't exist yet!
                    if (!rentable[3]) { errorMsg = errorMsg + " Journey too long!"; } //Journey too long!
                    MessageBox.Show(errorMsg); //WHy not rentable
                }
            }
            else if (!addJourneyWin.getValidity()) { MessageBox.Show("Invalid Data Inputs"); }


        }

        //Method to compute cost
        private void JourniesDisplayLvw_Compute(object sender, RoutedEventArgs e)
        {
            CostLbl.Content = R.computeRentalCost(JourniesDisplayLvw.SelectedItems);
        }

        //Process payment!
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            //Note verify customer details!!! - just Admin1 for now, Password1
            IList iL = JourniesDisplayLvw.SelectedItems; //Get the selected items
            if (iL.Count == 0) //If not journey selected, cannot process payment
            {
                MessageBox.Show("Select journies to pay for!...");
            }
            else if (UserNameTxt.Text == "Admin1" && PasswordTxt.Password == "Password1") //Username and Password is correct, and journey is selected to pay for
            {
                MessageBox.Show("Payment process underway!...");
                R.processRental(iL); //Update status to paid for selected rental
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter correct user details! - in this software version 1 (beta) this is Admin1, Password1"); //Message as user details were incorrect
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
            return R.getAllJournies();
        }

        //Helpers
        private void performRefresh()
        {
            displayedJournies = R.getUnpaidJourneys();
            //set binding
            JourniesDisplayLvw.SetBinding(ListView.ItemsSourceProperty,
               new Binding
               {
                   Path = new PropertyPath("displayedJournies"),
                   NotifyOnTargetUpdated = true
               });
            JourniesDisplayLvw.Items.Refresh();

        }

        //Put dates in a format
        private void formatDates(String format)
        {
            JourniesGridView.Columns[1].DisplayMemberBinding.StringFormat = format;
            JourniesGridView.Columns[2].DisplayMemberBinding.StringFormat = format;
            JourniesGridView.Columns[3].DisplayMemberBinding.StringFormat = format;
        }
    }
}
