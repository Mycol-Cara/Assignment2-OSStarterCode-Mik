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
        private int vehicleID;
        private List<Journey> allJournies;
        private List<Journey> unpaidJournies;
        public ArrayList displayedJournies { get; set; }
        public RentalWindow(int vehicleID, List<Journey> allJournies)
        {
            InitializeComponent();
            this.vehicleID = vehicleID; //ID of current vehicle here
            unpaidJournies = Journey.FindUnpaidJournies(allJournies, vehicleID); //The unpaid journies for the vehicle being displayed in the window
            this.allJournies = allJournies; //All the journey data to be updated!
            this.DataContext = this;
            performRefresh(); //Update binding information
        }

        //Method to compute cost
        private void JourniesDisplayLvw_Compute(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Computing Cost");
            double cost = 0; //initial cost
            IList iL = JourniesDisplayLvw.SelectedItems; //selected items (journies)
            for (int i = 0; i < iL.Count; i++) //Loop through selected objects (Journies)
            {
                cost = cost + ((Journey) iL[i]).JourneyCost(); //Add this cost
            }
            String str = "$" + cost + " will be automatically deducted from your staff account";
            CostLbl.Content = str;
        }

        //Process payment
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO verify customer details!!!
            MessageBox.Show("Payment process underway!...");
            //Update status to paid (assuming detail are good)
            IList iL = JourniesDisplayLvw.SelectedItems; //Get the selected items
            for (int i = 0; i < iL.Count; i++) //Loop through selected objects (Journies)
            {
                allJournies[allJournies.IndexOf((Journey)iL[i])].setPaid(true); //Find the journey in all journies and pay it (to be replaced by mySQL layer)
            }
            this.Close();
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

        private void JourniesDisplayLvw_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
