using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    /**
     * Starter code for Vehicle application. 
     * This class displays sample output to the console.
     * @author Michela Carandente
     */
    class VehicleStarter
    {

        /** Main entry point to the program
            * @param args the command line arguments
            */
        public static void main(String[] args)
        {
            Vehicle v = new Vehicle("Ford", "T812", 2014);

            // Vehicle sample distance
            v.addFuel(new Random().NextDouble() * 10, 1.3);

            v.printDetails();
            Console.WriteLine("\n\n");
        }

        
    }
}
