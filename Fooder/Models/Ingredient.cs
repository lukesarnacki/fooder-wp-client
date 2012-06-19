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

namespace Fooder.Models
{
    public class Ingredient
    {
        public String Name { get; set; }
        public String Number { get; set; }
        public String Description { get; set; }
        public String Origin { get; set; }
        public String DietaryRestrictions { get; set; }
        public String ProductsType { get; set; }
        public String SideEffects { get; set; }
        public String DailyIntake { get; set; }
        public String ID { get; set; }
    }
}
