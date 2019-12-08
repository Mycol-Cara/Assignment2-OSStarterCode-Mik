using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRentalSystem.UnitTests
{
    [TestClass]
    public class RentalTests
    {

        //Test for if the car has a journey at the same day
        [TestMethod]
        public void Rental_isJourneyRentableDateTaken_AssertEquals()
        {
            //Arrange
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];

            //Act 
            //Create Rentals
            Rental R0 = new Rental(vehicleData[0], journeyData, serviceData);
            Rental R1 = new Rental(vehicleData[1], journeyData, serviceData);
            Rental R2 = new Rental(vehicleData[2], journeyData, serviceData);
            //Create Journey for each car 
            Journey J0 = new Journey(600, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R0.getVehicle().getVehicleID(), false);
            Journey J1 = new Journey(400, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R1.getVehicle().getVehicleID(), false);
            Journey J2 = new Journey(-100, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R2.getVehicle().getVehicleID(), false);
            //Get rentability
            Boolean[] rentable0 = R0.isJourneyRentable(J0);
            Boolean[] rentable1 = R1.isJourneyRentable(J1);
            Boolean[] rentable2 = R2.isJourneyRentable(J2);

            //Assert
            //Check if date not taken (rentable[0] = true)
            Assert.AreEqual(true, rentable0[0]); //No journey on this day
            Assert.AreEqual(false, rentable1[0]); //Journey already on this day
            Assert.AreEqual(true, rentable2[0]); //No journey on this day

        }

        //Checks if car needs service
        [TestMethod]
        public void Rental_isJourneyRentableNeedsService_AssertEquals()
        {
            //Arrange
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];

            //Act 
            //Create Rentals
            Rental R0 = new Rental(vehicleData[0], journeyData, serviceData);
            Rental R1 = new Rental(vehicleData[1], journeyData, serviceData);
            Rental R2 = new Rental(vehicleData[2], journeyData, serviceData);
            //Create Journey for each car
            Journey J0 = new Journey(600, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R0.getVehicle().getVehicleID(), false);
            Journey J1 = new Journey(400, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R1.getVehicle().getVehicleID(), false);
            Journey J2 = new Journey(-100, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R2.getVehicle().getVehicleID(), false);
            //Get rentability
            Boolean[] rentable0 = R0.isJourneyRentable(J0);
            Boolean[] rentable1 = R1.isJourneyRentable(J1);
            Boolean[] rentable2 = R2.isJourneyRentable(J2);

            //Assert
            //Check if doesnt need service (rentable[1] = true)
            Assert.AreEqual(true, rentable0[1]); //Good
            Assert.AreEqual(false, rentable1[1]); //Needs a service! 
            Assert.AreEqual(true, rentable2[1]); //Good

        }

        //Checks if journey is before makeYear
        [TestMethod]
        public void Rental_isJourneyRentableCheckMakeYear_AssertEquals()
        {
            //Arrange
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];

            //Act 
            //Create Rentals
            Rental R0 = new Rental(vehicleData[0], journeyData, serviceData);
            Rental R1 = new Rental(vehicleData[1], journeyData, serviceData);
            Rental R2 = new Rental(vehicleData[2], journeyData, serviceData);
            //Create Journey for each car
            Journey J0 = new Journey(600, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R0.getVehicle().getVehicleID(), false);
            Journey J1 = new Journey(400, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R1.getVehicle().getVehicleID(), false);
            Journey J2 = new Journey(-100, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R2.getVehicle().getVehicleID(), false);
            //Get rentability
            Boolean[] rentable0 = R0.isJourneyRentable(J0);
            Boolean[] rentable1 = R1.isJourneyRentable(J1);
            Boolean[] rentable2 = R2.isJourneyRentable(J2);

            //Assert
            //Check if after or before make year (after make year, rentable[2] = true)
            Assert.AreEqual(true, rentable0[2]); //Journey is after make year, car is exists
            Assert.AreEqual(true, rentable1[2]); //Journey is after make year, car is exists  
            Assert.AreEqual(false, rentable2[2]); //Journey is before make year, Can't rent

        }

        //Check if Journey is of valid length, e.g. too long (TODO account for negative numbers!)
        [TestMethod]
        public void Rental_isJourneyRentableInvalidLength_AssertEquals()
        {
            //Arrange
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here
            List<Service> serviceData = (List<Service>)data[1];
            List<Journey> journeyData = (List<Journey>)data[2];

            //Act 
            //Create Rentals
            Rental R0 = new Rental(vehicleData[0], journeyData, serviceData);
            Rental R1 = new Rental(vehicleData[1], journeyData, serviceData);
            Rental R2 = new Rental(vehicleData[2], journeyData, serviceData);
            //Create Journey for each car
            Journey J0 = new Journey(600, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R0.getVehicle().getVehicleID(), false);
            Journey J1 = new Journey(400, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R1.getVehicle().getVehicleID(), false);
            Journey J2 = new Journey(-100, new DateTime(2018, 4, 3), DateTime.Now, DateTime.Now, R2.getVehicle().getVehicleID(), false);
            //Get rentability
            Boolean[] rentable0 = R0.isJourneyRentable(J0);
            Boolean[] rentable1 = R1.isJourneyRentable(J1);
            Boolean[] rentable2 = R2.isJourneyRentable(J2);

            //Assert
            //Check if journey length is okay (rentable[3] = true)
            Assert.AreEqual(false, rentable0[3]); //Longer than journey limit (500) can't rent
            Assert.AreEqual(true, rentable1[3]); //Less than journey limit, can rent   
            Assert.AreEqual(false, rentable2[3]); //negativ number, can't rent 

        }

    }
}
