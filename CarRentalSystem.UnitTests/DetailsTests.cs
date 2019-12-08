using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRentalSystem.UnitTests
{
    [TestClass]
    public class DetailsTests
    {
        [TestMethod]
        public void LoadAll_DataNotNull_ReturnsTrue()
        {
            //Arrange
            //Get data from SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");

            //Act 
            //Do nothing

            //Assert
            Assert.IsNotNull(data);
        }


        [TestMethod]
        public void Details_CheckModel_AssertsEqualsTrue()
        {
            //Arrange
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];

            //Act 
            //Create Details
            Details D0 = new Details(vehicleData[0], serviceData, journeyData);
            Details D1 = new Details(vehicleData[1], serviceData, journeyData);
            Details D2 = new Details(vehicleData[2], serviceData, journeyData);

            //Assert
            //Check vehicle details
            Assert.AreEqual("Fiesta 2010", D0.getModel());
            Assert.AreEqual("Barina 2015", D1.getModel());
            Assert.AreEqual("Mito 2019", D2.getModel());

        }

        [TestMethod]
        public void Details_CheckManufacturer_AssertsEqualsTrue()
        {
            //Arrange 
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];


            //Act
            //Create Details
            Details D0 = new Details(vehicleData[0], serviceData, journeyData);
            Details D1 = new Details(vehicleData[1], serviceData, journeyData);
            Details D2 = new Details(vehicleData[2], serviceData, journeyData);


            //Assert
            //Check Vehicles Details
            Assert.AreEqual("Ford", D0.getManufacturer());
            Assert.AreEqual("Holden", D1.getManufacturer());
            Assert.AreEqual("Alfa Romeo", D2.getManufacturer());

        }
        [TestMethod]
        public void Details_CheckServiceNum_AssertsEqualsTrue()
        {
            //Arrange 
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];


            //Act
            //Create Details
            Details D0 = new Details(vehicleData[0], serviceData, journeyData);
            Details D1 = new Details(vehicleData[1], serviceData, journeyData);
            Details D2 = new Details(vehicleData[2], serviceData, journeyData);


            //Assert
            //Check Vehicles Details
            Assert.AreEqual(3, D0.getServiceNum());
            Assert.AreEqual(1, D1.getServiceNum());
            Assert.AreEqual(1, D2.getServiceNum());

        }

        [TestMethod]
        public void Details_CheckDistanceTravelled_AssertsEqualsTrue()
        {
            //Arrange 
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];


            //Act
            //Create Details
            Details D0 = new Details(vehicleData[0], serviceData, journeyData);
            Details D1 = new Details(vehicleData[1], serviceData, journeyData);
            Details D2 = new Details(vehicleData[2], serviceData, journeyData);


            //Assert
            //Check Vehicles Details
            Assert.AreEqual(30500, D0.getTotDistance());
            Assert.AreEqual(19000, D1.getTotDistance());
            Assert.AreEqual(11000, D2.getTotDistance());

        }

        [TestMethod]
        public void Details_CheckRevenueInfo_AssertsEqualsTrue()
        {
            //Arrange 
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];


            //Act
            //Create Details
            Details D0 = new Details(vehicleData[0], serviceData, journeyData);
            Details D1 = new Details(vehicleData[1], serviceData, journeyData);
            Details D2 = new Details(vehicleData[2], serviceData, journeyData);


            //Assert
            //Check Vehicles Details
            Assert.AreEqual("300 (with 0 unpaid)", D0.getRevenueInfo());
            Assert.AreEqual("579 (with 0 unpaid)", D1.getRevenueInfo());
            Assert.AreEqual("731 (with 230 unpaid)", D2.getRevenueInfo());

        }


    }
}
