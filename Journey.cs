using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Journey
    {
        public static readonly int JOURNEY_RENT_KM_MAX_LIMIT = 500; //Can't rent longer than 500km
        public static readonly int JOURNEY_RENT_KM_MIN_LIMIT = 0; //Can't rent 0 KM
        private double journeyPricePerKm = 1.0; //$/km

        public int distanceTravelled { get; set; }
        public DateTime journeyAt { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int vehicleID { get; set; }
        public Boolean paid { get; set; }

   
        public Journey(int distanceTravelled , DateTime journeyAt , DateTime created, DateTime  updated, int vehicleID, Boolean paid)
        {
            this.distanceTravelled = distanceTravelled; //Added distance travelled to this record!
            this.journeyAt = journeyAt;  // Added Journey Date 
            this.created = created; //When it was put in the datebase
            this.updated = updated;
            this.vehicleID = vehicleID;
            this.paid = paid; //Whether or not the renter has paid for the the vehicle
        }

        public int getdistanceTravelled()
        {
            return this.distanceTravelled;
        }
        public void setdistanceTravelled(int distanceTravelled)
        {
            this.distanceTravelled = distanceTravelled;
        }
        public DateTime getjourneyAt()
        {
            return this.journeyAt;
        }
        public void setjourneyAt(DateTime jdate)
        {
            this.journeyAt = jdate;
        }
        public DateTime getDatecreated()
        {
            return this.created;
        }
        public void setDatecreated(DateTime dateCreated)
        {
           this.created = dateCreated;
        }
        public DateTime getDateupdated()
        {
            return this.updated;
        }
        public int getVehicleID()
        {
            return this.vehicleID;
        }
        public Boolean isPaid()
        {
            return this.paid;
        }
        public void setPaid(Boolean paid)
        {
            this.paid = paid;
        }


        //Payment methods
        public double JourneyCost()
        {
            double cost = journeyPricePerKm * distanceTravelled;
            return cost;
        }

        //Helper method to get journies from a list according to ID (selecting unpaid journies)
        public static List<Journey> FindUnpaidJournies(List<Journey> allJournies, int vehicleID)
        { //Go through jounreyData and match jounries to the car with a vehicleID
            List<Journey> carJournies; //intialise jouney data for a vehicle 
            carJournies = new List<Journey>(); //new empty list
            foreach (Journey j in allJournies) //Go through jounrey data
            {
                if (j.getVehicleID() == vehicleID && !j.isPaid()) //Add to specific car journies as Id is the same and hasn't been paid for!
                {
                    carJournies.Add(j);
                }
            }
            return carJournies;
        }
        //Helper method to get journies from a list according to ID
        public static List<Journey> FindJournies(List<Journey> allJournies, int vehicleID)
        { //Go through jounreyData and match jounries to the car with a vehicleID
            List<Journey> carJournies; //intialise jouney data for a vehicle 
            carJournies = new List<Journey>(); //new empty list
            foreach (Journey j in allJournies) //Go through jounrey data
            {
                if (j.getVehicleID() == vehicleID) //Add to specific car journies as Id is the same
                {
                    carJournies.Add(j);
                }
            }
            return carJournies;
        }

        //Helper method to remove journies for a given vehicleid in a list
        public static List<Journey> RemoveJournies(List<Journey> allJournies, int vehicleID)
        { //Go through journeyData and remove services of the car with a given vehicleID
            Journey j;
            for (int i = allJournies.Count - 1; i >= 0; i--) //Go through journies data backwards
            {
                j = allJournies[i]; //Get a journey to check
                if (j.getVehicleID() == vehicleID) //Add to specific car journies as Id is the same!
                {
                    allJournies.RemoveAt(i); //Remove the journies
                }
            }
            return allJournies;
        }

        //Check if date for journey exists already 
        public static Boolean JourneyDateExists(List<Journey> allJournies, Journey J)
        {
            foreach (Journey j in allJournies)
            {
                //Check if a date is within 24 hours!
                if (Math.Abs((j.getjourneyAt() - J.getjourneyAt()).TotalHours) < 24 && j.getVehicleID() == J.getVehicleID())
                {
                    return true;
                }
            }
            return false;
        }

        //OLD code
        /** 
         * Appends the distance parameter to {@link #kilometers}
         * @param kilometers the distance traveled 
         */
        public void addKilometers(double kilometers)
        {
            this.distanceTravelled = distanceTravelled + (int) kilometers;
          
        }

        /**
         * Getter method for total Kilometers traveled in this journey.
         * @return {@link #kilometers}
         */
        public double getKilometers()
        {
            //return kilometers;
            return (double) this.distanceTravelled;
        }

    }
}
