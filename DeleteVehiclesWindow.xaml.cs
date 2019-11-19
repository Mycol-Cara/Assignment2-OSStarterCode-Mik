using System;
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
    /// Interaction logic for DeleteVehiclesWindow.xaml
    /// </summary>
    public partial class DeleteVehiclesWindow : Window
    {
      
        protected Boolean deleteState;
        public DeleteVehiclesWindow(String option)
        {
            InitializeComponent();
            //String iconPath = System.AppDomain.CurrentDomain.BaseDirectory + "icon4.png";
            this.Title = "    Warning";
            //this.Icon = BitmapFrame.Create(new Uri(iconPath));
            deleteState = false; //Assume they will chicken out of the delete!
            if (option == "All") //Case where all items are being cleared, the message is changed
            {
                this.MessageLbl.Content = "Are you sure that you would like to\nclear all data!";
            }
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            //Verifiy the deletion with the delete state variable
            deleteState = true;
            this.Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Boolean deletionVerified() //Returns the true or false for whether or not to delete!
        {
            return deleteState;
        }
    
    }
}
