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
    public partial class solyear : PhoneApplicationPage,INotifyPropertyChanged
    {
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public solyear()
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

            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            var q = (from Question qs1 in catAppDB.Questions
                     where qs1.T_id == l
                     select qs1).Count();
            txtque.Text = q.ToString();
        }
        public void displaytimeyr()
        {

            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            var q = from Test qs1 in catAppDB.Tests
                    where qs1._id == l
                    select qs1;
            Tests = new ObservableCollection<Test>(q);
         }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> x = this.NavigationContext.QueryString;
            String a = Convert.ToString(x["selectedValue"]);
            textBox1.Text = a.ToString();
            base.OnNavigatedTo(e);

        }

        private void txtque_Loaded(object sender, RoutedEventArgs e)
        {
            displaynoqueyr();
        }  

        private void btnsSol_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Solution.xaml?TextData=" + textBox1.Text, UriKind.Relative));

        }

        
    }
}