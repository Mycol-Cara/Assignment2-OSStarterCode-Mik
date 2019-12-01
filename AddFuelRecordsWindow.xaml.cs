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
    /// Interaction logic for FuelRecordsWindow.xaml
    /// </summary>
    public partial class AddFuelRecordsWindow : Window
    {
        private FuelPurchase F; //The FuelPurchase to be created
        protected Boolean saveState; //was it saved on exit
        protected Boolean validFuelPurchase;  //input check
        public AddFuelRecordsWindow(int vehicleID)
        {
            InitializeComponent();
            this.F = new FuelPurchase(0, 1.0, DateTime.Now, DateTime.Now, vehicleID); //Random fuelPurchase
            saveState = false;           //when window constructed has not been saved yet
            validFuelPurchase = true;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            saveState = true; //set vehicle to saved before exit!
            UpdateFuel(); //Update the vehicle
            this.Close(); //CLOSE WINDOW!
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
                validFuelPurchase = false; //Due to likely poor data entry
                return;
            }

            F = new FuelPurchase(amount, price ,DateTime.Now, DateTime.Now, F.getVehicleID()); //New Fuel with all of the information!
            validFuelPurchase = true;
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
            return validFuelPurchase;
        }

    }
}
