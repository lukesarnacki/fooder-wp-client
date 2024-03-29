﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.ComponentModel;
using Fooder.Models;
using System.Collections.ObjectModel;

namespace Fooder.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public ObservableCollection<Ingredient> Items { get; private set; }

        public void LoadIngredients(ObservableCollection<Ingredient> ingredients)
        {
            Items = ingredients;
        }

        public void Load(Product product)
        {
            Name = product.Name;
            Description = product.Description;
        }
    }
}
