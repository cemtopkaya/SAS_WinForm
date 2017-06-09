using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Win32;

namespace websrvcSAS
{
    public static class Extensions
    {
        public static void LogException(this Exception _ex, string ErrorDescription)
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
                //MessageBox.Show(_sLogTopic + " İsimli kayıt bulunamadı.", "Veri Yok", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

    public class Lisans
    {

        static public string f_GetCPUId()
        {
            string cpuInfo = String.Empty;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == String.Empty)
                {// only return cpuInfo from first CPU
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        static public string f_HashedKey()
        {
            return GetMd5Sum(f_GenerateKey()).ToUpper() + f_GenerateKey();
        }

        static public string f_GenerateKey()
        {
            string sCpuId = f_GetCPUId().PadLeft(18, '0');
            string sTimeD = DateTime.Now.ToFileTime().ToString();

            string key = "";
            for (int i = sTimeD.Length - 1; i >= 0; i--)
            {
                key += sCpuId[i].ToString() + sTimeD[i].ToString();
            }
            return key;
        }

        static public string f_GenerateDateTimeAndCpuId(string key, out string cpu, out string time)
        {
            time = "";
            cpu = "";

            for (int i = key.Length - 1; i >= 0; i -= 2)
            {
                time += key[i];
                cpu += key[i - 1];
            }
            return time;
        }

        // Create an md5 sum string of this string
        static public string GetMd5Sum(string str)
        {
            // First we need to convert the string into bytes, which
            // means using a text encoder.
            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            // Now that we have a byte array we can ask the CSP to hash it
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            // And return it
            return sb.ToString();
        }

        static public bool isLicenced
        {
            get
            {
                string sAnahtar, sSerial;
                Lisans.f_GetLicenceSerialFromReg(out sAnahtar, out sSerial);
                if (string.IsNullOrEmpty(sAnahtar) || string.IsNullOrEmpty(sSerial))
                {
                    return false;
                }
                return Lisans.f_IsSerialValid(sAnahtar, sSerial);
            }
        }

        static public string f_GetSerialFromKey(string _sKey)
        {
            if (_sKey.Length != 68)
            {
                throw new Exception("Anahtarın uzunluğu geçerli değildir.");
            }

            string sHash = _sKey.Substring(0, 31);
            string sKey = _sKey.Substring(32);
            string sDateTime, sCpuId;
            f_GenerateDateTimeAndCpuId(sKey, out sCpuId, out sDateTime);
            long decimalCpuId = long.Parse(sCpuId, System.Globalization.NumberStyles.HexNumber);
            string sDecimalCpuId = decimalCpuId.ToString();
            return sDecimalCpuId.Substring(sDecimalCpuId.Length - 6);
        }

        static public bool f_IsSerialValid(string _sKey, string _sSerial)
        {
            return Lisans.f_GetSerialFromKey(_sKey).Equals(_sSerial);
        }

        static public bool f_IsSerialValidForThisMachine(string _sKey)
        {
            long decimalCpuId = long.Parse(Lisans.f_GetCPUId(), System.Globalization.NumberStyles.HexNumber);
            string sDecimalCpuId = decimalCpuId.ToString();
            string sSerialThisMachine = sDecimalCpuId.Substring(sDecimalCpuId.Length - 6);
            return Lisans.f_GetSerialFromKey(_sKey).Equals(sSerialThisMachine);
        }

        static public void f_GetLicenceSerialFromReg(out string _sAnahtar, out string _sSeriNo)
        {
            try
            {
                RegistryKey regSas;
                string sSubKey = @"Software\SAS";
                regSas = Registry.LocalMachine.OpenSubKey(sSubKey);
                //Registry.LocalMachine.DeleteSubKey(sSubKey);
                if (regSas == null)
                {
                    regSas = Registry.LocalMachine.CreateSubKey(sSubKey);
                }

                // anahtar MultiString tipindedir. Değeri varsa string[] olacaktır.
                object objAnahtar = Registry.GetValue(regSas.Name, "anahtar", "");
                object objSeriNo = Registry.GetValue(regSas.Name, "serino", "");
                if (objAnahtar == null || objSeriNo == null)
                {
                    _sAnahtar = null;
                    _sSeriNo = null;
                }
                string[] sArrAnahtar = (string[])objAnahtar;
                if (sArrAnahtar.Length > 0)
                {
                    _sAnahtar = sArrAnahtar[0];
                }
                else
                {
                    _sAnahtar = "";
                }
                _sSeriNo = objSeriNo.ToString();
            }
            catch (Exception ex)
            {
                ex.LogException("Seri numarası bilgisine ulaşılırken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
        }


        static public bool f_SetLicenceSerial(string _sSeriNo)
        {
            bool bSonuc = false;
            try
            {
                RegistryKey regSas;
                string sSubKey = @"Software\SAS";
                regSas = Registry.LocalMachine.OpenSubKey(sSubKey);
                //Registry.LocalMachine.DeleteSubKey(sSubKey);
                if (regSas == null)
                {
                    regSas = Registry.LocalMachine.CreateSubKey(sSubKey);
                }

                Registry.SetValue(regSas.Name, "anahtar", new string[] { GetMd5Sum(f_GenerateKey()) + f_GenerateKey() }, RegistryValueKind.MultiString);
                Registry.SetValue(regSas.Name, "serino", _sSeriNo, RegistryValueKind.String);
                bSonuc = true;
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Seri numarası sisteme girilirken hata oluştu.");
#if DEBUG
                throw (ex);
#endif
            }

            return bSonuc;
        }

        public static string f_GetCPUIdFromKey(string key)
        {
            return null;
        }
    }


    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Yonetim : System.Web.Services.WebService
    {
        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSAS"].ConnectionString);
        public AuthHeader M_Authentication;

        [WebMethod]
        [SoapHeader("M_Authentication", Required = true)]
        public DataTable f_Bolgeler()
        {
            try
            {
                if (f_KullaniciValidasyonu())
                {
                    if (cnn.State != ConnectionState.Open)
                    {
                        cnn.Open();
                    }
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM BOLGELER ORDER BY Adi";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtBolgeler = new DataTable("Bolgeler");
                    da.Fill(dtBolgeler);
                    return dtBolgeler;
                }
                else
                {
                    throw new UnauthorizedAccessException("Sisteme erişme yetkiniz yok!");
                }
            }
            catch (Exception ex)
            {
                //--IS
                throw (ex);
            }
        }

        bool f_KullaniciValidasyonu()
        {
            try
            {
                if (M_Authentication == null || string.IsNullOrEmpty(M_Authentication.Username) || string.IsNullOrEmpty(M_Authentication.Password))
                {
                    return false;
                }

                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }


                string key, time;
                Lisans.f_GenerateDateTimeAndCpuId(M_Authentication.Username, out key, out time);

                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT SeriNo FROM KULLANICILAR WHERE Anahtar LIKE '%" + key.Substring(0, 18).ReverseString().SqlAsteriks() + "'";
                object objSeriNo = cmd.ExecuteScalar();

                // Kullanıcı Validasyonu
                if (objSeriNo != null)
                {
                    if (M_Authentication.Password.Equals(objSeriNo.ToString()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return false;
        }


        [WebMethod]
        [SoapHeader("M_Authentication", Required = true)]
        public bool f_KullaniciEkle(string _sAnahtar, string _sSeriNo, string _sKurumAdi, int _iBolge_id, string _sYetkili)
        {
            if (string.IsNullOrEmpty(_sAnahtar) || string.IsNullOrEmpty(_sSeriNo))
            {
                throw new ArgumentNullException("Parametreler boş ya da null olamaz!");
            }

            try
            {
                string key, time;
                Lisans.f_GenerateDateTimeAndCpuId(M_Authentication.Username, out key, out time);

                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = @"
INSERT   INTO KULLANICILAR  
         (KurumAdi, YetkiliAdSoyad, refBolge_id, Anahtar, SeriNo, CpuId)
VALUES   (@KURUM, @YETKILI, @BOLGE, @ANAHTAR, @SERINO, @CPUID)";

                cmd.Parameters.Add("@KURUM", SqlDbType.VarChar, 250).Value = _sKurumAdi;
                cmd.Parameters.Add("@YETKILI", SqlDbType.VarChar, 50).Value = _sYetkili;
                cmd.Parameters.Add("@BOLGE", SqlDbType.Int, 4).Value = _iBolge_id;
                cmd.Parameters.Add("@ANAHTAR", SqlDbType.VarChar, 500).Value = _sAnahtar;
                cmd.Parameters.Add("@SERINO", SqlDbType.VarChar, 500).Value = _sSeriNo;
                cmd.Parameters.Add("@CPUID", SqlDbType.VarChar, 50).Value = key.Substring(0, 18);

                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
                int iEtkilenen = cmd.ExecuteNonQuery();
                if (iEtkilenen > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return false;
        }
    }
}
