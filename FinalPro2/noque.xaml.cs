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
    public partial class noque : PhoneApplicationPage,INotifyPropertyChanged
    { 
        //Create instance of data context
        bool timed;
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public noque()
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
            //Display total number of questions available in selected test 

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

            //Display time if timed test is taken 

            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            var q = from Test qs1 in catAppDB.Tests
                    where qs1._id == l
                    select qs1;
            Tests = new ObservableCollection<Test>(q);
            var time = Tests.ToList();
            var t = time[0] as Test;

            var tt = (t.Allowed_time) / 60;
            txttime.Text = tt.ToString();
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

        private void txttime_Loaded(object sender, RoutedEventArgs e)
        {
            displaytimeyr();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //After making choice of timer start the actual test 
            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);
            var timest = from Test t in catAppDB.Tests
                         where t._id == l
                         select t;
            Tests = new ObservableCollection<Test>(timest);
            var time = Tests.ToList();
            var t2 = time[0] as Test;

            var tt = t2.Allowed_time;

          
            string tmpstp = tt.ToString();
            int l1;
            //long uid = 0;
            int.TryParse(tmpstp, out l1);

            long uid = 0;
            IList<User> UserList = this.GetUserList();
            foreach (User usr1 in UserList)
            {
                uid = usr1._id;
            }

            string g;
            long ttid, i;
            long.TryParse(txtaid.Text, out ttid);
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                Attempt newAttempt = new Attempt
                {
                    T_id = l,
                    Timpstamp = l1,
                    _id = ttid,
                     U_id = uid,
                    Timed = timed,
                    Complete = true,     
                    Elapsed_time=600

                };


                catAppDB.Attempts.InsertOnSubmit(newAttempt);

                catAppDB.SubmitChanges();
                i = newAttempt._id;
                g = i.ToString();
               
            }

            String abc = textBox1.Text;
            String pqr = textBox2.Text;
          

            NavigationService.Navigate(new Uri(string.Format("/ActualTest.xaml?TextData={0}&TextData1={1}", abc, pqr), UriKind.Relative));      

        }

        public IList<Attempt> GetAttemptList()
        {
            // Fetching data from local database
            IList<Attempt> AttemptList = null;
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                IQueryable<Attempt> AttemptQuery = from attempt1 in catAppDB.Attempts select attempt1;
                AttemptList = AttemptQuery.ToList();
            }
            return AttemptList;
        }
        public IList<User> GetUserList()
        {
            // Fetching data from local database
            IList<User> UserList = null;
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                IQueryable<User> UserQuery = from Us in catAppDB.Users select Us;
                UserList = UserQuery.ToList();
            }
            return UserList;
        }

        int count = 1;
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (count % 2 == 0)
            {
                button2.Content = "OFF";

                String b = Convert.ToString(0);
                textBox2.Text = b;
                timed = false;

            }
            else
            {
                button2.Content = "ON";

                String b = Convert.ToString(1);
                textBox2.Text = b;
                timed = true;

            }
            count++; ;


        }
    }
}