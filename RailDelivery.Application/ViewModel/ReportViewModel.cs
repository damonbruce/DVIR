using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RailDelivery.Core.Models;
using RailDelivery.Core.Query;
using RailDelivery.Core.Query.Interface;
using RailDelivery.DVIRApp.Models;
using RailDelivery.DVIRApp.Views;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace RailDelivery.DVIRApp.ViewModel
{
    public class ReportViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly ITowedQuery _towedQuery;
        private readonly IPowerQuery _powerQuery;

        //Reporting and reviewing properties
        private string _remarks;

        public string Remarks
        {
            get
            {
                if (SelectedItem.ResultType != "Powered")
                    return SelectedTowed.Remark1;

                return string.Empty;
            }
        }

        public string ReviewingDriver
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.ReviewingDriverName;
                else
                    return SelectedTowed.ReviewingDriverName;
            }
        }

        public DateTime? ReviewingDate
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.ReviewingDriverDate;
                else
                    return SelectedTowed.ReviewingDriverDate;
            }
        }

        public bool RepairsMade
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.RepairsMadeFlag.GetValueOrDefault();
                else
                    return SelectedTowed.RepairsMadeFlag.GetValueOrDefault();
            }
        }

        public bool NoRepairsMaid
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return !SelectedPower.RepairsMadeFlag.GetValueOrDefault();
                else
                    return !SelectedTowed.RepairsMadeFlag.GetValueOrDefault();
            }
        }

        public string RONumber
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.RONumber;
                else
                    return SelectedTowed.RONumber;
            }
        }

        public string Location
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.CertifiedLocation;
                else
                    return SelectedTowed.CertifiedLocation;
            }
        }

        public string CertifyDriver
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.CertifiedBy;
                else
                    return SelectedTowed.CertifiedBy;
            }
        }

        public DateTime? RepairDate
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.CertifiedRepairsDate;
                else
                    return SelectedTowed.CertifiedRepairsDate;
            }
        }

        public string ReviewingDriverNumber
        {
            get
            {
                if (SelectedItem.ResultType == "Powered")
                    return SelectedPower.ReviewingDriverNumber;
                else
                    return SelectedTowed.ReviewingDriverNumber;
            }
        }

        public string ShopRemarks
        {
            get
            {
                if (SelectedItem.ResultType != "Powered")
                    return SelectedTowed.Remark2;
                return string.Empty;
            }
        }

        private QueryResults _selectedResults;
        public QueryResults SelectedItem
        {
            get
            {
                if (_selectedResults == null)
                {
                    SetSelected();
                }

                return _selectedResults;
            }
            set
            {
                _selectedResults = value;

                if (_selectedResults.ResultType == "Powered")
                    SelectedPower = _powerQuery.GetPowered(_selectedResults.UniqueId);
                else
                    SelectedTowed = _towedQuery.GetTowed(_selectedResults.UniqueId);
            }
        }

        private Powered _selectePowered;

        public Powered SelectedPower
        {
            get
            {
                if (_selectePowered == null && SelectedItem != null && SelectedItem.ResultType == "Powered")
                    _selectePowered = _powerQuery.GetPowered(SelectedItem.UniqueId);

                return _selectePowered;
            }
            set
            {
                _selectePowered = value;
            }
        }
        private Towed _selecteTowed;

        public Towed SelectedTowed
        {
            get
            {
                if (_selecteTowed == null && SelectedItem != null && SelectedItem.ResultType == "Towed")
                    _selecteTowed = _towedQuery.GetTowed(_selectedResults.UniqueId);

                return _selecteTowed;
            }
            set
            {
                _selecteTowed = value;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(Close);
                }
                return _cancelCommand;
            }
        }

        private ICommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                if (_printCommand == null)
                {
                    _printCommand = new RelayCommand(Print);
                }
                return _printCommand;
            }
        }

        public ReportViewModel() : this(new TowedQuery(), new PoweredQuery()) { }
        public ReportViewModel(ITowedQuery towedQuery, IPowerQuery poweredQuery)
        {
            if (towedQuery == null)
                throw new ArgumentNullException("towedQuery");

            if (poweredQuery == null)
                throw new ArgumentNullException("poweredQuery");

            _powerQuery = poweredQuery;
            _towedQuery = towedQuery;
        }

        public void SetSelected()
        {
            var win2 = Application.Current.Windows.OfType<Report>().SingleOrDefault(w => w.Name == "ReportWindow");

            if (win2 != null && win2.Results != null)
                SelectedItem = win2.Results;
        }

        private void Close()
        {
            var win2 = Application.Current.Windows.OfType<Report>().SingleOrDefault(w => w.Name == "ReportWindow");
            win2.Close();
        }

        private void Print()
        {
            var win2 = Application.Current.Windows.OfType<Report>().SingleOrDefault(w => w.Name == "ReportWindow");

            PrintVisual(win2);

            win2.Close();
        }

        private void PrintVisual(Report win2)
        {
            win2.PrintButtons.Visibility = Visibility.Collapsed;
            PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
            if (printDlg.ShowDialog() == true) //id print ok is pressed on print dialog
            {
                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / win2.ActualWidth,
                capabilities.PageImageableArea.ExtentHeight /
                win2.ActualHeight);

                //Transform the Visual to scale
                win2.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                win2.Measure(sz);
                win2.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                printDlg.PrintVisual(win2, "DriverInspectionReport");

                win2.PrintButtons.Visibility = Visibility.Visible;
            }
        }
    }
}
