using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpecLookUp.DAL;
using SpecLookUp.Model;

namespace SpecLookUp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DataTable _specLogList;
        private readonly MysqlWorker _database;
        public DataTable SpecLogList
        {
            get => _specLogList;
            set
            {
                _specLogList = value;
                RaisePropertyChanged("SpecLogList");
            }
        }

        public string SoTextBox { get; set; }
        public string SnTextBox { get; set; }

        public ViewModel()
        {
            _database = new MysqlWorker();
        }



        #region Commands

        public ICommand DeviceSearchCommand
        {
            get { return new RelayCommand(argument => SpecLogList = _database.GetFromDataBase(QueryCreator.Device(SoTextBox,SnTextBox))); }
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
