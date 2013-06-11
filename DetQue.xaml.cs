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

namespace FinalPro2
{
    public partial class DetQue : PhoneApplicationPage
    {
        public DetQue()
        {
            InitializeComponent();
            que q1;


            //get selected news from App Class
            var app = App.Current as App;
            q1 = app.selectedQues;
            //  List1.ItemsSource = q1;

            // show news details in pages
            titleTextBlock.Text = q1.Instruction;
            pubdateTextBlock.Text = q1.Quetext;

            BitmapImage image = new BitmapImage();
            string photo = topper.Photo;
            image.UriSource = new Uri(photo);
            photoOfTopper.Source = image;

        }
    }
}