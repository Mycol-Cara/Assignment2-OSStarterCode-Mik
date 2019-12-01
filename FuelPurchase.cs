using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
   public class FuelPurchase
    {
        //private double fuelEconomy;
        //private double litres = 0;
        // private double cost = 0;

        //private double pricePerL;

        public int amount { get; set; } //amount in L
        public double price { get; set; } //price in $/L
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int vehicleID { get; set; }

        /*
         * Constructor with no price calculation, cost is input
         */
        public FuelPurchase (int amount, double price, DateTime created, DateTime updated, int vehicleID)
        {
            this.amount = amount;   //Added amount to this record!
            this.price = price;  // Added price
            this.created = created; //When it was put in the datebase
            this.updated = updated;
            this.vehicleID = vehicleID;
        }

        public int getAmount()
        {
            return this.amount;
        }
        public void setAmount(int amount)
        {
            this.amount = amount;
        }
        public double getPrice()
        {
            return this.price;
        }
        public void setPrice(double price)
        {
           this.price = price;
        }
        public DateTime getDatecreated()
        {
            return this.created;
        }
        
        public DateTime getDateupdated()
        {
            return this.updated;
        }
        public void setDateupdated(DateTime updated)
        {
            this.updated = updated;
        }
        public int getVehicleID()
        {
            return this.vehicleID;
        }

        //Helper method to get fuel purchases for a given vehicle from a list of purchases
        public static List<FuelPurchase> FindFuelPurchases(List<FuelPurchase> allFuelPurchases, int vehicleID)
        { //Go through fuelData and match purchases to the car with a vehicleID
            List<FuelPurchase> carFuelPurchases; //intialise fuel data for a vehicle 
            carFuelPurchases = new List<FuelPurchase>(); //new empty list
            foreach (FuelPurchase fp in allFuelPurchases) //Go through purchase data
            {
                if (fp.getVehicleID() == vehicleID) //Add to specific car fuel purchases as Id is the same!
                {
                    carFuelPurchases.Add(fp);
                }
            }
            return carFuelPurchases;
        }

    }
}
