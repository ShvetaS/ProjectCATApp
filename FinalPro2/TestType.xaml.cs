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

namespace FinalPro2
{
    public partial class TestType : PhoneApplicationPage,INotifyPropertyChanged
    {
        // Data context for the local database
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public TestType()
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

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<Test> _tests;
        private ObservableCollection<Category> _cats;

        public ObservableCollection<Test> Tests
        {
            get
            {
                return _tests;
            }
            set
            {
                if (_tests != value)
                {
                    _tests = value;
                    NotifyPropertyChanged("Tests");
                }
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return _cats;
            }
            set
            {
                if (_cats != value)
                {
                    _cats = value;
                    NotifyPropertyChanged("Categories");
                }
            }
        }

        private void testsListBox_Loaded(object sender, RoutedEventArgs e)
        {
            var testsInDB = from Test test in catAppDB.Tests
                            select test;

            // Execute the query and place the results into a collection.
            Tests = new ObservableCollection<Test>(testsInDB);

        }

        private void catListBox_Loaded(object sender, RoutedEventArgs e)
        {
            //Disolay the categories of questions available

            var catsInDB = from Category cat in catAppDB.Categories
                           select cat;

            // Execute the query and place the results into a collection.
            Categories = new ObservableCollection<Category>(catsInDB);

        }

        private void catListBox_Tap(object sender, GestureEventArgs e)
        {
            //Display the number of questions that are there in selected categrory and test details 
            string selectId = "";
            var selected = catListBox.SelectedValue as Category;
            selectId = selected._id.ToString();
            NavigationService.Navigate(new Uri("/noquecat.xaml?selectedValue=" + selectId, UriKind.Relative));


        }

        private void testsListBox_Tap(object sender, GestureEventArgs e)
        {
            //Display the number of questions that are there in selected question paper and test details 
            string selectId = "";
            var selected = testsListBox.SelectedValue as Test;
             selectId = selected._id.ToString();
            NavigationService.Navigate(new Uri("/noque.xaml?selectedValue=" + selectId, UriKind.Relative));

        }
    }
}