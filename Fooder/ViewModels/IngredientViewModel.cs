using System;
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

namespace Fooder.ViewModels
{
    public class IngredientViewModel : ViewModelBase
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

        private string _productsType;
        public string ProductsType
        {
            get
            {
                return _productsType;
            }
            set
            {
                _productsType = value;
                NotifyPropertyChanged("ProductsType");
            }
        }

        private string _origin;
        public string Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
                NotifyPropertyChanged("Origin");
            }
        }

        private string _dietaryRestrictions;
        public string DietaryRestrictions
        {
            get
            {
                return _dietaryRestrictions;
            }
            set
            {
                _dietaryRestrictions = value;
                NotifyPropertyChanged("DietaryRestrictions");
            }
        }

        private string _dailyIntake;
        public string DailyIntake
        {
            get
            {
                return _dailyIntake;
            }
            set
            {
                _dailyIntake = value;
                NotifyPropertyChanged("DailyIntake");
            }
        }

        private string _sideEffects;
        public string SideEffects
        {
            get
            {
                return _sideEffects;
            }
            set
            {
                _sideEffects = value;
                NotifyPropertyChanged("SideEffects");
            }
        }

        private string _number;
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        public void Load(Ingredient ingredient)
        {
            Name = ingredient.Name;
            Description = ingredient.Description;
            Number = ingredient.Number;
            Origin = ingredient.Origin;
            ProductsType = ingredient.ProductsType;
            DietaryRestrictions = ingredient.DietaryRestrictions;
            SideEffects = ingredient.SideEffects;
            DailyIntake = ingredient.DailyIntake;
        }
    }
}
