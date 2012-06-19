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
using Fooder.Models;

namespace Fooder.Views
{
    public partial class ProductView : UserControl
    {
        App thisApp = Application.Current as App;

        public ProductView()
        {
            InitializeComponent();
            DataContext = App.ProductsViewModelInstance;
        }

        private void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (ProductsList.SelectedIndex == -1)
                return;

            thisApp.ActiveProduct = ProductsList.SelectedItem as Product;

            // Navigate to the new page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/ProductDetailsPage.xaml", UriKind.Relative));

            // Reset selected index to -1 (no selection)
            ProductsList.SelectedIndex = -1;
        }
    }
}
