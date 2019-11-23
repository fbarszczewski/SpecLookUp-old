using System.Collections.Generic;

namespace SpecLookUp.Model
{
    public class QueryCreator
    {
        private const string QueryBody = "SELECT " +
                                            "saveReference, serial, cmarLicense, comments, gpu, " +
                                            "model, Cpu, ramSize, ramPN, ramSN, " +
                                            "hddSize, hddPN, hddSN, hddHealth, " +
                                            "optical, resolution, chassisType," +
                                            "osName, osBuild,osLanguages, osSerial, osLicense,licenseLabel, " +
                                            "batteryPN, batteryHealth, batterySerial, batteryCharge, " +
                                            "DATE_FORMAT(TIMESTAMP(date,'1:00:00'),'%T %d/%m/%y') as date,rp,id,manufacturer,ramSizeSum,logType,visible  " +
                                            "FROM Devices";

        //DATE_FORMAT(TIMESTAMP(date,'2:00:00'),'%d/%m %T') as timeStamp,
        public static string Device(string so, string sn, string hdd,string cmar ,string logType, string limit, bool deepSearch)
        {
            var searchList = new List<string>();
            string query="";


            
            //search hidden logs if false

            if (!string.IsNullOrWhiteSpace(so)) searchList.Add($"saveReference LIKE '%{so.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(sn)) searchList.Add($"serial LIKE '%{sn.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(hdd)) searchList.Add($"hddSN LIKE '%{hdd.Trim()}%'");
            if (!string.IsNullOrWhiteSpace(cmar)) searchList.Add($"cmarLicense LIKE '%{cmar.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(logType)&&logType!="Any") searchList.Add($"logType = '{logType.Trim()}'");

            //if (!string.IsNullOrWhiteSpace(logType)) searchList.Add($"logType = '{logType.Trim()}'");


            if (!deepSearch) searchList.Add($"visible = '0'");

            if (searchList.Count>0)
            {
                query = $"{QueryBody} WHERE {string.Join(" AND ", searchList)} ORDER BY id DESC LIMIT {limit} ";
            }
            else
            {
                query = $"{QueryBody}  ORDER BY id DESC LIMIT {limit} ";
            }


            return query;
        }

        /// <summary>
        /// Query creator for search all devices logs by serial number
        /// </summary>
        public static string History(string sn)
        {
            return $"{QueryBody} WHERE serial='{sn}' ORDER BY id DESC";
        }
    }


}