using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SASLib
{
    public class Logger
    {
        public static void LogException(string ErrorDescription)
        {
            string Log = "SAS";

            if ((!(EventLog.SourceExists(Log))))
            {
                EventLog.CreateEventSource(Log, Log);
            }

            new EventLog
                {
                    Source = Log
                }.WriteEntry(ErrorDescription, EventLogEntryType.Error);
        }

        static public DataTable GetEventLogs(string _sLogTopic)
        {
            if (!EventLog.Exists(_sLogTopic))
            {
                MessageBox.Show(_sLogTopic + " İsimli kayıt bulunamadı.", "Veri Yok", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable("Logs");
                dt.Columns.Add(new DataColumn("Tipi", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Mesaj", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Hata Tarihi", typeof(System.DateTime)));

                EventLog log = new EventLog(_sLogTopic);
                foreach (EventLogEntry entry in log.Entries)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = entry.EntryType.ToString();
                    dr[1] = entry.Message;
                    dr[2] = entry.TimeGenerated;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return null;
        }
    }
}
