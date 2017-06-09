using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FMC.Turkiye.CSharp.Extensions
{
    static public class Extensions
    {
        [DebuggerHidden]
        public static T To<T>(this object value) where T : IConvertible
        {
            if (value == DBNull.Value)
                return default(T);

            if (typeof(T) == typeof(bool))
                return (T)Convert.ChangeType(Convert.ToInt32(value), typeof(T));

            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(Convert.ToString(value).Trim(), typeof(T));

            return (T)Convert.ChangeType(value, typeof(T));

        }

        static public string GetString(this byte[] val)
        {
            return Encoding.ASCII.GetString(val);
        }

        static public string GetInt_String(this byte[] val)
        {
            string sResult = "";
            foreach (byte b in val)
            {
                sResult += Convert.ToInt32(b).ToString();
            }
            return sResult;
        }
        static public string GetInt_String(this IEnumerable<byte> val)
        {
            string sResult = "";
            foreach (byte b in val)
            {
                sResult += Convert.ToInt32(b).ToString();
            }
            return sResult;
        }
        static public string GetString(this IEnumerable<byte> val)
        {
            return Encoding.ASCII.GetString(val.ToArray());
        }



        public static void LogToTempFile(this string _str, string _sDescription)
        {
            string sPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SAS.txt";
            StreamWriter sw = null;

            try
            {
                if (!File.Exists(sPath))
                {
                    sw = new StreamWriter(File.Create(sPath));
                }
                else
                {
                    sw = new StreamWriter(sPath, true, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(sPath + " Dosyası oluşturulurken ya da erişilirken istisna oluştu.", ex));
            }

            sw.WriteLine(_sDescription + Environment.NewLine + _str);
            sw.Close();
        }


        public static bool LogException(this Exception _ex, string ErrorDescription)
        {
            string Log = "SAS";

            if ((!(EventLog.SourceExists(Log))))
            {
                EventLog.CreateEventSource(Log, Log);
            }

            string sDesc = ErrorDescription +
                           Environment.NewLine + "Stack Trace:" + _ex.StackTrace +
                           Environment.NewLine + "Message:" + _ex.Message +
                           Environment.NewLine + "Source:" + _ex.Source;
            new EventLog
                {
                    Source = Log
                }.WriteEntry(sDesc, EventLogEntryType.Error);

            EventLogEntryCollection col = new EventLog("SAS").Entries;
            return col[col.Count - 1].Message == sDesc;
        }

        public static string ReverseString(this string s)
        {
            char[] carr = s.ToCharArray();
            Array.Reverse(carr);
            return new string(carr);
            string sResult = "";
            for (int i = s.Length - 1; i >= 0; i--)
            {
                sResult += s[i];
            }
            return sResult;
        }

        public static string SqlAsteriks(this string s)
        {
            string sResult = "";
            for (int i = 0; i < s.Length; i++)
            {
                sResult += s[i] + "_";
            }
            return sResult;
        }
    }
}
