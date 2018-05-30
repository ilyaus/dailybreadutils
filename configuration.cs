using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace DailyBreadUtil
{
    class configuration
    {
        private string strDBType;
        private string strDBFile;
        private string strConnectionString;
        private string strTempFolder;
        private string strOutFolder;

        public configuration()
        {
            strDBType = "MS SQL";
            strDBFile = "C:\\Users\\Ilya\\Documents\\Bible\\Bible_NIV.mdb";
            strConnectionString = "Data Source=ILYA-PC\\SQLEXPRESS;Initial Catalog=KeyVerse;User Id=ilya;Password=access;";
            strTempFolder = "C:\\Temp\\keyverse\\";
            strOutFolder = "C:\\Users\\Ilya\\Documents\\Bible\\test\\";
        }

        public string DBFile()
        {
            return strDBFile;
        }

        public bool ReadConfig()
        {
            return true;
        }

        public string ConnectionString()
        {
            return strConnectionString;
        }

        public string TempFolder()
        {
            return strTempFolder;
        }

        public string OutFolder()
        {
            return strOutFolder;
        }
    }
}
