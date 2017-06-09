using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using FMC.Turkiye.CSharp.Extensions;
using FirebirdSql.Data.FirebirdClient;

namespace FMC.Turkiye.SAS
{
    public class DAL : IDisposable
    {
        public FbCommand command = new FbCommand();
        public FbConnection connection;

        public string M_CnnStr
        {
            get
            {
                return DAL.f_ConnectionString();
            }
        }


        public DAL()
        {
            try
            {
                connection = f_PrepareConnection("");
                command = f_PrepareCommand("");
            }
            catch (Exception ex)
            {
                ex.LogException("DAL oluşturulurken istisna fırlatıldı.");
                throw (ex);
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }

        public List<Alarm> M_Alarm { get; set; }
        public List<Log> M_Log { get; set; }
        public InstantValues M_InstantValues { get; set; }


        public bool f_Validation()
        {
            try
            {
                if (M_Log != null)
                {
                    return M_Log.Where(p => p.M_StopByte != "#").FirstOrDefault() != null
                               ? true
                               : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region SQL
        /// <summary>
        /// Geri dönüşü sadece int olan INSERT, UPDATE gibi sorguları çalıştırır.
        /// </summary>
        /// <param name="_sSQL"></param>
        /// <returns></returns>
        public int f_ExecuteNonQuery(string _sSQL)
        {
            f_PrepareConnectionAndCommand(_sSQL);
            return command.ExecuteNonQuery();
        }

        public int f_ExecuteNonQuery(FbCommand _cmd)
        {
            f_PrepareConnectionAndCommand(_cmd);
            int i;
            using (FbTransaction transaction = connection.BeginTransaction())
            {
                command.Transaction = transaction;
                i = command.ExecuteNonQuery();
                transaction.Commit();
            }
            return i;
        }


        /// <summary>
        /// Tek satır DataRow almak için kullanılır.
        /// </summary>
        /// <param name="_sSQL"></param>
        /// <returns></returns>
        public DataRow f_ExecuteOneRow(string _sSQL)
        {
            return f_ExecuteDataTable(_sSQL).Rows[0];
        }

        /// <summary>
        /// SQL cümlesini DataReader'a çevirir.
        /// </summary>
        /// <param name="_sSQL"></param>
        /// <returns></returns>
        public FbDataReader f_ExecuteReader(string _sSQL)
        {
            try
            {
                f_PrepareConnectionAndCommand(_sSQL);

                FbDataReader fbdr = command.ExecuteReader(CommandBehavior.CloseConnection);
                return fbdr;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// SQL cümlesinin tüm satırlarını DataTable olarak almak için kullanılır.
        /// </summary>
        /// <param name="_sSQL"></param>
        /// <returns></returns>
        public DataTable f_ExecuteDataTable(string _sSQL)
        {
            try
            {
                using (FbDataReader dr = f_ExecuteReader(_sSQL))
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                command.Dispose();
            }
        }
        /// <summary>
        /// SQL cümlesinin tüm satırlarını DataTable olarak almak için kullanılır.
        /// </summary>
        /// <param name="_sSQL"></param>
        /// <returns></returns>
        public DataTable f_ExecuteDataTable(string _sSQL, string _sDataTableName)
        {
            try
            {
                using (FbDataReader dr = f_ExecuteReader(_sSQL))
                {
                    DataTable dt = new DataTable(_sDataTableName);
                    dt.Load(dr);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                command.Dispose();
            }
        }



        private FbConnection f_PrepareConnection(string _sSQL)
        {
            try
            {
                if (connection == null)
                {
                    connection = new FbConnection(M_CnnStr);
                }


                if (connection != null && connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Veritabanı bağlantısı hazırlanırken hata oluştu.");
                throw (new Exception("Veritabanına bağlantı açılamadı. Kullanımda olabilir.", ex));
            }

            return connection;
        }

        private FbConnection f_PrepareConnection(FbCommand _cmd)
        {
            try
            {
                if (connection == null)
                {
                    connection = _cmd.Connection;
                }


                if (connection != null && connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Veritabanı bağlantısı hazırlanırken hata oluştu.");
                throw (new Exception("Veritabanına bağlantı açılamadı. Kullanımda olabilir.", ex));
            }

            return connection;
        }

        private FbCommand f_PrepareCommand(string _sSQL)
        {
            command = new FbCommand(_sSQL ?? "", connection);
            command.Connection = connection;
            command.CommandText = _sSQL ?? "";
            command.CommandTimeout = 1000;
            return command;
        }

        private FbCommand f_PrepareCommand(FbCommand _cmd)
        {
            if (command == null)
            {
                if (_cmd != null)
                {
                    command = _cmd;
                }
            }

            return command;
        }

        private void f_PrepareConnectionAndCommand(string _sSQL)
        {
            f_PrepareConnection(_sSQL);
            f_PrepareCommand(_sSQL);
        }

        private void f_PrepareConnectionAndCommand(FbCommand _cmd)
        {
            f_PrepareConnection(_cmd);
            f_PrepareCommand(_cmd);
        }
        #endregion

        public void f_test()
        {
            //InstantValues.f_InsertTest(command, 40);
            using (FbTransaction transaction = connection.BeginTransaction())
            {
                command.Transaction = transaction;
                int i = command.ExecuteNonQuery();
                transaction.Commit();
            }
        }

        public int f_InsertAll(int _iRefDataRetrieve_id, List<Alarm> _alarm, List<Log> _log, InstantValues _instantValues, SettedValues _settedValues)
        {
            int iEtkilenen = 0;
            try
            {
                f_PrepareConnectionAndCommand("");
                iEtkilenen += f_ExecuteNonQuery(_instantValues.f_Insert(command, _iRefDataRetrieve_id));

                foreach (Log log in _log)
                {
                    iEtkilenen += f_ExecuteNonQuery(log.f_Insert(command, _iRefDataRetrieve_id));
                }

                foreach (Alarm alarm in _alarm)
                {
                    iEtkilenen += f_ExecuteNonQuery(alarm.f_Insert(command, _iRefDataRetrieve_id));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return iEtkilenen;
        }

        public object f_GetOneCell(string _sSQL)
        {
            f_PrepareConnectionAndCommand(_sSQL);
            return command.ExecuteScalar();
        }

        public DataTable f_GetGirisIletkenligi(DateTime _dtBas, DateTime _dtSon, string _sDeviceSerial)
        {
            string sLog = @"
SELECT     LOGS.FULLDATE, LOGS.INLETCONDUCTIVITY
FROM       LOGS 
INNER JOIN DATARETRIEVES ON DATARETRIEVES.DATARETRIEVE_ID = LOGS.REFDATARETRIEVE_ID
WHERE      DATARETRIEVES.DEVICESERIAL  = '" + _sDeviceSerial + @"'
AND        LOGS.FULLDATE BETWEEN '" + _dtBas.ToString("dd.MM.yyyy hh:mm:ss") + "' AND '" + _dtSon.ToString("dd.MM.yyyy hh:mm:ss") + @"' 
ORDER BY   LOGS.FULLDATE ASC;";
            f_PrepareConnectionAndCommand("");
            DataTable dt = f_ExecuteDataTable(sLog);
            return dt;
        }

        public DataTable f_GetUretimIletkenligi(DateTime _dtBas, DateTime _dtSon, string _sDeviceSerial)
        {
            string sLog = @"
SELECT     LOGS.FULLDATE, LOGS.PRODUCTINCONDUCTIVITY
FROM       LOGS 
INNER JOIN DATARETRIEVES ON DATARETRIEVES.DATARETRIEVE_ID = LOGS.REFDATARETRIEVE_ID
WHERE      DATARETRIEVES.DEVICESERIAL  = '" + _sDeviceSerial + @"'
AND        LOGS.FULLDATE BETWEEN '" + _dtBas.ToString("dd.MM.yyyy hh:mm:ss") + "' AND '" + _dtSon.ToString("dd.MM.yyyy hh:mm:ss") + @"' 
ORDER BY   LOGS.FULLDATE ASC;";
            f_PrepareConnectionAndCommand(sLog);
            DataTable dt = f_ExecuteDataTable(sLog);
            return dt;
        }

        public DataTable f_GetSicaklik(DateTime _dtBas, DateTime _dtSon, string _sDeviceSerial)
        {
            string sLog = @"
SELECT     LOGS.FULLDATE, LOGS.TEMPERATURE
FROM       LOGS 
INNER JOIN DATARETRIEVES ON DATARETRIEVES.DATARETRIEVE_ID = LOGS.REFDATARETRIEVE_ID
WHERE      DATARETRIEVES.DEVICESERIAL  = '" + _sDeviceSerial + @"'
AND        LOGS.FULLDATE BETWEEN '" + _dtBas.ToString("dd.MM.yyyy hh:mm:ss") + "' AND '" + _dtSon.ToString("dd.MM.yyyy hh:mm:ss") + @"' 
ORDER BY   LOGS.FULLDATE ASC;";
            f_PrepareConnectionAndCommand(sLog);
            DataTable dt = f_ExecuteDataTable(sLog);
            return dt;
        }

        public DataTable f_GetVerim(DateTime _dtBas, DateTime _dtSon, string _sDeviceSerial)
        {
            string sLog = @"
SELECT     LOGS.FULLDATE, CAST((LOGS.INLETCONDUCTIVITY-LOGS.PRODUCTINCONDUCTIVITY)  AS decimal(2,2)) * 100 / LOGS.INLETCONDUCTIVITY 
FROM       LOGS 
INNER JOIN DATARETRIEVES ON DATARETRIEVES.DATARETRIEVE_ID = LOGS.REFDATARETRIEVE_ID
WHERE      LOGS.INLETCONDUCTIVITY>0 AND LOGS.PRODUCTINCONDUCTIVITY>0
AND        DATARETRIEVES.DEVICESERIAL  = '" + _sDeviceSerial + @"'
AND        LOGS.FULLDATE BETWEEN '" + _dtBas.ToString("dd.MM.yyyy hh:mm:ss") + "' AND '" + _dtSon.ToString("dd.MM.yyyy hh:mm:ss") + @"' 
ORDER BY   LOGS.FULLDATE ASC;";
            f_PrepareConnectionAndCommand(sLog);
            DataTable dt = f_ExecuteDataTable(sLog);
            return dt;
        }

        /// <summary>
        /// AssemblyDirectory metodu ile dll'imizin yerini tespit ediyoruz. VT mizde aynı klasörde olduğuna göre sonuna sadece SAS.FDB eklememiz yeterli.
        /// </summary>
        /// <returns>c:\\program files\\SAS gibi bir dizin yolu dönecek.</returns>
        public static string f_AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// VT mize (SAS.FDB) bağlanmamız için gerekli connection string.
        /// </summary>
        /// <returns></returns>
        public static string f_ConnectionString()
        {
            //string sDbFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + sDbFileName;
            //return "Data Source= " + sDbFilePath + ";Password=q1w2e3r4.";

            try
            {
                //get the full location of the assembly with DaoTests in it
                string fullPath = System.Reflection.Assembly.GetAssembly(typeof(DAL)).Location;

                //get the folder that's in
                string theDirectory = Path.GetDirectoryName(fullPath);
                
                theDirectory.LogToTempFile("Veritabanı dosyasının bulunduğu dizin.");

                string sPath = theDirectory + @"\SAS.FDB";
                if (!File.Exists(sPath))
                {
                    throw new Exception("Veritabanına erişilemedi. Uygulama veritabanı olmaksızın çalışamaz!!!");
                }

                //sPath = ConfigurationManager.AppSettings["dbPath"].ToString();
                FbConnectionStringBuilder cnnStr = new FbConnectionStringBuilder();
                cnnStr.Database = sPath;
                cnnStr.ServerType = FbServerType.Embedded;
                cnnStr.UserID = "SYSDBA";
                cnnStr.Password = "masterkey";
                
                //cnnStr.ToString().LogToTempFile("Veritabanına erişilirken kullanılacak connectionstring!");
                
                return cnnStr.ToString();
            }
            catch (Exception ex)
            {
                ex.LogException("Veritabanı dosyasına erişimde istisna fırlatıldı.");
                throw(ex);
            }
        }

        /// <summary>
        /// _sDeviceSerial null ya da boş olduğunda son çekilen verileri getirecek, 
        /// dolu olduğunda _sDeviceSerial'a bağlı son çekimin bilgilerini getirecek.
        /// </summary>
        /// <param name="_sDeviceSerial"></param>
        /// <returns></returns>
        public DataTable f_GetLastInstantValuesOfLastDataretrieve(string _sDeviceSerial)
        {
            try
            {
                string sqlLastRetrieveId = @"
SELECT    MAX(DATARETRIEVES.DATARETRIEVE_ID) 
FROM      DATARETRIEVES 
WHERE     DATARETRIEVES.DEVICESERIAL = '" + _sDeviceSerial + @"' ";
                if (string.IsNullOrEmpty(_sDeviceSerial))
                {
                    sqlLastRetrieveId = @"
SELECT    MAX(DATARETRIEVES.DATARETRIEVE_ID) 
FROM      DATARETRIEVES ";
                }

                int iDataretrieveId = Convert.ToInt32(this.f_GetOneCell(sqlLastRetrieveId));
                string sqlLastInstantValues = "SELECT * FROM INSTANTVALUES WHERE INSTANTVALUES.REFCEKME_ID=" + iDataretrieveId;

                DataTable dt = f_ExecuteDataTable(sqlLastInstantValues);
                return dt;
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Son çekime ait anlık değerleri çekerken genel istisna oluştu.");
                throw (ex);
            }
        }

        public DataTable f_GetLastAlarmsOfLastDataretrieve(string _sDeviceSerial)
        {
            try
            {
                string sqlLastRetrieveId = @"
SELECT    MAX(DATARETRIEVES.DATARETRIEVE_ID) 
FROM      DATARETRIEVES 
WHERE     DATARETRIEVES.DEVICESERIAL = '" + _sDeviceSerial + @"' ";
                if (string.IsNullOrEmpty(_sDeviceSerial))
                {
                    sqlLastRetrieveId = @"
SELECT    MAX(DATARETRIEVES.DATARETRIEVE_ID) 
FROM      DATARETRIEVES ";
                }

                int iDataretrieveId = Convert.ToInt32(this.f_GetOneCell(sqlLastRetrieveId));
                string sqlLastAlarms = "SELECT * FROM ALARMS WHERE ALARMS.REFDATARETRIEVE_ID=" + iDataretrieveId;

                DataTable dt = f_ExecuteDataTable(sqlLastAlarms);
                return dt;
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Son çekime ait alarmları çekerken genel istisna oluştu.");
                throw (ex);
            }
        }

        public DataTable f_GetDeviceSerials()
        {
            try
            {
                string sqlSerials = "SELECT DISTINCT DATARETRIEVES.DEVICESERIAL FROM DATARETRIEVES ORDER BY DATARETRIEVES.DATARETRIEVE_ID DESC";

                DataTable dt = f_ExecuteDataTable(sqlSerials);
                return dt;
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Veritabanındaki cihazların seri numaralarını çekerken genel istisna oluştu.");
                throw (ex);
            }
        }

        public int f_GetNotSyncDataretrieveCount()
        {
            try
            {
                string sqlSerials = "SELECT COUNT(1) FROM DATARETRIEVES WHERE SYNC=0";

                DataRow dr = f_ExecuteOneRow(sqlSerials);
                return Convert.ToInt32(dr[0]);
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Eşleştirilmemiş cihaz veri sayısını çekerken genel istisna oluştu.");
                throw (ex);
            }
        }

        public DateTime f_LastRetrieveLogsEndDate()
        {
            string sQL = @"
SELECT   MAX(FULLDATE) 
FROM     LOGS 
WHERE    REFDATARETRIEVE_ID=( SELECT MAX(DATARETRIEVE_ID) FROM DATARETRIEVES )";
            DateTime ilkKayitTarihi = this.f_GetOneCell(sQL) is DateTime
                                          ? (DateTime)this.f_GetOneCell(sQL)
                                          : new DateTime();
            return ilkKayitTarihi;
        }

        public DateTime f_LastRetrieveLogsStartDate()
        {
            string sQL = @"
SELECT   MIN(FULLDATE) 
FROM     LOGS 
WHERE    REFDATARETRIEVE_ID=( SELECT MAX(DATARETRIEVE_ID) FROM DATARETRIEVES )";
            DateTime sonKayitTarihi = this.f_GetOneCell(sQL) is DateTime
                                          ? (DateTime)this.f_GetOneCell(sQL)
                                          : new DateTime();
            return sonKayitTarihi;
        }

        // Cihaza ait VT içindeki ilk verinin tarihi
        public DateTime f_GetFirstLogDateOfDeviceFromDB(string _sDeviceSerial)
        {
            string sQL = @"
SELECT     MIN(LOGS.FULLDATE)
FROM       LOGS 
INNER JOIN DATARETRIEVES ON DATARETRIEVES.DATARETRIEVE_ID = LOGS.REFDATARETRIEVE_ID
WHERE      DATARETRIEVES.DEVICESERIAL  = '" + _sDeviceSerial + @"'";
            DateTime sonKayitTarihi = this.f_GetOneCell(sQL) is DateTime
                                          ? (DateTime)this.f_GetOneCell(sQL)
                                          : new DateTime();
            return sonKayitTarihi;
        }

        // Cihaza ait VT içindeki ilk verinin tarihi
        public DateTime f_GetLastLogDateOfDeviceFromDB(string _sDeviceSerial)
        {
            string sQL = @"
SELECT     MAX(LOGS.FULLDATE)
FROM       LOGS 
INNER JOIN DATARETRIEVES ON DATARETRIEVES.DATARETRIEVE_ID = LOGS.REFDATARETRIEVE_ID
WHERE      DATARETRIEVES.DEVICESERIAL  = '" + _sDeviceSerial + @"'";
            DateTime sonKayitTarihi = this.f_GetOneCell(sQL) is DateTime
                                          ? (DateTime)this.f_GetOneCell(sQL)
                                          : new DateTime();
            return sonKayitTarihi;
        }
    }
}
