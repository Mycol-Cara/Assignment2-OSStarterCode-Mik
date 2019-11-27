﻿using System;
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
