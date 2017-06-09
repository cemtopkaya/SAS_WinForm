using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using SASLib;

namespace FMC.Turkiye.SAS
{
    public partial class HataKayitlari : Form
    {
        public HataKayitlari()
        {
            InitializeComponent();
            btnTazele_Click(null,null);
        }

        private void btnTazele_Click(object sender, EventArgs e)
        {
            DataTable dt = Logger.GetEventLogs(ConfigurationManager.AppSettings["LogName"]);
            dgvLogs.ReadOnly = false;
            dgvLogs.DataSource = dt;
            dgvLogs.Columns[0].Width = 100;
            dgvLogs.Columns[1].Width = (this.ClientSize.Width - 270);
            dgvLogs.Columns[2].Width = 100;
            dgvLogs.ReadOnly = true;
        }
    }
}