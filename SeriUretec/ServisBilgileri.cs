using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FMC.Turkiye.SAS;

namespace SeriUretec
{
    public partial class ServisBilgileri : Form
    {
        public ServisBilgileri()
        {
            InitializeComponent();

            try
            {
                tbVeriAlma.Text = ConfigurationManager.AppSettings["WSAlma"];
                tbVeriGonderme.Text = ConfigurationManager.AppSettings["WSGonderme"];
                tbYonetim.Text = ConfigurationManager.AppSettings["WSYonetim"];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                AppSettingsSection appSettingSection = (AppSettingsSection)config.GetSection("appSettings");
                appSettingSection.Settings["WSAlma"].Value = tbVeriAlma.Text;
                appSettingSection.Settings["WSGonderme"].Value = tbVeriGonderme.Text;
                appSettingSection.Settings["WSYonetim"].Value = tbYonetim.Text;
                appSettingSection.CurrentConfiguration.Save(); 

                MessageBox.Show("Başarıyla Kaydedildi.\nProgramı kapatıp tekrar çalıştırdığınızda geçerli olacaktır.", "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
