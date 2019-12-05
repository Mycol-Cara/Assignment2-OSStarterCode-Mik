using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class UpdateSQL
    {

        public UpdateSQL()
        {
        }

        //Load All() Gets everything in the Sql
        public static Object[] LoadAll()
        {
            String conStr = "user id = cars; persistsecurityinfo = True; server = localhost; database = cars; password=Password1;";
            String p1, p2, p3, p4, p5, p6, p7;
            DataTable table;
            MySqlDataAdapter adapter;
            MySqlCommand cmd;

            try
            {
                //Create the connection
                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    con.Open(); //Open

                    //Vehicle data
                    List<Vehicle> vehicleData = new List<Vehicle>(); //fresco
                    table = new DataTable(); //new table
                    cmd = new MySqlCommand("SELECT * FROM `vehicles`", con); //get table command
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(table); //fill table
                    Vehicle v;
                    foreach (DataRow row in table.Rows) //go through table rows
                    {
                        Object[] arr = row.ItemArray; //get row as objects
                        v = new Vehicle((int)arr[1], arr[3].ToString(), arr[2].ToString(), (int)arr[4], (int)arr[5], arr[6].ToString(), (int)arr[7]); //new vehicle
                        vehicleData.Add(v);
                    }

                    //Service Data
                    List<Service> serviceData = new List<Service>();  //new list
                    table = new DataTable();  // new table
                    cmd = new MySqlCommand("SELECT * FROM `services`", con);  // get tabl;e command
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(table);  //fill the table
                    Service s;
                    foreach (DataRow row in table.Rows)   //going through table rows
                    {
                        Object[] arr = row.ItemArray;  // get row as obects
                        s = new Service((int)arr[1], (int)arr[2], (int)arr[3], (DateTime)arr[4], (DateTime)arr[5], (DateTime)arr[6]);
                        serviceData.Add(s);
                    }

                    //Journey Data
                    List<Journey> journeyData = new List<Journey>();
                    table = new DataTable();
                    cmd = new MySqlCommand("SELECT * FROM `journies`", con);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(table);  //fill the table
                    Journey j;
                    foreach (DataRow row in table.Rows)
                    {
                        Object[] arr = row.ItemArray;
                        Boolean paid = (((int)arr[6]) == 1); //convert to boolean
                        j = new Journey((int)arr[2], (DateTime)arr[3], (DateTime)arr[4], (DateTime)arr[5], (int)arr[1],paid);
                        journeyData.Add(j);
                    }

                    //Fuel Data
                    List<FuelPurchase> fuelData = new List<FuelPurchase>();
                    table = new DataTable();
                    cmd = new MySqlCommand("SELECT * FROM `fuelpurchases`", con);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(table);  //fill the table
                    FuelPurchase fp;
                    foreach (DataRow row in table.Rows)
                    {
                        Object[] arr = row.ItemArray;
                        fp = new FuelPurchase((int)arr[2], (double)arr[3], (DateTime)arr[4], (DateTime)arr[5], (int)arr[1]);
                        fuelData.Add(fp);
                    }

                    con.Close(); //Close
                    return new Object[] { vehicleData, serviceData, journeyData, fuelData };
                }
            } catch (Exception mse) { Console.WriteLine(mse.ToString()); Console.WriteLine("Error SQL"); return null; }
            
        }

        //ReplaceAll() Delets and adds all to the sql
        public static void ReplaceAll(List<Vehicle> vehicleData, List<Service> serviceData, List<Journey> journeyData, List<FuelPurchase> fuelData)
        {
            //Connection string
            String conStr = "user id = cars; persistsecurityinfo = True; server = localhost; database = cars; password=Password1;";
            String p1, p2, p3, p4, p5, p6, p7;
            //this.adminMode = vl.getAdminMode(); //get the admin mode, incase has disabled...

            try
            {
                //Create the connection
                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    con.Open(); //Open

                    //Vehicles
                    //Clear table to save over
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "TRUNCATE TABLE `vehicles`";
                    cmd.ExecuteNonQuery();

                    //Save data for each vehicle
                    foreach (Vehicle v in vehicleData)
                    {
                        p1 = v.getVehicleID().ToString(); p2 = v.getModel(); p3 = v.getManufacturer(); p4 = v.getMakeYear().ToString(); p5 = v.getOdometerReading().ToString(); p6 = v.getRegistrationNumber(); p7 = v.getTankCapacity().ToString();
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO `vehicles`(`vehicleid`,`model`,`manufacturer`,`makeyear`,`odometer`,`registration`,`tankcapacity`) VALUES(" + p1 + ", '" + p2 + "', '" + p3 + "', " + p4 + ", " + p5 + ", '" + p6 + "', " + p7 + ")";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }


                    //Services
                    cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "TRUNCATE TABLE `services`";
                    cmd.ExecuteNonQuery();

                    //Save data for each service
                    foreach (Service s in serviceData)
                    {
                        p1 = s.getVehicleID().ToString(); p2 = s.getLastServiceOdometerKm().ToString(); p3 = s.getServiceCount().ToString(); p4 = s.getLastServiceDate().ToString("yyyy-MM-dd H:mm:ss"); p5 = s.getDateCreated().ToString("yyyy-MM-dd H:mm:ss"); p6 = s.getDateUpdated().ToString("yyyy-MM-dd H:mm:ss");
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO `services`(`vehicleid`,`odometer`,`serviceCount`,`serviceDate`, `created`, `updated`) VALUES(" + p1 + ", " + p2 + ", " + p3 + ", '" + p4 + "', '" + p5 + "', '" + p6 + "')";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }


                    //Journies
                    cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "TRUNCATE TABLE `journies`";
                    cmd.ExecuteNonQuery();

                    //Save data for each journey
                    foreach (Journey j in journeyData)
                    {
                        p1 = j.getVehicleID().ToString(); p2 = j.getdistanceTravelled().ToString(); p3 = j.getjourneyAt().ToString("yyyy-MM-dd H:mm:ss"); p4 = j.getDatecreated().ToString("yyyy-MM-dd H:mm:ss"); p5 = j.getDateupdated().ToString("yyyy-MM-dd H:mm:ss"); 
                        if (j.isPaid()) { p6 = "1"; } else { p6 = "0"; }
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO `journies`(`vehicleid`,`distanceTravelled`,`journeyAt`, `created`, `updated`,`paid`) VALUES(" + p1 + ", " + p2 + ", '" + p3 + "', '" + p4 + "', '" + p5 + "','" + p6 +"')";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    //Fuel
                    cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "TRUNCATE TABLE `fuelpurchases`";
                    cmd.ExecuteNonQuery();

                    //Save data for each fuelpurchase
                    foreach (FuelPurchase fp in fuelData)
                    {
                        p1 = fp.getVehicleID().ToString(); p2 = fp.getAmount().ToString(); p3 = fp.getPrice().ToString(); p4 = fp.getDatecreated().ToString("yyyy-MM-dd H:mm:ss"); p5 = fp.getDateupdated().ToString("yyyy-MM-dd H:mm:ss");
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO `fuelpurchases`(`vehicleid`,`amount`,`price`, `created`, `updated`) VALUES(" + p1 + ", " + p2 + ", " + p3 + ",'" + p4 + "', '" + p5 + "')";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }



                    con.Close(); //Close
                }


            }
            catch (MySqlException mse) { Console.WriteLine(mse.ToString()); Console.WriteLine("Error SQL"); }

        }

        //TODO dyanmics
    }
}
