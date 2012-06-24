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
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Fooder.Models;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using System.Text.RegularExpressions;


using System.Xml.Linq;

namespace Fooder.Views
{
    public partial class IngredientView : UserControl
    {
        App thisApp = Application.Current as App;

        public IngredientView()
        {
            InitializeComponent();
            DataContext = App.IngredientsViewModelInstance;
        }


        private void IngredientView_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void IngredientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (IngredientsList.SelectedIndex == -1)
                return;

            thisApp.ActiveIngredient = IngredientsList.SelectedItem as Ingredient;

            // Navigate to the new page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/IngredientDetailsPage.xaml", UriKind.Relative));

            // Reset selected index to -1 (no selection)
            IngredientsList.SelectedIndex = -1;
        }

        private void IngredientsList_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void searchBox_Focus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = String.Empty;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            App.IngredientsViewModelInstance.Filter(searchBox.Text);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            App.IngredientsViewModelInstance.CancelFilter();
        }
    }
}
