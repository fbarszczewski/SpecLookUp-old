using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecLookUp.Model
{
    public class Device :INotifyPropertyChanged
    {
        private string _deviceModel;
        public int Id { get; set; }
        public string Rp { get; set; }
        public string So { get; set; }
        public string Comments { get; set; }
        public string Date { get; set; }
        public string Manufacturer { get; set; }
        public string DeviceSerial { get; set; }

        public string DeviceModel
        {
            get => _deviceModel;
            set
            { 
                _deviceModel = value;
                RaisePropertyChanged("DeviceModel");
            }
        }

        public string Chassis { get; set; }
        public string Description
        {
            get => $"{DeviceModel} {Cpu}\n{RamSizeSum}GB/{HddSize.Replace(Environment.NewLine,"/")}/{Optical}/{Resolution}/{OsLabel}";
        }
        public string RamSizeSum { get; set; }
        public string RamSize { get; set; }

        public string RamPn { get; set; }
        public string RamSn { get; set; }
        public string Cpu { get; set; }
        public string HddSize { get; set; }
        public string HddPn { get; set; }
        public string HddSn { get; set; }
        public string HddHealth { get; set; }
        public string Optical { get; set; }
        public string NetDevices { get; set; }
        public string Resolution { get; set; }
        public string Gpu { get; set; }
        public string OsName { get; set; }
        public string OsBuild { get; set; }
        public string OsLang { get; set; }
        public string OsSerial { get; set; }
        public string OsKey { get; set; }
        public string OsLabel { get; set; }
        public string OsCmar { get; set; }
        public string BatteryHealth { get; set; }
        public string BatteryPn { get; set; }
        public string BatterySn { get; set; }
        public string BatteryCharge { get; set; }

        #region INotify Property handler

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
