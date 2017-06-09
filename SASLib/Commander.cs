using System;
using System.Collections.Generic;
using FMC.Turkiye.ExternalDevice.Control;

namespace FMC.Turkiye.SAS
{
    public class Commander
    {
        #region Props

        public PortControl M_PortControl { get; set; }

        private List<Log> m_Logs;
        public List<Log> M_Logs
        {
            get
            {
                return m_Logs ??
                       (m_Logs = M_PortControl.f_GetLogs());
            }
        }

        private List<Alarm> m_Alarms;
        public List<Alarm> M_Alarms
        {
            get
            {
                return m_Alarms ??
                       (m_Alarms = M_PortControl.f_GetAlarmLogs());
            }
        }

        private InstantValues m_InstantValues;
        public InstantValues M_InstantValues
        {
            get
            {
                return m_InstantValues ??
                       (m_InstantValues = new InstantValues(M_PortControl));
            }
        }

        private SettedValues m_SettedValues;
        public SettedValues M_SettedValues
        {
            get
            {
                return m_SettedValues ??
                       (m_SettedValues = new SettedValues(M_PortControl));
            }
        }
        #endregion

        #region Constructors
        public Commander(PortControl _pc)
        {
            M_PortControl = _pc;
        }

        public System.Windows.Forms.ProgressBar pb;
        public Commander(PortControl pc, System.Windows.Forms.ProgressBar pb)
        {
            M_PortControl = pc;
            this.pb = pb;
        }
        #endregion

        public int f_RetrieveAndInsert()
        {
            // Uygulama kayıt ettirilmişmi?
            string sAnahtar, sSeriNo;
            Lisans.f_GetLicenceSerialFromReg(out sAnahtar, out sSeriNo);
            if (!Lisans.f_IsSerialValid(sAnahtar, sSeriNo) && !Lisans.f_IsSerialValidForThisMachine(sAnahtar))
            {
                throw new Exception("Uygulamayı kayıt ettirmeden cihazdan bilgi çekemezsiniz!!!!");
            }


            int iEtkilenen = 0;
            string cihazSeriNo = M_PortControl.f_GetSerial();
            using (DAL dal = new DAL())
            {
                DataRetrieve dataRetrieve = new DataRetrieve(cihazSeriNo, sAnahtar, sSeriNo);
                iEtkilenen = dataRetrieve.f_Insert();
                if (iEtkilenen > 0)
                {
                    iEtkilenen += dal.f_InsertAll(dataRetrieve.M_DataRetrieve_ID, M_Alarms, M_Logs, M_InstantValues, M_SettedValues);
                }
            }
            return iEtkilenen;
        }

        public string f_Retrieve(string _sBilgi)
        {
            switch (_sBilgi)
            {
                case "z":
                    return M_PortControl.f_ConnectionState() ? "Bağlı" : "Değil";
                case "0":
                    return M_PortControl.f_GetSerial();
                case "1":
                    return M_PortControl.f_GetInletWaterCond();
                case "2":
                    return M_PortControl.f_GetProductWaterCond();
                case "3":
                    return M_PortControl.f_GetWaterTemp();
                case "4":
                    return M_PortControl.f_GetWorkHour();
                case "5":
                    return M_PortControl.f_GetAlarmCount();
                case "6":
                    return M_PortControl.f_GetDisinfectionCount();
                case "7":
                    return M_PortControl.f_GetProductWaterCondLimit();
                case "8":
                    return M_PortControl.f_GetInletWaterCondLimit();
                case "9":
                    return M_PortControl.f_GetDisinfectionDuration();
                case ":":
                    return M_PortControl.f_GetRinseDuration();
                case ";":
                    return M_PortControl.f_GetCleaningDuration();
                case "<":
                    return "Alarm Log çekilemez";
                case "=":
                    return "Tüm Log çekilemez";
                case ">":
                    return "HandShake ile kayıt silinir. GÖNDERİLEMEZ!";
                case "?":
                    return M_PortControl.f_GetLogCount().ToString();
                case " ":
                    return "Henüz versiyon çekilmiyor.";
                default:
                    return _sBilgi + " talebi anlaşılamadı.";
            }
        }
    }
}
