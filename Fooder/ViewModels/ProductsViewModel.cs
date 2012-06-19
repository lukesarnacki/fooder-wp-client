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
    public class ProductsViewModel : ViewModelBase
    {

        WebClient remoteXml;
        public ProductsViewModel()
        {
            this.Items = new ObservableCollection<Product>();
            remoteXml = new WebClient();
            remoteXml.DownloadStringCompleted += new DownloadStringCompletedEventHandler(remoteXml_DownloadStringCompleted);
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Product> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few MyTaskItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            String txtUri = "http://fooder.herokuapp.com/products.xml";
            txtUri = Uri.EscapeUriString(txtUri); 
            Uri uri = new Uri(txtUri, UriKind.Absolute); 
            remoteXml.DownloadStringAsync(uri);
            
            // Sample data; replace with real data
            this.IsDataLoaded = true;
        }
        

        private void remoteXml_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            XDocument xdoc = XDocument.Parse(e.Result);

            List<Product> products;

            products = (from item in xdoc.Descendants("object")
                        select new Product()
                        {
                            Name = item.Element("name").Value,
                            Description = item.Element("description").Value
                        }).ToList();         

            foreach (var product in products)
            {
                this.Items.Add(product);
            }
        }
    }
}