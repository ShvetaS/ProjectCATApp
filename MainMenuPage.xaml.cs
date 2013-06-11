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

namespace FinalPro2
{
    public partial class MainMenuPage : PhoneApplicationPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }
        private void btn_taketest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestType.xaml", UriKind.Relative));

        }
         private void PanoramaItem_MouseMove(object sender, MouseEventArgs e)
        {
            NavigationService.Navigate(new Uri("/QueOpt.xaml", UriKind.Relative));
        }

         private void CATNews_Click(object sender, RoutedEventArgs e)
         {
             NavigationService.Navigate(new Uri("/CATNewsPage.xaml", UriKind.Relative));
         }

         private void ToppersSay_Click(object sender, RoutedEventArgs e)
         {
             NavigationService.Navigate(new Uri("/ToppersSayPage.xaml", UriKind.Relative));
         }

         private void QuestionADay_Click(object sender, RoutedEventArgs e)
         {
             NavigationService.Navigate(new Uri("/QuestionADayPage.xaml", UriKind.Relative));
         }
    }
}