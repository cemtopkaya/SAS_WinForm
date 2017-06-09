using System;
using System.IO.Ports;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using FMC.Turkiye.CSharp.Extensions;
using FMC.Turkiye.SAS;

namespace FMC.Turkiye.ExternalDevice.Control
{
    public class PortControl
    {
        /*
z Bağlantı
0 Seri No
1 Anlık Giriş İletkenliği
2 Anlık Üretim İletkenliği
3 Sıcaklık
4 Çalışma Saati 
5 Alarm Sayıs
6 Dezenfeksiyon Sayısı
7 Üretim Limit İletkenlik
8 Giriş Limit İletkenlik
9 Dezenfeksiyon Süresi
: Durulama Süresi
; Temizleme Süresi
< Alarm Log
= Tüm Loglar
> Hand Shake
? Kayıt Sayısı
  Versiyon No
         */

        public const int iDisinfectionCount = 2;
        public const int iWorkHour = 2;
        public const int iWaterTemp = 1;
        public const int iProductWaterCond = 1;
        public const int iProductWaterCondLimit = 1;
        public const int iInletWaterCondLimit = 2;
        public const int iInletWaterCond = 2;
        public const int iSerial = 4;
        public const int iDisinfectionDuration = 1;
        public const int iRinseDuration = 1;
        public const int iCleaningDuration = 1;
        public const int iAlarmCount = 2;
        public const int iLogCount = 3;
        private int _toplamOkunacak;
        private int _toplamOkunan;

        public int okunan = 0;

        public int ToplamOkunacakByte
        {
            get
            {
                int iAlarmCount = Convert.ToInt32(f_GetAlarmCount());
                int iAlarmLogs = (iAlarmCount > 10 ? 10 : iAlarmCount) * 16;
                int iLogs = f_GetLogCount() * 8;

                _toplamOkunacak = 0;
                _toplamOkunacak += iLogs + iAlarmLogs + iDisinfectionCount + iInletWaterCond + iProductWaterCond +
                                   iSerial + iWaterTemp + iWorkHour;

                return _toplamOkunacak;
            }
        }

        private byte BYTE_DIFFERENCE = 48;

        #region Properties
        private string m_PortName;
        public string M_PortName
        {
            get
            {
                return m_PortName;
            }
            private set
            {
                m_PortName = value;
            }
        }
        #endregion

        #region Fields
        private SerialPort spConnection;
        /// <summary>
        /// Serial Porta bilgi gelme işi verilen ThreshHold a göre tamamlandıysa
        /// </summary>
        private bool bDataRecievedCompleted;
        /// <summary>
        /// Serial Porttan alınan bilgi tamamlandıysa.
        /// </summary>
        private List<byte> lstRecievedData = new List<byte>();
        #endregion

        #region Constructors
        public PortControl(string _sPortName, int _iWriteTimeOut = 1000, int _iReadTimeOut = 1000)
        {
            if (string.IsNullOrEmpty(_sPortName))
            {
                throw new Exception("Bağlantı kurulacak port adı tayin edilmemiş.");
            }

            spConnection = new SerialPort();

            spConnection.PortName = M_PortName = _sPortName;
            spConnection.ReadTimeout = _iReadTimeOut;
            spConnection.WriteTimeout = _iWriteTimeOut;

            spConnection.ErrorReceived += spConnection_ErrorReceived;
            spConnection.DataReceived += spConnection_DataReceived;
        }
        #endregion

        static public string[] f_GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        #region Bağlantı Aç/Kapat Durumu.
        /// <summary>
        /// Bizim için bağlantının açık olması;
        /// 1. Seri Port bağlantısı açık ve
        /// 2. CİHAZDAN 10 değerinin okunması durumudur.
        /// </summary>
        /// <param name="_bOpen"></param>
        /// <returns></returns>
        public bool f_Connection(bool _bOpen)
        {
            try
            {
                if (_bOpen && !f_ConnectionState())
                {
                    spConnection.Open();
                }
                else
                {
                    spConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return f_ConnectionState();
        }

        /// <summary>
        /// Sadece IsOpen bizim için yeterli değil. 
        /// Eğer isOpen sonucu TRUE ise bir veri okumaya çalışıyoruz ki 
        /// gerçekten bağlandığımızı bilelim.
        /// </summary>
        /// <returns>True: bağlantı sağlandı. False: bağlantı yok.</returns>
        public bool f_ConnectionState()
        {
            if (spConnection.IsOpen)
            {
                try
                {
                    spConnection.ReceivedBytesThreshold = 1;
                    spConnection.Write("z");
                    int rx_data = spConnection.ReadByte();
                    if (rx_data == 10)
                    {
                        return true;
                    }
                    else
                    {
                        spConnection.Close();
                        return false;
                    }
                }
                catch (TimeoutException tex)
                {
                    
                    spConnection.Close();
                    throw new TimeoutException(spConnection.PortName + " Portuna bağlantı kurma zaman aşımına uğradı!", tex);
                }
                catch (Exception ex)
                {
                    spConnection.Close();
                    throw ex;
                }
            }
            return false;
        }
        #endregion

        #region Cihazla Konuşma/Veri Çekme
        /// <summary>
        /// seri porta yazılacak bilgi ve porttan okunacak bilginin uzunluğu
        /// </summary>
        /// <param name="_sBilgi"></param>
        /// <param name="_i"></param>
        /// <returns></returns>
        private byte[] f_YazOku(string _sBilgi, int _i)
        {
            try
            {
                if (spConnection.ReadBufferSize < _i)
                {
                    spConnection.Close();
                    spConnection.ReadBufferSize = _i * 2; //8192 set etsemde 8190 okuyordu Bu yüzden _i = 8192 gelsede 2 ile çarpıyorum.
                }

                spConnection.ReceivedBytesThreshold = 1;

                if (!spConnection.IsOpen)
                {
                    spConnection.Open();
                }

                spConnection.Write(_sBilgi);
                DateTime dt = DateTime.Now;

                while (lstRecievedData.Count < _i)
                {
                    Thread.Sleep(10);
                }
                //while (!bDataRecievedCompleted)
                //{
                //    //if ((DateTime.Now - dt).Seconds > 3)
                //    //{
                //    //    bDataRecievedCompleted = true;
                //    //}

                //    bDataRecievedCompleted = true;
                //}
                //bDataRecievedCompleted = false;
                byte[] bArrReturn = lstRecievedData.ToArray();
                lstRecievedData.Clear();

                return bArrReturn;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            #region Eski Okuma
            //try
            //{
            //    byte[] buffer = new byte[spConnection.ReadBufferSize];
            //    int totalByte = spConnection.Read(buffer, 0, buffer.Length);
            //    byte[] brr = buffer.Take(totalByte).ToArray();
            //    return brr;
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //} 
            #endregion
        }


        private byte[] f_YazOku1(string _sBilgi, int _i)
        {
            try
            {
                if (spConnection.ReadBufferSize < _i)
                {
                    spConnection.Close();
                    spConnection.ReadBufferSize = _i * 2; //8192 set etsemde 8190 okuyordu Bu yüzden _i = 8192 gelsede 2 ile çarpıyorum.
                }

                spConnection.ReceivedBytesThreshold = _i;

                if (!spConnection.IsOpen)
                {
                    spConnection.Open();
                }

                spConnection.Write(_sBilgi);
                DateTime dt = DateTime.Now;

                while (!bDataRecievedCompleted)
                {
                    if ((DateTime.Now - dt).Seconds > 3)
                    {
                        bDataRecievedCompleted = true;
                    }

                    if (spConnection.BytesToRead == _i)
                    {
                        bDataRecievedCompleted = true;
                    }
                }
                bDataRecievedCompleted = false;
                return lstRecievedData.ToArray();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            #region Eski Okuma
            //try
            //{
            //    byte[] buffer = new byte[spConnection.ReadBufferSize];
            //    int totalByte = spConnection.Read(buffer, 0, buffer.Length);
            //    byte[] brr = buffer.Take(totalByte).ToArray();
            //    return brr;
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //} 
            #endregion
        }


        private byte[] f_Yaz(string _sBilgi, int _i)
        {
            spConnection.ReceivedBytesThreshold = _i;
            spConnection.Write(_sBilgi);
            string s = spConnection.ReadExisting();
            byte[] b = s.ToCharArray().Select(p => p.To<byte>()).ToArray();
            return b;

            #region Eski Okuma

            //try
            //{
            //    byte[] buffer = new byte[spConnection.ReadBufferSize];
            //    int totalByte = spConnection.Read(buffer, 0, buffer.Length);
            //    byte[] brr = buffer.Take(totalByte).ToArray();
            //    return brr;
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //} 

            #endregion
        }

        #region Eventler
        void spConnection_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            int totalByte = 0;
            byte[] buffer = new byte[port.ReadBufferSize];
            totalByte = ((SerialPort)sender).Read(buffer, 0, buffer.Length);

            // Sınıfın okuduğu toplam byte a eklensin
            okunan += totalByte;

            lstRecievedData.AddRange(buffer.Take(totalByte).ToList()); //= buffer.Take(totalByte).ToList();
        }


        void spConnection_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            int totalByte = 0;
            byte[] buffer = new byte[port.ReadBufferSize];
            totalByte = ((SerialPort)sender).Read(buffer, 0, buffer.Length);

            lstRecievedData = buffer.Take(totalByte).ToList();
            bDataRecievedCompleted = true;
        }

        void spConnection_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Debugger.Break();
        }
        #endregion
        #endregion

        #region Bilgi bilgi çekme
        public string f_GetSerial()
        {
            try
            {
                // 0 : Seri numarası   
                byte[] bArr = f_YazOku("0", iSerial);

                string sSerial = "";
                foreach (byte b in bArr)
                {
                    sSerial += Convert.ToChar(b + BYTE_DIFFERENCE);
                }
                return sSerial;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        #region Anlık Değerler
        public string f_GetInletWaterCond()
        {
            try
            {
                byte[] bArr = f_YazOku("1", iInletWaterCond);

                string sCond = Convert.ToString(bArr[0] * 256 + bArr[1]);
                return sCond;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetProductWaterCond()
        {
            try
            {
                byte[] bArr = f_YazOku("2", iProductWaterCond);

                string sProductCond = Convert.ToString(bArr[0]);
                return sProductCond;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetWaterTemp()
        {
            try
            {
                byte[] bArr = f_YazOku("3", iWaterTemp);

                string sTemp = Convert.ToString(bArr[0]);
                return sTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetWorkHour()
        {
            try
            {
                byte[] bArr = f_YazOku("4", iWorkHour);

                string sWorkHour = Convert.ToString(bArr[0] * 256 + bArr[1]);
                return sWorkHour;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public string f_GetDisinfectionCount()
        {
            try
            {
                byte[] bArr = f_YazOku("6", iDisinfectionCount);

                string sDisinfectionCount = Convert.ToString(bArr[0] * 256 + bArr[1]);
                return sDisinfectionCount;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Ayarlanan Değerler
        public string f_GetProductWaterCondLimit()
        {
            try
            {
                byte[] bArr = f_YazOku("7", iProductWaterCondLimit);

                string sTemp = Convert.ToString(bArr[0]);
                return sTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetInletWaterCondLimit()
        {
            try
            {
                byte[] bArr = f_YazOku("8", iInletWaterCondLimit);

                string sResult = Convert.ToString(bArr[0] * 256 + bArr[1]);
                return sResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetDisinfectionDuration()
        {
            try
            {
                byte[] bArr = f_YazOku("9", iDisinfectionDuration);

                string sResult = Convert.ToString(bArr[0]);
                return sResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetRinseDuration()
        {
            try
            {
                byte[] bArr = f_YazOku(":", iRinseDuration);

                string sResult = Convert.ToString(bArr[0]);
                return sResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string f_GetCleaningDuration()
        {
            try
            {
                byte[] bArr = f_YazOku(";", iCleaningDuration);

                string sResult = Convert.ToString(bArr[0]);
                return sResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Alarm Log
        public string f_GetAlarmCount()
        {
            try
            {
                byte[] bArr = f_YazOku("5", iAlarmCount);

                string sWorkHour = Convert.ToString(bArr[0] + bArr[1] * 256);
                return sWorkHour;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Alarm> f_GetAlarmLogs()
        {
            try
            {

                int iAlarmCount = Convert.ToInt32(f_GetAlarmCount());
                iAlarmCount = iAlarmCount > 10
                                  ? 10
                                  : iAlarmCount;

                // Tüm alarmları cihazdan çek (maks 10 tane gelir)
                return f_GetAlarmLog(iAlarmCount * 16);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Alarm> f_GetAlarmLog(int _iReadLen)
        {
            try
            {
                byte[] bArrAlarm = f_YazOku("<", _iReadLen);
                //string sAlarmString = String.Join("", bArrAlarm.Select(p => Convert.ToChar(p + BYTE_DIFFERENCE).ToString()).ToArray());
                List<Alarm> lstAlarms = new List<Alarm>();
                for (int i = 0; i < _iReadLen / 16; i++)
                {
                    lstAlarms.Add(new Alarm(bArrAlarm.Skip(i * 16).Take(16).GetString()));
                }
                return lstAlarms;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Kayıtlar
        /// <summary>
        /// Cihazda şu andaki kayıt sayısını döndürür.
        /// </summary>
        /// <returns></returns>
        public int f_GetLogCount()
        {
            int kayit_no_kucuk, kayit_no_buyuk, tur, kayit_sayisi;
            try
            {
                byte[] bArr = f_YazOku("?", iLogCount); //Kayit Sayisi

                kayit_no_kucuk = bArr[0];
                kayit_no_buyuk = bArr[1] - 32;
                tur = bArr[2];
                kayit_sayisi = (tur == 1)
                                   ? Math.Floor(8192 / 8.0d).To<int>()
                                   : (kayit_no_buyuk * 256 + kayit_no_kucuk) / 8;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return kayit_sayisi;
        }

        public List<Log> f_GetLog(int _iLogCount)
        {
            try
            {
                if (_iLogCount <= 0)
                {
                    // Eğer çekilecek bir log yoksa boş dönüyoruz
                    return null;
                }

                byte[] bArrLog = f_YazOku("=", (_iLogCount * 8));
                //string sLogString = String.Join("", bArrLog.Select(p => Convert.ToChar(p + BYTE_DIFFERENCE).ToString()).ToArray());
                List<Log> lstLogs = new List<Log>();
                for (int i = 0; i < _iLogCount; i++)
                {
                    //byte[] bArrLog = f_Yaz("=", 8);
                    //lstLogs.Add(new Log(bArrLog));
                    lstLogs.Add(new Log(bArrLog.Skip(i * 8).Take(8).ToArray()));
                }
                return lstLogs;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Log> f_GetLogs()
        {
            try
            {
                int iRecCount = f_GetLogCount();
                return f_GetLog(iRecCount);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #endregion

    }
}
