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
    public partial class HelpPage1 : PhoneApplicationPage
    {
        public HelpPage1()
        {
            InitializeComponent();
        }

        private void btn_document_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Helpdocument.xaml", UriKind.Relative));
        }

        private void btn_video_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wtb = new WebBrowserTask();
            wtb.Uri = new Uri("http://www.youtube.com/watch?feature=player_embedded&v=2MfkRfuYePU", UriKind.Absolute);
            wtb.Show();
        }
    }
}