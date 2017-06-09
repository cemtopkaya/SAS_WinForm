using System;
using System.Data;
using FMC.Turkiye.CSharp.Extensions;
using FirebirdSql.Data.FirebirdClient;

namespace FMC.Turkiye.SAS
{
    public class DataRetrieve
    {
        #region Props
        public int M_DataRetrieve_ID { get; set; }
        public DateTime M_RetrieveDate { get; set; }
        public string M_DeviceSerial { get; set; }

        private string m_SoftwareKey;
        public string M_SoftwareKey
        {
            get
            {
                return m_SoftwareKey;
            }
            set { m_SoftwareKey = value; }
        }

        private string m_SoftwareSerialNo;
        public string M_SoftwareSerialNo
        {

            get
            {
                return m_SoftwareSerialNo;
            }
            set { m_SoftwareSerialNo = value; }
        }
        #endregion

        #region CTORs
        /// <summary>
        /// DB den ilgili cekme satirini getirir.
        /// </summary>
        /// <param name="_iDataRetrieveId"></param>
        public DataRetrieve(int _iDataRetrieveId)
        {
            if (_iDataRetrieveId < 1)
            {
                throw new ArgumentOutOfRangeException("DataRetrieve_id 1 den küçük olamaz.");
            }

            using (DAL dal = new DAL())
            {
                string sQSelect = "SELECT * FROM CEKMELER WHERE DATARETRIEVE_ID=" + _iDataRetrieveId + ";";
                DataRow dr = dal.f_ExecuteOneRow(sQSelect);

                M_DataRetrieve_ID = dr["DATARETRIEVE_ID"].To<int>();
                M_RetrieveDate = dr["RETRIEVEDATE"].To<DateTime>();
                M_DeviceSerial = dr["DEVICESERIAL"].To<string>();
                M_SoftwareKey = dr["LICENCE"].To<string>();
            }
        }

        public DataRetrieve(string _CihazSeriNo, string _sYazilimKey, string _sYazilimSerialNo)
        {
            this.M_SoftwareSerialNo = _sYazilimSerialNo;
            this.M_SoftwareKey = _sYazilimKey;
            this.M_DeviceSerial = _CihazSeriNo;
        }
        #endregion


        public FbCommand f_Insert(FbCommand _cmd)
        {
            _cmd.CommandText =
                @"
INSERT INTO DATARETRIEVES
(
  DEVICESERIAL,
  SOFTWAREKEY,
  SOFTWARESERIALNO
) 
VALUES (
  @DEVICESERIAL,
  @SOFTWAREKEY,
  @SOFTWARESERIALNO
);";

            _cmd.CommandType = CommandType.Text;

            _cmd.Parameters.Clear();

            _cmd.Parameters.Add("@DEVICESERIAL", M_DeviceSerial);
            _cmd.Parameters.Add("@SOFTWAREKEY", M_SoftwareKey);
            _cmd.Parameters.Add("@SOFTWARESERIALNO", M_SoftwareSerialNo);

            return _cmd;
        }

        /// <summary>
        /// INSERT INTO DATARETRIEVES ....
        /// </summary>
        /// <returns>Returns affected row count.</returns>
        public int f_Insert()
        {
            try
            {
                using (DAL dal = new DAL())
                {
                    int iResult = dal.f_ExecuteNonQuery(f_Insert(dal.command));
                    if (iResult > 0)
                    {
                        int iCekme_id = dal.f_GetOneCell("SELECT MAX(DATARETRIEVE_ID) FROM DATARETRIEVES;").To<int>();
                        this.M_DataRetrieve_ID = iCekme_id;
                    }
                    return iResult;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Çekme bilgisi veritabanına girilirken istisna fırlatıldı.", ex));
            }
        }
    }
}
