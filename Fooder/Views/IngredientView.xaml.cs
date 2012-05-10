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

namespace Fooder.Views
{
    public partial class IngredientView : UserControl
    {
        public IngredientView()
        {
            InitializeComponent();
            DataContext = App.IngredientsViewModelInstance;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void IngredientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (IngredientsList.SelectedIndex == -1)
                return;

            // Navigate to the new page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/IngredientDetailsPage.xaml?selectedItem=" + IngredientsList.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            IngredientsList.SelectedIndex = -1;
        }
    }
}
