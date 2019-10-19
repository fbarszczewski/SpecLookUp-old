using System.Collections.Generic;

namespace SpecLookUp.Model
{
    public class QueryCreator
    {
        private const string DeviceSelect = "SELECT " +
                                            "saveReference, serial, cmarLicense, comments, gpu, " +
                                            "model, Cpu, ramSize, ramPN, ramSN, " +
                                            "hddSize, hddPN, hddSN, hddHealth, " +
                                            "optical, resolution, chassisType," +
                                            "osName, osBuild,osLanguages, osSerial, osLicense,licenseLabel, " +
                                            "batteryPN, batteryHealth, batterySerial, batteryCharge, " +
                                            "date,rp,id,manufacturer,ramSizeSum  " +
                                            "FROM Devices";

        private const string Visible = "visible ='0'";

        public static string Device(string so, string sn)
        {
            var searchList = new List<string>();

            var whereCmd = " ";

            searchList.Add(Visible);

            if (!string.IsNullOrWhiteSpace(so)) searchList.Add($"saveReference LIKE '%{so.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(sn)) searchList.Add($"serial LIKE '%{sn.Trim()}%'");

            whereCmd = $"WHERE {string.Join(" AND ", searchList)} ";


            return $"{DeviceSelect} {whereCmd} ORDER BY id DESC LIMIT 50";
        }
    }
}