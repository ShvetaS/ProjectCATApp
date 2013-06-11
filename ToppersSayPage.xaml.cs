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
using System.Xml.Serialization;
using System.Xml.Linq;

namespace FinalPro2
{
    public partial class ToppersSayPage : PhoneApplicationPage
    {
        public ToppersSayPage()
        {
            InitializeComponent();
            // is there network connection available
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No network connection available!");
                return;
            }
            // start loading XML-data
            WebClient downloader = new WebClient();
            Uri uri = new Uri("http://iwpublish.herokuapp.com/api/v1/ToppersSays.xml", UriKind.Absolute);
            downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(topsaydownloaded);
            downloader.DownloadStringAsync(uri);
        }
        void topsaydownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Result == null || e.Error != null)
            {
                MessageBox.Show("There was an error downloading the XML-file!");
            }
            else
            {
                // Deserialize if download succeeds
                XmlSerializer serializer = new XmlSerializer(typeof(Toppers));
                XDocument document = XDocument.Parse(e.Result);
                Toppers toppers = (Toppers)serializer.Deserialize(document.CreateReader());
                // bind data to ListBox

                var tops = from query in document.Descendants("topper")
                           select new Topper
                           {
                               Photo = (string)query.Element("photo"),
                               Name = (string)query.Element("name"),
                               Percentile = (string)query.Element("percentile"),
                               Year = (string)query.Element("year"),
                               Quote = (string)query.Element("quote"),
                               Say = (string)query.Element("say").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", "")

                           };

                toppersList.ItemsSource = tops;

            }
        }

        private void toppersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = App.Current as App;
            app.selectedTopper = (Topper)toppersList.SelectedItem;
            this.NavigationService.Navigate(new Uri("/TopperPage.xaml", UriKind.Relative));
        }
    }
}