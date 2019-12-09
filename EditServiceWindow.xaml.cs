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
    /// Interaction logic for EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        private Service S; //The service to be created
        protected Boolean saveState; //was it saved on exit
        protected Boolean validService;  //input check

        public EditServiceWindow(Service S)
        {
            InitializeComponent();
            this.S = S;
            saveState = false;           //when window constructed has not been saved yet
            validService = true;        //TODO code for this
            displayService();
        }

        private void displayService() //set the vehicle data into the text fields!
        {
            ServiceCountTxt.Text = S.getServiceCount().ToString();
            OdometerReadingTxt.Text = S.getLastServiceOdometerKm().ToString();
            ServiceDayTxt.Text = S.getLastServiceDate().Day.ToString();
            ServiceMonthTxt.Text = S.getLastServiceDate().Month.ToString();
            ServiceYearTxt.Text = S.getLastServiceDate().Year.ToString();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateService(); //Update the service
            if (validService)
            {
                saveState = true; //set service to saved before exit!
                this.Close(); //CLOSE WINDOW!
            }
            else
            {
                MessageBox.Show("Error with inputs, please try again!");
            }
        }

        private void UpdateService()
        {
            validService = true; //start valid
            int sNum;
            int odom;
            DateTime sdate;
            int day, month, year;
            try
            {
                sNum = System.Convert.ToInt32(ServiceCountTxt.Text); //service count convert to int
                odom = System.Convert.ToInt32(OdometerReadingTxt.Text); //odometer convert to int
                day = System.Convert.ToInt32(ServiceDayTxt.Text);
                month = System.Convert.ToInt32(ServiceMonthTxt.Text);
                year = System.Convert.ToInt32(ServiceYearTxt.Text);
                sdate = new DateTime(year, month, day); //create the new service date
            }
            catch
            {
                Console.WriteLine("Error reading numeric values!");
                validService = false; //Due to likely poor data entry //Invalid
                return;
            }

            //TODO additional check to ensure is valid! 

            //Make change the service
            S.setLastServiceOdometerKm(odom);
            S.setServiceCount(sNum);
            S.setLastServiceDate(sdate);
            S.setDateUpdated(DateTime.Now);
           
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //CLOSE WINDOW!
        }

        public Service getService()
        {
            return this.S;
        }
        public Boolean getSaveState()
        {
            return saveState;
        }

        public Boolean getValidity()
        {
            return validService;
        }
    }
}
