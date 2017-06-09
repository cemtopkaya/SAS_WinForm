using System;
using System.Data;
using System.Diagnostics;
using FMC.Turkiye.CSharp.Extensions;
using FirebirdSql.Data.FirebirdClient;

namespace FMC.Turkiye.SAS
{
    [DebuggerDisplay("{SLog.Length} {SLog}")]
    public class Log
    {
        public int M_Day { get; set; }
        public int M_Month { get; set; }
        public int M_Year { get; set; }

        public int M_Hour { get; set; }

        public string M_TarihSaat
        {
            get
            {
                string sTarihSaat = M_Day + ":" + M_Month + ":" + M_Year + " - " + M_Hour;
                return sTarihSaat;
            }
        }

        public string m_FullDate;
        public string M_FullDate
        {
            get
            {
                int iYil = DateTime.Now.Year;
                int iBuAy = DateTime.Now.Month;
                // Gelen veri 10,11,12. aylardan birine aitse ve içinde bulunduğumuz ay bu aylardan biriyse DateTime.Now.Year
                // 
                if (M_Month == 10 || M_Month == 11 || M_Month == 12)
                {
                    if (iBuAy==11||iBuAy==12)
                    {
                        iYil = DateTime.Now.Year;
                    }
                    else
                    {
                        iYil = DateTime.Now.Year - 1;
                    }
                }
                return String.Format("{0}.{1}.{2}  {3}:00", iYil,M_Month, M_Day, M_Hour);
            }
            set { m_FullDate = value; }
        }
        public int M_InletCond { get; set; }
        public int M_ProdWaterCond { get; set; }
        public int M_Temp { get; set; }
        public int M_Efficiency
        {
            get { return (M_InletCond - M_ProdWaterCond) * 100 / (M_InletCond + 1); }
        }

        public string M_StopByte { get; set; }

        public string SLog { get; set; }

        #region CTORS
        public Log(string _sLog)
        {
            SLog = _sLog;
            //12061205010922# => (15 karakter geliyor)
            //         12 : Gun, 06 : Ay, 12 : Saat, 
            //         05 : Giris Iletkenlik, 
            //         01 : Giris Iletkenlik Kucuk, 
            //         09 : Uretim Iletkenlik, 
            //         22 : Sıcaklık, 
            //         # : Dur İşareti

            M_Day = Convert.ToInt32(_sLog.Substring(0, 2));
            M_Month = Convert.ToInt32(_sLog.Substring(2, 2));
            M_Hour = Convert.ToInt32(_sLog.Substring(4, 2));

            M_InletCond = Convert.ToInt32(_sLog.Substring(6, 2)) * 256 + Convert.ToInt32(_sLog.Substring(8, 2));
            M_ProdWaterCond = Convert.ToInt32(_sLog.Substring(10, 2));
            M_Temp = Convert.ToInt32(_sLog.Substring(12, 2));

            M_StopByte = _sLog.Substring(14);

            M_FullDate = "2011" + "." + M_Month + "." + M_Day + "  " + M_Hour + ":00";
        }

        public Log(byte[] _bArrLog)
        {
            SLog = _bArrLog.GetString();
            //12061205010922# => (15 karakter geliyor)
            //         12 : Gun, 06 : Ay, 12 : Saat, 
            //         05 : Giris Iletkenlik, 
            //         01 : Giris Iletkenlik Kucuk, 
            //         09 : Uretim Iletkenlik, 
            //         22 : Sıcaklık, 
            //         # : Dur İşareti

            M_Day = _bArrLog[1].To<int>();
            M_Month = _bArrLog[0].To<int>();
            M_Hour = _bArrLog[2].To<int>();

            M_InletCond = _bArrLog[3].To<int>() * 256 + _bArrLog[4].To<int>();
            M_ProdWaterCond = _bArrLog[5].To<int>();
            M_Temp = _bArrLog[6].To<int>();

            M_StopByte = _bArrLog[7].To<int>().ToString();
        }

        #endregion

        public override string ToString()
        {
            string sResult = base.ToString();
            sResult += Environment.NewLine + "\tTarih-Saat: " + this.M_TarihSaat;
            sResult += Environment.NewLine + "\tGiriş İletkenliği: " + M_InletCond;
            sResult += Environment.NewLine + "\tÜretim Su İletkenliği: " + this.M_ProdWaterCond;
            sResult += Environment.NewLine + "\tVerimlilik: " + this.M_Efficiency;
            return sResult;
        }

        //public string ToString(TextBox _tb)
        //{
        //    string sResult = base.ToString();
        //    sResult += Environment.NewLine + "\tTarih-Saat: " + this.M_TarihSaat;
        //    sResult += Environment.NewLine + "\tGiriş İletkenliği: " + this.M_InletCond;
        //    sResult += Environment.NewLine + "\tÜretim Su İletkenliği: " + this.M_ProdWaterCond;
        //    sResult += Environment.NewLine + "\tVerimlilik: " + this.M_Efficiency;
        //    _tb.Text += sResult;
        //    return sResult;
        //}

        public FbCommand f_Insert(FbCommand _cmd, int _iRefDataRetrieve_id)
        {
            _cmd.CommandText = @"
INSERT INTO LOGS
(
  AY,
  GUN,
  SAAT,
  INLETCONDUCTIVITY,
  PRODUCTINCONDUCTIVITY,
  TEMPERATURE,
  FULLDATE,
  REFDATARETRIEVE_ID
) 
VALUES (
  @AY,
  @GUN,
  @SAAT,
  @INLETCONDUCTIVITY,
  @PRODUCTINCONDUCTIVITY,
  @TEMPERATURE,
  @FULLDATE,
  @REFDATARETRIEVE_ID
);";
            _cmd.CommandType = CommandType.Text;

            _cmd.Parameters.Clear();

            _cmd.Parameters.Add("@AY", M_Month);
            _cmd.Parameters.Add("@GUN", M_Day);
            _cmd.Parameters.Add("@SAAT", M_Hour);
            _cmd.Parameters.Add("@INLETCONDUCTIVITY", M_InletCond);
            _cmd.Parameters.Add("@PRODUCTINCONDUCTIVITY", M_ProdWaterCond);
            _cmd.Parameters.Add("@TEMPERATURE", M_Temp);
            _cmd.Parameters.Add("@FULLDATE", M_FullDate);
            _cmd.Parameters.Add("@REFDATARETRIEVE_ID", _iRefDataRetrieve_id);

            return _cmd;
        }
    }
}
