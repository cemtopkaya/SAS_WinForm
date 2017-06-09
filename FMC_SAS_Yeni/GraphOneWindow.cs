using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FMC.Turkiye.CSharp.Extensions;
using FMC.Turkiye.SAS;

namespace FMC.Turkiye.SAS
{
    public partial class GraphOneWindow : Form
    {
        private DAL dal = new DAL();

        public GraphOneWindow(string _sCaption)
        {
            InitializeComponent();
            this.Text = _sCaption;
            f_DeviceSerials();

            // Chart üzerinde zoom aktif...
            //ch.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            ch.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            ch.MouseClick += new MouseEventHandler(chart_MouseClick);
        }

        void f_DeviceSerials()
        {
            DataTable dtSerials = dal.f_GetDeviceSerials();
            if (dtSerials == null && dtSerials.Rows.Count == 0)
            {
                MessageBox.Show("Bir su arıtma cihazından çekilmiş veri bulunamadı.", "Veri Yok", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnGoruntule.Enabled = false;
                return;
            }
            cbDeviceSerials.ValueMember = "DEVICESERIAL";
            cbDeviceSerials.DisplayMember = "DEVICESERIAL";
            cbDeviceSerials.DataSource = dtSerials;
        }

        private void btnGoruntule_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDeviceSerials.SelectedItem == null)
                {
                    MessageBox.Show("Bir cihaz seri numarası seçmeniz gerekiyor!", "Cihaz Seri Numarası", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Graphs.f_GraphSeries(dal, dtpBas.Value, dtpSon.Value, ch, cbDeviceSerials.Text, this.Text);
            }
            catch (Exception ex)
            {
                //--IS
                ex.LogException("Grafikler çizdirilirken genel istisna fırlatıldı:");
                MessageBox.Show("İSTİSNA OLUŞTU" + Environment.NewLine + ex.Message, "Grafik Görüntüleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void chart_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as Chart) == null)
            {
                return;
            }

            // Sağ tuşla grafik eski haline dönecek.
            if (e.Button == MouseButtons.Right)
            {
                Chart ch = sender as Chart;
                ch.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
                ch.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);

                ch.ChartAreas[0].CursorX.SelectionStart = double.NaN;
                ch.ChartAreas[0].CursorY.SelectionEnd = double.NaN;
            }
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

                foreach (Series series in ch.Series)
                {
                    series.ChartType = chartType;
                }
            }
        }
    }
}
