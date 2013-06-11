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
using Microsoft.Phone.Shell;

namespace FinalPro2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            StandardTileData SecTitle = new StandardTileData();
            SecTitle.Title = "CAT App";
            SecTitle.BackgroundImage = new Uri("/Images/CATapp.png", UriKind.RelativeOrAbsolute);
            //   SecTitle.Count = 70;
            var URINav = "/MainMenuPage.xaml?state=Sec Tile";
            ShellTile.Create(new Uri(URINav, UriKind.RelativeOrAbsolute), SecTitle);

        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}