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
using Microsoft.Phone.Tasks;

namespace FinalPro2
{
    public partial class StartPage1 : PhoneApplicationPage
    {
        //Constructor
        public StartPage1()
        {
            InitializeComponent();
        }
        private void btn_taketest_Click(object sender, RoutedEventArgs e)
        {
            //Navigation to take a test page
            NavigationService.Navigate(new Uri("/TestType.xaml", UriKind.Relative));

        }
        private void PanoramaItem_MouseMove(object sender, MouseEventArgs e)
        {
            //Navigation to QueOpt page
            NavigationService.Navigate(new Uri("/QueOpt.xaml", UriKind.Relative));
        }

        private void CATNews_Click(object sender, RoutedEventArgs e)
        {
            //Navigation to catnews page
            NavigationService.Navigate(new Uri("/CATNewsPage.xaml", UriKind.Relative));
        }

        private void ToppersSay_Click(object sender, RoutedEventArgs e)
        {
            //Navigation to toppersay page
            NavigationService.Navigate(new Uri("/ToppersSayPage.xaml", UriKind.Relative));
        }

        private void QuestionADay_Click(object sender, RoutedEventArgs e)
        {
            //Navigation to questionaday page
            NavigationService.Navigate(new Uri("/QuestionADayPage.xaml", UriKind.Relative));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //Divert to catapp link
            WebBrowserTask wtb = new WebBrowserTask();
            wtb.Uri = new Uri("http://catapp.in/", UriKind.Absolute);
            wtb.Show(); 
             
        }

        private void btn_likeus_Click(object sender, RoutedEventArgs e)
        {
            //Open the facebook page of catapp
            WebBrowserTask wtb = new WebBrowserTask();
            wtb.Uri = new Uri("https://www.facebook.com/CATappMobile?fref=ts", UriKind.Absolute);
            wtb.Show(); 
        }

        private void btn_help_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpPage1.xaml", UriKind.Relative));
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml",UriKind.Relative));
        }

        private void btn_solution_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestType1.xaml", UriKind.Relative));
        }

        private void btn_progress_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ProgressReport1.xaml",UriKind.Relative));
        }

       
    }
}