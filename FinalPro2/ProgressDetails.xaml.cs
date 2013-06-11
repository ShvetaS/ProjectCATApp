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
    public partial class ProgressDetails : PhoneApplicationPage, INotifyPropertyChanged
    {
        private CatAppDataClasses catAppDB;
        long a1, a2, a3;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";

        public ProgressDetails()
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





        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> x = this.NavigationContext.QueryString;
            String a = Convert.ToString(x["selectedValue"]);
            txtBox1.Text = a.ToString();
            base.OnNavigatedTo(e);

        }

        private void txtTotal_Loaded(object sender, RoutedEventArgs e)
        {
            string s = txtBox1.Text.ToString();
            long l;
            long.TryParse(s, out l);
            var total1 = (from Question que in catAppDB.Questions
                          where que.T_id == l
                          select que).Count();
            //s  Questions = new ObservableCollection<Question>(total1);
            txtTotal.Text = total1.ToString();

        }

        private void txtAttempted_Loaded(object sender, RoutedEventArgs e)
        {
            string s = txtBox1.Text.ToString();
            long l;
            long.TryParse(s, out l);
            var total = (from Attempt att in catAppDB.Attempts
                         join Attempt_detail atd in catAppDB.Attempt_details on att._id equals atd.A_id
                         where att.T_id == l
                         select atd).Count();
           // float tot;
            //float.TryParse(total.ToString(), out tot);
            txtAttempted.Text = total.ToString();

        }

        private void txtCorrect_Loaded(object sender, RoutedEventArgs e)
        {
            string s = txtBox1.Text.ToString();
            long l;
            long.TryParse(s, out l);
            var ss = (from Attempt_detail atd in catAppDB.Attempt_details
                      join Option op in catAppDB.Options on atd.O_id equals op._id
                      where atd.Q_id==op.Q_id && atd.O_id == op._id && op.Correct == true
                      select atd).Count();
            txtCorrect.Text = ss.ToString();






        }

        private void txtincorrect_Loaded(object sender, RoutedEventArgs e)
        {
            string attemp = txtAttempted.Text.ToString();
            long al;
            long.TryParse(attemp, out al);

            string correct = txtCorrect.Text.ToString();
            long cl;
            long.TryParse(correct, out cl);

            a1 = al - cl;
            a2 = cl;
            txtincorrect.Text = (al - cl).ToString();

        }

        private void txtnonattempted_Loaded(object sender, RoutedEventArgs e)
        {
            string attemp = txtAttempted.Text.ToString();
            long al;
            long.TryParse(attemp, out al);

            string total = txtTotal.Text.ToString();
            long tl;
            long.TryParse(total, out tl);
            a3 = tl;
            txtnonattempted.Text = (tl - al).ToString();
        }

        private void txtScore_Loaded(object sender, RoutedEventArgs e)
        {
            var sst = (a2 * 1) - (a1 * 0.25);
            txtScore.Text = ((sst / a3) * 100).ToString() + "%";

        }

    }
}