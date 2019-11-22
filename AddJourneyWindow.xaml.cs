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
    /// Interaction logic for AddJourneyWindow.xaml
    /// </summary>
    public partial class AddJourneyWindow : Window
    {
        private Journey J; //The journey to be created
        protected Boolean saveState; //was it saved on exit
        protected Boolean validJourney;  //input check

        public AddJourneyWindow(int vehicleID)
        {
            InitializeComponent();
            this.J = new Journey(0, DateTime.Now, DateTime.Now, DateTime.Now, vehicleID); //Random journey
            saveState = false;           //when window constructed has not been saved yet
            validJourney = true;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            saveState = true; //set vehicle to saved before exit!
            UpdateJourney(); //Update the vehicle
            this.Close(); //CLOSE WINDOW!
        }

        private void UpdateJourney()
        {
            int dt;
            DateTime jdate;
            int day, month, year;
            try
            {
                dt = System.Convert.ToInt32(DistanceTravelledTxt.Text);
                day = System.Convert.ToInt32(ServiceDayTxt.Text);
                month = System.Convert.ToInt32(ServiceMonthTxt.Text);
                year = System.Convert.ToInt32(ServiceYearTxt.Text);
                jdate = new DateTime(year, month, day);
            } catch
            {
                Console.WriteLine("Error reading numeric values!");
                validJourney = false; //Due to likely poor data entry
                return;
            }
            
            J = new Journey(dt, jdate, DateTime.Now, DateTime.Now, J.getVehicleID()); //New journey with all of the information!
            validJourney = true;
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


    }
}
