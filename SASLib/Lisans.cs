using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using FMC.Turkiye.CSharp.Extensions;
using Microsoft.Win32;

namespace FMC.Turkiye.SAS
{
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
                string[] sArrAnahtar = (string[])Registry.GetValue(regSas.Name, "anahtar", new string[] { "" });
                string sArrSeriNo = Registry.GetValue(regSas.Name, "serino", "") as string;

                _sAnahtar = string.IsNullOrEmpty(sArrAnahtar[0])
                                ? ""
                                : sArrAnahtar[0];

                _sSeriNo = sArrSeriNo ?? "";
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
    }
}
