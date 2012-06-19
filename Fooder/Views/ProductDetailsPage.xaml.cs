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
using System.Windows.Navigation;
using Fooder.Models;

namespace Fooder.Views
{
    public partial class ProductDetailsPage : PhoneApplicationPage
    {
        ViewModels.ProductViewModel view = new ViewModels.ProductViewModel();
        App thisApp = Application.Current as App;

        public ProductDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e){
        
            view.Load(thisApp.ActiveProduct);
            DataContext = view;
            IngredientsList.DataContext = App.IngredientsViewModelInstance;
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
    }
}