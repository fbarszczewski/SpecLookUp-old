using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpecLookUp.Converters;
using System.Collections.Generic;

namespace SpecLookUp.Model
{
    public static class ExcelExport
    {


        public static void DeviceExport(string savePath,string fileName, List<Device> deviceLogs)
        {
           using (SpreadsheetDocument document = SpreadsheetDocument.Create(savePath, SpreadsheetDocumentType.Workbook))
           {
                // Add a WorkbookPart to the document.
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                // Add Scheets to WorkPart
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                //Create new scheet.
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = fileName };
                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                sheetData.AppendChild(DeviceRowHeader());

                foreach (Device log in deviceLogs)
                {
                    sheetData.AppendChild(ConstructDeviceRow(log));
                }

                worksheetPart.Worksheet.Save();
           }
        }

        public static void CmarExport(string savePath,string fileName, List<CmarLog> cmarLogs)
        {
           using (SpreadsheetDocument document = SpreadsheetDocument.Create(savePath, SpreadsheetDocumentType.Workbook))
           {
                // Add a WorkbookPart to the document.
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                // Add Scheets to WorkPart.
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                //Create new scheet.
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = fileName };
                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                sheetData.AppendChild(CmarRowHeader());

                foreach (CmarLog log in cmarLogs)
                {
                    sheetData.AppendChild(ConstructCmarRow(log));
                }

                worksheetPart.Worksheet.Save();
           }
        }



        /// <summary>
        /// Creates row header for Device logs.
        /// </summary>
        private static Row DeviceRowHeader()
        {
            //create new row
            Row rowHeader = new Row();

            //add value to cells
            rowHeader.Append(
           ConstructCell("Log ID", CellValues.String),
           ConstructCell("TimeStamp", CellValues.String),
           ConstructCell("log Type", CellValues.String),
           ConstructCell("Ref. ID", CellValues.String),
           ConstructCell("RP", CellValues.String),
           ConstructCell("Description", CellValues.String),
           ConstructCell("Device Serial", CellValues.String),
           ConstructCell("CMAR", CellValues.String),
           ConstructCell("Storage Serial", CellValues.String),
           ConstructCell("Storage Name", CellValues.String),
           ConstructCell("Storage Size", CellValues.String),
           ConstructCell("Storage Health", CellValues.String),
           ConstructCell("Comment's", CellValues.String),
           ConstructCell("GPU", CellValues.String),
           ConstructCell("Resolution", CellValues.String),
           ConstructCell("Device Model", CellValues.String),
           ConstructCell("CPU", CellValues.String),
           ConstructCell("Memory Bank's", CellValues.String),
           ConstructCell("Memory Serial", CellValues.String),
           ConstructCell("Memory PN", CellValues.String),
           ConstructCell("Optical", CellValues.String),
           ConstructCell("OS Name", CellValues.String),
           ConstructCell("OS Build", CellValues.String),
           ConstructCell("OS Language", CellValues.String),
           ConstructCell("OS Install ID", CellValues.String),
           ConstructCell("OS License Key", CellValues.String),
           ConstructCell("OS Label", CellValues.String),
           ConstructCell("Battery Health", CellValues.String),
           ConstructCell("Battery Charge", CellValues.String),
           ConstructCell("Battery PN", CellValues.String),
           ConstructCell("Is Hidden", CellValues.String)
           );

            return rowHeader;
        }

        /// <summary>
        /// Creates row header for Cmar logs.
        /// </summary>
        private static Row CmarRowHeader()
        {
            //create new row
            Row rowHeader = new Row();

            //add value to cells
            rowHeader.Append(
           ConstructCell("Log ID", CellValues.String),
           ConstructCell("Install Date", CellValues.String),
           ConstructCell("RP", CellValues.String),
           ConstructCell("SO", CellValues.String),
           ConstructCell("License Type", CellValues.String),
           ConstructCell("New License", CellValues.String),
           ConstructCell("Old License", CellValues.String),
           ConstructCell("Manufacturer", CellValues.String),
           ConstructCell("Model", CellValues.String),
           ConstructCell("Device Type", CellValues.String),
           ConstructCell("Serial", CellValues.String),
           ConstructCell("CPU", CellValues.String)
           );

            return rowHeader;
        }

        /// <summary>
        /// Construct Row from given Device object.
        /// </summary>
        private static Row ConstructDeviceRow(Device log)
        {
            //create new row
            Row row = new Row();

            row.Append(
           ConstructCell(log.Id.ToString(), CellValues.Number),
           ConstructCell(log.Date, CellValues.String),
           ConstructCell(log.LogType, CellValues.String),
           ConstructCell(log.So, CellValues.String),
           ConstructCell(log.Rp, CellValues.String),
           ConstructCell(log.Description, CellValues.String),
           ConstructCell(log.DeviceSerial, CellValues.String),
           ConstructCell(log.OsCmar, CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.HddSn), CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.HddPn), CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.HddSize), CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.HddHealth), CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.Comments), CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.Gpu), CellValues.String),
           ConstructCell(log.Resolution, CellValues.String),
           ConstructCell(log.DeviceModel, CellValues.String),
           ConstructCell(log.Cpu, CellValues.String),
           ConstructCell(log.RamSize, CellValues.String),
           ConstructCell(log.RamSn, CellValues.String),
           ConstructCell(log.RamPn, CellValues.String),
           ConstructCell(StringConverter.NewLineToSlash(log.Optical), CellValues.String),
           ConstructCell(log.OsName, CellValues.String),
           ConstructCell(log.OsBuild, CellValues.String),
           ConstructCell(log.OsLang, CellValues.String),
           ConstructCell(log.OsSerial, CellValues.String),
           ConstructCell(log.OsKey, CellValues.String),
           ConstructCell(log.OsLabel, CellValues.String),
           ConstructCell(log.BatteryHealth, CellValues.String),
           ConstructCell(log.BatteryCharge, CellValues.String),
           ConstructCell(log.BatteryPn, CellValues.String),
           ConstructCell(log.LogHiden.ToString(), CellValues.String)
           );

            return row;
        }

        /// <summary>
        /// Construct Row from given CMARlogs object.
        /// </summary>
        private static Row ConstructCmarRow(CmarLog log)
        {
            //create new row
            Row row = new Row();

            row.Append(
           ConstructCell(log.Id.ToString(), CellValues.String),
           ConstructCell(log.InstallDate, CellValues.String),
           ConstructCell(log.Rp, CellValues.String),
           ConstructCell(log.So, CellValues.String),
           ConstructCell(log.LicenseType, CellValues.String),
           ConstructCell(log.Cmar, CellValues.String),
           ConstructCell(log.OldCoa, CellValues.String),
           ConstructCell(log.Manufacturer, CellValues.String),
           ConstructCell(log.Model, CellValues.String),
           ConstructCell(log.DeviceType, CellValues.String),
           ConstructCell(log.Serial, CellValues.String),
           ConstructCell(log.Cpu, CellValues.String)
           );

            return row;
        }

        private static Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }



    }
}
