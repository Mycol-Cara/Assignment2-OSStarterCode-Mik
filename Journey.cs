using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Journey
    {

        public int distanceTravelled { get; set; }
        public DateTime journeyAt { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int vehicleID { get; set; }

        public Journey(int distanceTravelled , DateTime journeyAt , DateTime created, DateTime  updated, int vehicleID)
        {
            this.distanceTravelled = distanceTravelled; //Added distance travelled to this record!
            this.journeyAt = DateTime.Now;  // Added Journey Date 
            this.created = DateTime.Now; //When it was put in the datebase
            this.updated = DateTime.Now;
            this.vehicleID = vehicleID;
        }

        public int getdistanceTravelled()
        {
            return this.distanceTravelled;
        }
        public int setdistanceTravelled()
        {
            return this.distanceTravelled;
        }
        public DateTime getjourneyAt()
        {
            return this.journeyAt;
        }
        public DateTime setjourneyAt()
        {
            return this.journeyAt;
        }
        public DateTime getDatecreated()
        {
            return this.created;
        }
        public DateTime setDatecreated()
        {
            return this.created;
        }
        public DateTime getDateupdated()
        {
            return this.updated;
        }
        public int getVehicleID()
        {
            return this.vehicleID;
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
