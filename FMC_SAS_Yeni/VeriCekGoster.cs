using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FMC.Turkiye.CSharp.Extensions;
using FMC.Turkiye.ExternalDevice.Control;
using FMC.Turkiye.SAS;
using SASLib;

namespace FMC.Turkiye.SAS
{
    public partial class VeriCekGoster : Form
    {
        static SeriesChartType m_CurrentChartType = SeriesChartType.Line;
        public static bool bAktivasyonOk;
        bool bKapatilacak;
        List<Chart> lstCharts = new List<Chart>();
        #region Global Degiskenler
        private PortControl pc;
        private DAL dal = null;
        private static VeriAl.VeriAl GetWebSrvc()
        {
            VeriAl.VeriAl webSrvc = new VeriAl.VeriAl();

            // Web servisi adresini app.config den okuyoruz.
            webSrvc.Url = ConfigurationManager.AppSettings["WSGonderme"];

            return webSrvc;
        }
        #endregion

        public VeriCekGoster()
        {
            InitializeComponent();
            try
            {
                //"VeriCekGoster() yapıcı metodunun içindeyim".LogToTempFile("Yapıcı metot içi:");
                dal = new DAL();
                bool bLisansli = Lisans.isLicenced;
                while (!bLisansli)
                {
                    DialogResult dr = (new UrunuKaydet()).ShowDialog();
                    bLisansli = Lisans.isLicenced;

                    if (dr == DialogResult.OK && bLisansli)
                    {
                        break;
                    }

                    if (dr == DialogResult.Cancel)
                    {
                        bKapatilacak = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Ürün lisanslaması kontrolünde genel istisna fırlatıldı.");
                MessageBox.Show(ex.Message);
#if DEBUG
                throw (ex);
#endif
            }

            //(new UrunuKaydet()).ShowDialog();

            CheckForIllegalCrossThreadCalls = false;
            f_CihazSeriNumaralariniBagla();
            f_SetCharts();

        }

        private void f_SetCharts()
        {
            try
            {
                foreach (Control gbox in this.pnlGraph.Controls)
                {
                    if ((gbox as GroupBox) != null)
                    {
                        foreach (Control ctl in gbox.Controls)
                        {
                            Chart chart = ctl as Chart;
                            if (chart != null)
                            {
                                lstCharts.Add(chart);

                                foreach (Series series in chart.Series)
                                {
                                    series.ChartType = SeriesChartType.Line;
                                    series.Name = chart.Legends[0].Title;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Ana ekranda grafik bileşenlerinin başlangıç değerleri atanırken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
        }


        public void f_CihazSeriNumaralariniBagla()
        {
            try
            {
                DataTable dtSerials = dal.f_GetDeviceSerials();
                if (dtSerials == null && dtSerials.Rows.Count == 0)
                {
                    MessageBox.Show("Bir su arıtma cihazından çekilmiş veri bulunamadı.", "Cihaz Seri Numaraları", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                cbDeviceSerials.ValueMember = "DEVICESERIAL";
                cbDeviceSerials.DisplayMember = "DEVICESERIAL";
                cbDeviceSerials.DataSource = dtSerials;
            }
            catch (Exception ex)
            {
                ex.LogException("Kayıtlı Cihaz Seri numaralarını çekerken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
        }

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

                        btnUpdate.Enabled = true;

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
                tex.LogException("Seri porta bağlanma zaman aşımına uğradı.");
                rbBaglantiDurumu.Text = "...";
                MessageBox.Show("Bağlantı zaman aşımına uğradı. HATA:\n" + tex.Message, "Port Bağlantısı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                uaEx.LogException("Seri porta bağlanma yetkisiz erişim nedeniyle istisnaya neden oldu.");
                rbBaglantiDurumu.Text = "...";
                MessageBox.Show(pc.M_PortName + " Portuna erişim yetkiniz yok! HATA:\n" + uaEx, "Port Bağlantısı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ex.LogException("Seri porta bağlanma esnasında genel istisna meydana geldi.");
                rbBaglantiDurumu.Text = "...";
                MessageBox.Show(ex.ToString(), "Port Bağlantısı", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        btnUpdate.Enabled = false;

                        btnBaglanti.Click -= btnBaglanti_BaglantiyiKes_Click;
                        btnBaglanti.Click += btnBaglanti_Baglan_Click;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Seri porttan bağlantı kesilirken genel istisna fırlatıldı:");
                MessageBox.Show("Bağlantı kesilirken istisna fırlatıldı. \n\nİSTİSNA:\n" + ex, "Port Bağlantısı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnPortTazele_Click(object sender, EventArgs e)
        {
            try
            {
                f_PortlariBagla();
            }
            catch (Exception ex)
            {
                ex.LogException("Bilgisayara bağlı port bilgilerini tazelerken çekilirken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
        }

        private void VeriCekGoster_Load(object sender, EventArgs e)
        {
            try
            {
                f_PortlariBagla();
                cbPorts.SelectedIndex = 1;
                btnBaglanti.Click += btnBaglanti_Baglan_Click;
            }
            catch (Exception ex)
            {
                ex.LogException("Bilgisayara bağlı port bilgileri çekilirken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
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


                //MessageBox.Show(pc.ToplamOkunacakByte.ToString());


                Thread thMaxVal = new Thread(pbar =>
                                                 {
                                                     MessageBox.Show("Toplam Okunacak Byte:" + Math.Abs(pc.ToplamOkunacakByte));
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
                                                      MessageBox.Show(iEtkilenen + " Kayıt sisteme eklendi", "Cihaz Verilerinin Çekilmesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                      f_ThreadStop();
                                                  });
                pb.Maximum = pc.ToplamOkunacakByte;
                pb.Value = 0;

                //thMaxVal.Start(pb);
                thPbUpdate.Start(cmd);
                thRetrieveInsert.Start(cmd);

                // Verilerin çekilmesinin peşine bağlantıyı kopartıyorum.
                btnBaglanti_Baglan_Click(null, null);
            }

        }

        private void f_ThreadStop()
        {
            try
            {
                if (thRetrieveInsert != null) thRetrieveInsert.Abort();
                if (thPbUpdate != null) thPbUpdate.Abort();
            }
            catch (Exception ex)
            {
                ex.LogException("Thread durdurulurken genel istisna fırlatıldı:");
#if DEBUG
                throw (ex);
#endif
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (pc != null)
                {
                    Commander cmd = new Commander(pc, pb);

                    MessageBox.Show(pc.ToplamOkunacakByte.ToString(), "Veri Çekme", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                                                          MessageBox.Show(iEtkilenen + " Kayıt sisteme eklendi", "Veri Çekme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                          f_ThreadStop();
                                                      });

                    pb.Maximum = pc.ToplamOkunacakByte;
                    pb.Value = 0;

                    //thMaxVal.Start(pb);
                    thPbUpdate.Start(cmd);
                    thRetrieveInsert.Start(cmd);

                    f_AnlikDegerler(null);
                    f_Alarmlar(null);
                    f_CihazSeriNumaralariniBagla();
                    
                }

            }
            catch (Exception ex)
            {
                ex.LogException("Cihazdaki veriler çekilirken genel istisna fırlatıldı:");
#if DEBUG
                throw (ex);
#endif
            }

        }

        private void f_Alarmlar(string _sDeviceSerial)
        {
            try
            {
                DataTable dt = dal.f_GetLastAlarmsOfLastDataretrieve(_sDeviceSerial);
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Alarmları DataGridView'a bağlıyoruz.
                    //dgvSonuclar.DataSource = dt;

                    // Alarmları gösterdiğimiz ListBox'ı temizliyoruz ve yenilerini ekliyoruz.
                    lbAlarmlar.Items.Clear();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        string sAlarmTipi = "";
                        switch (dataRow["TIP"].ToString())
                        {
                            case "1":
                                sAlarmTipi = "Yüksek İletkenlik Alarmı";
                                break;
                            case "2":
                                sAlarmTipi = "Şebeke Basınç Alarmı";
                                break;
                            case "3":
                                sAlarmTipi = "Seviye Sensörü Alarmı";
                                break;
                            case "4":
                                sAlarmTipi = "Yüksek Sıcaklık Alarmı";
                                break;
                            case "5":
                                sAlarmTipi = "Termik Alarmı";
                                break;
                            case "6":
                                sAlarmTipi = "Yüksek Giriş İletkenlik Alarmı";
                                break;
                            default:
                                sAlarmTipi = "Tanımsız";
                                break;
                        }
                        lbAlarmlar.Items.Add("[" + dataRow["ALARMDATE"] + "] - " + sAlarmTipi);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Alarm bilgileri veritabanından çekilirken genel istisna fırlatıldı:");
#if DEBUG
                throw (ex);
#endif
            }
        }


        private void f_AnlikDegerler(string _sDeviceSerial)
        {
            try
            {
                DataTable dt = dal.f_GetLastInstantValuesOfLastDataretrieve(_sDeviceSerial);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    tbAnlikAlarmSayisi.Text = row["ALARMCOUNT"].ToString();
                    tbAnlikUretimIletkenlik.Text = row["PRODUCTWATERCOND"].ToString();
                    tbAnlikGirisIletkenlik.Text = row["INLETWATERCOND"].ToString();
                    tbAnlikSicaklik.Text = row["WATERTEMP"].ToString();
                    tbAnlikDezenfeksiyonSayisi.Text = row["DISINFECTIONCOUNT"].ToString();
                    tbAnlikCalismaSaati.Text = row["WORKHOUR"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Veritabanındaki anlık değerler çekilirken genel istisna fırlatıldı:");
#if DEBUG
                throw (ex);
#endif
            }
        }


        void menuItemBuyukGrafikGoster(object sender, EventArgs e)
        {
            var menuItem = (sender as ToolStripMenuItem);
            if (menuItem != null)
            {
                (new GraphOneWindow(menuItem.Text)).ShowDialog();
            }
        }



        private void f_CihazinTumBilgileri(string _sDeviceSerial)
        {
            try
            {
                if (cbDeviceSerials.SelectedItem == null)
                {
                    MessageBox.Show("Bir cihaz seri numarası seçmeniz gerekiyor!", "Cihaz Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DateTime dtBas = dal.f_GetFirstLogDateOfDeviceFromDB(_sDeviceSerial);
                DateTime dtBit = dal.f_GetLastLogDateOfDeviceFromDB(_sDeviceSerial);
                //f_CihazSeriNumaralariniBagla();
                f_AnlikDegerler(_sDeviceSerial);
                f_Alarmlar(_sDeviceSerial);
                Graphs.f_GraphSeries(dal, dtBas, dtBit, chGirisIletkenligi, _sDeviceSerial, "GİRİŞ İLETKENLİĞİ");
                chGirisIletkenligi.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chGirisIletkenligi.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                Graphs.f_GraphSeries(dal, dtBas, dtBit, chUretimIletkenligi, _sDeviceSerial, "ÜRETİM İLETKENLİĞİ");
                chUretimIletkenligi.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chUretimIletkenligi.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                Graphs.f_GraphSeries(dal, dtBas, dtBit, chVerim, _sDeviceSerial, "VERİM");
                chVerim.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chVerim.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                Graphs.f_GraphSeries(dal, dtBas, dtBit, chSuSicakligi, _sDeviceSerial, "SU SICAKLIĞI");
                chSuSicakligi.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chSuSicakligi.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Grafikler çizdirilirken genel istisna fırlatıldı:");
                MessageBox.Show("İstisna Oluştu.", "Grafik Çizdirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if DEBUG
                throw (ex);
#endif
            }
        }


        private void menuItemEtiketleriGoster_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            this.chGirisIletkenligi.Legends[0].Enabled = (sender as ToolStripMenuItem).Checked;
            this.chUretimIletkenligi.Legends[0].Enabled = (sender as ToolStripMenuItem).Checked;
            this.chSuSicakligi.Legends[0].Enabled = (sender as ToolStripMenuItem).Checked;
            this.chVerim.Legends[0].Enabled = (sender as ToolStripMenuItem).Checked;
        }

        private void menuItemChartType_click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                SeriesChartType chartType = SeriesChartType.Line;

                // Tüm menulerin seçimlerini kaldır.
                ToolStrip toolStripGrafikTuru = menuItem.GetCurrentParent();
                foreach (ToolStripMenuItem stripMenuItem in toolStripGrafikTuru.Items)
                {
                    stripMenuItem.Checked = false;
                }
                (sender as ToolStripMenuItem).Checked = true;
                // menu tipine göre seçim yap
                switch (menuItem.Text)
                {
                    case "Line":
                        chartType = SeriesChartType.Line;
                        break;
                    case "Bar":
                        chartType = SeriesChartType.Bar;
                        break;
                    case "Point":
                        chartType = SeriesChartType.Point;
                        break;
                    case "Spline":
                        chartType = SeriesChartType.Spline;
                        break;
                    case "Area":
                        chartType = SeriesChartType.Area;
                        break;
                    default:
                        break;
                }

                VeriCekGoster.m_CurrentChartType = chartType;

                foreach (Chart ctl in lstCharts)
                {
                    foreach (Series series in ctl.Series)
                    {
                        series.ChartType = chartType;
                    }
                }
            }
        }

        private void menuItemEslestirmeBaslat_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validasyon
                if (dal.f_GetNotSyncDataretrieveCount() == 0)
                {
                    MessageBox.Show("Eşleştirilecek veriniz bulunmamaktadır. Güncel durumdasınız.", "Eşleştirme Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!InternetCS.IsConnectedToInternet())
                {
                    MessageBox.Show("İnternet bağlantısının olduğundan emin olunuz ve tekrar eşleştirme yapınız.", "Eşleştirme Durumu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                #endregion

                bool bSonuc = f_CekGonder();
                if (bSonuc)
                {
                    MessageBox.Show(bSonuc ? "Eşleştirme başarıyla tamamlandı." : "Eşleştirme tamamlanamadı!", "Eşleştirme Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Eşleştirme yapılırken genel istisna fırlatıldı:");
#if DEBUG
                throw (ex);
#endif
            }
        }


        bool f_CekGonder()
        {
            // Verileri çekiyoruz sonrada Web Servisine gönderiyoruz.
            DataSet ds = new DataSet("ds");
            DAL dal = new DAL();

            string sqlDataRetrieves = @"
SELECT  DATARETRIEVES.* 
FROM    DATARETRIEVES 
WHERE   SYNC=0
";
            DataTable dtNonSync = dal.f_ExecuteDataTable(sqlDataRetrieves, "DATARETRIEVES");
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

            // WebServisine gönder
            bool bBasari = f_WebServiseGonder(ds);

            // Başarılı ise SYNC=1 olarak işaretle
            if (bBasari)
            {
                string sqlUpdateDataretrieves = @"
UPDATE  DATARETRIEVES
SET     SYNC=1
WHERE   DATARETRIEVE_ID IN (" + sNonSyncRefCekmeIds + ")";
                dal.f_ExecuteNonQuery(sqlUpdateDataretrieves);
            }
            return bBasari;
        }


        bool f_WebServiseGonder(DataSet _ds)
        {
            VeriAl.VeriAl webSrvc = GetWebSrvc();

            try
            {
                if (string.IsNullOrEmpty(webSrvc.Url))
                {
                    MessageBox.Show(
                        "Verileri gönderebilmek için Web Servisi ayarları yapmalısınız."
                        + Environment.NewLine
                        + "Bunun için Ayarlar->Servis Ayarları ekranını kullanabilirsiniz.", "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                // Girilmiş web servisi adresi doğru mu?
                if (!Uri.IsWellFormedUriString(webSrvc.Url, UriKind.Absolute))
                {
                    MessageBox.Show("Kayıtlı olan web servisi adresi doğru tanımlanmamıştır.", "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.LogException("Verileri eşleştirmek için kullanılacak Web Servisi adresi doğrulanırken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }

            // Verileri web servisi ile gönderme kısmı.
            bool bSonuc = false;
            try
            {
                
                bSonuc = webSrvc.f_VeriAl(_ds);
            }
            catch (Exception ex)
            {
                //--IS
                MemoryStream ms = new MemoryStream();
                _ds.WriteXml(ms);
                StreamReader sr = new StreamReader(ms);
                ex.LogException(sr.ReadToEnd());
#if DEBUG
                throw (ex);
#endif
            }
            return bSonuc;
        }

        private void menuItemServisAyarlari_Click(object sender, EventArgs e)
        {
            (new ServisAyarlari()).ShowDialog();
        }

        private void menuItemHataKayitlari_Click(object sender, EventArgs e)
        {
            (new HataKayitlari()).ShowDialog();
        }

        private void VeriCekGoster_Shown(object sender, EventArgs e)
        {
            if (bKapatilacak)
            {
                this.Close();
            }
        }

        private void VeriCekGoster_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Aktif thread varsa kapatıyoruz.
            f_ThreadStop();
            // Bağlantı varsa kesilmesini sağlayacağız ki bir sonraki açılışta bağlantı sorunu yaşanmasın.
            btnBaglanti_BaglantiyiKes_Click(null,null);
        }

        private void menuItemHakkinda_Click(object sender, EventArgs e)
        {
            (new Hakkinda()).ShowDialog();
        }

        private void btnGoruntule_Click(object sender, EventArgs e)
        {
            f_CihazinTumBilgileri(cbDeviceSerials.Text);
        }

    }
}
