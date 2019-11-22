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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private Boolean result;
       

        public LoginForm()
        {
            InitializeComponent();
            result = false;

        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTxt.Text == "Admin1" && PasswordTxt.Password == "Password1")
            {
                result = true;
                this.Close();
            } else
            {
                MessageTxt.Content = "Incorrect username or password";
            }
        }

      
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Boolean getResult()
        {
            return this.result;
        }

        private void LoginBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                LoginBtn_Click(this, new RoutedEventArgs());
               
            }
        }
    }
}
