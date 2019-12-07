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

        public Details(Vehicle v, List<Service> allServices, List<Journey> allJournies)
        {
            computeVehicleDetails(v);
            computeServiceHistoryDetails(v, allServices);
            computeJourneyHistoryDetails(v, allJournies);
        }

        //private methods to compute details
        private void computeVehicleDetails(Vehicle v)
        {
            this.modelVehicle = v.getModel() + " " + v.getMakeYear();
            this.manufacturer = v.getManufacturer();
            this.distanceTravelled = (int)v.getOdometerReading();
        }
        private void computeServiceHistoryDetails(Vehicle v, List<Service> allServices)
        {
            //Get data 
            List<Service> vServices = Service.FindServices(allServices, v.getVehicleID()); //Services for this vehicle

            //Compute number of services
            int numberOfServices = vServices.Count;

            //Compute Distance since service
            int distanceSinceService = Service.DistanceSinceServiceKm(vServices, v);

            //Store details
            this.serviceNumber = numberOfServices;
            this.distanceSinceService = distanceSinceService;
        }
        private void computeJourneyHistoryDetails(Vehicle v, List<Journey> allJournies)
        {
            //Get data 
            List<Journey> vJournies = Journey.FindJournies(allJournies, v.getVehicleID()); //Get the journies for this vehicle!

            //Compute revenue info
            double revenue = 0; double unpaid = 0; //initialise costings
            foreach (Journey j in vJournies)
            {  //loop through journies for vehicle
                revenue = revenue + j.JourneyCost(); //Sum journey costs
                if (!j.isPaid()) { unpaid = unpaid + j.JourneyCost(); } //Sum unpaid costs for this vehicle
            }
            this.revenueInfo = revenue + " (with " + unpaid + " unpaid)";

        }


        //Getter and setter

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



    
    }
}
