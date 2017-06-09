using System;
using System.Data;
using FMC.Turkiye.ExternalDevice.Control;
using FirebirdSql.Data.FirebirdClient;

namespace FMC.Turkiye.SAS
{
    public class SettedValues
    {
        private PortControl _portControl;

        #region PROPS

        private int? m_SettedValueId;
        public int? M_SettedValue_ID
        {
            get { return m_SettedValueId; }
            set { m_SettedValueId = value; }
        }

        private int? m_ProductWaterCondLimit;
        public int? M_ProductWaterCondLimit
        {
            get
            {
                return m_ProductWaterCondLimit ??
                       (m_ProductWaterCondLimit = Convert.ToInt32(_portControl.f_GetProductWaterCondLimit()));
            }
            set { m_ProductWaterCondLimit = value; }
        }

        private int? m_InletWaterCondLimit;
        public int? M_InletWaterCondLimit
        {
            get
            {
                return m_InletWaterCondLimit ??
                       (m_InletWaterCondLimit = Convert.ToInt32(_portControl.f_GetInletWaterCondLimit()));
            }
            set { m_InletWaterCondLimit = value; }
        }

        private int? m_DisinfectionDuration;
        public int? M_DisinfectionDuration
        {
            get
            {
                return m_DisinfectionDuration ??
                       (m_DisinfectionDuration = Convert.ToInt32(_portControl.f_GetDisinfectionDuration()));
            }
            set { m_DisinfectionDuration = value; }
        }

        private int? m_RinseDuration;
        public int? M_RinseDuration
        {
            get
            {
                return m_RinseDuration ??
                       (m_RinseDuration = Convert.ToInt32(_portControl.f_GetRinseDuration()));
            }
            set { m_RinseDuration = value; }
        }

        private int? m_CleaningDuration;
        public int? M_CleaningDuration
        {
            get
            {
                return m_CleaningDuration ??
                       (m_CleaningDuration = Convert.ToInt32(_portControl.f_GetCleaningDuration()));
            }
            set { m_CleaningDuration = value; }
        }

        #endregion

        #region CTORS
        public SettedValues(PortControl _pc)
        {
            _portControl = _pc;
        }
        #endregion

        public override string ToString()
        {
            string sResult = base.ToString();
            sResult += Environment.NewLine + "\tTemizleme Süresi: " + this.M_CleaningDuration;
            sResult += Environment.NewLine + "\tDezenfeksiyon Süresi: " + this.M_DisinfectionDuration;
            sResult += Environment.NewLine + "\tGiriş Su İletkenliği Limiti: " + this.M_InletWaterCondLimit;
            sResult += Environment.NewLine + "\tÜrün İletkenliği Limiti: " + this.M_ProductWaterCondLimit;
            sResult += Environment.NewLine + "\tDurulama Süresi: " + this.M_RinseDuration;
            return sResult;
        }

        public void toString()
        {
            string sResult = base.ToString();
            sResult += Environment.NewLine + "\tTemizleme Süresi: " + this.M_CleaningDuration;
            sResult += Environment.NewLine + "\tDezenfeksiyon Süresi: " + this.M_DisinfectionDuration;
            sResult += Environment.NewLine + "\tGiriş Su İletkenliği Limiti: " + this.M_InletWaterCondLimit;
            sResult += Environment.NewLine + "\tÜrün İletkenliği Limiti: " + this.M_ProductWaterCondLimit;
            sResult += Environment.NewLine + "\tDurulama Süresi: " + this.M_RinseDuration;
            
            //return sResult;
        }

        
        //public string ToString(TextBox _tb)
        //{
        //    string sResult = base.ToString();
        //    sResult += Environment.NewLine + "\tTemizleme Süresi: " + this.M_CleaningDuration;
        //    sResult += Environment.NewLine + "\tDezenfeksiyon Süresi: " + this.M_DisinfectionDuration;
        //    sResult += Environment.NewLine + "\tGiriş Su İletkenliği Limiti: " + this.M_InletWaterCondLimit;
        //    sResult += Environment.NewLine + "\tÜrün İletkenliği Limiti: " + this.M_ProductWaterCondLimit;
        //    sResult += Environment.NewLine + "\tDurulama Süresi: " + this.M_RinseDuration;
        //    _tb.Text += sResult;
        //    return sResult;
        //}

        public FbCommand f_Insert(FbCommand _cmd, int _iCekmeId)
        {
            _cmd.CommandText = @"
INSERT INTO 
  SETTEDVALUES
(
  PRODUCTINCONDUCTIVITYLIMIT,
  INLETCONDUCTIVITYLIMIT,
  DISINFECTIONDURATION,
  RINSEDURATION,
  CLEANINGDURATIUN,
  REFDATARETRIEVE_ID
) 
VALUES (
  @PRODUCTINCONDUCTIVITYLIMIT,
  @INLETCONDUCTIVITYLIMIT,
  @DISINFECTIONDURATION,
  @RINSEDURATION,
  @CLEANINGDURATIUN,
  @REFDATARETRIEVE_ID
);
";
            _cmd.CommandType = CommandType.Text;

            _cmd.Parameters.Clear();

            _cmd.Parameters.Add("@PRODUCTINCONDUCTIVITYLIMIT", M_ProductWaterCondLimit);
            _cmd.Parameters.Add("@INLETCONDUCTIVITYLIMIT", M_InletWaterCondLimit);
            _cmd.Parameters.Add("@DISINFECTIONDURATION", M_DisinfectionDuration);
            _cmd.Parameters.Add("@RINSEDURATION", M_RinseDuration);
            _cmd.Parameters.Add("@CLEANINGDURATIUN", M_CleaningDuration);
            _cmd.Parameters.Add("@REFDATARETRIEVE_ID", _iCekmeId);

            return _cmd;
        }


    }
}
