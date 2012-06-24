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
using System.Collections.ObjectModel;
using System.Windows.Data;

using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;
using Fooder.Models;

namespace Fooder.ViewModels
{
    public class IngredientsViewModel : ViewModelBase
    {
        WebClient remoteXml;
        App thisApp = Application.Current as App;

        public IngredientsViewModel()
        {
            // Insert code required on object creation below this point.
            this.Items = new ObservableCollection<Ingredient>();

            this.ProgressBarVisibility = "Visible";

            remoteXml = new WebClient();
            remoteXml.DownloadStringCompleted += new DownloadStringCompletedEventHandler(remoteXml_DownloadStringCompleted);
        }


        private ObservableCollection<Ingredient> _ingredients;

        private CollectionViewSource _ingredientsView;
        public CollectionViewSource IngredientsView
        {
            get
            {
                if (null == _ingredientsView)
                {
                    _ingredientsView = new CollectionViewSource();
                    //_riversView.Filter += FilterRiversView;
                }
                //System.deb_ingredientsView.View.CurrentItem;
                return _ingredientsView;
            }
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set
            {
                if (_ingredients != value)
                {
                    _ingredients = value;
                    NotifyPropertyChanged("Ingredients");
                    this.IngredientsView.Source = _ingredients;
                }
            }
        }
 

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Ingredient> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        private String _progressBarVisibility;
        public String ProgressBarVisibility
        {
            get
            {
                return _progressBarVisibility;
            }
            set {
                _progressBarVisibility = value;
                NotifyPropertyChanged("ProgressBarVisibility");
            }
        }

        private String _ingredientsListVisibility = "Visible";
        public String IngredientsListVisibility
        {
            get
            {
                return _ingredientsListVisibility;
            }
            set
            {
                _ingredientsListVisibility = value;
                NotifyPropertyChanged("IngredientsListVisibility");
            }
        }

        private String _searchVisibility = "Visible";
        public String SearchVisibility
        {
            get
            {
                return _searchVisibility;
            }
            set
            {
                _searchVisibility = value;
                NotifyPropertyChanged("SearchVisibility");
            }
        }

        private String _filterTitleVisibility = "Collapsed";
        public String FilterTitleVisibility
        {
            get
            {
                return _filterTitleVisibility;
            }
            set
            {
                _filterTitleVisibility = value;
                NotifyPropertyChanged("FilterTitleVisibility");
            }
        }

        /// <summary>
        /// Creates and adds a few MyTaskItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            String txtUri = "http://fooder.developers.stoliczku.pl/ingredients.xml";
            txtUri = Uri.EscapeUriString(txtUri);
            Uri uri = new Uri(txtUri, UriKind.Absolute);
            remoteXml.DownloadStringAsync(uri);
        }

        private void remoteXml_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            XDocument xdoc = XDocument.Parse(e.Result);

            List<Ingredient> ingredients;

            ingredients = (from item in xdoc.Descendants("object")
                        select new Ingredient()
                        {
                            ID = Convert.ToInt16(item.Element("id").Value),
                            Name = item.Element("name").Value,
                            Number = item.Element("number").Value,
                            Description = (string) item.Element("description") ?? "Brak danych.",
                            Origin = (string)item.Element("origin") ?? "Brak danych.",
                            ProductsType = (string)item.Element("products-type") ?? "Brak danych.",
                            DailyIntake = (string)item.Element("daily-intake") ?? "Brak danych.",
                            SideEffects = (string)item.Element("side-effects") ?? "Brak danych.",
                            DietaryRestrictions = (string)item.Element("dietary-restrictions") ?? "Brak danych."
                        }).ToList();


            foreach (var ingredient in ingredients)
            {
                this.Items.Add(ingredient);
            }

            this.Ingredients = this.Items;
            this.ProgressBarVisibility = "Collapsed";
            
        }

        public void Filter(String searchText)
        {

            this.IngredientsView.View.Filter = r =>
            {
                if (null == r) return true;
                var rm = (Ingredient)r;
                var meets = rm.Number.ToLowerInvariant().Contains(searchText.ToLowerInvariant())
                    || rm.Name.ToLowerInvariant().Contains(searchText.ToLowerInvariant());
                return meets;
            };
        }

        public void FilterIds(List<short> ids)
        {


            this.IngredientsView.View.Filter = i =>
            {
                if (null == i) return true;
                var ingredient = (Ingredient)i;
                var meets = ids.Contains(ingredient.ID);
                return meets;
            };

            App.IngredientsViewModelInstance.ProgressBarVisibility = "Collapsed";
            App.IngredientsViewModelInstance.SearchVisibility = "Collapsed";
            App.IngredientsViewModelInstance.IngredientsListVisibility = "Visible";
            App.IngredientsViewModelInstance.FilterTitleVisibility = "Visible";
            thisApp.FilterIds = true;
            
        }

        public void CancelFilter()
        {
            this.IngredientsView.View.Filter = i =>
            {
                return true;
            };
            App.IngredientsViewModelInstance.SearchVisibility = "Visible";
            App.IngredientsViewModelInstance.FilterTitleVisibility = "Collapsed";
        }
    }
}