using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Details
    {
        public String modelVehicle { get; set; }
        public String manufacturer { get; set; }
        public int serviceNumber { get; set; }
        public int distanceTravelled { get; set; }
        public String tipsToTraveller { get; set; }


        public Details (String model, String manufacturer, int serviceNum, int distance, String tips)
        {
            this.modelVehicle = model;
            this.manufacturer = manufacturer;
            this.serviceNumber = serviceNum;
            this.distanceTravelled = distance;
            this.tipsToTraveller = tips;
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
        public String getTips()
        {
            return this.tipsToTraveller;
        }
        public void setTips(String tipsToTraveller)
        {
            this.tipsToTraveller = tipsToTraveller;
        }
    }
}
