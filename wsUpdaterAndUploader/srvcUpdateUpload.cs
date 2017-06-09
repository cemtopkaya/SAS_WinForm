using System;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using FMC.Turkiye.CSharp.Extensions;
using FMC.Turkiye.SAS;
using SASLib;

namespace wsUpdaterAndUploader
{
    /*
     * In every 10 minutes, timer_elapsed method will be call and run these:
     * Check Internet Connection
     * Find the DATARETRIEVE_ID which isn't commited to the server
     * Collect the records which are binded the DATARETRIEVE_ID above as a DataTable and put them into a DataSet
     * Send DataSet over web service 
     * if true comes from the web service, it means, alles ok then I'll put SYNC = 1 for the all DATARETRIEVE_ID that I retrieved from db.
     */
    public partial class srvcUpdateUpload : ServiceBase
    {
        public srvcUpdateUpload()
        {
            InitializeComponent();
        }

        Timer timer = new Timer();
        protected override void OnStart(string[] args)
        {
            timer.Interval = 60000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!InternetCS.IsConnectedToInternet())
            {
                return;
            }

            f_CekGonder();
        }

        protected override void OnStop()
        {

        }


        void f_CekGonder()
        {
            DataSet ds = new DataSet("ds");
            DAL dal = new DAL();

            string sqlDataRetrieves = @"
SELECT  DATARETRIEVES.* 
FROM    DATARETRIEVES 
WHERE   SYNC=0
";
            DataTable dtNonSync = dal.f_ExecuteDataTable(sqlDataRetrieves, "DATARETRIEVES");
            ds.Tables.Add(dtNonSync);

            string sDEVICESERIAL = "";
            string sNonSyncRefCekmeIds = "";
            foreach (DataRow row in dtNonSync.Rows)
            {
                sNonSyncRefCekmeIds += row["DATARETRIEVE_ID"] + ",";
                sDEVICESERIAL = row["DEVICESERIAL"].ToString();
            }
            sNonSyncRefCekmeIds = sNonSyncRefCekmeIds.Trim(",".ToCharArray());

            // INSTANTVALUES
            string sqlInstantValues = @"
SELECT  INSTANTVALUES.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL 
FROM    INSTANTVALUES 
WHERE   REFCEKME_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtInstantValues = dal.f_ExecuteDataTable(sqlInstantValues, "INSTANTVALUES");
            ds.Tables.Add(dtInstantValues);

            // LOGS
            string sqlLogs = @"
SELECT  LOGS.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL
FROM    LOGS
WHERE   REFDATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtLogs = dal.f_ExecuteDataTable(sqlLogs, "LOGS");
            ds.Tables.Add(dtLogs);

            // ALARMS
            string sqlAlarms = @"
SELECT  ALARMS.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL
FROM    ALARMS
WHERE   REFDATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtAlarmCount = dal.f_ExecuteDataTable(sqlAlarms, "ALARMS");
            ds.Tables.Add(dtAlarmCount);

            // SETTEDVALUES
            string sqlSettedValues = @"
SELECT  SETTEDVALUES.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL
FROM    SETTEDVALUES
WHERE   REFDATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtSettedValues = dal.f_ExecuteDataTable(sqlSettedValues, "SETTEDVALUES");
            ds.Tables.Add(dtSettedValues);

            // WebServisine gönder
            bool bBasari = f_WebServiseGonder(ds);

            // Başarılı ise SYNC=1 olarak işaretle
            if (bBasari)
            {
                string sqlUpdateDataretrieves = @"
UPDATE  DATARETRIEVES
SET     SYNC=1
WHERE   DATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";
                dal.f_ExecuteNonQuery(sqlUpdateDataretrieves);
            }
        }


        bool f_WebServiseGonder(DataSet _ds)
        {
            bool bSonuc = false;
            try
            {
                VeriAl.VeriAl webSrvc = new VeriAl.VeriAl();
                bSonuc = webSrvc.f_VeriAl(_ds);
            }
            catch (Exception ex)
            {
                //--IS
                MemoryStream ms = new MemoryStream();
                _ds.WriteXml(ms);
                StreamReader sr = new StreamReader(ms);
                ex.LogException(sr.ReadToEnd());

                throw (ex);
            }
            return bSonuc;
        }

    }
}
