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
    public partial class LisansBilgileri : Form
    {
        public LisansBilgileri()
        {
            InitializeComponent();

            try
            {
                tbAnahtar.Text = Lisans.f_HashedKey();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void btnAktiveEt_Click(object sender, EventArgs e)
        {
            bool bLisanslandi = Lisans.f_SetLicenceSerial(tbSeriNo.Text);
            if (bLisanslandi)
            {
                // Lisanslama işlemi tamamlandı devam.
                MessageBox.Show("Lisans bilgileri alındı.\nDoğrulamanın ardından devam edilecektir. ", "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else
            {
                MessageBox.Show("Lisans bilgileri girilemedi.", "Lisanslama", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
