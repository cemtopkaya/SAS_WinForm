using System;
using System.Data;
//using System.Windows.Forms;
using FMC.Turkiye.CSharp.Extensions;
using FirebirdSql.Data.FirebirdClient;

namespace FMC.Turkiye.SAS
{
    public enum ALARM_TURLERI
    {
        YUKSEK_ILETKENLIK = 1, SEBEKE_BASINCI = 2, SWITCH_ARIZA = 3, YUKSEK_SICAKLIK = 4, TERMIK = 5, YUKSEK_GIRIS_ILETKENLIK = 6
    }
    public class Alarm
    {
        public int M_Year { get; set; }
        public int M_Month { get; set; }
        public int M_Day { get; set; }
        public int M_Hour { get; set; }
        public int M_Minute { get; set; }
        public ALARM_TURLERI M_Type { get; set; }
        public DateTime M_FullDateTime { get; set; }

        #region CTORS
        public Alarm(string _sAlarm)
        {
            // 216:12/18-01-11\0 => 2 : Alarm Türü, 16:12 : saati, 18-01-11 : tarihi
            string[] sArrTypeTime = _sAlarm.Split("\0".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            M_Type = (ALARM_TURLERI)Enum.Parse(typeof(ALARM_TURLERI), sArrTypeTime[0][0].ToString());
            string[] sArrTimeDate = sArrTypeTime[0].Substring(1).Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] sArrHourMin = sArrTimeDate[0].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] sArrDayMonthYear = sArrTimeDate[1].Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            M_Day = sArrDayMonthYear[0].To<int>();
            M_Month = sArrDayMonthYear[1].To<int>();
            M_Year = sArrDayMonthYear[2].To<int>() + 2000;
            M_Hour = sArrHourMin[0].To<int>();
            M_Minute = sArrHourMin[1].To<int>();

            M_FullDateTime = new DateTime(M_Year, M_Month, M_Day, M_Hour, M_Minute, 0);
        }
        #endregion

        public override string ToString()
        {
            string sResult = base.ToString();
            sResult += " \n\tTipi:" + this.M_Type + Environment.NewLine + "\tTarihi:" + this.M_FullDateTime.ToString();
            return sResult;
        }
        
        //public string ToString(TextBox _tb)
        //{
        //    string sResult = base.ToString();
        //    sResult += " \n\tTipi:" + this.M_Type + Environment.NewLine + "\tTarihi:" + this.M_FullDateTime.ToString();
        //    _tb.Text = sResult;
        //    return sResult;
        //}
        
        internal FbCommand f_Insert(FbCommand _cmd, int _iRefDataRetrieve_id)
        {

            _cmd.CommandText = @"
INSERT INTO ALARMS
(
  REFDATARETRIEVE_ID,
  TIP,
  ALARMDATE
) 
VALUES (
  @REFDATARETRIEVE_ID,
  @TIP,
  @ALARMDATE
);";
            _cmd.CommandType = CommandType.Text;

            _cmd.Parameters.Clear();

            _cmd.Parameters.Add("@REFDATARETRIEVE_ID", _iRefDataRetrieve_id);
            _cmd.Parameters.Add("@TIP", M_Type);
            _cmd.Parameters.Add("@ALARMDATE", M_FullDateTime);

            return _cmd;
        }
    }
}
