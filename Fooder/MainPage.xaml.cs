﻿using System;
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

      /*  private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (IngredientsList.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            IngredientsList.SelectedIndex = -1;
        }*/

    }
}