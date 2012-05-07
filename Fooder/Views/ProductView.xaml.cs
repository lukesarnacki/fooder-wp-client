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

namespace Fooder.Views
{
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
            DataContext = App.ProductsViewModelInstance;
        }

        private void FirstListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
