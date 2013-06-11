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
using System.Windows.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FinalPro2
{
    public partial class ProgressReport1 : PhoneApplicationPage, INotifyPropertyChanged
    {
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";

        public ProgressReport1()
        {
            InitializeComponent();
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

        private ObservableCollection<Test> _tests;
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

        private ObservableCollection<Attempt> _atts;
        public ObservableCollection<Attempt> Attempts
        {
            get
            {
                return _atts;
            }
            set
            {
                if (_atts != value)
                {
                    _atts = value;
                    NotifyPropertyChanged("Attempts");
                }
            }
        }

        private void testsListBox_Loaded(object sender, RoutedEventArgs e)
        {
            var q1 = (from Test ts in catAppDB.Tests
                      join Attempt at in catAppDB.Attempts on ts._id equals at.T_id
                      where ts._id == at.T_id
                      select ts).Count();

            var q = from Test ts in catAppDB.Tests
                    join Attempt at in catAppDB.Attempts on ts._id equals at.T_id
                    where ts._id == at.T_id
                    select ts;


            Tests = new ObservableCollection<Test>(q);


        }

        private void testsListBox_Tap(object sender, GestureEventArgs e)
        {

            string selectId = "";
            var selected = testsListBox.SelectedValue as Test;
            // selectedText = selected.Name;
            selectId = selected._id.ToString();
            NavigationService.Navigate(new Uri("/ProgressDetails.xaml?selectedValue=" + selectId, UriKind.Relative));


        }

    }


}
