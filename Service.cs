using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Service
    {
        // Constant to indicate that the vehicle needs to be serviced every 10,000km
        public static readonly int SERVICE_KILOMETER_LIMIT = 10000;

        //Note public get the grid view to bind
        public int lastServiceOdometerKm { get; set; }
        public int serviceCount { get; set; }
        public DateTime lastServiceDate { get; set; } // DateTime(year, month, day);
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int vehicleID { get; set; }

        /**
         * The Service creates a service from the total distance traveled by the car, 
         * the number of services the car has had, and the date of service
         * @param distance 
         * @param serviceNumber
         * @param serviceDate
         * @vehicleID
         */
        public Service(int vehicleID, int distance, int serviceNumber, DateTime serviceDate, DateTime created, DateTime updated)
        {
            this.lastServiceOdometerKm = distance;
            this.serviceCount = serviceNumber;
            this.lastServiceDate = serviceDate; //Added last service date to this record!
            this.created = created; //When it was put in the datebase
            this.updated = updated;
            this.vehicleID = vehicleID;
        }

        // return how many services the car has had
        public int getServiceCount()
        {
            return this.serviceCount;
        }
        //set how many services the car has had
        public void setServiceCount(int serviceCount)
        {
            this.serviceCount = serviceCount;
        }

        // return the last service date
        public DateTime getLastServiceDate()
        {
            return this.lastServiceDate;
        }
        //set the last service date
        public void setLastServiceDate(DateTime lastServiceDate)
        {
            this.lastServiceDate = lastServiceDate;
        }

        // return the last service distance
        public int getLastServiceOdometerKm()
        {
            return this.lastServiceOdometerKm;
        }
        // set the last service distance
        public void setLastServiceOdometerKm(int lastServiceOdometerKm)
        {
            this.lastServiceOdometerKm = lastServiceOdometerKm;
        }

        // return the creation date
        public DateTime getDateCreated()
        {
            return this.created;
        }

        //return the updated date
        public DateTime getDateUpdated()
        {
            return this.updated;
        }
        //set the updated date
        public void setDateUpdated(DateTime updated)
        {
            this.updated = updated;
        }

        public int getVehicleID()
        {
            return this.vehicleID;
        }

        //Helper method to find services for a given vehicle in a list
        public static List<Service> FindServices(List<Service> allServices, int vehicleID)
        { //Go through serviceData and match services to the car with a vehicleID
            List<Service> carServices; //intialise service data for a vehicle 
            carServices = new List<Service>(); //new empty list
            foreach (Service s in allServices) //Go through service data
            {
                if (s.getVehicleID() == vehicleID) //Add to specific car services as Id is the same!
                {
                    carServices.Add(s);
                }
            }
            return carServices;
        }

        //Helper method to remove services for a given vehicleid in a list
        public static List<Service> RemoveServices(List<Service> allServices, int vehicleID)
        { //Go through serviceData and remove services of the car with a given vehicleID
            Service s;
            for (int i = allServices.Count-1; i>=0;i--) //Go through service data backwards
            {
                s = allServices[i]; //Get a service to check
                if (s.getVehicleID() == vehicleID) //Add to specific car services as Id is the same!
                {
                    allServices.RemoveAt(i); //Remove the service
                }
            }
            return allServices;
        }

        /**
         * Calculates the total services by dividing kilometers by
         * {@link #SERVICE_KILOMETER_LIMIT} and floors the value. 
         * 
         * @return the number of services needed per SERVICE_KILOMETER_LIMIT
         */
        public int getTotalScheduledServices()
        {
            decimal f = lastServiceOdometerKm / SERVICE_KILOMETER_LIMIT;
            return (int) Math.Floor(f);
        }
    }
}
