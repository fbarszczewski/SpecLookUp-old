using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace SpecLookUp.Model
{
    public class MyDocuments
    {
        private readonly string _myDocumentsPath;
        private readonly string _folderName = "SpecSniffer Exports";
        public string  ExportPath { get=> $"{_myDocumentsPath}\\{_folderName}"; }
        public MyDocuments()
        {
             _myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FindOrCreate();
        }

        /// <summary>
        /// Checks if 'Export' folder exists in My Documents.
        /// </summary>
        private bool FolderExists()
        {
            return Directory.Exists($"{_myDocumentsPath}\\{_folderName}") ? true : false;
        }

        /// <summary>
        /// Create 'Export' folder in My Documents.
        /// </summary>
        private void CreateFolder()
        {
            try
            {
                Directory.CreateDirectory($"{_myDocumentsPath}\\{_folderName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Search for 'Export' folder in my documents, if not found creates one.
        /// </summary>
        private void FindOrCreate()
        {

            if(!FolderExists())
                CreateFolder();
        }

        /// <summary>
        /// Returns number of files in 'Export' folder.
        /// </summary>
        private int FilesCount()
        {
            return Directory.GetFiles(ExportPath).Count();
        }

        /// <summary>
        /// Creates unique, in Export folder name
        /// </summary>
        public string UniqueFileName(string savedDataName)
        {
            //Number of files in Export folder.
            int fileCount= FilesCount();

            // Increasing fileCount till filename is unique.
            while(FileExists($"{ExportPath}\\{savedDataName}{fileCount}.xlsx"))
            {
                fileCount++;
            }

            //return unique name
            return $"{ExportPath}\\{savedDataName}{fileCount}.xlsx";
        }

        /// <summary>
        /// Check if this file name exists in My Documents.
        /// </summary>
        private bool FileExists(string path)
        {
            return File.Exists(path) ? true : false;
        }
    }
}
