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

namespace Fooder
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ProductsViewModelInstance.IsDataLoaded)
            {
                App.ProductsViewModelInstance.LoadData();
            }

            if (!App.IngredientsViewModelInstance.IsDataLoaded)
            {
                App.IngredientsViewModelInstance.LoadData();
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            PhoneApplicationFrame root = Application.Current.RootVisual as PhoneApplicationFrame;
            root.Navigate(new Uri("/Views/Camera.xaml", UriKind.Relative));
        }
    }
}