namespace FMC.Turkiye.SAS
{
    partial class Graphs
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dtpBas = new System.Windows.Forms.DateTimePicker();
            this.dtpSon = new System.Windows.Forms.DateTimePicker();
            this.btnGoruntule = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.grafikTipiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çizgiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çubukToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chSuSicakligi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chVerim = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chUretimIletkenligi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chGirisIletkenligi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chSuSicakligi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chVerim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chUretimIletkenligi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGirisIletkenligi)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpBas
            // 
            this.dtpBas.Location = new System.Drawing.Point(13, 34);
            this.dtpBas.Name = "dtpBas";
            this.dtpBas.Size = new System.Drawing.Size(200, 20);
            this.dtpBas.TabIndex = 2;
            this.dtpBas.ValueChanged += new System.EventHandler(this.dtBas_ValueChanged);
            // 
            // dtpSon
            // 
            this.dtpSon.Location = new System.Drawing.Point(234, 34);
            this.dtpSon.Name = "dtpSon";
            this.dtpSon.Size = new System.Drawing.Size(200, 20);
            this.dtpSon.TabIndex = 3;
            // 
            // btnGoruntule
            // 
            this.btnGoruntule.Location = new System.Drawing.Point(451, 31);
            this.btnGoruntule.Name = "btnGoruntule";
            this.btnGoruntule.Size = new System.Drawing.Size(75, 23);
            this.btnGoruntule.TabIndex = 4;
            this.btnGoruntule.Text = "Görüntüle";
            this.btnGoruntule.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grafikTipiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // grafikTipiToolStripMenuItem
            // 
            this.grafikTipiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.çizgiToolStripMenuItem,
            this.çubukToolStripMenuItem});
            this.grafikTipiToolStripMenuItem.Name = "grafikTipiToolStripMenuItem";
            this.grafikTipiToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.grafikTipiToolStripMenuItem.Text = "Grafik Tipi";
            // 
            // çizgiToolStripMenuItem
            // 
            this.çizgiToolStripMenuItem.Checked = true;
            this.çizgiToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.çizgiToolStripMenuItem.Name = "çizgiToolStripMenuItem";
            this.çizgiToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.çizgiToolStripMenuItem.Text = "Çizgi";
            // 
            // çubukToolStripMenuItem
            // 
            this.çubukToolStripMenuItem.Name = "çubukToolStripMenuItem";
            this.çubukToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.çubukToolStripMenuItem.Text = "Çubuk";
            // 
            // chSuSicakligi
            // 
            this.chSuSicakligi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ca1";
            this.chSuSicakligi.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend1.Name = "Legend1";
            legend1.Title = "SU SICAKLIĞI";
            legend1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chSuSicakligi.Legends.Add(legend1);
            this.chSuSicakligi.Location = new System.Drawing.Point(13, 552);
            this.chSuSicakligi.Name = "chSuSicakligi";
            series1.ChartArea = "ca1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "seriesGirisIletkenligi";
            this.chSuSicakligi.Series.Add(series1);
            this.chSuSicakligi.Size = new System.Drawing.Size(759, 155);
            this.chSuSicakligi.TabIndex = 24;
            this.chSuSicakligi.Text = "Giriş İletkenliği";
            // 
            // chVerim
            // 
            this.chVerim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ca1";
            this.chVerim.ChartAreas.Add(chartArea2);
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend2.Name = "Legend1";
            legend2.Title = "VERİM";
            legend2.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chVerim.Legends.Add(legend2);
            this.chVerim.Location = new System.Drawing.Point(13, 388);
            this.chVerim.Name = "chVerim";
            series2.ChartArea = "ca1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "seriesGirisIletkenligi";
            this.chVerim.Series.Add(series2);
            this.chVerim.Size = new System.Drawing.Size(759, 155);
            this.chVerim.TabIndex = 23;
            this.chVerim.Text = "Giriş İletkenliği";
            // 
            // chUretimIletkenligi
            // 
            this.chUretimIletkenligi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ca1";
            this.chUretimIletkenligi.ChartAreas.Add(chartArea3);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend3.Name = "Legend1";
            legend3.Title = "ÜRETİM İLETKENLİĞİ";
            legend3.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chUretimIletkenligi.Legends.Add(legend3);
            this.chUretimIletkenligi.Location = new System.Drawing.Point(13, 224);
            this.chUretimIletkenligi.Name = "chUretimIletkenligi";
            series3.ChartArea = "ca1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "seriesGirisIletkenligi";
            this.chUretimIletkenligi.Series.Add(series3);
            this.chUretimIletkenligi.Size = new System.Drawing.Size(759, 155);
            this.chUretimIletkenligi.TabIndex = 22;
            this.chUretimIletkenligi.Text = "Giriş İletkenliği";
            // 
            // chGirisIletkenligi
            // 
            this.chGirisIletkenligi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ca1";
            this.chGirisIletkenligi.ChartAreas.Add(chartArea4);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend4.Name = "Legend1";
            legend4.Title = "GİRİŞ İLETKENLİĞİ";
            legend4.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chGirisIletkenligi.Legends.Add(legend4);
            this.chGirisIletkenligi.Location = new System.Drawing.Point(13, 60);
            this.chGirisIletkenligi.Name = "chGirisIletkenligi";
            series4.ChartArea = "ca1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "seriesGirisIletkenligi";
            this.chGirisIletkenligi.Series.Add(series4);
            this.chGirisIletkenligi.Size = new System.Drawing.Size(759, 155);
            this.chGirisIletkenligi.TabIndex = 21;
            this.chGirisIletkenligi.Text = "Giriş İletkenliği";
            // 
            // Graphs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 719);
            this.Controls.Add(this.chSuSicakligi);
            this.Controls.Add(this.chVerim);
            this.Controls.Add(this.chUretimIletkenligi);
            this.Controls.Add(this.chGirisIletkenligi);
            this.Controls.Add(this.btnGoruntule);
            this.Controls.Add(this.dtpSon);
            this.Controls.Add(this.dtpBas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Graphs";
            this.Text = "Graphs";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chSuSicakligi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chVerim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chUretimIletkenligi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGirisIletkenligi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpBas;
        private System.Windows.Forms.DateTimePicker dtpSon;
        private System.Windows.Forms.Button btnGoruntule;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem grafikTipiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çizgiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çubukToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chSuSicakligi;
        private System.Windows.Forms.DataVisualization.Charting.Chart chVerim;
        private System.Windows.Forms.DataVisualization.Charting.Chart chUretimIletkenligi;
        private System.Windows.Forms.DataVisualization.Charting.Chart chGirisIletkenligi;
    }
}