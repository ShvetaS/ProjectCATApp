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
using System.Xml.Linq;
using System.Xml.Serialization;


namespace FinalPro2
{
    public partial class QueOpt : PhoneApplicationPage
    {
        que q1;

        public QueOpt()
        {
            InitializeComponent();
           
            //get selected news from App Class
            var app = App.Current as App;
            q1 = app.selectedQues;
            //  List1.ItemsSource = q1;

            // show news details in page
            InstTextBlock.Text = q1.Instruction;
            QueTextBlock.Text = q1.Quetext;
            HintTextBlock.Text = q1.Hint;
            SolTextBlock.Text = q1.Solution;

        }
    }
}