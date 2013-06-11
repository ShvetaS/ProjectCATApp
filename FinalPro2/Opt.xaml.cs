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
    public partial class Opt : PhoneApplicationPage,INotifyPropertyChanged
    {
        public string IsAnswerCorrect = null;
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public Opt()
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

        private ObservableCollection<Attempt> _attempts;
        public ObservableCollection<Attempt> Attempts
        {
            get
            {
                return _attempts;
            }
            set
            {
                if (_attempts != value)
                {
                    _attempts = value;
                    NotifyPropertyChanged("Attempts");
                }
            }
        }

        private void radioButton1_Click_1(object sender, RoutedEventArgs e)
        {
           
            RadioButton radio = sender as RadioButton;
            Boolean b = true;

            try
            {
                if (radio.Tag.ToString().Equals(b.ToString()))
                {
                    IsAnswerCorrect = "true";
                    radio.IsChecked = true;
                }
                else
                {
                    IsAnswerCorrect = "false";
                    //radio.IsChecked = false;
                }

                string cn = radio.Content.ToString();

                string id = txtquesID.Text.ToString();
                long qid;
                long.TryParse(id, out qid);
                var opp = (from Option opt in catAppDB.Options
                           where opt.Q_id == qid
                           select opt).Count();

                var opp1 = from Option opt in catAppDB.Options
                           where opt.Q_id == qid
                           select opt;

                var opt1list = opp1.ToList();

                for (int i = 0; i < opp; i++)
                {
                    var opt2 = opt1list[i] as Option;
                    string opt3 = opt2.Content.ToString();
                    if (opt3 == cn)
                    {
                        txtoptid.Text = opt2._id.ToString();
                    }

                }

                long optid;
                long.TryParse(txtoptid.Text, out optid);
                long atmid;
                long.TryParse(txtattemptid.Text, out atmid);

                using (catAppDB = new CatAppDataClasses(DBConnectionString))
                {
                    Attempt_detail newAttempt_detail = new Attempt_detail
                    {

                        Q_id = qid,
                        O_id = optid,

                        A_id = atmid,
                        Mark_flag = true
                    };

                    catAppDB.Attempt_details.InsertOnSubmit(newAttempt_detail);
                    catAppDB.SubmitChanges();

                }
            }//try
            catch
            {
                if (radio.Tag.ToString().Equals(b.ToString()))
                {
                    IsAnswerCorrect = "true";
                    radio.IsChecked = true;
                }
                else
                {
                    IsAnswerCorrect = "false";
                    //radio.IsChecked = false;
                }
            }
            
        }

        public IList<Attempt_detail> GetAttempt_detailList()
        {
            // Fetching data from local database
            IList<Attempt_detail> Attempt_detailList = null;
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                IQueryable<Attempt_detail> Attempt_detailQuery = from attempt1 in catAppDB.Attempt_details select attempt1;
                Attempt_detailList = Attempt_detailQuery.ToList();
            }
            return Attempt_detailList;
        }

        public IList<Option> GetOptionList()
        {
            // Fetching data from local database
            IList<Option> OptionList = null;
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                IQueryable<Option> OptionQuery = from option1 in catAppDB.Options select option1;
                OptionList = OptionQuery.ToList();
            }
            return OptionList;
        }
      

        private void optListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestType.xaml", UriKind.Relative));
        }

        private void attemptListbox_Loaded(object sender, RoutedEventArgs e)
        {

            var atcount = (from Attempt atttcount in catAppDB.Attempts
                           select atttcount).Count();
            var at = from Attempt attt in catAppDB.Attempts
                     select attt;
            var atm = at.ToList();
            for (int j = 0; j < atcount; j++)
            {
                var atm1 = atm[j] as Attempt;

                txtattemptid.Text = atm1._id.ToString();
            }
          
        }
    }
}