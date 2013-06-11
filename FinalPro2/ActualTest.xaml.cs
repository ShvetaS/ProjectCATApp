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
    public partial class ActualTest : PhoneApplicationPage,INotifyPropertyChanged
    {
        // Because we have not specified a namespace, this
        // will be a System.Windows.Forms.Timer instance

        // Whether or not the timer is currently running
        DispatcherTimer dt = new DispatcherTimer();
        DateTime startDt = new DateTime();
        TimeSpan ts = new TimeSpan();


        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public ActualTest()
        {
            InitializeComponent();
            // Connect to the database and instantiate data context.
            catAppDB = new CatAppDataClasses(DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            //Start the timer for timed test

            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dt.Tick += new EventHandler(dt_Tick);
            startDt = DateTime.Now;
            dt.Start();

        }
        void dt_Tick(object sender, EventArgs e)
        {

            ts = DateTime.Now.Subtract(startDt);
            txtHour.Text = ts.Hours.ToString();
            txtMin.Text = ts.Minutes.ToString();
            txtSec.Text = ts.Seconds.ToString();

            if (txtHour.Text.Length == 1) txtHour.Text = "0" + txtHour.Text;
            if (txtMin.Text.Length == 1) txtMin.Text = "0" + txtMin.Text;
            if (txtSec.Text.Length == 1) txtSec.Text = "0" + txtSec.Text;

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


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> x = this.NavigationContext.QueryString;
            String a = Convert.ToString(x["TextData"]);
            textBox1.Text = a.ToString();
            base.OnNavigatedTo(e);
            IDictionary<string, string> y = this.NavigationContext.QueryString;
            try
            {
                String b = Convert.ToString(y["TextData1"]);
                textBox2.Text = b.ToString();
                String timeonoff = b.ToString();
                long tmonoff;
                long.TryParse(timeonoff, out tmonoff);
                if (tmonoff == 0)
                {
                    txtHour.Visibility = Visibility.Collapsed;
                    txtMin.Visibility = Visibility.Collapsed;
                    txtSec.Visibility = Visibility.Collapsed;
                    textBlock1.Visibility = Visibility.Collapsed;
                    textBlock3.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                //Timer is not available 
                txtHour.Visibility = Visibility.Collapsed;
                txtMin.Visibility = Visibility.Collapsed;
                txtSec.Visibility = Visibility.Collapsed;
                textBlock1.Visibility = Visibility.Collapsed;
                textBlock3.Visibility = Visibility.Collapsed;
            }
            base.OnNavigatedTo(e);
           
        }

        //for displaying options and questions of specific category
        public void displaycatwise()
        {

            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            var q = (from Question qs1 in catAppDB.Questions
                     join Question_category qs2 in catAppDB.Question_categories on qs1._id equals qs2.Q_id
                     where qs2.C_id == l
                     select qs1).Count();
            var q1 = from Question qs1 in catAppDB.Questions
                     join Question_category qs2 in catAppDB.Question_categories on qs1._id equals qs2.Q_id
                     where qs2.C_id == l
                     select qs1;
            Questions = new ObservableCollection<Question>(q1);



            var v = Questions.ToList();

            int count = 1;

            //Generate pivot item for each question and add items to it 
            for (int i = 0; i < q; i++)
            {
                PivotItem pi = new PivotItem();
                pi.Header = string.Format("Q {0}", count);
                Opt puc = new Opt();
             
                puc.DataContext = v[i] as Question;
                pi.Content = puc;

                pivot1.Items.Add(pi);
                count++;
            }

        }

        //for displaying options and questions of specific selected question paper
        public void displayyearwise()
        {

            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            var q = (from Question qs1 in catAppDB.Questions
                     where qs1.T_id == l
                     select qs1).Count();
            var q1 = from Question qs1 in catAppDB.Questions
                     where qs1.T_id == l
                     select qs1;
            Questions = new ObservableCollection<Question>(q1);



            var v = Questions.ToList();

            int count = 1;
           
            for (int i = 0; i < q; i++)
            {
                PivotItem pi = new PivotItem();
                pi.Header = string.Format("Q {0}", count);
                Opt puc = new Opt();
             
                puc.DataContext = v[i] as Question;
                pi.Content = puc;

                pivot1.Items.Add(pi);


                count++;
           }

            
          }
        

        private void TestSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            string s = textBox1.Text;
            long l;
            long.TryParse(s, out l);

            pivot1.Visibility = Visibility.Collapsed;
            
            double correct = 0.00, incorrect = 0.00, unanswered = 0.00;
            var collection = pivot1.Items;

           
            for (int i = 0; i < collection.Count; i++)
            {
                if (((collection[i] as PivotItem).Content as Opt).IsAnswerCorrect == null)
                {
                    unanswered++;
                }
                else if (((collection[i] as PivotItem).Content as Opt).IsAnswerCorrect.Equals("true"))
                {
                    correct++;

                }
                else if (((collection[i] as PivotItem).Content as Opt).IsAnswerCorrect.Equals("false"))
                {

                    incorrect++;
                }

            }

            var correctscore = correct * 1;
            var incorrectscore = incorrect * 0.25;
            var score = correctscore - incorrectscore;


            txtCorrect.Text = string.Format("Correct = {0}", correct);
            txtInCorrect.Text = string.Format("Incorrect = {0}", incorrect);
            txtUnanswered.Text = string.Format("Unanswered = {0}", unanswered);
            txtScore.Text = string.Format("Score={0}", score);
           
            MessageGrid.Visibility = Visibility.Visible;
           
            submit.Visibility = Visibility.Collapsed;
            txtHour.Visibility = Visibility.Collapsed;
            txtMin.Visibility = Visibility.Collapsed;
            txtSec.Visibility = Visibility.Collapsed;
            textBlock1.Visibility = Visibility.Collapsed;
            textBlock3.Visibility = Visibility.Collapsed;
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {

            DialogClosed("OK");
        }

        private void DialogClosed(string ClosedBy)
        {
            txtCorrect.Text = string.Empty;
            txtInCorrect.Text = string.Empty;
            txtUnanswered.Text = string.Empty;

            if (ClosedBy.Equals("OK"))
            {
                //Submit
                MessageBox.Show("Submitted successfully");
                NavigationService.Navigate(new Uri("/StartPage1.xaml", UriKind.Relative));
                
            }
            else
            {
                //Cancel
            }
        }


        private void catwise_Loaded(object sender, RoutedEventArgs e)
        {
            displaycatwise();
        }



        private void yearwise_Loaded(object sender, RoutedEventArgs e)
        {
            noque nq = new noque();
            Opt op = new Opt();
            string st = nq.button2.Content.ToString();
            string st2 = "ON";
            if (st.Equals(st2))
            {

                displayyearwise();

            }


            else if (!(st.Equals(st2)))
            {
                displayyearwise();

            }

        }

       
    }
}