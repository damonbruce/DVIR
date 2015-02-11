using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight5.Model;
using RailDelivery.Application;
using RailDelivery.Core.Command;
using RailDelivery.Core.Command.Interface;
using RailDelivery.Core.Models;
using RailDelivery.Core.Query;
using RailDelivery.Core.Query.Interface;
using RailDelivery.DVIRApp.Helpers;
using RailDelivery.DVIRApp.Models;
using RailDelivery.DVIRApp.Views;
using StructureMap.Query;
using Telerik.Pivot.Queryable.Filtering;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace RailDelivery.DVIRApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public RelayCommand OpenModalDialog { get; private set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private readonly ITowedQuery _towedQuery;
        private readonly IPowerQuery _powerQuery;
        private readonly ITowedCommand _towedCommand;
        private readonly IPoweredCommand _poweredCommand;

        private bool localSet = false;
        private int lastId = 0;

        private QueryResults _selectedItem;
        public QueryResults SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                OnPropertyChanged("SelectedValid");
                OnPropertyChanged("IsPowered");
                OnPropertyChanged("IsTowed");
                OnPropertyChanged("PoweredTab");
                OnPropertyChanged("TowedTab");
                OnPropertyChanged("ObjControls");

                var viewmodel =
                        Application.Current.Windows.OfType<MainWindow>()
                            .SingleOrDefault(w => w.Name == "MainWindowView");

                if (_selectedItem != null && _selectedItem.ResultType == "Powered")
                    SelectedPower = _powerQuery.GetPowered(_selectedItem.UniqueId);
                else
                    SelectedTowed = _towedQuery.GetTowed(_selectedItem.UniqueId);


                if (_selectedItem != null && _selectedItem.ResultType == "Powered" &&
                    ((_selectePowered.CertifiedRepairsDate == null ||
                      _selectePowered.CertifiedRepairsDate.GetValueOrDefault().Date == DateTime.Now.Date)) &&
                    string.IsNullOrEmpty(_selectePowered.CertifiedBy))
                {
                    CanBeCertify = true;
                    viewmodel.CertifyButton.IsEnabled = true;
                }
                else if (_selectedItem != null && _selectedItem.ResultType == "Towed" &&
                         ((_selectedTowed.CertifiedRepairsDate == null ||
                           _selectedTowed.CertifiedRepairsDate.GetValueOrDefault().Date == DateTime.Now.Date)) &&
                         string.IsNullOrEmpty(_selectedTowed.CertifiedBy))
                {
                    CanBeCertify = true;
                    viewmodel.CertifyButton.IsEnabled = true;
                }
                else
                {
                    CanBeCertify = false;
                    viewmodel.CertifyButton.IsEnabled = false;
                }

                if ((_selectePowered != null && _selectePowered.CertifiedRepairsDate != null) || (_selectedTowed != null && _selectedTowed.CertifiedRepairsDate != null))
                {
                    CanBeCertify = true;
                    viewmodel.CertifyButton.IsEnabled = true;
                }

                OnPropertyChanged("CanBeCertify");

                if (SelectedItem != null && SelectedItem.ResultType == "Powered" &&
                    (_selectePowered.CreateDate != null && !string.IsNullOrEmpty(_selectePowered.CertifiedBy)) &&
                    string.IsNullOrEmpty(_selectePowered.ReviewingDriverName) &&
                    (!_selectePowered.ReviewingDriverDate.HasValue ||
                     _selectePowered.ReviewingDriverDate.Value.Date == DateTime.Now.Date))
                {
                    CanReview = true;
                    viewmodel.ReviewButton.IsEnabled = true;
                }
                else if (SelectedItem != null && SelectedItem.ResultType == "Towed" &&
                         (_selectedTowed.CreateDate != null && !string.IsNullOrEmpty(_selectedTowed.CertifiedBy)) &&
                         string.IsNullOrEmpty(_selectedTowed.ReviewingDriverName) &&
                         (!_selectedTowed.ReviewingDriverDate.HasValue ||
                          _selectedTowed.ReviewingDriverDate.Value.Date == DateTime.Now.Date))
                {
                    CanReview = true;
                    viewmodel.ReviewButton.IsEnabled = true;
                }
                else
                {
                    CanReview = false;
                    viewmodel.ReviewButton.IsEnabled = false;
                }

                if ((_selectePowered != null && _selectePowered.ReviewingDriverDate != null) || (_selectedTowed != null && _selectedTowed.ReviewingDriverDate != null))
                {
                    CanReview = true;
                    viewmodel.ReviewButton.IsEnabled = true;
                }

                OnPropertyChanged("CanReview");
            }
        }

        public bool _update;
        public bool Update
        {
            set
            {
                _update = value;
                localSet = false;
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("CanBeCertify");
                OnPropertyChanged("CanReview");
            }
        }

        public void WindowClosed(object sender, EventArgs args)
        {
            //OnPropertyChanged("SelectedItem");
            SelectedItem = _selectedItem;
        }

        private bool _canCertify;
        public bool CanBeCertify
        {
            get { return _canCertify; }
            set { _canCertify = value; }
        }

        private bool _canReview;
        public bool CanReview
        {
            get { return _canReview; }
            set { _canReview = value; }
        }

        private Powered _selectePowered;

        public Powered SelectedPower
        {
            get { return _selectePowered; }
            set
            {
                _selectePowered = value;
                OnPropertyChanged("SelectedPower");
            }
        }

        private Towed _selectedTowed;

        public Towed SelectedTowed
        {
            get { return _selectedTowed; }
            set
            {
                _selectedTowed = value;
                OnPropertyChanged("SelectedTowed");
            }
        }

        private int _poweredTab;
        public int PoweredTab
        {
            get
            {
                if (SelectedItem != null && SelectedItem.ResultType == "Powered")
                    return 0;
                else
                    return 1;
            }
            set { _poweredTab = value; }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                if (_startDate == DateTime.MinValue)
                    _startDate = DateTime.Now.AddDays(-30);
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged("CanQuery");
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get
            {
                if (_endDate == DateTime.MinValue)
                    _endDate = DateTime.Now;

                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged("CanQuery");
            }
        }

        private string _unitNumber;

        public string UnitNumber
        {
            get { return _unitNumber; }
            set
            {
                _unitNumber = value;
                OnPropertyChanged("CanQuery");
            }
        }

        private bool _powerSelect;
        public bool PowerSelect
        {
            get { return _powerSelect; }
            set
            {
                _powerSelect = value;
                OnPropertyChanged("CanQuery");
            }
        }

        private bool _towedSelect;
        public bool TowedSelect
        {
            get { return _towedSelect; }
            set
            {
                _towedSelect = value;
                OnPropertyChanged("CanQuery");
            }
        }

        public bool CanQuery
        {
            get
            {
                if ((PowerSelect || TowedSelect) && StartDate != DateTime.MinValue && EndDate != DateTime.MinValue && !string.IsNullOrEmpty(UnitNumber) && StartDate <= EndDate)
                    return true;

                return false;
            }
        }

        public bool SelectedValid
        {
            get
            {
                if (SelectedItem != null)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        public bool IsPowered
        {
            get
            {
                if (SelectedItem != null && SelectedItem.ResultType == "Powered")
                    return true;
                else
                    return false;
            }
        }

        public bool IsTowed
        {
            get
            {
                if (SelectedItem != null && SelectedItem.ResultType == "Towed")
                    return true;
                else
                    return false;
            }
        }

        public bool ObjControls
        {
            get
            {
                if (SelectedItem != null)
                    return true;
                else
                    return false;
            }
        }

        private ObservableCollection<QueryResults> _results;
        public ObservableCollection<QueryResults> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(SaveObject);
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
                    _cancelCommand = new RelayCommand(CancelObject);
                }
                return _cancelCommand;
            }
        }

        private ICommand _selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                {
                    _selectCommand = new RelayCommand(SelectObject);
                }
                return _selectCommand;
            }
        }

        private ICommand _reportCommand;
        public ICommand ReportCommand
        {
            get
            {
                if (_reportCommand == null)
                {
                    _reportCommand = new RelayCommand(ProduceReport);
                }
                return _reportCommand;
            }
        }

        private ICommand _certifyCommand;
        public ICommand CertifyCommand
        {
            get
            {
                if (_certifyCommand == null)
                {
                    _certifyCommand = new RelayCommand(Certify);
                }
                return _certifyCommand;
            }
        }

        private ICommand _repairDriverCommand;
        public ICommand RepairDriverCommand
        {
            get
            {
                if (_repairDriverCommand == null)
                {
                    _repairDriverCommand = new RelayCommand(ReviewDriver);
                }
                return _repairDriverCommand;
            }
        }

        private readonly IDataService _dataService;

        public MainViewModel() : this(new TowedQuery(), new PoweredQuery(), new DataService(), new TowedCommand(), new PoweredCommand()) { }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ITowedQuery towedQuery, IPowerQuery poweredQuery, IDataService dataService, ITowedCommand towedCommand, IPoweredCommand poweredCommand)
        {
            if (towedQuery == null)
                throw new ArgumentNullException("towedQuery");

            if (poweredQuery == null)
                throw new ArgumentNullException("poweredQuery");

            if (dataService == null)
                throw new ArgumentNullException("dataService");

            if (towedCommand == null)
                throw new ArgumentNullException("towedCommand");

            if (poweredCommand == null)
                throw new ArgumentNullException("poweredCommand");

            _powerQuery = poweredQuery;
            _towedQuery = towedQuery;
            _dataService = dataService;
            _poweredCommand = poweredCommand;
            _towedCommand = towedCommand;

            OnPropertyChanged("CanReview");
            OnPropertyChanged("CanCertify");

            var win = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(w => w.Name == "MainWindowView");
            win.Activated += WindowClosed;
        }

        private void ProduceReport()
        {
            var report = new Report(SelectedItem);
            report.ShowDialog();
        }

        private void Certify()
        {
            if (SelectedItem.ResultType == "Powered")
            {
                var item = new CertifyRepair()
                {
                    CertifiedBy = _selectePowered.CertifiedBy,
                    CertifyDate = _selectePowered.CertifiedRepairsDate.GetValueOrDefault(),
                    CertifyLocation = _selectePowered.CertifiedLocation,
                    CertifyRepairsMade = _selectePowered.RepairsMadeFlag.GetValueOrDefault(),
                    RepairsMade = _selectePowered.RONumber
                };

                var report = new CertifyRepairs(SelectedItem, item);

                report.ShowDialog();
            }
            else
            {
                var report = new CertifyRepairs(SelectedItem, new CertifyRepair()
                {
                    CertifiedBy = _selectedTowed.CertifiedBy,
                    CertifyDate = _selectedTowed.CertifiedRepairsDate.GetValueOrDefault(),
                    CertifyLocation = _selectedTowed.CertifiedLocation,
                    CertifyRepairsMade = _selectedTowed.RepairsMadeFlag.GetValueOrDefault(),
                    RepairsMade = _selectedTowed.RONumber
                });
                report.ShowDialog();
            }
        }

        private void ReviewDriver()
        {
            if (SelectedItem.ResultType == "Powered")
            {
                var driver = new ReviewDriver()
                {
                    DriverName = _selectePowered.ReviewingDriverName,
                    DriverNumber = _selectePowered.ReviewingDriverNumber,
                    ReviewDate = _selectePowered.ReviewingDriverDate.GetValueOrDefault()
                };
                var report = new ReviewingDriver(driver, SelectedItem);

                report.ShowDialog();
            }
            else
            {
                var driver = new ReviewDriver()
                {
                    DriverName = _selectedTowed.ReviewingDriverName,
                    DriverNumber = _selectedTowed.ReviewingDriverNumber,
                    ReviewDate = _selectedTowed.ReviewingDriverDate.GetValueOrDefault()
                };
                var report = new ReviewingDriver(driver, SelectedItem);

                report.ShowDialog();
            }
        }

        private void SaveObject()
        {
            if (SelectedItem == null)
                return;

            if (SelectedItem.ResultType == "Powered")
                _poweredCommand.SavePowered(SelectedPower);
            else
                _towedCommand.SaveTowed(SelectedTowed);
        }

        private void CancelObject()
        {
            SelectedItem = null;
            SelectedPower = null;
            SelectedTowed = null;
        }

        private void SelectObject()
        {
            if ((!PowerSelect && !TowedSelect) || StartDate == DateTime.MinValue || EndDate == DateTime.MinValue ||
                string.IsNullOrEmpty(UnitNumber) || StartDate > EndDate)
            {
                return;
            }

            localSet = false;

            if (PowerSelect) //Query the Powered Results
            {
                var results = _powerQuery.FindPowered(new Core.Models.Search()
                {
                    EndDate = EndDate,
                    StartDate = StartDate,
                    UnitNumber = UnitNumber
                });

                //Start over with a fresh result set.
                var returnResults = new ObservableCollection<QueryResults>();

                //fill the results collection
                results.ForEach(x => returnResults.Add(new QueryResults()
                {
                    CreateDate = x.CreateDate.GetValueOrDefault(),
                    DriverNumber = x.DriverNo.GetValueOrDefault(),
                    UnitNumber = x.TractorId,
                    ResultType = "Powered",
                    UniqueId = x.DVIRPowerId
                }));
                Results = returnResults;

                if (Results != null && Results.Count > 0)
                {
                    var viewmodel =
                        Application.Current.Windows.OfType<MainWindow>()
                            .SingleOrDefault(w => w.Name == "MainWindowView");

                    viewmodel.rgvResults.SelectedItem = Results.First();
                }
            }
            else if (TowedSelect) //Query the Towed Results
            {
                var results = _towedQuery.FindTowed(new Core.Models.Search()
                {
                    EndDate = EndDate,
                    StartDate = StartDate,
                    UnitNumber = UnitNumber
                });

                //Start over with a fresh result set.
                var returnResults = new ObservableCollection<QueryResults>();

                //fill the results collection
                results.ForEach(x => returnResults.Add(new QueryResults()
                {
                    CreateDate = x.CreateDate.GetValueOrDefault(),
                    DriverNumber = x.DriverNo.GetValueOrDefault(),
                    UnitNumber = x.TrailerChassis,
                    ResultType = "Towed",
                    UniqueId = x.DVIRTowedId
                }));
                Results = returnResults;

                if (Results != null && Results.Count > 0)
                {
                    var viewmodel =
                        Application.Current.Windows.OfType<MainWindow>()
                            .SingleOrDefault(w => w.Name == "MainWindowView");

                    viewmodel.rgvResults.SelectedItem = Results.First();
                }
            }
            else
            {
                return; //Display some error
            }

        }

        public void GetResults()
        {

        }

        #region DialogControls
        #endregion
    }
}