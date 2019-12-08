using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class Rental
    {
        private Vehicle v;
        private List<Journey> allJournies; //All the current Journies in the database
        private List<Service> allServices; //All the current Services in the database
        private List<Journey> unpaidJournies; //All the current unpaid Journies
        private Journey temporaryJourney; //A temporary journey to pay

        public Rental(Vehicle v, List<Journey> allJournies, List<Service> allServices)
        {
            this.v = v; //Current vehicle
            unpaidJournies = Journey.FindUnpaidJournies(allJournies, v.getVehicleID()); //The unpaid journies for the vehicle being displayed in the rentalwindow.xaml
            this.allJournies = allJournies; //All the journey data (for rent to be paid or to be add new journey to)
            this.allServices = allServices;
        }

        //Check if the vehicle v can be used (rented) for a journey J
        /*
         * returns Boolean[] for each rental condition: Date taken, service required, car doesn't exist yet (earlier than makeYear), Journey too long.
         */
        public Boolean[] isJourneyRentable(Journey J)
        {
            Boolean[] rentable = new bool[] {true,true,true,true}; //Can rent for all reasons

            if (Journey.JourneyDateExists(allJournies, J) == true) 
            { rentable[0] = false;  } //Check if journey of same date exists in database!

            if (Service.DistanceSinceServiceKm(Service.FindServices(allServices, v.getVehicleID()), v) >= Service.SERVICE_KILOMETER_LIMIT) 
            { rentable[1] = false; } //Needs service

            if (J.getjourneyAt().Year - v.getMakeYear() < 0) 
            { rentable[2] = false;  } //Car doesn't exist yet!

            if (J.getdistanceTravelled() > Journey.JOURNEY_RENT_KM_MAX_LIMIT || J.getdistanceTravelled() <= Journey.JOURNEY_RENT_KM_MIN_LIMIT) 
            { rentable[3] = false; } //Journey too long!

            Console.WriteLine("Rentability: " + rentable[0] + ", "+rentable[1] + ", "+rentable[2] + ", "+rentable[3]);
            return rentable;
        }

        //Add temporary rental and if not paid (after checking isJourneyRentable) for remove later
        public void addTemporaryRental(Journey J)
        {
            temporaryJourney = J; //add to list as not paid waiting payment (or remove from list)
            unpaidJournies.Add(temporaryJourney);   //  Add to the list
            allJournies.Add(temporaryJourney);      // Add to all journies
        }

        //Process payment a (Check if temporaryRental/Journey is in list of items (to see if being pay for) else remove
        public void processRental(IList iL)
        {
            Boolean tempJourneySelected = false; //Status for whether the added journey is paid for!
            for (int i = 0; i < iL.Count; i++) //Loop through selected objects (Journies)
            {
                if ((Journey)iL[i] == temporaryJourney) { tempJourneySelected = true; } //user added journey is logged
                allJournies[allJournies.IndexOf((Journey)iL[i])].setPaid(true); //Find the journey in all journies and pay it (to be replaced by mySQL layer)
            }
            if (tempJourneySelected == false) { allJournies.Remove(temporaryJourney); } //Remove if not paid for
        }

        //Returns rental cost of selection
        public String computeRentalCost(IList iL)
        {
            Console.WriteLine("Computing Cost");
            double cost = 0; //initial cost
            for (int i = 0; i < iL.Count; i++) //Loop through selected objects (Journies)
            {
                cost = cost + ((Journey)iL[i]).JourneyCost(); //Add this cost
            }
            return "$" + cost + " will be automatically deducted from your user account";
        }

        //Returns unpaid journeys to agree with binding in rentalwindow.xaml (arraylist)
        public ArrayList getUnpaidJourneys()
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < unpaidJournies.Count; i++) //Clone all values to the new list!
            {
                newAL.Add(unpaidJournies[i]);
            }
            return newAL;
        }

        //Get all journeys
        public List<Journey> getAllJournies()
        {
            return this.allJournies;
        }

        public Vehicle getVehicle()
        {
            return this.v;
        }
    }
}
