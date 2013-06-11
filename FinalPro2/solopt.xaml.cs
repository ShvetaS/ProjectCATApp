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
    public partial class solopt : PhoneApplicationPage,INotifyPropertyChanged
    {
        // Data context for the local database
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public string IsAnswerCorrect = null;

        public solopt()
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

        private void radioButton1_Click_1(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            // RadioButton rad = new RadioButton();
            Boolean b = true;

            if (radio.Tag.Equals(b))
            {
                radio.IsEnabled = true;
                IsAnswerCorrect = "true";

            }
            else
            {
                radio.IsEnabled = false;
                IsAnswerCorrect = "false";
            }


        }

        private void btnsol_Click(object sender, RoutedEventArgs e)
        {
            txtcontent.Visibility = Visibility.Collapsed;
            txtheader.Visibility = Visibility.Collapsed;
            quesID.Visibility = Visibility.Collapsed;
            optListBox.Visibility = Visibility.Collapsed;
            btnback.Visibility = Visibility.Collapsed;

            txtsol.Visibility = Visibility.Visible;
            imgsol.Visibility = Visibility.Visible;
           
            string s = quesID.Text.ToString();
            long l,m;
            long.TryParse(s, out l);
            m = l;
            var s1 = from Question ss1 in catAppDB.Questions
                     where ss1._id == l
                     select ss1;
            Questions = new ObservableCollection<Question>(s1);

            btnsol.Visibility = Visibility.Collapsed;
            image1.Visibility = Visibility.Collapsed;
            btnback.Visibility = Visibility.Visible;
            
          
        }

      

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            txtsol.Visibility = Visibility.Collapsed;
            imgsol.Visibility = Visibility.Collapsed;

            txtcontent.Visibility = Visibility.Visible;
            txtheader.Visibility = Visibility.Visible;
            quesID.Visibility = Visibility.Collapsed;
            optListBox.Visibility = Visibility.Visible;
         
            btnsol.Visibility = Visibility.Visible;
            image1.Visibility = Visibility.Visible;
            btnback.Visibility = Visibility.Collapsed;
        }

       

    }
}