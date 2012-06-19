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

using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;
using Fooder.Models;

namespace Fooder.ViewModels
{
    public class IngredientsViewModel : ViewModelBase
    {
        WebClient remoteXml;

        public IngredientsViewModel()
        {
            // Insert code required on object creation below this point.
            this.Items = new ObservableCollection<Ingredient>();

            this.ProgressBarVisibility = "Visible";

            remoteXml = new WebClient();
            remoteXml.DownloadStringCompleted += new DownloadStringCompletedEventHandler(remoteXml_DownloadStringCompleted);
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

        /// <summary>
        /// Creates and adds a few MyTaskItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            String txtUri = "http://fooder.herokuapp.com/ingredients.xml";
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
                            ID = item.Element("id").Value,
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
            //IEnumerable<Ingredient> matches = Items.Where(p => p.Name == "");

            this.ProgressBarVisibility = "Collapsed";
            
        }
    }
}