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
    public partial class Que : PhoneApplicationPage
    {
        public Que()
        {
            InitializeComponent();
             InitializeComponent();
            // is there network connection available
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No network connection available!");
                return;
            }
            // start loading XML-data
            WebClient downloader = new WebClient();
            Uri uri = new Uri("http://iwpublish.herokuapp.com/api/v1/QadQuestions.xml", UriKind.Absolute);
            downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(QueDownloaded);
            downloader.DownloadStringAsync(uri);
        }
        void QueDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Result == null || e.Error != null)
            {
                MessageBox.Show("There was an error downloading the XML-file!");
            }
            else
            {
                // Deserialize if download succeeds
                XmlSerializer serializer = new XmlSerializer(typeof(ques));
                XDocument document = XDocument.Parse(e.Result);
                var ques1 = from query in document.Descendants("question")
                           select new que
                           {
                               Quedate = (string)query.Element("quedate"),
                               Quename = (string)query.Element("quename"),//.Value.ToString().Replace("<![CDATA[", "").Replace("]]>", "").Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "").Replace("<p style=", "").Replace("text-align: justify; ", "").Replace(">", "").Replace(@"""", "").Replace(@"<>", "").Substring(0, 20) + "...",
                               Instruction = (query.Element("instruction")==null) ? "" : (string)query.Element("instruction").Value.ToString(),//.Value.ToString().Replace("<![CDATA[", "").Replace("]]>", "").Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "").Replace("<p style=", "").Replace("text-align: justify; ", "").Replace(">", "").Replace(@"""", ""),
                               Quetext = (query.Element("quetext")==null)? "" :(string)query.Element("quetext").Value.ToString().Replace("<br />",""),
                               Hint = (query.Element("hint") == null) ? "" : (string)query.Element("hint").Value.ToString().Replace("<br />",""),
                               Solution = (query.Element("solution") == null) ? "" : (string)query.Element("solution").Value.ToString().Replace("<br />",""),
                                Photo = (string)query.Element("photo")
                             //  Option = (string)query.Element("option")
                               //Title = (query.Element("title") == null) ? "" : (string)query.Element("title").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", ""),

                               //Pubdate = (query.Element("pubdate") == null) ? "" : (string)query.Element("pubdate").Value.ToString()
                           };
                newsList.ItemsSource = ques1;
            }
        }

        private void queList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var app = App.Current as App;
                app.selectedQues = (que)e.AddedItems[0];
                var targetpage = new Uri("/DetQue.xaml", UriKind.Relative);
                //reset selection of Listbox
                ((ListBox)sender).SelectedIndex = -1;
                //Change page navigation
                NavigationService.Navigate(targetpage);
                FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
                root.DataContext = app.selectedQues;
            }

        }
        }
    }
