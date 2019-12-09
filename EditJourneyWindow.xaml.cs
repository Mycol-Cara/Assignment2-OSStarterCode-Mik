using System;
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
    /// Interaction logic for EditJourneyWindow.xaml
    /// </summary>
    public partial class EditJourneyWindow : Window
    {
        private Journey J; //The journey to be created
        protected Boolean saveState; //was it saved on exit
        protected Boolean validJourney;  //input check
        private Boolean Checked; //Radio button status
        public EditJourneyWindow(Journey J)
        {
            InitializeComponent();
            this.J = J;
            saveState = false;           //when window constructed has not been saved yet
            validJourney = true;        //TODO code for this
            Checked = J.isPaid();
            displayJourney();
        }
        private void displayJourney()  // set the vehicle data into the text field
        {
            DistanceTravelledTxt.Text = J.getdistanceTravelled().ToString();
            JourneyDayTxt.Text = J.getjourneyAt().Day.ToString();
            JourneyMonthTxt.Text = J.getjourneyAt().Month.ToString();
            JourneyYearTxt.Text = J.getjourneyAt().Year.ToString();
            PaidRadioBtn.IsChecked = J.isPaid();

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateJourney(); //Update the jounrey
            if (validJourney)
            {
                saveState = true; //set journey to saved before exit!
                this.Close(); //CLOSE WINDOW!
            }
            else
            {
                MessageBox.Show("Error with inputs, please try again!");
            }
        }

        private void UpdateJourney()
        {
            validJourney = true; //start true
            int dt;
            DateTime jdate;
            int day, month, year;
            Boolean jPaid = (Boolean) PaidRadioBtn.IsChecked;
            try
            {
                dt = System.Convert.ToInt32(DistanceTravelledTxt.Text);
                day = System.Convert.ToInt32(JourneyDayTxt.Text);
                month = System.Convert.ToInt32(JourneyMonthTxt.Text);
                year = System.Convert.ToInt32(JourneyYearTxt.Text);
                jdate = new DateTime(year, month, day);
            }
            catch
            {
                Console.WriteLine("Error reading numeric values!");
                validJourney = false; //Due to likely poor data entry //false
                return;
            }

            J = new Journey(dt, jdate, J.getDatecreated(), DateTime.Now, J.getVehicleID(),jPaid); //New journey with all of the information!
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public Journey getJourney()
        {
            return this.J;
        }
        public Boolean getSaveState()
        {
            return saveState;
        }

        public Boolean getValidity()
        {
            return validJourney;
        }

        private void PaidRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Checked)
            {
                PaidRadioBtn.IsChecked = false;
                Checked = false;
            } else
            {
                Checked = true;
            }
            
        }
    }
}
