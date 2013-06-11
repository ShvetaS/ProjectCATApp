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
    public partial class Detailnews : PhoneApplicationPage
    {
        Article article;
        public Detailnews()
        {
            InitializeComponent();
            //get selected news from App Class
            var app = App.Current as App;
            article = app.selectedNews;

            // show news details in page
            PageTitle.Text = article.Title;
            string desc = article.Description1;
            webBrowserDesc1.NavigateToString(desc);
        }   
       
    }
}