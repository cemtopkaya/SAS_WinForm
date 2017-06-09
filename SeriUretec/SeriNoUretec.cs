using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Windows.Forms;
using FMC.Turkiye.CSharp.Extensions;
using Extensions = FMC.Turkiye.CSharp.Extensions.Extensions;
using Lisans = FMC.Turkiye.SAS.Lisans;

namespace SeriUretec
{
    public partial class SeriNoUretec : Form
    {
        private Yonetim.Yonetim wsrc =new Yonetim.Yonetim();
        private bool bKapatilacak;
        public SeriNoUretec()
        {
            if (ConfigurationManager.AppSettings["WSYonetim"] == null)
            {
                MessageBox.Show("Lütfen web servisi adres bilgilerini kontrol ediniz.", "Sunucuya Bilgileri.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                wsrc.Url = ConfigurationManager.AppSettings["WSYonetim"];                
            }
            
            InitializeComponent();
            try
            {
                bool bLisansli = Lisans.isLicenced;
                while (!bLisansli)
                {
                    DialogResult dr = (new LisansBilgileri()).ShowDialog();
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

                    MessageBox.Show("Lisans bilgileriniz geçerli değil! \n Lütfen yeniden deneyin.", "Lisans Doğrulama", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Extensions.LogException(ex, "Ürün lisanslaması kontrolünde genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
            // Web servisi verilerini çekmeden önce lisans bilgilerini girelim.
            f_AuthWebService();
        }

        void f_AuthWebService()
        {
            string sKey, sSerial;
            Lisans.f_GetLicenceSerialFromReg(out sKey, out sSerial);
            wsrc.AuthHeaderValue = new Yonetim.AuthHeader()
                                       {
                                           Username = sKey,
                                           Password = sSerial
                                       };
        }

        private void f_BolgeleriBagla()
        {
            DataTable dt = wsrc.f_Bolgeler();
            cbBolgesi.ValueMember = "Bolge_id";
            cbBolgesi.DisplayMember = "Adi";
            cbBolgesi.DataSource = dt;
        }

        private void btnGenerateSerial_Click(object sender, EventArgs e)
        {
            try
            {
                tbSeriNo.Text = Lisans.f_GetSerialFromKey(tbAnahtar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if DEBUG
                throw (ex);
#endif
            }
        }

        private void SeriNoUretec_Shown(object sender, EventArgs e)
        {
            if (bKapatilacak)
            {
                this.Close();
                return;
            }

            try
            {
                f_BolgeleriBagla();
            }
            catch (UnauthorizedAccessException uaex)
            {
                MessageBox.Show("Sisteme erişme yetkiniz yok. Kullanıcı bilgilerinizi gözden geçiriniz.", "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if DEBUG
                throw (uaex);
#endif
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ConnectFailure||wex.Status == WebExceptionStatus.SendFailure)
                {
                    MessageBox.Show("Lütfen web servisi adres bilgilerini kontrol ediniz.\n" + wex.Message, "Sunucuya Bağlanamadı.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    menuItemServisBilgileri_Click(null, null);
                }
#if DEBUG
                //throw (wex);
#endif
            }
            catch (Exception ex)
            {
                //--IS
                MessageBox.Show(ex.Message, "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if DEBUG
                throw (ex);
#endif
            }
        }

        private void menuItemServisBilgileri_Click(object sender, EventArgs e)
        {
            (new ServisBilgileri()).Show();
        }

        private void menuItemLisansBilgileri_Click(object sender, EventArgs e)
        {
            (new LisansBilgileri()).Show();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validasyon
                if (tbAnahtar.Text.Length != 68)
                {
                    MessageBox.Show("Anahtarın uzunluğu 68 karakter olmalıdır.", "Anahtar Uzunluğu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (tbSeriNo.Text.Length != 6)
                {
                    MessageBox.Show("Seri Numarasının uzunluğu 6 karakter olmalıdır.", "Seri No Uzunluğu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cbBolgesi.SelectedIndex < 0)
                {
                    MessageBox.Show("Bölgenin seçilmiş olması gereklidir.", "Bölge Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                bool bKayitSonucu = wsrc.f_KullaniciEkle(tbAnahtar.Text, tbSeriNo.Text, tbKurumAdi.Text, cbBolgesi.SelectedValue.To<int>(), tbYetkili.Text);
                if (bKayitSonucu)
                {
                    MessageBox.Show("Kayıt işlemi BAŞARIYLA tamamlanmıştır.", "Yeni KULLANICI KAYDI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kayıt işlemi TAMAMLANAMADI!.", "Yeni KULLANICI KAYDI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Extensions.LogException(ex, "Kullanıcı kaydı yapılırken istisna fırlatıldı.");
                MessageBox.Show("Kayıt işlemi yapılırken genel istisna fırlatıldı.", "Yeni KULLANICI KAYDI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
#if DEBUG
                throw (ex);
#endif
            }
        }
    }
}
