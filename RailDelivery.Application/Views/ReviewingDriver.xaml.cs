using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RailDelivery.Core.Models;
using RailDelivery.DVIRApp.Models;

namespace RailDelivery.DVIRApp.Views
{
    /// <summary>
    /// Interaction logic for ReviewingDriver.xaml
    /// </summary>
    public partial class ReviewingDriver : Window
    {
        public QueryResults ReviewingDriverItem { get; set; }
        public ReviewDriver ReviewedDriver { get; set; }
        public ReviewingDriver(ReviewDriver driver, QueryResults result)
        {
            InitializeComponent();
            ReviewingDriverItem = result;
            ReviewedDriver = driver;
        }
    }
}
