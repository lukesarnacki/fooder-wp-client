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

namespace Fooder.Models
{
    public class Product
    {
        public System.Collections.ICollection ingredientIds;
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
