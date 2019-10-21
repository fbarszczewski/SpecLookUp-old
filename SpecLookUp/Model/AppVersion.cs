using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SpecLookUp.DAL;

namespace SpecLookUp.Model
{
    public class AppVersion
    {
        private static string _thisAppVer = "1.00";
        public static void IsUpdated()
        {
            if (_thisAppVer != MysqlWorker.CurrentAppVersion())
            {
                MessageBox.Show("There is new version of this app.\n" +
                                $"{MysqlWorker.ChangeLog()}");

                Process.Start("https://www.dropbox.com/sh/svpay4c31hqat4i/AAD-QUtDT4DpKZ03Rlh4YTY5a?dl=0");
            }
        }
    }
}
