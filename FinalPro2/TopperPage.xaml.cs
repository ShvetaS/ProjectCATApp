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
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace FinalPro2
{
    public partial class TopperPage : PhoneApplicationPage
    {
        Topper topper;
        public TopperPage()
        {
            InitializeComponent();

            var app = App.Current as App;
            topper = app.selectedTopper;

            // show topper's details in page
            nameTextBlock.Text = topper.Name;
            percentileTextBlock.Text =topper.Percentile+"%";
            yearTextBlock.Text =topper.Year;

            //saysTextBlock.Text = topper.Say;

            //For displaying image

           /* BitmapImage image = new BitmapImage();
            string photo = topper.Photo;
            image.UriSource = new Uri(photo);
            photoOfTopper.Source = image;*/

        }

        private void webBrowserSay_Loaded(object sender, RoutedEventArgs e)
        {
            //Displays the details
            string say1 = topper.Say;
            webBrowserSay.NavigateToString(say1);
        }
    }
}