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
    /// Interaction logic for EditFuelRecordsWindow.xaml
    /// </summary>
    public partial class EditFuelRecordsWindow : Window
    {

        private FuelPurchase F; //The Fuel Purchase to be created
        protected Boolean saveState; //was it saved on exit
        protected Boolean validFuel;  //input check
        public EditFuelRecordsWindow(FuelPurchase fuelPurchase)
        {
            InitializeComponent();
            this.F = fuelPurchase;
            saveState = false;           //when window constructed has not been saved yet
            validFuel = true;        //TODO code for this
            displayFuel();
        }

        private void displayFuel()  // set the vehicle data into the text field
        {
            AmountTxt.Text = F.getAmount().ToString();
            PriceTxt.Text = F.getPrice().ToString();
           
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            saveState = true;
            UpdateFuel();
            this.Close();
        }


        private void UpdateFuel()
        {
            int amount;
            double price;
            
            try
            {
                amount = System.Convert.ToInt32(AmountTxt.Text);
                price = System.Convert.ToDouble(PriceTxt.Text);
            }
            catch
            {
                Console.WriteLine("Error reading numeric values!");
                validFuel = false; //Due to likely poor data entry
                return;
            }

            F = new FuelPurchase(amount, price, F.getDatecreated(), DateTime.Now, F.getVehicleID()); //New fuelpurchase with all of the information!
            validFuel = true;
        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public FuelPurchase getFuelPurchase()
        {
            return this.F;
        }
        public Boolean getSaveState()
        {
            return saveState;
        }

        public Boolean getValidity()
        {
            return validFuel;
        }
    }
}
