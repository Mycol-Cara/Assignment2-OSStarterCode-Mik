﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for AddVehiclesWindow.xaml
    /// </summary>
    public partial class AddVehiclesWindow : Window
    {
    
        private Vehicle V; //The vehicle to be created
        protected Boolean saveState; //was it saved on exit
        protected Boolean validVehicle; //vehicle is valid
  
        public AddVehiclesWindow()
        {       
            InitializeComponent();
            this.Title = "    New Vehicle";
            this.V = new Vehicle("Ford", "T812", 2014, 0,"AAA123",60); //Default vehicle!
            saveState = false;           //when window constructed has not been saved yet
            validVehicle = true;
        }
        
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            saveState = true; //set vehicle to saved before exit!
            UpdateVehicle(); //Update the vehicle
            this.Close(); //CLOSE WINDOW!
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //CLOSE WINDOW!
        }

        private void UpdateVehicle()
        {
            
            //Read numeric values
            int year = V.getMakeYear();
            float odom = V.getOdometerReading();
            float capacity = V.getTankCapacity();
            try
            {
                year = System.Convert.ToInt32(MakeYearTxt.Text); //Change text entry to integer!
                odom = (float)System.Convert.ToDecimal(OdometerReadingTxt.Text); //Change text entry to float!
                capacity = (float)System.Convert.ToDecimal(TankCapacityTxt.Text); //Change text entry to float!
            } catch { 
                Console.WriteLine("Error reading numeric values!");
                validVehicle = false; //Due to likely poor data entry
                return; 
            }

            //TODO additional check to ensure vehicle is valid! I.e. belongs to a set list of models, make years and possible registration numbers!!!

            //THIS METHOD UPDATES THE VEHICLE CLASS WITH ALL OF THE TEXT CONTENT!
            V.setMakeYear(year);
            V.setOdometerReading(odom);
            V.setTankCapacity(capacity);
            V.setManufacturer(ManufacturerTxt.Text);
            V.setModel(ModelTxt.Text);
            V.setRegistrationNumber(RegistrationTxt.Text);
            validVehicle = true;
        }

        public Vehicle getVehicle()
        {
            return this.V;
        }
        public Boolean getSaveState()
        {
            return saveState;
        }

        public Boolean getValidity()
        {
            return validVehicle;
        }
    }
}
