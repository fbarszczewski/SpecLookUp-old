using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SpecLookUp.DAL;
using SpecLookUp.Model;

namespace SpecLookUp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Device _selectedDevice;
        private List<Device> _deviceList;


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

        public ViewModel()
        {
            GetDeviceList();
            AppVersion.IsUpdated();
        }


        private void GetDeviceList()
        {
            DeviceList = MysqlWorker.GetDevices(QueryCreator.Device(SoTextBox,SnTextBox));
        }

        private void DisplayEditWindow()
        {
            var editWindow=new EditWindow(SelectedDevice);
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

