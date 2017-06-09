using System;
using System.Configuration;
using System.Windows.Forms;
using FMC.Turkiye.CSharp.Extensions;

namespace FMC.Turkiye.SAS
{
    public partial class ServisAyarlari : Form
    {
        public ServisAyarlari()
        {
            InitializeComponent();

            try
            {
                this.MaximizeBox = false;
                tbVeriGonderme.Text = ConfigurationManager.AppSettings["WSGonderme"];
                tbVeriAlma.Text = ConfigurationManager.AppSettings["WSAlma"];
            }
            catch (Exception ex)
            {
                ex.LogException("Uygulama ayarları app.config dosyasından okunurken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(tbVeriGonderme.Text, UriKind.Absolute))
                {
                    MessageBox.Show("Kayıtlı olan veri gönderme web servisi adresi doğru girilmemiştir.", "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!Uri.IsWellFormedUriString(tbVeriAlma.Text, UriKind.Absolute))
                {
                    MessageBox.Show("Kayıtlı olan veri alma web servisi adresi doğru girilmemiştir.", "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                AppSettingsSection appSettingSection = (AppSettingsSection)config.GetSection("appSettings");
                appSettingSection.Settings["WSAlma"].Value = tbVeriAlma.Text;
                appSettingSection.Settings["WSGonderme"].Value = tbVeriGonderme.Text;
                appSettingSection.CurrentConfiguration.Save();

                MessageBox.Show("Başarıyla Kaydedildi.\nProgramı kapatıp tekrar çalıştırdığınızda geçerli olacaktır.", "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Close();
            }
            catch (Exception ex)
            {
                ex.LogException("Uygulama ayarları app.config dosyasına kaydedilirken genel istisna fırlatıldı.");
#if DEBUG
                throw (ex);
#endif
            }
        }
    }
}
