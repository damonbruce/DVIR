using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RailDelivery.Application;
using RailDelivery.Core.Command;
using RailDelivery.Core.Command.Interface;
using RailDelivery.Core.Models;
using RailDelivery.DVIRApp.Models;
using RailDelivery.DVIRApp.Views;

namespace RailDelivery.DVIRApp.ViewModel
{
    public class ReviewingDriverViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private void ViewLoaded(object sender, EventArgs routedEventArgs)
        {
            var win2 = Application.Current.Windows.OfType<ReviewingDriver>().SingleOrDefault(w => w.Name == "ReviewingDriverWindow");
            if (win2 == null)
                return;

            ReviewDate = win2.ReviewedDriver.ReviewDate;
            DriverName = win2.ReviewedDriver.DriverName;
            DriverNumber = win2.ReviewedDriver.DriverNumber;
        }

        public ReviewDriver _reviewedDriver;
        public ReviewDriver ReviewedDriver
        {
            get { return _reviewedDriver; }
            set
            {
                _reviewedDriver = value;
                OnPropertyChanged("ReviewedDriver");
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(Save);
                }
                return _saveCommand;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(Cancel);
                }
                return _cancelCommand;
            }
        }

        private bool _canSave;

        public bool CanSave
        {
            get
            {
                if (!string.IsNullOrEmpty(DriverName) && !string.IsNullOrEmpty(DriverNumber) && ReviewDate != DateTime.MinValue)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private IReviewCommand _reviewCommand;

        private DateTime _reviewDate;

        public DateTime ReviewDate
        {
            get
            {
                if (_reviewDate == DateTime.MinValue)
                    _reviewDate = DateTime.Now;

                return _reviewDate;
            }
            set
            {
                _reviewDate = value;
                OnPropertyChanged("ReviewDate");
                OnPropertyChanged("CanSave");
            }
        }

        private string _driverName;

        public string DriverName
        {
            get { return _driverName; }
            set
            {
                _driverName = value;
                OnPropertyChanged("DriverName");
                OnPropertyChanged("CanSave");
            }
        }


        private string _driverNumber;

        public string DriverNumber
        {
            get { return _driverNumber; }
            set
            {
                _driverNumber = value;
                OnPropertyChanged("DriverNumber");
                OnPropertyChanged("CanSave");
            }
        }

        public QueryResults SelectedItem { get; set; }

        public ReviewingDriverViewModel() : this(new ReviewCommand()) { }
        public ReviewingDriverViewModel(IReviewCommand reviewCommand)
        {
            if(reviewCommand == null)
                throw new ArgumentNullException("reviewCommand");

            _reviewCommand = reviewCommand;

            var win2 = Application.Current.Windows.OfType<ReviewingDriver>().SingleOrDefault(w => w.Name == "ReviewingDriverWindow");
            win2.Activated += ViewLoaded;
        }
        private void Cancel()
        {
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ReviewingDriverWindow");
            win.Close();
        }

        private void Save()
        {
            var review = new ReviewDriver()
            {
                DriverName = DriverName,
                DriverNumber = DriverNumber,
                ReviewDate = ReviewDate
            };

            var win2 = Application.Current.Windows.OfType<ReviewingDriver>().SingleOrDefault(w => w.Name == "ReviewingDriverWindow");
            var selectedRepair = win2.ReviewingDriverItem;

            if (selectedRepair.ResultType == "Powered")
                review.DVIRPoweredId = selectedRepair.UniqueId;
            else
                review.DVIRTowedId = selectedRepair.UniqueId;

            _reviewCommand.ReviewingDriver(review);

            win2.Close();

            var viewmodel = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(w => w.Name == "MainWindowView");

            viewmodel.UpdateAll.IsChecked = true;
            //viewmodel.ReviewButton.IsEnabled = false;
        }

    }
}
