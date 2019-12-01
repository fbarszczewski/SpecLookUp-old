using SpecLookUp.DAL;
using SpecLookUp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SpecLookUp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _refreshTimer;
        private List<Device> _mainLogs;
        private List<Device> _historyLogs;
        private List<CmarLog> _cmarLogs;
        private string _refreshBtnText;
        private int _refreshCount = 10;
        private bool _refreshIsChecked;
        private bool _historyTabSelected;
        private Device _selectedLog;
        private string _runDirectory;

        private string runDirectory 
            { 
                get=>_runDirectory;
                set
                {
                    _runDirectory=value.Substring(0, value.LastIndexOf("\\", StringComparison.Ordinal));
                }
            }

        private MyDocuments _doc = new MyDocuments();

        public ViewModel()
        {
            //populate dataGrid
            //check program version
            AppVersion.IsUpdated();

            //initialize timer
            _refreshTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            _refreshTimer.Tick += RefreshTimer_Tick;
            RefreshIsChecked = false;
            RefreshBtnText = "Auto Refresh";
            SearchLimitInput="50";
            LogTypeInput="SO";
            SearchHiddenInput=false;
          
            MainLogs = MysqlWorker.GetDevices(QueryCreator.Device(LogReferenceInput, DeviceSnInput,StorageSnInput,CmarInput,LogTypeInput,SearchLimitInput,SearchHiddenInput));

        }

        public int MonthCmar { get; set; }
        public string YearCmar { get; set; }
        public string Cmar { get; set; }
        public List<Device> MainLogs
        {
            get => _mainLogs;
            set
            {
                _mainLogs = value;

                RaisePropertyChanged("MainLogs");
                RaisePropertyChanged("DeviceLogCount");
            }
        }

        public List<Device> HistoryLogs
        {
            get => _historyLogs;
            set
            {
                _historyLogs = value;

                RaisePropertyChanged("HistoryLogs");
                RaisePropertyChanged("HistoryLogCount");
            }
        }

        public List<CmarLog> CmarLogs
        {
            get => _cmarLogs;
            set
            {
                _cmarLogs = value;

                RaisePropertyChanged("CmarLogs");
                RaisePropertyChanged("HistoryLogCount");
            }
        }

        public bool HistoryTabSelected 
            { 
            get => _historyTabSelected;
            set
            {
                _historyTabSelected=value;
                RaisePropertyChanged("HistoryTabSelected");
            }
            }


        public string DeviceLogCount => $"{MainLogs.Count} found";

        public string HistoryLogCount => HistoryLogs!=null? $"{HistoryLogs.Count} found":"";
        public string CmarLogCount => CmarLogs!=null? $"{CmarLogs.Count} found":"";

        public Device SelectedLog
        {
            get => _selectedLog;
            set
            {
                _selectedLog = value;
                RaisePropertyChanged("SelectedLog");
            }
        }
        public string LogTypeInput { get; set; }
        public string LogReferenceInput { get; set; }
        public string DeviceSnInput { get; set; }
        public string StorageSnInput { get; set; }
        public string CmarInput { get; set; }
        public string SearchLimitInput { get; set; }
        public bool SearchHiddenInput { get; set; }

        public bool RefreshIsChecked
        {
            get => _refreshIsChecked;
            set
            {
                _refreshIsChecked = value;
                if (_refreshIsChecked)
                {
                    _refreshCount = 10;
                    _refreshTimer.Start();
                }

                else
                {
                    RefreshStop();
                }
            }
        }

        public string RefreshBtnText
        {
            get => _refreshBtnText;
            set
            {
                _refreshBtnText = value;
                RaisePropertyChanged("RefreshBtnText");
            }
        }


        private void RefreshStop()
        {
            if (_refreshTimer != null && _refreshTimer.IsEnabled)
            {
                RefreshBtnText = "Auto Refresh";
                _refreshTimer.Stop();
                if (RefreshIsChecked)
                    RefreshIsChecked = false;
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            _refreshCount--;
            RefreshBtnText = $"Refresh in {_refreshCount}s";

            if (_refreshCount == 0)
            {
                _refreshTimer.Stop();
                GetDeviceList();
                _refreshCount = 10;

                _refreshTimer.Start();
            }
        }

        private void GetDeviceList()
        {
            //populate dataGrid
            MainLogs = MysqlWorker.GetDevices(QueryCreator.Device(LogReferenceInput, DeviceSnInput,StorageSnInput,CmarInput,LogTypeInput,SearchLimitInput,SearchHiddenInput));

            //stop refresh 
            RefreshStop();
        }
        private void GetDeviceHistory()
        {
            //populate dataGrid
            HistoryLogs = MysqlWorker.GetDevices(QueryCreator.History(SelectedLog.DeviceSerial));

            //stop refresh 
            RefreshStop();

            //focus history tab
            HistoryTabSelected=true;
        }

        private void GetCmarList()
        {
            //populate dataGrid
            CmarLogs = MysqlWorker.GetCmars(QueryCreator.Cmar(MonthCmar,YearCmar,Cmar));
            //stop refresh 
            RefreshStop();
        }


        private void DisplayEditWindow()
        {
            var editWindow = new EditWindow(SelectedLog);
            editWindow.ShowDialog();
            GetDeviceList();
        }

        /// <summary>
        /// Exports displayed datagrid device and open folder where file was saved.
        /// </summary>
        private void ExportDeviceAndOpen(string savedDataName)
        {
            //Creates exel file.
            ExcelExport.DeviceExport(_doc.UniqueFileName(savedDataName), savedDataName, MainLogs);
            //open documents folder.
            Process.Start(_doc.ExportPath);
        }

        /// <summary>
        /// Exports displayed datagrid CMAR and open folder where file was saved.
        /// </summary>
        private void ExportCmarAndOpen(string savedDataName)
        {
            //Creates exel file.
            ExcelExport.CmarExport(_doc.UniqueFileName(savedDataName), savedDataName, CmarLogs);
            //open documents folder.
            Process.Start(_doc.ExportPath);
        }

        #region Commands
        public ICommand ExportDeviceCommand
        {
            get { return new RelayCommand(argument =>ExportDeviceAndOpen("DeviceLogs")); }
        }
        public ICommand ExportCmarCommand
        {
            get { return new RelayCommand(argument =>ExportCmarAndOpen("CmarLogs")); }
        }

        public ICommand EditSelectedCommand
        {
            get { return new RelayCommand(argument => DisplayEditWindow()); }
        }

        public ICommand DeviceSearchCommand
        {
            get { return new RelayCommand(argument => GetDeviceList()); }
        }

        public ICommand CmarSearchCommand
        {
            get { return new RelayCommand(argument => GetCmarList()); }
        }

        public ICommand DeviceHistoryCommand
        {
            get { return new RelayCommand(argument => GetDeviceHistory()); }
        }

        #endregion

        #region INotify Property handler

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}