using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SpecLookUp.Model;

namespace SpecLookUp.DAL
{
    public class MysqlWorker
    {
        private readonly MySqlConnection _connection;

        private readonly string _connString =
            "SERVER= remotemysql.com" +
            ";USERID= IalS35jGSf" +
            ";PASSWORD= lHxoGp4AQC" +
            ";DATABASE= IalS35jGSf" +
            ";Connection Timeout=10;";

        #region command string

        private readonly string _selectBody = "SELECT saveReference, serial, cmarLicense, comments, gpu, " +
                                              "CONCAT(model,' ',cpu,'/',ramSizeSum,'GB/',REPLACE(hddSize,'\\r\\n','/'),'/',optical,'/',resolution,'/',LicenseLabel) as description, " +
                                              "model, Cpu, ramSize, ramPN, ramSN, hddSize, hddPN, hddSN, hddHealth, optical, resolution, chassisType," +
                                              "osName, osBuild,osLanguages, osSerial, osLicense,licenseLabel, batteryPN, batteryHealth, batterySerial, batteryCharge, date,rp,manufacturer  FROM Devices";

        #endregion

        private MySqlCommand _command;

        public MysqlWorker()
        {
            _connection = new MySqlConnection {ConnectionString = _connString};
        }




        public List<Device> GetDevices(string cmd)
        {
            var dt = new DataTable();
            var devices = new List<Device>();
            try
            {
                _connection.Open();

                _command = new MySqlCommand(cmd, _connection);
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var device = new Device
                    {
                        Id = reader.GetInt32("Id"),
                        Manufacturer = reader.GetString("manufacturer"),
                        DeviceSerial = reader.GetString("serial"),
                        Chassis = reader.GetString("chassisType"),
                        RamSize = reader.GetString("ramSize"),
                        RamSizeSum = reader.GetString("ramSizeSum"),
                        RamPn = reader.GetString("ramPN"),
                        RamSn = reader.GetString("ramSN"),
                        Cpu = reader.GetString("Cpu"),
                        HddSize = reader.GetString("hddSize"),
                        HddPn = reader.GetString("hddPN"),
                        HddSn = reader.GetString("hddSN"),
                        HddHealth = reader.GetString("hddHealth"),
                        Optical = reader.GetString("optical"),
                        Resolution = reader.GetString("resolution"),
                        Gpu = reader.GetString("gpu"),
                        OsName = reader.GetString("osName"),
                        OsBuild = reader.GetString("osBuild"),
                        OsLang = reader.GetString("osLanguages"),
                        OsSerial = reader.GetString("osSerial"),
                        OsKey = reader.GetString("osLicense"),
                        BatteryPn = reader.GetString("batteryPN"),
                        BatteryHealth = reader.GetString("batteryHealth"),
                        BatterySn = reader.GetString("batterySerial"),
                        BatteryCharge = reader.GetString("batteryCharge"),
                        Comments = reader.GetString("comments"),
                        So = reader.GetString("saveReference"),
                        DeviceModel = reader.GetString("model"),
                        OsLabel = reader.GetString("licenseLabel"),
                        OsCmar = reader.GetString("cmarLicense"),
                        Date = reader.GetString("date"),
                        Rp = reader.GetString("rp")
                    };

                    devices.Add(device);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }

            return devices;
        }
    }
}