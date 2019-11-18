using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using Newtonsoft.Json;

namespace CarRentalSystem
{
    /// <summary>
    /// Interaction logic for ListCompanies.xaml
    /// </summary>
    public partial class ListCompanies : Window 
    {
        protected AddCompany addCompany;
        protected EditCompany editCompany;
        protected DeleteCompany deleteCompany;
        protected ArrayList companies;
        public ArrayList displayedCompanies { get; set; }

        public ListCompanies(ArrayList comp)
        {
            InitializeComponent();
            //String iconPath = System.AppDomain.CurrentDomain.BaseDirectory + "icon4.png";
            this.Title = "    List";
            //this.Icon = BitmapFrame.Create(new Uri(iconPath));
            
            //Get the (a copy of) company list from the dashboard class
            companies = comp;
            displayedCompanies = comp;

            //Data context for displayed list of companies
            this.DataContext = this;
        }

        private ArrayList copyArrayList(ArrayList AL)
        {
            ArrayList newAL = new ArrayList();
            for (int i = 0; i < AL.Count; i++) //Clone all values to the new list!
            {
                newAL.Add((Company) AL[i]);    
            }
            return newAL;       
        }

        private void beforeEffects()
        {
            this.VisualOpacity = 0.5;
            this.VisualEffect = new BlurEffect();
        }
        private void afterEffects()
        {
            this.VisualOpacity = 1;
            this.VisualEffect = null;
            //Update displayed list of companies!
           // displayedCompanies = companies; //update the displayed companies (remove any filter)
            performFilterAndRefresh(SearchTxt.Text); //perform the filter on the displayed companies!
        }
        private void AddCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            addCompany = new AddCompany();
            addCompany.ShowDialog();
            if (addCompany.getSaveState()) //If a company was saved!
            {
                Company c = addCompany.getCompany(); //Get the saved comopany
                companies.Add(c); //Add the company to the list of companies
                Console.WriteLine("Saved: "+c.getName());
            }
            afterEffects();
        }

        private void EditCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();
            //Get indicies to edit
            IList iL = DisplayLvw.SelectedItems; //selected items (companies)
            int[] selectInd = new int[iL.Count]; //Selected indicies array
            for (int i = 0; i < iL.Count; i++)
            {
                selectInd[i] = companies.IndexOf((Company)iL[i]); //Store all the indices for editing
            }
            //Run the edits
            for (int i = 0; i < selectInd.Length; i++) //Loop through the edits (because we can select more than one company to edit!)
            {
                editCompany = new EditCompany((Company)companies[(selectInd[i])]); //Send company to from list to be edited
                editCompany.ShowDialog();
                //Check if saved or not
                if (editCompany.getSaveState()) //If a company was saved!
                {
                    Company c = editCompany.getCompany(); //Get the saved comopany
                    companies[(selectInd[i])] = c; //Overide the company at the editted location!
                    Console.WriteLine("Saved: " + c.getName());
                }
            }
            
            afterEffects();
        }

        private void DeleteCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            beforeEffects();

            IList iL = DisplayLvw.SelectedItems; //selected items from list view (companies)
            int[] selectInd = new int[iL.Count]; //Selected indicies array
            for (int i = 0; i < iL.Count; i++)
            {
                selectInd[i] = companies.IndexOf((Company) iL[i]); //Store all the indices
            }

            deleteCompany = new DeleteCompany("Standard"); //Delete company verification using standard proceedure (message)
            deleteCompany.ShowDialog();

            if (deleteCompany.deletionVerified()) //Check that the user said Yes to the delete action!
            {
                //Removal of the selected companies
                for (int j = selectInd.Length - 1; j >= 0; j = j - 1) //start at the end (Length-1) and count backwards
                { //Loop through selected items
                    companies.RemoveAt(selectInd[j]);
                }
            }

            afterEffects();
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Perform the filter function using the search text
            performFilterAndRefresh(SearchTxt.Text);      
        }

        private void performFilterAndRefresh(String keyWord)
        {
            displayedCompanies = copyArrayList(companies); //Initialise displayed companies (before filter)

            if (keyWord.Length > 0) //Only if there is a keyword try to filter!
            {
                Company c; //Company var
                int i = 0; //Index var
                while (i < displayedCompanies.Count) //loop through companies to display
                {
                    c = (Company) displayedCompanies[i]; //get company
                    if (match(keyWord, c.getName())) //evaluate if there is a match with keyword
                    {
                        i++; //iterate past the match, do nothing
                    }
                    else
                    {
                        displayedCompanies.RemoveAt(i); //Remove the non match!
                    }
                }
            } 

            //Console.WriteLine(JsonConvert.SerializeObject(displayedCompanies)); //Check object in console

            //Manually reset binding and refresh -works! (note XAML binding only seemed to work on window creation!)
            DisplayLvw.SetBinding(ListView.ItemsSourceProperty,
                new Binding
                {
                    Path = new PropertyPath("displayedCompanies"),
                    NotifyOnTargetUpdated = true
                });
            DisplayLvw.Items.Refresh();  
        
        }

        private Boolean match(String child, String parent)
        {
            //Simple search using substring method in lowercase
            child = child.ToLower(); //lower case conversion
            parent = parent.ToLower();
            return parent.Contains(child); //Return contains boolean!
        }

        public ArrayList getCompanyList() {
            return copyArrayList(companies);
        }
    }
}
