/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RailDelivery.Application"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmLight5.Model;
using RailDelivery.Core.Command;
using RailDelivery.Core.Command.Interface;
using RailDelivery.Core.Query;
using RailDelivery.Core.Query.Interface;

namespace RailDelivery.DVIRApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IPowerQuery, PoweredQuery>();
            SimpleIoc.Default.Register<ITowedQuery, TowedQuery>();
            SimpleIoc.Default.Register<ICertifyCommand, CertifyCommand>();
            SimpleIoc.Default.Register<IReviewCommand, ReviewCommand>();
            
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CertifyRepairViewModel>();
            SimpleIoc.Default.Register<ReviewingDriverViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}