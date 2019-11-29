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

        public Details (String model, String manufacturer, int serviceNumber, int distance)
        {
            this.modelVehicle = model;
            this.manufacturer = manufacturer;
            this.serviceNumber = serviceNumber;
            this.distanceTravelled = distance;
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

    }
}
