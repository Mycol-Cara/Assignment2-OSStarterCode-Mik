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
        public int cost { get; set; } //cost in $
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int vehicleID { get; set; }

        /*
         * Constructor with no price calculation, cost is input
         */
        public FuelPurchase (int amount, int cost, DateTime created, DateTime updated, int vehicleID)
        {
            this.amount = amount;   //Added amount to this record!
            this.cost = cost;  // Added Cost
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
        public int getCost()
        {
            return this.cost;
        }
        public void setCost(int cost)
        {
           this.cost = cost;
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

      

    }
}
