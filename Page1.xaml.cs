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

namespace QueDay1
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            que q1;
         
          
            //get selected news from App Class
            var app = App.Current as App;
            q1 = app.selectedQues;
          //  List1.ItemsSource = q1;

            // show news details in pages
            titleTextBlock.Text = q1.Instruction;
            pubdateTextBlock.Text= q1.Quetext;

        }    

        }
    }
