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

namespace Fooder.Views
{
    public partial class IngredientDetailsPage : PhoneApplicationPage
    {
        ViewModels.IngredientViewModel view = new ViewModels.IngredientViewModel();

        public IngredientDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App thisApp = Application.Current as App;
            view.Load(thisApp.ActiveIngredient);
            DataContext = view;
        }
    }
}