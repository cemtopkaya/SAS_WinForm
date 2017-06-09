using System;
using System.Data;
using FMC.Turkiye.CSharp.Extensions;
using FMC.Turkiye.ExternalDevice.Control;
using FirebirdSql.Data.FirebirdClient;

namespace FMC.Turkiye.SAS
{
    /// <summary>
    /// Anlık değerler
    /// </summary>
    public class InstantValues
    {
        private PortControl _portControl;

        #region PROPS

        private int? m_InstantValue_id;
        public int? M_InstantValue_ID
        {
            get { return m_InstantValue_id; }
            set { m_InstantValue_id = value; }
        }

        int? m_InletWaterCond;
        public int? M_InletWaterCond
        {
            get
            {
                return m_InletWaterCond ??
                    (m_InletWaterCond = Convert.ToInt32(_portControl.f_GetInletWaterCond()));
            }
            set { m_InletWaterCond = value; }
        }

        int? m_ProductWaterCond;
        public int? M_ProductWaterCond
        {
            get
            {
                return m_ProductWaterCond ??
                    (m_ProductWaterCond = Convert.ToInt32(_portControl.f_GetProductWaterCond()));
            }
            set { m_ProductWaterCond = value; }
        }

        int? m_WaterTemp;
        public int? M_WaterTemp
        {
            get
            {
                return m_WaterTemp ??
                    (m_WaterTemp = Convert.ToInt32(_portControl.f_GetWaterTemp()));
            }
            set { m_WaterTemp = value; }
        }

        int? m_WorkHour;
        public int? M_WorkHour
        {
            get
            {
                return m_WorkHour ??
                    (m_WorkHour = Convert.ToInt32(_portControl.f_GetWorkHour()));
            }
            set { m_WorkHour = value; }
        }

        int? m_AlarmCount;
        public int? M_AlarmCount
        {
            get
            {
                return
                    m_AlarmCount ??
                    (m_AlarmCount = Convert.ToInt32(_portControl.f_GetAlarmCount()));
            }
            set { m_AlarmCount = value; }
        }

        int? m_DisinfectionCount;
        public int? M_DisinfectionCount
        {
            get
            {
                return m_DisinfectionCount ??
                    (m_DisinfectionCount = Convert.ToInt32(_portControl.f_GetDisinfectionCount()));
            }
            set { m_DisinfectionCount = value; }
        }

        public int? M_RefDataRetrieve_id { get; set; }

        #endregion

        #region CTOR
        public InstantValues(PortControl _pc)
        {
            _portControl = _pc;
        }

        public InstantValues(int _iINSTANTVALUE_ID)
        {
            if (_iINSTANTVALUE_ID < 1)
            {
                throw new ArgumentOutOfRangeException("AnlikDeger_id 1 den küçük olamaz.");
            }

            using (DAL dal = new DAL())
            {
                string sQSelect = @"
SELECT 
  INSTANTVALUE_ID,
  ALARMCOUNT,
  PRODUCTWATERCOND,
  INLETWATERCOND,
  WATERTEMP,
  WORKHOUR,
  DISINFECTIONCOUNT,
  REFCEKME_ID
FROM 
  INSTANTVALUES 
WHERE INSTANTVALUE_ID=" + _iINSTANTVALUE_ID;

                DataRow dr = dal.f_ExecuteOneRow(sQSelect);

                this.M_AlarmCount = dr["ALARMCOUNT"].To<int>();
                this.M_DisinfectionCount = dr["DISINFECTIONCOUNT"].To<int>();
                this.M_ProductWaterCond = dr["PRODUCTWATERCOND"].To<int>();
                this.M_InletWaterCond = dr["INLETWATERCOND"].To<int>();
                this.M_WaterTemp = dr["WATERTEMP"].To<int>();
                this.M_WorkHour = dr["WORKHOUR"].To<int>();
                this.M_RefDataRetrieve_id = dr["REFDATARETRIEVE_ID"].To<int>();
            }
        }
        #endregion


        public override string ToString()
        {
            try
            {
                string sResult = base.ToString();
                sResult += Environment.NewLine + "\tAlarm Sayısı: " + this.M_AlarmCount;
                sResult += Environment.NewLine + "\tDezenfeksiyon Sayısı: " + this.M_DisinfectionCount;
                sResult += Environment.NewLine + "\tGiriş Su İletkenliği: " + this.M_InletWaterCond;
                sResult += Environment.NewLine + "\tÜrün Su İletkenliği: " + this.M_ProductWaterCond;
                sResult += Environment.NewLine + "\tSu Sıcaklığı: " + this.M_WaterTemp;
                sResult += Environment.NewLine + "\tÇalışma Saati: " + this.M_WorkHour;
                return sResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //public string ToString(TextBox _tb)
        //{
        //    try
        //    {
        //        string sResult = base.ToString();
        //        sResult += Environment.NewLine + "\tAlarm Sayısı: " + this.M_AlarmCount;
        //        sResult += Environment.NewLine + "\tDezenfeksiyon Sayısı: " + this.M_DisinfectionCount;
        //        sResult += Environment.NewLine + "\tGiriş Su İletkenliği: " + this.M_InletWaterCond;
        //        sResult += Environment.NewLine + "\tÜrün Su İletkenliği: " + this.M_ProductWaterCond;
        //        sResult += Environment.NewLine + "\tSu Sıcaklığı: " + this.M_WaterTemp;
        //        sResult += Environment.NewLine + "\tÇalışma Saati: " + this.M_WorkHour;
        //        _tb.Text += sResult;
        //        return sResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        public FbCommand f_Insert(FbCommand _cmd, int _iRefDataRetrieve_id)
        {

            string insert = @"
INSERT INTO INSTANTVALUES
(
  ALARMCOUNT,
  PRODUCTWATERCOND,
  INLETWATERCOND,
  WATERTEMP,
  WORKHOUR,
  DISINFECTIONCOUNT,
  REFCEKME_ID
) 
VALUES (
  @ALARMCOUNT,
  @PRODUCTWATERCOND,
  @INLETWATERCOND,
  @WATERTEMP,
  @WORKHOUR,
  @DISINFECTIONCOUNT,
  @REFCEKME_ID
);
";
            _cmd.CommandText = insert;
            _cmd.CommandType = CommandType.Text;

            _cmd.Parameters.Clear();

            _cmd.Parameters.Add("@ALARMCOUNT", 123);
            _cmd.Parameters.Add("@PRODUCTWATERCOND", 321);
            _cmd.Parameters.Add("@INLETWATERCOND", 213);
            _cmd.Parameters.Add("@WATERTEMP", 10);
            _cmd.Parameters.Add("@WORKHOUR", 23);
            _cmd.Parameters.Add("@DISINFECTIONCOUNT", 21);
            _cmd.Parameters.Add("@REFCEKME_ID", _iRefDataRetrieve_id);

            return _cmd;
        }
    }
}
