using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using FMC.Turkiye.CSharp.Extensions;
using FMC.Turkiye.ExternalDevice.Control;
using FMC.Turkiye.SAS;


namespace FMC.Turkiye.SAS
{
    public partial class YeniForm : Form
    {
        #region Global Degiskenler
        private PortControl pc;
        #endregion

        public YeniForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Portları ComboBox içine elemanları bağlıyorum
            f_PortlariBagla();
            cbPorts.SelectedIndex = 1;
            btnBaglanti.Click += btnBaglanti_Baglan_Click;
        }

        private void f_PortlariBagla()
        {
            cbPorts.Items.Clear();
            cbPorts.Items.AddRange(PortControl.f_GetPorts());
            cbPorts.SelectedIndex = 1;
        }

        private Thread thPbUpdate, thRetrieveInsert;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (pc != null)
            {
                Commander cmd = new Commander(pc, pb);


                MessageBox.Show(pc.ToplamOkunacakByte.ToString());


                Thread thMaxVal = new Thread(pbar =>
                                                 {
                                                     ((ProgressBar)pbar).Maximum = Math.Abs(pc.ToplamOkunacakByte);
                                                 });



                thPbUpdate = new Thread(c =>
                                                  {
                                                      while (pb.Value <= pb.Maximum)
                                                      {
                                                          Thread.Sleep(1);
                                                          int iOkunan = ((Commander)c).M_PortControl.okunan;
                                                          pb.Maximum = (iOkunan > pb.Maximum)
                                                                           ? iOkunan
                                                                           : pb.Maximum;
                                                          ((Commander)c).pb.Value = iOkunan;
                                                          btnUpdate.Text = iOkunan.ToString();
                                                      }
                                                  });


                thRetrieveInsert = new Thread(command =>
                                                         {
                                                             int iEtkilenen = ((Commander)command).f_RetrieveAndInsert();
                                                             MessageBox.Show(iEtkilenen + " Kayıt sisteme eklendi");
                                                             f_ThreadStop();
                                                         });
                pb.Maximum = pc.ToplamOkunacakByte;
                pb.Value = 0;

                //thMaxVal.Start(pb);
                thPbUpdate.Start(cmd);
                thRetrieveInsert.Start(cmd);
            }

        }

        private void f_ThreadStop()
        {
            try
            {
                thRetrieveInsert.Abort();
                thPbUpdate.Abort();
            }
            catch (Exception ex)
            {
                ex.LogException("Thread durdurulurken genel istisna fırlatıldı:");
#if DEBUG
                throw (ex);
#endif
            }
        }

        #region eski


        #region Bağlan/Bağlantıyı Kes Eventleri
        private void btnBaglanti_Baglan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbPorts.SelectedItem != null)
                {
                    rbBaglantiDurumu.Text = "Bekleyiniz...";

                    pc = new PortControl(cbPorts.SelectedItem.ToString(), 4000, 4000);
                    if (pc.f_Connection(true))
                    {
                        rbBaglantiDurumu.Checked = true;
                        rbBaglantiDurumu.Text = "Bağlandi";
                        btnBaglanti.Text = "Bağlantıyı Kes";

                        btnBaglanti.Click -= btnBaglanti_Baglan_Click;
                        btnBaglanti.Click += btnBaglanti_BaglantiyiKes_Click;
                    }
                    else
                    {
                        rbBaglantiDurumu.Checked = false;
                        rbBaglantiDurumu.Text = "Baglantı Yok!";
                    }
                }
            }
            catch (TimeoutException tex)
            {
                rbBaglantiDurumu.Text = "...";
                MessageBox.Show("Bağlantı zaman aşımına uğradı. HATA:\n" + tex.Message);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                rbBaglantiDurumu.Text = "...";
                MessageBox.Show(pc.M_PortName + " Portuna erişim yetkiniz yok! HATA:\n" + uaEx);
            }
            catch (Exception ex)
            {
                rbBaglantiDurumu.Text = "...";
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBaglanti_BaglantiyiKes_Click(object sender, EventArgs e)
        {
            try
            {
                if (pc != null)
                {
                    if (pc.f_ConnectionState())
                    {
                        pc.f_Connection(false);
                        rbBaglantiDurumu.Checked = false;
                        rbBaglantiDurumu.Text = "...";
                        btnBaglanti.Text = "Bağlan";

                        btnBaglanti.Click -= btnBaglanti_BaglantiyiKes_Click;
                        btnBaglanti.Click += btnBaglanti_Baglan_Click;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı kesilirken istisna fırlatıldı. \n\nİSTİSNA:\n" + ex);
            }
        }
        #endregion


        private void btnPortTazele_Click(object sender, EventArgs e)
        {
            f_PortlariBagla();
        }


        #endregion

        private void btnGetir_Click(object sender, EventArgs e)
        {
            using (DAL dal = new DAL()) //üretim iletkenliği, giriş iletkenliği ve sıcaklık grafikleri için
            {
                //
                string sorgu = @"SELECT * FROM LOGS WHERE FULLDATE BETWEEN '" + dtpBas.Value.Date + "' AND '" + dtpSon.Value.Date + "'";

                DataTable Data_Table = dal.f_ExecuteDataTable(sorgu);
                //dgvSonuclar.DataSource = Data_Table;

                giris_iletkenlik_grafik.Series[0].XValueMember = "FULLDATE";
                giris_iletkenlik_grafik.Series[0].YValueMembers = "INLETCONDUCTIVITY";
                giris_iletkenlik_grafik.DataSource = Data_Table;
                giris_iletkenlik_grafik.DataBind();

                uretim_iletkenlik_grafik.Series[0].XValueMember = "FULLDATE";
                uretim_iletkenlik_grafik.Series[0].YValueMembers = "PRODUCTINCONDUCTIVITY";
                uretim_iletkenlik_grafik.DataSource = Data_Table;
                uretim_iletkenlik_grafik.DataBind();

                sicaklik_grafik.Series[0].XValueMember = "FULLDATE";
                sicaklik_grafik.Series[0].YValueMembers = "TEMPERATURE";
                sicaklik_grafik.DataSource = Data_Table;
                sicaklik_grafik.DataBind();
            }

            using (DAL dal = new DAL()) //verim grafiği için
            {
                string sorgu = @"SELECT (CAST(INLETCONDUCTIVITY AS FLOAT)/CAST(PRODUCTINCONDUCTIVITY AS FLOAT)) as PRODUCTIVITY, FULLDATE FROM LOGS
                                 WHERE FULLDATE BETWEEN '" + dtpBas.Value.Date + "' AND '" + dtpSon.Value.Date + "'";

                DataTable Data_Table = dal.f_ExecuteDataTable(sorgu);
                //dgvSonuclar.DataSource = Data_Table;

                verim_grafik.Series[0].XValueMember = "FULLDATE";
                verim_grafik.Series[0].YValueMembers = "PRODUCTIVITY";
                verim_grafik.DataSource = Data_Table;
                verim_grafik.DataBind();
            }

            using (DAL dal = new DAL()) //anlik değerler için
            {
                string iSonCekim = dal.f_GetOneCell("SELECT MAX(INSTANTVALUE_ID) FROM INSTANTVALUES").ToString();

                string sorgu = @"SELECT * FROM INSTANTVALUES
                                 WHERE INSTANTVALUE_ID =" + iSonCekim;

                DataTable Data_Table = dal.f_ExecuteDataTable(sorgu);
                //dgvSonuclar.DataSource = Data_Table;

                anlik_GirisIletkenlik_tb.Text = Data_Table.Rows[0]["INLETWATERCOND"].ToString() + " uS";
                anlik_UretimIletkenlik_tb.Text = Data_Table.Rows[0]["PRODUCTWATERCOND"].ToString() + " uS";
                anlik_Sicaklik_tb.Text = Data_Table.Rows[0]["WATERTEMP"].ToString() + " °C";
                anlik_CalismaSaati_tb.Text = Data_Table.Rows[0]["WORKHOUR"].ToString();
                anlik_AlarmSayisi_tb.Text = Data_Table.Rows[0]["ALARMCOUNT"].ToString();
                anlik_DezenfeksiyonSayisi_tb.Text = Data_Table.Rows[0]["DISINFECTIONCOUNT"].ToString();
            }

            using (DAL dal = new DAL()) //alarmlar listesi için
            {
                string iSonCekim = dal.f_GetOneCell("SELECT MAX(REFDATARETRIEVE_ID) FROM ALARMS").ToString();

                string sorgu = @"SELECT * FROM ALARMS
                                 WHERE REFDATARETRIEVE_ID = " + iSonCekim;

                DataTable Data_Table = dal.f_ExecuteDataTable(sorgu);
                dgvSonuclar.DataSource = Data_Table;

                alarmlar_lb.Items.Clear();

                int alarm_sayisi = Data_Table.Rows.Count;

                for (int i = 0; i < alarm_sayisi; i++)
                {
                    switch (Data_Table.Rows[i]["TIP"].ToString())
                    {
                        case "1":
                            sorgu = "Yüksek İletkenlik Alarmı";
                            break;
                        case "2":
                            sorgu = "Şebeke Basınç Alarmı";
                            break;
                        case "3":
                            sorgu = "Seviye Sensörü Alarmı";
                            break;
                        case "4":
                            sorgu = "Yüksek Sıcaklık Alarmı";
                            break;
                        case "5":
                            sorgu = "Termik Alarmı";
                            break;
                        case "6":
                            sorgu = "Yüksek Giriş İletkenlik Alarmı";
                            break;
                        default:
                            sorgu = "Tanımsız";
                            break;
                    }
                    alarmlar_lb.Items.Add(Data_Table.Rows[i]["ALARMDATE"].ToString() + " - " + sorgu);
                }
            }
        }

        private void dtpBas_ValueChanged(object sender, EventArgs e)
        {
            dtpSon.MinDate = dtpBas.Value;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet("ds");
            DAL dal = new DAL();

            DataTable dtNonSync = dal.f_ExecuteDataTable("SELECT DATARETRIEVES.* FROM DATARETRIEVES WHERE SYNC=0", "DATARETRIEVES");
            ds.Tables.Add(dtNonSync);

            string sDEVICESERIAL = "";
            string sNonSyncRefCekmeIds = "";
            foreach (DataRow row in dtNonSync.Rows)
            {
                sNonSyncRefCekmeIds += row["DATARETRIEVE_ID"] + ",";
                sDEVICESERIAL = row["DEVICESERIAL"].ToString();
            }
            sNonSyncRefCekmeIds = sNonSyncRefCekmeIds.Trim(",".ToCharArray());

            // INSTANTVALUES
            string sqlInstantValues = @"
SELECT  INSTANTVALUES.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL 
FROM    INSTANTVALUES 
WHERE   REFCEKME_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtInstantValues = dal.f_ExecuteDataTable(sqlInstantValues, "INSTANTVALUES");
            ds.Tables.Add(dtInstantValues);

            // LOGS
            string sqlLogs = @"
SELECT  LOGS.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL
FROM    LOGS
WHERE   REFDATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtLogs = dal.f_ExecuteDataTable(sqlLogs, "LOGS");
            ds.Tables.Add(dtLogs);

            // ALARMS
            string sqlAlarms = @"
SELECT  ALARMS.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL
FROM    ALARMS
WHERE   REFDATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtAlarmCount = dal.f_ExecuteDataTable(sqlAlarms, "ALARMS");
            ds.Tables.Add(dtAlarmCount);

            // SETTEDVALUES
            string sqlSettedValues = @"
SELECT  SETTEDVALUES.*, '" + sDEVICESERIAL + @"' AS DEVICESERIAL
FROM    SETTEDVALUES
WHERE   REFDATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";

            DataTable dtSettedValues = dal.f_ExecuteDataTable(sqlSettedValues, "SETTEDVALUES");
            ds.Tables.Add(dtSettedValues);
            f_WebServiseGonder(ds);
        }


        void f_WebServiseGonder(DataSet _ds)
        {
           
        }

        private void btnBaglanti_Click(object sender, EventArgs e)
        {

        }
    }
}
