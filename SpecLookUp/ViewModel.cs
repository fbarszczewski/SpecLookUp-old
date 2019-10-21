using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SpecLookUp.DAL;
using SpecLookUp.Model;

namespace SpecLookUp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _refreshTimer;
        private List<Device> _deviceList;
        private string _refreshBtnText;
        private int _refreshCount = 10;
        private bool _refreshIsChecked;
        private Device _selectedDevice;

        public ViewModel()
        {

            GetDeviceList();
            AppVersion.IsUpdated();

            _refreshTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            _refreshTimer.Tick += RefreshTimer_Tick;
            RefreshIsChecked = false;
            RefreshBtnText = "Auto Refresh";
        }


        public List<Device> DeviceList
        {
            get => _deviceList;
            set
            {
                _deviceList = value;
                RaisePropertyChanged("DeviceList");
            }
        }

        public Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                RaisePropertyChanged("SelectedDevice");
            }
        }

        public string SoTextBox { get; set; }
        public string SnTextBox { get; set; }

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
                    if (_refreshTimer != null && _refreshTimer.IsEnabled)
                    {
                        RefreshBtnText = "Auto Refresh";
                        _refreshTimer.Stop();
                    }
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


        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            _refreshCount--;
            RefreshBtnText = $"Refresh in {_refreshCount}s";

            if (_refreshCount == 0)
            {
                _refreshCount = 10;
                GetDeviceList();
                MessageBox.Show("Test");
            }
        }

        private void GetDeviceList()
        {
            DeviceList = MysqlWorker.GetDevices(QueryCreator.Device(SoTextBox, SnTextBox));
        }

        private void DisplayEditWindow()
        {
            var editWindow = new EditWindow(SelectedDevice);
            editWindow.ShowDialog();
            GetDeviceList();
        }


        #region Commands

        public ICommand EditSelectedCommand
        {
            get { return new RelayCommand(argument => DisplayEditWindow()); }
        }

        public ICommand SearchCommand
        {
            get { return new RelayCommand(argument => GetDeviceList()); }
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