using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Details
    {
        protected String modelVehicle;
        protected String manufacturer;
        protected int serviceNumber;
        protected int distanceTravelled;
        protected int distanceSinceService;
        protected String revenueInfo;

        public Details (String model, String manufacturer, int serviceNumber, int distance, int distanceSinceService,String revenueInfo)
        {
            this.modelVehicle = model;
            this.manufacturer = manufacturer;
            this.serviceNumber = serviceNumber;
            this.distanceTravelled = distance;
            this.distanceSinceService = distanceSinceService;
            this.revenueInfo = revenueInfo;
        }

        public String getModel()
        {
            return this.modelVehicle;
        }
        public void setModel(String modelVehicle)
        {
            this.modelVehicle = modelVehicle;
        }
        public String getManufacturer()
        {
            return this.manufacturer;
        }
        public void setManufacturer(String manufacturer)
        {
            this.manufacturer = manufacturer;
        }
        public int getServiceNum()
        {
            return this.serviceNumber;
        }
        public void setServiceNum(int serviceNumber)
        {
            this.serviceNumber = serviceNumber;
        }
        public int getTotDistance()
        {
            return this.distanceTravelled;
        }
        public void setTotDistance(int distanceTravelled)
        {
            this.distanceTravelled = distanceTravelled;
        }
        public String getRevenueInfo()
        {
            return this.revenueInfo;
        }
        public int getDistanceSinceLastService()
        {
            return this.distanceSinceService;
        }

        //Helper method to compute details
        public static Details computeDetails(Vehicle v, List<Service> allServices, List<Journey> allJournies)
        {
            //Get data 
            List<Service> vServices = Service.FindServices(allServices, v.getVehicleID()); //Services for this vehicle
            List<Journey> vJournies = Journey.FindJournies(allJournies, v.getVehicleID()); //Get the journies for this vehicle!

            //Compute number of services
            int numberOfServices = vServices.Count;

            //Compute Distance since service
            int lastServiceOdom = 0; //initialise
            foreach (Service s in vServices) //loop through services s of vehicle v
            {
                //Find highest odometer reading
                if (s.getLastServiceOdometerKm() > lastServiceOdom) { lastServiceOdom = s.getLastServiceOdometerKm(); }
            }
            int distanceSinceService = (int)v.getOdometerReading() - lastServiceOdom;

            //Compute revenue info
            double revenue = 0; double unpaid = 0; //initialise costings
            foreach (Journey j in vJournies)
            {  //loop through journies for vehicle
                revenue = revenue + j.JourneyCost(); //Sum journey costs
                if (!j.isPaid()) { unpaid = unpaid + j.JourneyCost(); } //Sum unpaid costs for this vehicle
            }
            String revenueInfo = revenue + " (with " + unpaid + " unpaid)";

            //Create details
            return new Details(v.getModel() + " " + v.getMakeYear(), v.getManufacturer(), numberOfServices, (int)v.getOdometerReading(), distanceSinceService, revenueInfo);
        }
    }
}
