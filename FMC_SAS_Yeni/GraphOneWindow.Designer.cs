namespace FMC.Turkiye.SAS
{
    partial class GraphOneWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphOneWindow));
            this.ch = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblBitTar = new System.Windows.Forms.Label();
            this.lblBasTar = new System.Windows.Forms.Label();
            this.lblSeriNo = new System.Windows.Forms.Label();
            this.cbDeviceSerials = new System.Windows.Forms.ComboBox();
            this.btnGoruntule = new System.Windows.Forms.Button();
            this.dtpSon = new System.Windows.Forms.DateTimePicker();
            this.dtpBas = new System.Windows.Forms.DateTimePicker();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuItemGrafikler = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGiris = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUretim = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVerim = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSicaklik = new System.Windows.Forms.ToolStripMenuItem();
            this.grafikTuruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ch)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ch
            // 
            this.ch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.CursorX.IntervalOffset = 1D;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ca1";
            this.ch.ChartAreas.Add(chartArea1);
            this.ch.Location = new System.Drawing.Point(12, 74);
            this.ch.Name = "ch";
            series1.ChartArea = "ca1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "seriesGirisIletkenligi";
            this.ch.Series.Add(series1);
            this.ch.Size = new System.Drawing.Size(890, 492);
            this.ch.TabIndex = 26;
            this.ch.Text = "Giriş İletkenliği";
            // 
            // lblBitTar
            // 
            this.lblBitTar.AutoSize = true;
            this.lblBitTar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBitTar.Location = new System.Drawing.Point(310, 30);
            this.lblBitTar.Name = "lblBitTar";
            this.lblBitTar.Size = new System.Drawing.Size(55, 13);
            this.lblBitTar.TabIndex = 63;
            this.lblBitTar.Text = "Bitiş Tarihi";
            // 
            // lblBasTar
            // 
            this.lblBasTar.AutoSize = true;
            this.lblBasTar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBasTar.Location = new System.Drawing.Point(138, 30);
            this.lblBasTar.Name = "lblBasTar";
            this.lblBasTar.Size = new System.Drawing.Size(82, 13);
            this.lblBasTar.TabIndex = 64;
            this.lblBasTar.Text = "Başlangıç Tarihi";
            // 
            // lblSeriNo
            // 
            this.lblSeriNo.AutoSize = true;
            this.lblSeriNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSeriNo.Location = new System.Drawing.Point(13, 30);
            this.lblSeriNo.Name = "lblSeriNo";
            this.lblSeriNo.Size = new System.Drawing.Size(71, 13);
            this.lblSeriNo.TabIndex = 62;
            this.lblSeriNo.Text = "Cihaz Seri No";
            // 
            // cbDeviceSerials
            // 
            this.cbDeviceSerials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeviceSerials.FormattingEnabled = true;
            this.cbDeviceSerials.Location = new System.Drawing.Point(12, 46);
            this.cbDeviceSerials.Name = "cbDeviceSerials";
            this.cbDeviceSerials.Size = new System.Drawing.Size(121, 21);
            this.cbDeviceSerials.TabIndex = 61;
            // 
            // btnGoruntule
            // 
            this.btnGoruntule.Image = global::FMC.Turkiye.SAS.Properties.Resources.run;
            this.btnGoruntule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoruntule.Location = new System.Drawing.Point(485, 45);
            this.btnGoruntule.Name = "btnGoruntule";
            this.btnGoruntule.Size = new System.Drawing.Size(90, 23);
            this.btnGoruntule.TabIndex = 60;
            this.btnGoruntule.Text = "Görüntüle";
            this.btnGoruntule.UseVisualStyleBackColor = true;
            this.btnGoruntule.Click += new System.EventHandler(this.btnGoruntule_Click);
            // 
            // dtpSon
            // 
            this.dtpSon.Location = new System.Drawing.Point(313, 46);
            this.dtpSon.Name = "dtpSon";
            this.dtpSon.Size = new System.Drawing.Size(166, 20);
            this.dtpSon.TabIndex = 59;
            // 
            // dtpBas
            // 
            this.dtpBas.Location = new System.Drawing.Point(141, 46);
            this.dtpBas.Name = "dtpBas";
            this.dtpBas.Size = new System.Drawing.Size(166, 20);
            this.dtpBas.TabIndex = 58;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGrafikler,
            this.grafikTuruToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(914, 24);
            this.menu.TabIndex = 65;
            this.menu.Text = "menuStrip1";
            // 
            // menuItemGrafikler
            // 
            this.menuItemGrafikler.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGiris,
            this.menuItemUretim,
            this.menuItemVerim,
            this.menuItemSicaklik});
            this.menuItemGrafikler.Name = "menuItemGrafikler";
            this.menuItemGrafikler.Size = new System.Drawing.Size(63, 20);
            this.menuItemGrafikler.Text = "Grafikler";
            // 
            // menuItemGiris
            // 
            this.menuItemGiris.Image = global::FMC.Turkiye.SAS.Properties.Resources.tPaloOutput_icon32;
            this.menuItemGiris.Name = "menuItemGiris";
            this.menuItemGiris.Size = new System.Drawing.Size(183, 22);
            this.menuItemGiris.Text = "GİRİŞ İLETKENLİĞİ";
            this.menuItemGiris.Click += new System.EventHandler(this.menuItemBuyukGrafikGoster);
            // 
            // menuItemUretim
            // 
            this.menuItemUretim.Image = global::FMC.Turkiye.SAS.Properties.Resources.input;
            this.menuItemUretim.Name = "menuItemUretim";
            this.menuItemUretim.Size = new System.Drawing.Size(183, 22);
            this.menuItemUretim.Text = "ÜRETİM İLETKENLİĞİ";
            this.menuItemUretim.Click += new System.EventHandler(this.menuItemBuyukGrafikGoster);
            // 
            // menuItemVerim
            // 
            this.menuItemVerim.Image = global::FMC.Turkiye.SAS.Properties.Resources.productive_icon_2_;
            this.menuItemVerim.Name = "menuItemVerim";
            this.menuItemVerim.Size = new System.Drawing.Size(183, 22);
            this.menuItemVerim.Text = "VERİM";
            this.menuItemVerim.Click += new System.EventHandler(this.menuItemBuyukGrafikGoster);
            // 
            // menuItemSicaklik
            // 
            this.menuItemSicaklik.Image = global::FMC.Turkiye.SAS.Properties.Resources.temperature_1_1;
            this.menuItemSicaklik.Name = "menuItemSicaklik";
            this.menuItemSicaklik.Size = new System.Drawing.Size(183, 22);
            this.menuItemSicaklik.Text = "SU SICAKLIĞI";
            this.menuItemSicaklik.Click += new System.EventHandler(this.menuItemBuyukGrafikGoster);
            // 
            // grafikTuruToolStripMenuItem
            // 
            this.grafikTuruToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineToolStripMenuItem,
            this.barToolStripMenuItem,
            this.areaToolStripMenuItem,
            this.splineToolStripMenuItem,
            this.pointToolStripMenuItem});
            this.grafikTuruToolStripMenuItem.Image = global::FMC.Turkiye.SAS.Properties.Resources.pie_chart;
            this.grafikTuruToolStripMenuItem.Name = "grafikTuruToolStripMenuItem";
            this.grafikTuruToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.grafikTuruToolStripMenuItem.Text = "Grafik Türü";
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Checked = true;
            this.lineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lineToolStripMenuItem.Image = global::FMC.Turkiye.SAS.Properties.Resources.Actions_office_chart_line_stacked_icon_1_;
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.menuItemChartType_click);
            // 
            // barToolStripMenuItem
            // 
            this.barToolStripMenuItem.Image = global::FMC.Turkiye.SAS.Properties.Resources.Actions_office_chart_bar_icon_1_;
            this.barToolStripMenuItem.Name = "barToolStripMenuItem";
            this.barToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.barToolStripMenuItem.Text = "Bar";
            this.barToolStripMenuItem.Click += new System.EventHandler(this.menuItemChartType_click);
            // 
            // areaToolStripMenuItem
            // 
            this.areaToolStripMenuItem.Image = global::FMC.Turkiye.SAS.Properties.Resources.Actions_office_chart_area_stacked_icon_1_;
            this.areaToolStripMenuItem.Name = "areaToolStripMenuItem";
            this.areaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.areaToolStripMenuItem.Text = "Area";
            this.areaToolStripMenuItem.Click += new System.EventHandler(this.menuItemChartType_click);
            // 
            // splineToolStripMenuItem
            // 
            this.splineToolStripMenuItem.Image = global::FMC.Turkiye.SAS.Properties.Resources.Chart_About_Stack_Spline_Charts_01_1_;
            this.splineToolStripMenuItem.Name = "splineToolStripMenuItem";
            this.splineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.splineToolStripMenuItem.Text = "Spline";
            this.splineToolStripMenuItem.Click += new System.EventHandler(this.menuItemChartType_click);
            // 
            // pointToolStripMenuItem
            // 
            this.pointToolStripMenuItem.Image = global::FMC.Turkiye.SAS.Properties.Resources.Actions_office_chart_scatter_icon;
            this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            this.pointToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pointToolStripMenuItem.Text = "Point";
            this.pointToolStripMenuItem.Click += new System.EventHandler(this.menuItemChartType_click);
            // 
            // GraphOneWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 578);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.lblBitTar);
            this.Controls.Add(this.lblBasTar);
            this.Controls.Add(this.lblSeriNo);
            this.Controls.Add(this.cbDeviceSerials);
            this.Controls.Add(this.btnGoruntule);
            this.Controls.Add(this.dtpSon);
            this.Controls.Add(this.dtpBas);
            this.Controls.Add(this.ch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GraphOneWindow";
            this.Text = "GraphOneWindow";
            ((System.ComponentModel.ISupportInitialize)(this.ch)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ch;
        private System.Windows.Forms.Label lblBitTar;
        private System.Windows.Forms.Label lblBasTar;
        private System.Windows.Forms.Label lblSeriNo;
        private System.Windows.Forms.ComboBox cbDeviceSerials;
        private System.Windows.Forms.Button btnGoruntule;
        private System.Windows.Forms.DateTimePicker dtpSon;
        private System.Windows.Forms.DateTimePicker dtpBas;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuItemGrafikler;
        private System.Windows.Forms.ToolStripMenuItem menuItemGiris;
        private System.Windows.Forms.ToolStripMenuItem menuItemUretim;
        private System.Windows.Forms.ToolStripMenuItem menuItemVerim;
        private System.Windows.Forms.ToolStripMenuItem menuItemSicaklik;
        private System.Windows.Forms.ToolStripMenuItem grafikTuruToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem areaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
    }
}