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
using Microsoft.Practices.ServiceLocation;
using RailDelivery.Core.Models;
using RailDelivery.DVIRApp.Models;
using RailDelivery.DVIRApp.ViewModel;

namespace RailDelivery.DVIRApp.Views
{
    /// <summary>
    /// Interaction logic for CertifyRepairs.xaml
    /// </summary>
    public partial class CertifyRepairs : Window
    {
        public QueryResults Results { get; set; }
        public CertifyRepair Repair { get; set; }
        public CertifyRepairs(QueryResults results, CertifyRepair repair)
        {
            InitializeComponent();
            Results = results;
            Repair = repair;
        }
    }
}
