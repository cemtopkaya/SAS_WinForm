using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FMC.Turkiye.SAS;

namespace FMC.Turkiye.SAS
{
    public partial class Graphs : Form
    {
        DAL dal = new DAL();

        public Graphs()
        {
            InitializeComponent();
            dtpBas.Value = new DateTime(2011, 1, 1);
        }

        private double xBas, yBas, xSon, ySon;
        void SetChartZoomable()
        {   // Zoom and Scale
            chGirisIletkenligi.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chGirisIletkenligi.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            //chart1.ChartAreas[0].CursorX.AutoScroll= true;
            //chart1.ChartAreas[0].CursorY.AutoScroll= true;

            chGirisIletkenligi.MouseClick += delegate(object sender, MouseEventArgs e)
                                     {
                                         if (e.Button == MouseButtons.Left)
                                         {
                                             xBas = e.X;
                                             yBas = e.Y;
                                         }
                                     };

            chGirisIletkenligi.MouseUp += delegate(object sender, MouseEventArgs e)
                                  {
                                      if (e.Button == MouseButtons.Left)
                                      {
                                          xSon = e.X;
                                          ySon = e.Y;
                                          chart1_Zoom();
                                      }
                                  };
        }

        public static Series f_GraphSeries(DAL _dal, DateTime _dtBas, DateTime _dtSon, Chart _ch, string _sDeviceSerial, string _sSerieName)
        {
            DataTable dt = null;
            switch (_sSerieName)
            {
                case "GİRİŞ İLETKENLİĞİ":
                    dt = _dal.f_GetGirisIletkenligi(_dtBas, _dtSon, _sDeviceSerial);
                    break;

                case "ÜRETİM İLETKENLİĞİ":
                    dt = _dal.f_GetUretimIletkenligi(_dtBas, _dtSon, _sDeviceSerial);
                    break;

                case "VERİM":
                    dt = _dal.f_GetVerim(_dtBas, _dtSon, _sDeviceSerial);
                    break;

                case "SU SICAKLIĞI":
                    dt = _dal.f_GetSicaklik(_dtBas, _dtSon, _sDeviceSerial);
                    break;
            }


            if (dt != null)
            {
                _ch.Series.Clear();
                Series serie = new Series(_sDeviceSerial);
                serie.ChartType = SeriesChartType.Line;
                serie.XValueType = ChartValueType.DateTime;

                if (dt.Rows.Count > 0)
                {
                    _ch.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(dt.Rows[0][1]);

                    foreach (DataRow row in dt.Rows)
                    {
                        double y = Convert.ToDouble(row[1]);
                        _ch.ChartAreas[0].AxisY.Minimum = y < _ch.ChartAreas[0].AxisY.Minimum
                                                              ? y
                                                              : _ch.ChartAreas[0].AxisY.Minimum;
                        serie.Points.AddXY(row[0], y);
                    }
                }
                _ch.Series.Add(serie);
                return serie;
            }
            return null;
        }


        private void chart1_Zoom()
        {
            chGirisIletkenligi.ChartAreas[0].AxisX.ScaleView.Zoom(xSon, xBas);
            chGirisIletkenligi.ChartAreas[0].AxisY.ScaleView.Zoom(ySon, yBas);
            chGirisIletkenligi.ChartAreas[0].CursorX.Position = 100;
            chGirisIletkenligi.ChartAreas[0].CursorY.Position = 50;
            //chart1.ChartAreas[0].Position.X = (float) 10;
            //chart1.ChartAreas[0].Position.Y = (float) 10;
            //chart1.ChartAreas[0].CursorX.SetCursorPosition(xSon);
            //chart1.ChartAreas[0].CursorY.SetCursorPosition(ySon);


        }

        private void dtBas_ValueChanged(object sender, EventArgs e)
        {
            dtpSon.MinDate = dtpBas.Value;
        }
    }
}
