namespace FMC.Turkiye.SAS
{
    partial class YeniForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.btnBaglanti = new System.Windows.Forms.Button();
            this.rbBaglantiDurumu = new System.Windows.Forms.RadioButton();
            this.spBaglanti = new System.IO.Ports.SerialPort(this.components);
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnPortTazele = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dtpBas = new System.Windows.Forms.DateTimePicker();
            this.dtpSon = new System.Windows.Forms.DateTimePicker();
            this.dgvSonuclar = new System.Windows.Forms.DataGridView();
            this.giris_iletkenlik_grafik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnGetir = new System.Windows.Forms.Button();
            this.uretim_iletkenlik_grafik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.verim_grafik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sicaklik_grafik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.anlik_GirisIletkenlik_tb = new System.Windows.Forms.TextBox();
            this.anlik_UretimIletkenlik_tb = new System.Windows.Forms.TextBox();
            this.anlik_Sicaklik_tb = new System.Windows.Forms.TextBox();
            this.anlik_CalismaSaati_tb = new System.Windows.Forms.TextBox();
            this.giris_iletkenlik_label = new System.Windows.Forms.Label();
            this.uretim_iletkenlik_label = new System.Windows.Forms.Label();
            this.sicaklik_label = new System.Windows.Forms.Label();
            this.calisma_saati_label = new System.Windows.Forms.Label();
            this.anlik_AlarmSayisi_tb = new System.Windows.Forms.TextBox();
            this.dezenfeksiyon_sayisi_label = new System.Windows.Forms.Label();
            this.anlik_DezenfeksiyonSayisi_tb = new System.Windows.Forms.TextBox();
            this.alarm_sayisi_label = new System.Windows.Forms.Label();
            this.anlik_gb = new System.Windows.Forms.GroupBox();
            this.alarmlar_lb = new System.Windows.Forms.ListBox();
            this.alarm_listesi_label = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSonuclar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.giris_iletkenlik_grafik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uretim_iletkenlik_grafik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verim_grafik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sicaklik_grafik)).BeginInit();
            this.anlik_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(9, 14);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(86, 21);
            this.cbPorts.TabIndex = 0;
            // 
            // btnBaglanti
            // 
            this.btnBaglanti.Location = new System.Drawing.Point(156, 12);
            this.btnBaglanti.Name = "btnBaglanti";
            this.btnBaglanti.Size = new System.Drawing.Size(89, 23);
            this.btnBaglanti.TabIndex = 5;
            this.btnBaglanti.Text = "Bağlan";
            this.btnBaglanti.UseVisualStyleBackColor = true;
            this.btnBaglanti.Click += new System.EventHandler(this.btnBaglanti_Click);
            // 
            // rbBaglantiDurumu
            // 
            this.rbBaglantiDurumu.AutoCheck = false;
            this.rbBaglantiDurumu.AutoSize = true;
            this.rbBaglantiDurumu.CausesValidation = false;
            this.rbBaglantiDurumu.Location = new System.Drawing.Point(251, 15);
            this.rbBaglantiDurumu.Name = "rbBaglantiDurumu";
            this.rbBaglantiDurumu.Size = new System.Drawing.Size(34, 17);
            this.rbBaglantiDurumu.TabIndex = 6;
            this.rbBaglantiDurumu.TabStop = true;
            this.rbBaglantiDurumu.Text = "...";
            this.rbBaglantiDurumu.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(319, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Güncelle";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnPortTazele
            // 
            this.btnPortTazele.Location = new System.Drawing.Point(102, 12);
            this.btnPortTazele.Name = "btnPortTazele";
            this.btnPortTazele.Size = new System.Drawing.Size(48, 23);
            this.btnPortTazele.TabIndex = 9;
            this.btnPortTazele.Text = "Yenile";
            this.btnPortTazele.UseVisualStyleBackColor = true;
            this.btnPortTazele.Click += new System.EventHandler(this.btnPortTazele_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(400, 12);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(100, 23);
            this.pb.TabIndex = 15;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Şebeke İletkenliği",
            "Saf Su İletkenliği",
            "Sıcaklık",
            "Verim"});
            this.comboBox1.Location = new System.Drawing.Point(9, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 16;
            // 
            // dtpBas
            // 
            this.dtpBas.Location = new System.Drawing.Point(9, 89);
            this.dtpBas.Name = "dtpBas";
            this.dtpBas.Size = new System.Drawing.Size(200, 20);
            this.dtpBas.TabIndex = 18;
            this.dtpBas.ValueChanged += new System.EventHandler(this.dtpBas_ValueChanged);
            // 
            // dtpSon
            // 
            this.dtpSon.Location = new System.Drawing.Point(215, 89);
            this.dtpSon.Name = "dtpSon";
            this.dtpSon.Size = new System.Drawing.Size(200, 20);
            this.dtpSon.TabIndex = 19;
            // 
            // dgvSonuclar
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSonuclar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSonuclar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSonuclar.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSonuclar.Location = new System.Drawing.Point(9, 460);
            this.dgvSonuclar.Name = "dgvSonuclar";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSonuclar.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSonuclar.Size = new System.Drawing.Size(488, 150);
            this.dgvSonuclar.TabIndex = 20;
            // 
            // giris_iletkenlik_grafik
            // 
            chartArea1.AxisX.Title = "Zaman";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea1.AxisY.Title = "İletkenlik ( uS )";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea1.Name = "ChartArea1";
            this.giris_iletkenlik_grafik.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.giris_iletkenlik_grafik.Legends.Add(legend1);
            this.giris_iletkenlik_grafik.Location = new System.Drawing.Point(524, 12);
            this.giris_iletkenlik_grafik.Name = "giris_iletkenlik_grafik";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Blue;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerBorderWidth = 10;
            series1.Name = "Series1";
            this.giris_iletkenlik_grafik.Series.Add(series1);
            this.giris_iletkenlik_grafik.Size = new System.Drawing.Size(661, 172);
            this.giris_iletkenlik_grafik.TabIndex = 21;
            this.giris_iletkenlik_grafik.Tag = "";
            this.giris_iletkenlik_grafik.Text = "giris_iletkenlik_grafik";
            title1.BackColor = System.Drawing.Color.PowderBlue;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            title1.Name = "Title1";
            title1.Text = "Giriş İletkenliği";
            this.giris_iletkenlik_grafik.Titles.Add(title1);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(136, 60);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(79, 23);
            this.btnExcelExport.TabIndex = 22;
            this.btnExcelExport.Text = "Excel\'e Aktar";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            // 
            // btnGetir
            // 
            this.btnGetir.Location = new System.Drawing.Point(422, 85);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(75, 23);
            this.btnGetir.TabIndex = 23;
            this.btnGetir.Text = "Getir";
            this.btnGetir.UseVisualStyleBackColor = true;
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // uretim_iletkenlik_grafik
            // 
            chartArea2.AxisX.Title = "Zaman";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea2.AxisY.Title = "İletkenlik ( uS )";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea2.Name = "ChartArea1";
            this.uretim_iletkenlik_grafik.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.uretim_iletkenlik_grafik.Legends.Add(legend2);
            this.uretim_iletkenlik_grafik.Location = new System.Drawing.Point(524, 190);
            this.uretim_iletkenlik_grafik.Name = "uretim_iletkenlik_grafik";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Cyan;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.MarkerBorderWidth = 10;
            series2.Name = "Series1";
            this.uretim_iletkenlik_grafik.Series.Add(series2);
            this.uretim_iletkenlik_grafik.Size = new System.Drawing.Size(661, 172);
            this.uretim_iletkenlik_grafik.TabIndex = 24;
            this.uretim_iletkenlik_grafik.Tag = "";
            this.uretim_iletkenlik_grafik.Text = "uretim_iletkenlik_grafik";
            title2.BackColor = System.Drawing.Color.PowderBlue;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            title2.Name = "Title1";
            title2.Text = "Üretim İletkenliği";
            this.uretim_iletkenlik_grafik.Titles.Add(title2);
            // 
            // verim_grafik
            // 
            chartArea3.AxisX.Title = "Zaman";
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea3.AxisY.Title = "Verim ( % )";
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea3.Name = "ChartArea1";
            this.verim_grafik.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.verim_grafik.Legends.Add(legend3);
            this.verim_grafik.Location = new System.Drawing.Point(524, 368);
            this.verim_grafik.Name = "verim_grafik";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.MarkerBorderWidth = 10;
            series3.Name = "Series1";
            this.verim_grafik.Series.Add(series3);
            this.verim_grafik.Size = new System.Drawing.Size(661, 172);
            this.verim_grafik.TabIndex = 25;
            this.verim_grafik.Tag = "";
            this.verim_grafik.Text = "verim_grafik";
            title3.BackColor = System.Drawing.Color.PowderBlue;
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            title3.Name = "Title1";
            title3.Text = "Verim";
            this.verim_grafik.Titles.Add(title3);
            // 
            // sicaklik_grafik
            // 
            chartArea4.AxisX.Title = "Zaman";
            chartArea4.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea4.AxisY.Title = "Sıcaklık ( °C )";
            chartArea4.AxisY.TitleFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            chartArea4.Name = "ChartArea1";
            this.sicaklik_grafik.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.sicaklik_grafik.Legends.Add(legend4);
            this.sicaklik_grafik.Location = new System.Drawing.Point(524, 546);
            this.sicaklik_grafik.Name = "sicaklik_grafik";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Red;
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.MarkerBorderWidth = 10;
            series4.Name = "Series1";
            this.sicaklik_grafik.Series.Add(series4);
            this.sicaklik_grafik.Size = new System.Drawing.Size(661, 172);
            this.sicaklik_grafik.TabIndex = 26;
            this.sicaklik_grafik.Tag = "";
            this.sicaklik_grafik.Text = "sicaklik_grafik";
            title4.BackColor = System.Drawing.Color.PowderBlue;
            title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            title4.Name = "Title1";
            title4.Text = "Su Sıcaklığı";
            this.sicaklik_grafik.Titles.Add(title4);
            // 
            // anlik_GirisIletkenlik_tb
            // 
            this.anlik_GirisIletkenlik_tb.Enabled = false;
            this.anlik_GirisIletkenlik_tb.Location = new System.Drawing.Point(132, 22);
            this.anlik_GirisIletkenlik_tb.Name = "anlik_GirisIletkenlik_tb";
            this.anlik_GirisIletkenlik_tb.Size = new System.Drawing.Size(39, 20);
            this.anlik_GirisIletkenlik_tb.TabIndex = 27;
            // 
            // anlik_UretimIletkenlik_tb
            // 
            this.anlik_UretimIletkenlik_tb.Enabled = false;
            this.anlik_UretimIletkenlik_tb.Location = new System.Drawing.Point(132, 48);
            this.anlik_UretimIletkenlik_tb.Name = "anlik_UretimIletkenlik_tb";
            this.anlik_UretimIletkenlik_tb.Size = new System.Drawing.Size(39, 20);
            this.anlik_UretimIletkenlik_tb.TabIndex = 28;
            // 
            // anlik_Sicaklik_tb
            // 
            this.anlik_Sicaklik_tb.Enabled = false;
            this.anlik_Sicaklik_tb.Location = new System.Drawing.Point(132, 74);
            this.anlik_Sicaklik_tb.Name = "anlik_Sicaklik_tb";
            this.anlik_Sicaklik_tb.Size = new System.Drawing.Size(39, 20);
            this.anlik_Sicaklik_tb.TabIndex = 29;
            // 
            // anlik_CalismaSaati_tb
            // 
            this.anlik_CalismaSaati_tb.Enabled = false;
            this.anlik_CalismaSaati_tb.Location = new System.Drawing.Point(132, 100);
            this.anlik_CalismaSaati_tb.Name = "anlik_CalismaSaati_tb";
            this.anlik_CalismaSaati_tb.Size = new System.Drawing.Size(39, 20);
            this.anlik_CalismaSaati_tb.TabIndex = 30;
            // 
            // giris_iletkenlik_label
            // 
            this.giris_iletkenlik_label.AutoSize = true;
            this.giris_iletkenlik_label.Location = new System.Drawing.Point(10, 25);
            this.giris_iletkenlik_label.Name = "giris_iletkenlik_label";
            this.giris_iletkenlik_label.Size = new System.Drawing.Size(78, 13);
            this.giris_iletkenlik_label.TabIndex = 31;
            this.giris_iletkenlik_label.Text = "Giriş İletkenlik :";
            // 
            // uretim_iletkenlik_label
            // 
            this.uretim_iletkenlik_label.AutoSize = true;
            this.uretim_iletkenlik_label.Location = new System.Drawing.Point(10, 57);
            this.uretim_iletkenlik_label.Name = "uretim_iletkenlik_label";
            this.uretim_iletkenlik_label.Size = new System.Drawing.Size(88, 13);
            this.uretim_iletkenlik_label.TabIndex = 32;
            this.uretim_iletkenlik_label.Text = "Üretim İletkenlik :";
            // 
            // sicaklik_label
            // 
            this.sicaklik_label.AutoSize = true;
            this.sicaklik_label.Location = new System.Drawing.Point(10, 81);
            this.sicaklik_label.Name = "sicaklik_label";
            this.sicaklik_label.Size = new System.Drawing.Size(50, 13);
            this.sicaklik_label.TabIndex = 33;
            this.sicaklik_label.Text = "Sıcaklık :";
            // 
            // calisma_saati_label
            // 
            this.calisma_saati_label.AutoSize = true;
            this.calisma_saati_label.Location = new System.Drawing.Point(10, 107);
            this.calisma_saati_label.Name = "calisma_saati_label";
            this.calisma_saati_label.Size = new System.Drawing.Size(76, 13);
            this.calisma_saati_label.TabIndex = 34;
            this.calisma_saati_label.Text = "Çalışma Saati :";
            // 
            // anlik_AlarmSayisi_tb
            // 
            this.anlik_AlarmSayisi_tb.Enabled = false;
            this.anlik_AlarmSayisi_tb.Location = new System.Drawing.Point(132, 126);
            this.anlik_AlarmSayisi_tb.Name = "anlik_AlarmSayisi_tb";
            this.anlik_AlarmSayisi_tb.Size = new System.Drawing.Size(39, 20);
            this.anlik_AlarmSayisi_tb.TabIndex = 35;
            // 
            // dezenfeksiyon_sayisi_label
            // 
            this.dezenfeksiyon_sayisi_label.AutoSize = true;
            this.dezenfeksiyon_sayisi_label.Location = new System.Drawing.Point(10, 159);
            this.dezenfeksiyon_sayisi_label.Name = "dezenfeksiyon_sayisi_label";
            this.dezenfeksiyon_sayisi_label.Size = new System.Drawing.Size(113, 13);
            this.dezenfeksiyon_sayisi_label.TabIndex = 36;
            this.dezenfeksiyon_sayisi_label.Text = "Dezenfeksiyon Sayısı :";
            // 
            // anlik_DezenfeksiyonSayisi_tb
            // 
            this.anlik_DezenfeksiyonSayisi_tb.Enabled = false;
            this.anlik_DezenfeksiyonSayisi_tb.Location = new System.Drawing.Point(132, 152);
            this.anlik_DezenfeksiyonSayisi_tb.Name = "anlik_DezenfeksiyonSayisi_tb";
            this.anlik_DezenfeksiyonSayisi_tb.Size = new System.Drawing.Size(39, 20);
            this.anlik_DezenfeksiyonSayisi_tb.TabIndex = 37;
            // 
            // alarm_sayisi_label
            // 
            this.alarm_sayisi_label.AutoSize = true;
            this.alarm_sayisi_label.Location = new System.Drawing.Point(10, 133);
            this.alarm_sayisi_label.Name = "alarm_sayisi_label";
            this.alarm_sayisi_label.Size = new System.Drawing.Size(69, 13);
            this.alarm_sayisi_label.TabIndex = 38;
            this.alarm_sayisi_label.Text = "Alarm Sayısı :";
            // 
            // anlik_gb
            // 
            this.anlik_gb.Controls.Add(this.alarm_sayisi_label);
            this.anlik_gb.Controls.Add(this.anlik_DezenfeksiyonSayisi_tb);
            this.anlik_gb.Controls.Add(this.dezenfeksiyon_sayisi_label);
            this.anlik_gb.Controls.Add(this.anlik_AlarmSayisi_tb);
            this.anlik_gb.Controls.Add(this.calisma_saati_label);
            this.anlik_gb.Controls.Add(this.sicaklik_label);
            this.anlik_gb.Controls.Add(this.uretim_iletkenlik_label);
            this.anlik_gb.Controls.Add(this.giris_iletkenlik_label);
            this.anlik_gb.Controls.Add(this.anlik_CalismaSaati_tb);
            this.anlik_gb.Controls.Add(this.anlik_Sicaklik_tb);
            this.anlik_gb.Controls.Add(this.anlik_UretimIletkenlik_tb);
            this.anlik_gb.Controls.Add(this.anlik_GirisIletkenlik_tb);
            this.anlik_gb.Location = new System.Drawing.Point(9, 130);
            this.anlik_gb.Name = "anlik_gb";
            this.anlik_gb.Size = new System.Drawing.Size(188, 183);
            this.anlik_gb.TabIndex = 39;
            this.anlik_gb.TabStop = false;
            this.anlik_gb.Text = "Anlık Değerler";
            // 
            // alarmlar_lb
            // 
            this.alarmlar_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.alarmlar_lb.FormattingEnabled = true;
            this.alarmlar_lb.ItemHeight = 15;
            this.alarmlar_lb.Location = new System.Drawing.Point(218, 159);
            this.alarmlar_lb.Name = "alarmlar_lb";
            this.alarmlar_lb.Size = new System.Drawing.Size(303, 154);
            this.alarmlar_lb.TabIndex = 40;
            // 
            // alarm_listesi_label
            // 
            this.alarm_listesi_label.AutoSize = true;
            this.alarm_listesi_label.Location = new System.Drawing.Point(215, 143);
            this.alarm_listesi_label.Name = "alarm_listesi_label";
            this.alarm_listesi_label.Size = new System.Drawing.Size(65, 13);
            this.alarm_listesi_label.TabIndex = 41;
            this.alarm_listesi_label.Text = "Alarm Listesi";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(204, 338);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 42;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // YeniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 745);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.alarm_listesi_label);
            this.Controls.Add(this.alarmlar_lb);
            this.Controls.Add(this.anlik_gb);
            this.Controls.Add(this.sicaklik_grafik);
            this.Controls.Add(this.verim_grafik);
            this.Controls.Add(this.uretim_iletkenlik_grafik);
            this.Controls.Add(this.btnGetir);
            this.Controls.Add(this.btnExcelExport);
            this.Controls.Add(this.giris_iletkenlik_grafik);
            this.Controls.Add(this.dgvSonuclar);
            this.Controls.Add(this.dtpSon);
            this.Controls.Add(this.dtpBas);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btnPortTazele);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.rbBaglantiDurumu);
            this.Controls.Add(this.btnBaglanti);
            this.Controls.Add(this.cbPorts);
            this.Name = "YeniForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSonuclar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.giris_iletkenlik_grafik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uretim_iletkenlik_grafik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verim_grafik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sicaklik_grafik)).EndInit();
            this.anlik_gb.ResumeLayout(false);
            this.anlik_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.Button btnBaglanti;
        private System.Windows.Forms.RadioButton rbBaglantiDurumu;
        private System.IO.Ports.SerialPort spBaglanti;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnPortTazele;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dtpBas;
        private System.Windows.Forms.DateTimePicker dtpSon;
        private System.Windows.Forms.DataGridView dgvSonuclar;
        private System.Windows.Forms.DataVisualization.Charting.Chart giris_iletkenlik_grafik;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Button btnGetir;
        private System.Windows.Forms.DataVisualization.Charting.Chart uretim_iletkenlik_grafik;
        private System.Windows.Forms.DataVisualization.Charting.Chart verim_grafik;
        private System.Windows.Forms.DataVisualization.Charting.Chart sicaklik_grafik;
        private System.Windows.Forms.TextBox anlik_GirisIletkenlik_tb;
        private System.Windows.Forms.TextBox anlik_UretimIletkenlik_tb;
        private System.Windows.Forms.TextBox anlik_Sicaklik_tb;
        private System.Windows.Forms.TextBox anlik_CalismaSaati_tb;
        private System.Windows.Forms.Label giris_iletkenlik_label;
        private System.Windows.Forms.Label uretim_iletkenlik_label;
        private System.Windows.Forms.Label sicaklik_label;
        private System.Windows.Forms.Label calisma_saati_label;
        private System.Windows.Forms.TextBox anlik_AlarmSayisi_tb;
        private System.Windows.Forms.Label dezenfeksiyon_sayisi_label;
        private System.Windows.Forms.TextBox anlik_DezenfeksiyonSayisi_tb;
        private System.Windows.Forms.Label alarm_sayisi_label;
        private System.Windows.Forms.GroupBox anlik_gb;
        private System.Windows.Forms.ListBox alarmlar_lb;
        private System.Windows.Forms.Label alarm_listesi_label;
        private System.Windows.Forms.Button btnTest;
    }
}

