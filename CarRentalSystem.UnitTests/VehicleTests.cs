using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CarRentalSystem.UnitTests
{
    [TestClass]
    public class VehicleTests
    {
        [TestMethod]
        public void Vehicle_isRegistrationValid_AssertsEquals()
        {
            //Arrange
            //Get data fom SQL
            Object[] data = SQLConnector.LoadAll("user id = cars; persistsecurityinfo = True; server = localhost; database = cars_test; password=Password1;");
            List<Vehicle> vehicleData = (List<Vehicle>)data[0]; //put list from object in here

            //Act 
            //Create Vehicles
            Vehicle V0 = vehicleData[0];
            Vehicle V1 = vehicleData[1];
            Vehicle V2 = vehicleData[2];

            //Assert
            //Check vehicle details
            Assert.AreEqual(true, V0.isRegistrationValid());
            Assert.AreEqual(false, V1.isRegistrationValid());
            Assert.AreEqual(false, V2.isRegistrationValid());

        }
    }

}