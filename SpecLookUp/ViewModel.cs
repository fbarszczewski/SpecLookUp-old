using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecLookUp.DAL;
using SpecLookUp.Model;

namespace SpecLookUp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DataTable _specLogList;
        private MysqlWorker _database;
        public DataTable SpecLogList
        {
            get => _specLogList;
            set
            {
                _specLogList = value;
                RaisePropertyChanged("SpecLogList");
            }
        }

        public SpecLog SpecLogSelected { get; set; }

        public ViewModel()
        {
            _database=new MysqlWorker();
            SpecLogList = _database.GetFromDataBase("SELECT * FROM Devices");
        }


        #region INotify Property handler

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
