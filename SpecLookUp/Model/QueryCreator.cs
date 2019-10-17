using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecLookUp.Model
{
    public class QueryCreator
    {
        private static readonly string _deviceSelect = "SELECT saveReference, serial, cmarLicense, comments, gpu, " +
                                              "model, Cpu, ramSize, ramPN, ramSN, hddSize, hddPN, hddSN, hddHealth, optical, resolution, chassisType," +
                                              "osName, osBuild,osLanguages, osSerial, osLicense,licenseLabel, batteryPN, batteryHealth, batterySerial, batteryCharge, date,rp,id,manufacturer,ramSizeSum  FROM Devices";

        private static readonly string _deviceNotRemoved = "removed ='0'";

        public static string Device(string so,string sn)
        {
            List<string> searchList = new List<string>();

            string whereCmd = " ";

            searchList.Add(_deviceNotRemoved);

            if(!string.IsNullOrWhiteSpace(so))
            {
                searchList.Add($"saveReference LIKE '%{so.Trim()}%'");
            }

            if(!string.IsNullOrWhiteSpace(sn))
            {
                searchList.Add($"serial LIKE '%{sn.Trim()}%'");
            }

            whereCmd = $"WHERE {string.Join(" AND ",searchList)} ";


            return $"{_deviceSelect} {whereCmd} ORDER BY id DESC LIMIT 50";
        }
    }
}
