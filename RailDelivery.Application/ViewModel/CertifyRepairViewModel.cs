using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RailDelivery.Application;
using RailDelivery.Core.Command;
using RailDelivery.Core.Command.Interface;
using RailDelivery.Core.Models;
using RailDelivery.Core.Query;
using RailDelivery.Core.Query.Interface;
using RailDelivery.DVIRApp.Models;
using RailDelivery.DVIRApp.Views;

namespace RailDelivery.DVIRApp.ViewModel
{
    public class CertifyRepairViewModel : GalaSoft.MvvmLight.ViewModelBase, INotifyPropertyChanged
    {
        private readonly ICertifyCommand _certifyCommand;
        private readonly ITowedQuery _towedQuery;
        private readonly IPowerQuery _powerQuery;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private QueryResults _selectedResult;

        public QueryResults SelectedRepair
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                OnPropertyChanged("SelectedRepair");
            }
        }

        private RailDelivery.Core.Models.CertifyRepair _certifiedRepair;
        public RailDelivery.Core.Models.CertifyRepair CertRepair
        {
            get { return _certifiedRepair; }
            set
            {
                _certifiedRepair = value;
                OnPropertyChanged("CanSave");
            }
        }

        public void ViewLoaded(object sender, EventArgs routedEventArgs)
        {

            var win2 = Application.Current.Windows.OfType<CertifyRepairs>().SingleOrDefault(w => w.Name == "CertifyRepairsWindow");
            if (win2 == null)
                return;

            CertRepair = win2.Repair;

            CertifiedBy = win2.Repair.CertifiedBy;
            CertifiedDate = win2.Repair.CertifyDate;
            CertifiedLocation = win2.Repair.CertifyLocation;
            RepairsMade = win2.Repair.RepairsMade;
            if (win2.Repair.CertifyRepairsMade)
                CertifiedRepairsMade = true;
            else
                CertifiedNoRepairsMade = true;
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand<Window>(Save);
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
                    _cancelCommand = new RelayCommand<Window>(Cancel);
                }
                return _cancelCommand;
            }
        }

        private bool _canSave;

        public bool CanSave
        {
            get
            {
                if (!string.IsNullOrEmpty(CertifiedBy) &&
                    CertifiedDate != DateTime.MinValue &&
                    !string.IsNullOrEmpty(CertifiedLocation) &&
                    !string.IsNullOrEmpty(RepairsMade) &&
                    (CertifiedRepairsMade || CertifiedNoRepairsMade))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private string _certfiedBy;
        public string CertifiedBy
        {
            get { return _certfiedBy; }
            set
            {
                _certfiedBy = value;
                OnPropertyChanged("CertifiedBy");
                OnPropertyChanged("CanSave");
            }
        }

        private string _certifiedLocation;
        public string CertifiedLocation
        {
            get { return _certifiedLocation; }
            set
            {
                _certifiedLocation = value;
                OnPropertyChanged("CertifiedLocation");
                OnPropertyChanged("CanSave");
            }
        }

        private DateTime _certifiedDate;
        public DateTime CertifiedDate
        {
            get
            {
                if (_certifiedDate == DateTime.MinValue)
                    _certifiedDate = DateTime.Now;
                return _certifiedDate;
            }
            set
            {
                _certifiedDate = value;
                OnPropertyChanged("CertifiedDate");
                OnPropertyChanged("CanSave");
            }
        }

        private string _repairsMade;
        public string RepairsMade
        {
            get { return _repairsMade; }
            set
            {
                _repairsMade = value;
                OnPropertyChanged("RepairsMade");
                OnPropertyChanged("CanSave");
            }
        }

        private bool _certifiedRepairsMade;
        public bool CertifiedRepairsMade
        {
            get { return _certifiedRepairsMade; }
            set
            {
                _certifiedRepairsMade = value;
                OnPropertyChanged("CertifiedRepairsMade");
                OnPropertyChanged("CanSave");
            }
        }

        private bool _certifiedNoRepairsMade;
        public bool CertifiedNoRepairsMade
        {
            get { return _certifiedNoRepairsMade; }
            set
            {
                _certifiedNoRepairsMade = value;
                OnPropertyChanged("CertifiedNoRepairsMade");
                OnPropertyChanged("CanSave");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public CertifyRepairViewModel() : this(null, new CertifyCommand(), new TowedQuery(), new PoweredQuery()) { }
        public CertifyRepairViewModel(QueryResults queryResult, ICertifyCommand certifyCommand, ITowedQuery towedQuery, IPowerQuery powerQuery)
        {
            if (certifyCommand == null)
                throw new ArgumentNullException("certifyCommand");

            if (towedQuery == null)
                throw new ArgumentNullException("towedQuery");

            if (powerQuery == null)
                throw new ArgumentNullException("powerQuery");

            _powerQuery = powerQuery;
            _towedQuery = towedQuery;
            _certifyCommand = certifyCommand;

            CertRepair = new CertifyRepair();

            var view = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "CertifyRepairsWindow");
            view.Activated += ViewLoaded;
        }

        private void Cancel(Window window)
        {
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "CertifyRepairsWindow");
            win.Close();
        }

        private void Save(Window window)
        {
            CertRepair = new CertifyRepair()
            {
                CertifiedBy = CertifiedBy,
                CertifyDate = CertifiedDate,
                CertifyLocation = CertifiedLocation,
                CertifyRepairsMade = (CertifiedRepairsMade),
                RepairsMade = RepairsMade
            };


            var win2 = Application.Current.Windows.OfType<CertifyRepairs>().SingleOrDefault(w => w.Name == "CertifyRepairsWindow");
            SelectedRepair = win2.Results;

            if (SelectedRepair.ResultType == "Powered")
                CertRepair.DVIRPowerId = SelectedRepair.UniqueId;
            else
                CertRepair.DVIRTowedId = SelectedRepair.UniqueId;

            _certifyCommand.CertifyRepairs(CertRepair);

            win2.Close();

            var viewmodel = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(w => w.Name == "MainWindowView");

            viewmodel.UpdateAll.IsChecked = true;
            //viewmodel.CertifyButton.IsEnabled = false;
            viewmodel.ReviewButton.IsEnabled = true;
        }
    }

}
