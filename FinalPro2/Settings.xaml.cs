using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace FinalPro2
{
    public partial class Settings : PhoneApplicationPage,INotifyPropertyChanged
    {
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public Settings()
        {
            InitializeComponent();
            // Connect to the database and instantiate data context.
            catAppDB = new CatAppDataClasses(DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private ObservableCollection<User> _user;
        public ObservableCollection<User> Users
        {
            get
            {
                return _user;
            }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    NotifyPropertyChanged("Users");
                }
            }
        }





        private void listBox1_Loaded(object sender, RoutedEventArgs e)
        {
            var q1 = from User us1 in catAppDB.Users
                     select us1;
            Users = new ObservableCollection<User>(q1);

            var us = Users.ToList();
            var p1 = us[0] as User;

            txtName.Text = p1.Fullname;
            txtPhNo.Text = p1.Phoneno;
            txtEmail.Text = p1.Email;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var q1 = from User us1 in catAppDB.Users
                     select us1;
            Users = new ObservableCollection<User>(q1);

            var us = Users.ToList();
            foreach (User use in us)
            {


                if (txtName.Text == "")
                {
                    MessageBox.Show("Name should not be empty !!");
                    txtName.Focus();
                }
                else
                {
                    use.Fullname = txtName.Text;
                }

                Regex validator = new Regex("^[3-9]{1}[0-9]{9}$");
                string match = validator.Match(txtPhNo.Text).Value.ToString();
                if (txtPhNo.Text != "")
                {
                    if (match.Length == 10)
                    {
                        //MessageBox.Show("Phone number is valid");
                        use.Phoneno = txtPhNo.Text;
                        catAppDB.SubmitChanges();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Phone Number !!");
                        txtPhNo.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Phone number cant be empty !!");
                    txtPhNo.Focus();
                }
                
                use.Email = txtEmail.Text;
            }

            try
            {

                catAppDB.SubmitChanges();

            }
            catch (Exception e1)
            {
                Console.WriteLine(e1);
                // Provide for exceptions.
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StartPage1.xaml",UriKind.Relative));
        }

        private void txtPhNo_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}