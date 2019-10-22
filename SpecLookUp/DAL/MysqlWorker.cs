using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using SpecLookUp.Model;

namespace SpecLookUp.DAL
{
    public static class MysqlWorker
    {
        private const string ConnString = "SERVER = remotemysql.com" + ";USERID= IalS35jGSf" + ";PASSWORD= lHxoGp4AQC" +
                                          ";DATABASE= IalS35jGSf" + ";Connection Timeout=10;";

        private static readonly MySqlConnection Connection = new MySqlConnection {ConnectionString = ConnString};


        public static List<Device> GetDevices(string cmd)
        {
            var devices = new List<Device>();
            var command = new MySqlCommand(cmd, Connection);

            try
            {
                Connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                    devices.Add(new Device
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
                    });
            }
            catch (Exception e)
            {
                devices.Add(new Device {DeviceModel = e.Message});
            }
            finally
            {
                if (Connection != null) Connection.Close();
            }

            command.Dispose();
            return devices;
        }

        public static bool UpdateDevice(Device selectedDevice)
        {
            var succeed = false;
            var cmd = new MySqlCommand();

            try
            {
                Connection.Open();
                cmd.Connection = Connection;
                cmd.CommandText = "INSERT INTO Devices " +
                                  "(manufacturer,serial,model,chassisType," +
                                  "ramSizeSum,ramSize,ramPN,ramSN," +
                                  "Cpu,hddSize, hddPN, hddSN, hddHealth, " +
                                  "optical, netDevices, resolution, gpu, osName, " +
                                  "osBuild, osLanguages, osSerial, osLicense, batteryPN, " +
                                  "batteryHealth, batterySerial, batteryCharge, deviceList, date," +
                                  "comments, saveReference, rp, licenseLabel, cmarLicense ) " +
                                  "SELECT " +
                                  "manufacturer,serial,model,chassisType," +
                                  "ramSizeSum,ramSize,ramPN,ramSN," +
                                  "Cpu,hddSize, hddPN, hddSN, hddHealth, " +
                                  "optical, netDevices, resolution, gpu, osName, " +
                                  "osBuild, osLanguages, osSerial, osLicense, batteryPN, " +
                                  "batteryHealth, batterySerial, batteryCharge, deviceList, date," +
                                  "@comments, @saveReference, @rp, @licenseLabel, @cmarLicense  " +
                                  "FROM Devices " +
                                  "WHERE id=@id";

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@comments", selectedDevice.Comments);
                cmd.Parameters.AddWithValue("@saveReference", selectedDevice.So);
                cmd.Parameters.AddWithValue("@rp", selectedDevice.Rp);
                cmd.Parameters.AddWithValue("@licenseLabel", selectedDevice.OsLabel);
                cmd.Parameters.AddWithValue("@cmarLicense", selectedDevice.OsCmar);
                cmd.Parameters.AddWithValue("@id", selectedDevice.Id);

                cmd.ExecuteNonQuery();
                succeed = true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Couldn't update log\n{e.Message}");
            }
            finally
            {
                if (Connection != null) Connection.Close();
            }

            if (succeed)
                HideDevice(selectedDevice.Id.ToString());

            cmd.Dispose();
            return succeed;
        }

        public static bool HideDevice(string deviceId)
        {
            var succeed = false;
            var cmd = new MySqlCommand();

            try
            {
                Connection.Open();
                cmd.Connection = Connection;
                cmd.CommandText = "UPDATE Devices SET visible='1' WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", deviceId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                succeed = true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Couldn't hide old log\n{e.Message}");
            }
            finally
            {
                if (Connection != null) Connection.Close();
            }

            cmd.Dispose();
            return succeed;
        }

        public static string CurrentAppVersion()
        {
            var ver = "";
            var cmd = new MySqlCommand();

            try
            {
                Connection.Open();
                cmd.Connection = Connection;
                cmd.CommandText = "SELECT ver FROM Version WHERE app='viewer'";
                var dr = cmd.ExecuteReader();
                dr.Read();
                ver = dr.GetValue(0).ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\n{e.InnerException}");
            }
            finally
            {
                if (Connection != null) Connection.Close();
            }

            cmd.Dispose();
            return ver;
        }

        public static string ChangeLog()
        {
            var changelog = "";
            var cmd = new MySqlCommand();

            try
            {
                Connection.Open();
                cmd.Connection = Connection;
                cmd.CommandText = "SELECT changelog FROM Version WHERE app='viewer'";
                var dr = cmd.ExecuteReader();
                dr.Read();
                changelog = dr.GetValue(0).ToString().Replace("//", "\n");
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (Connection != null) Connection.Close();
            }

            cmd.Dispose();
            return changelog;
        }

        public static bool DatabaseConnection()
        {
            var connected = false;
            try
            {
                Connection.Open();
                connected = true;
            }
            catch
            {
            }
            finally
            {
                if (Connection != null) Connection.Close();
            }

            return connected;
        }
    }
}