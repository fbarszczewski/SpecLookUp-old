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
                                              "CONCAT(model,' ',cpu,'/',ramSizeSum,'GB/',REPLACE(hddSize,'\\r\\n','/'),'/',optical,'/',resolution,'/',LicenseLabel) as description, " +
                                              "model, Cpu, ramSize, ramPN, ramSN, hddSize, hddPN, hddSN, hddHealth, optical, resolution, chassisType," +
                                              "osName, osBuild,osLanguages, osSerial, osLicense,licenseLabel, batteryPN, batteryHealth, batterySerial, batteryCharge, date,rp  FROM Devices";

        public static string Device(string so,string sn)
        {
            List<string> searchList = new List<string>();

            string whereCmd = " ";

            if(!string.IsNullOrWhiteSpace(so))
            {
                searchList.Add($"saveReference LIKE '%{so.Trim()}%'");
            }

            if(!string.IsNullOrWhiteSpace(sn))
            {
                searchList.Add($"serial LIKE '%{sn.Trim()}%'");
            }

            if(searchList.Count>0)
            {
                whereCmd = $"WHERE {string.Join(" AND ",searchList)} ";
            }

            return $"{_deviceSelect} {whereCmd} ORDER BY id DESC LIMIT 100";
        }
    }
}
