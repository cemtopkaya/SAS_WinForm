using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;
using FMC.Turkiye.SAS;

namespace FMC.Turkiye.SAS
{
    public partial class UrunuKaydet : Form
    {
        public UrunuKaydet()
        {
            InitializeComponent();
            tbKey.Text = Lisans.f_HashedKey();

           
        }

        static public string f_GetCPUId()
        {
            string cpuInfo = String.Empty;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == String.Empty)
                {// only return cpuInfo from first CPU
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            string command = "mailto:erkan.gencer@fmc-ag.com?subject=SAS Uygulaması aktivasyonu&body=Ürün aktivasyonu için anahtar bilgisi: "+tbKey.Text;
            Process.Start(command); 
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSeriNo.Text))
            {
                MessageBox.Show("Seri numarasını girdiğinizden emin olunuz.", "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!Lisans.f_IsSerialValid(tbKey.Text,tbSeriNo.Text))
            {
                MessageBox.Show("Seri numarasını doğru girdiğinizden emin olunuz!", "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

           if(Lisans.f_SetLicenceSerial(tbSeriNo.Text))
           {
               VeriCekGoster.bAktivasyonOk = true;
               this.Close();
           }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            VeriCekGoster.bAktivasyonOk = false;
            this.Close();
        }


    }
}
