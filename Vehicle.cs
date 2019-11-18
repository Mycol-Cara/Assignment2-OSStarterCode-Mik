using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Vehicle
    {

        //private String manufacturer;
        //private String model;
        //private int makeYear;
        //private String registrationNumber;
        //private float odometerReading; //in KM
        //private float tankCapacity; //in L

       //Change to allow it to bind in the grid view
        public String manufacturer { get; set; }
        public String model { get; set; }
        public int makeYear { get; set; }
        public String registrationNumber { get; set; }
        public float odometerReading { get; set; } //in KM
        public float tankCapacity { get; set; } //in L

        private FuelPurchase fuelPurchase; //Fuel purchase history of the car

        /**
         * Class constructor specifying name of make (manufacturer), model and year
         * of make.
         * @param manufacturer
         * @param model
         * @param makeYear
         * @param odometerReading
         * @param registrationNumber
         * @param tankCapacity
         */
        public Vehicle(String manufacturer, String model, int makeYear, float odometerReading, String registrationNumber, float tankCapacity)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            this.makeYear = makeYear;
            this.odometerReading = odometerReading;
            this.registrationNumber = registrationNumber;
            this.tankCapacity = tankCapacity;
            fuelPurchase = new FuelPurchase();
        }

        //Getter methods
        public String getManufacturer() { return this.manufacturer; }
        public String getModel() { return this.model; }
        public int getMakeYear() { return this.makeYear; }
        public String getRegistrationNumber() { return this.registrationNumber; }
        public float getOdometerReading() { return this.odometerReading; }
        public float getTankCapacity() { return this.tankCapacity; }
        //Setter methods
        public void setManufacturer(String manufacturer) { this.manufacturer = manufacturer; }
        public void setModel(String model) { this.model = model; }
        public void setMakeYear(int makeYear) { this.makeYear = makeYear; }
        public void setRegistrationNumber(String registrationNumber) { this.registrationNumber = registrationNumber; }
        public void setOdometerReading(float odometerReading) { this.odometerReading = odometerReading; }
        public void setTankCapacity(float tankCapacity) { this.tankCapacity = tankCapacity; }

        /**
         * Prints details for {@link Vehicle}
         */
        public void printDetails()
        {
            Console.WriteLine("Vehicle: " + makeYear + " " + manufacturer + " " + model);
            // TODO Display additional information about this vehicle
        }


        // and adds distance it to the odometer reading. 
        public void addKilometers(float distance)
        {
            odometerReading += distance;
        }

        // adds fuel to the car
        public void addFuel(double litres, double price)
        {
            fuelPurchase.purchaseFuel(litres, price);
        }
    }
}
