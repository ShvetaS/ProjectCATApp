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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FinalPro2
{
    public partial class noquecat : PhoneApplicationPage,INotifyPropertyChanged
    {
        //instance of datacontext

        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public noquecat()
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



        private ObservableCollection<Question> _ques;
        public ObservableCollection<Question> Questions
        {
            get
            {
                return _ques;
            }
            set
            {
                if (_ques != value)
                {
                    _ques = value;
                    NotifyPropertyChanged("Questions");
                }
            }
        }

        private ObservableCollection<Option> _opts;
        public ObservableCollection<Option> Options
        {
            get
            {
                return _opts;
            }
            set
            {
                if (_opts != value)
                {
                    _opts = value;
                    NotifyPropertyChanged("Options");
                }
            }
        }
        private ObservableCollection<Category> _cats;
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

        private ObservableCollection<Question_category> _quecats;
        public ObservableCollection<Question_category> Question_categories
        {
            get
            {
                return _quecats;
            }
            set
            {
                if (_quecats != value)
                {
                    _quecats = value;
                    NotifyPropertyChanged("Question_categories");
                }
            }
        }
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

        public void displaynoqueyr()
        {

            string s = txtBox1.Text;

            long l;
            long.TryParse(s, out l);

            var q = (from Question_category qs1 in catAppDB.Question_categories
                     where qs1.C_id == l
                     select qs1).Count();
            txtque.Text = q.ToString();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Displaying the details and start the actual test
            IDictionary<string, string> x = this.NavigationContext.QueryString;
            String a = Convert.ToString(x["selectedValue"]);
            txtBox1.Text = a.ToString();
            base.OnNavigatedTo(e);

        }

        private void txtque_Loaded(object sender, RoutedEventArgs e)
        {
            displaynoqueyr();
        }

        private void btnstart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ActualTest.xaml?TextData=" + txtBox1.Text, UriKind.Relative));
        }
    }
}