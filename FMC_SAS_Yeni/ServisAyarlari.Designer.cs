namespace FMC.Turkiye.SAS
{
    partial class ServisAyarlari
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServisAyarlari));
            this.gbServisAdresleri = new System.Windows.Forms.GroupBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.tbVeriAlma = new System.Windows.Forms.TextBox();
            this.lblAlma = new System.Windows.Forms.Label();
            this.tbVeriGonderme = new System.Windows.Forms.TextBox();
            this.lblGonderme = new System.Windows.Forms.Label();
            this.gbServisAdresleri.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbServisAdresleri
            // 
            this.gbServisAdresleri.Controls.Add(this.btnKaydet);
            this.gbServisAdresleri.Controls.Add(this.tbVeriAlma);
            this.gbServisAdresleri.Controls.Add(this.lblAlma);
            this.gbServisAdresleri.Controls.Add(this.tbVeriGonderme);
            this.gbServisAdresleri.Controls.Add(this.lblGonderme);
            this.gbServisAdresleri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbServisAdresleri.Location = new System.Drawing.Point(0, 0);
            this.gbServisAdresleri.Name = "gbServisAdresleri";
            this.gbServisAdresleri.Size = new System.Drawing.Size(284, 132);
            this.gbServisAdresleri.TabIndex = 2;
            this.gbServisAdresleri.TabStop = false;
            this.gbServisAdresleri.Text = "Web Servis Adresleri";
            // 
            // btnKaydet
            // 
            this.btnKaydet.Image = global::FMC.Turkiye.SAS.Properties.Resources.save;
            this.btnKaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKaydet.Location = new System.Drawing.Point(120, 97);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(151, 23);
            this.btnKaydet.TabIndex = 2;
            this.btnKaydet.Text = "&KAYDET ve KAPAT";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // tbVeriAlma
            // 
            this.tbVeriAlma.Location = new System.Drawing.Point(15, 71);
            this.tbVeriAlma.Name = "tbVeriAlma";
            this.tbVeriAlma.Size = new System.Drawing.Size(256, 20);
            this.tbVeriAlma.TabIndex = 1;
            // 
            // lblAlma
            // 
            this.lblAlma.AutoSize = true;
            this.lblAlma.Location = new System.Drawing.Point(12, 55);
            this.lblAlma.Name = "lblAlma";
            this.lblAlma.Size = new System.Drawing.Size(54, 13);
            this.lblAlma.TabIndex = 4;
            this.lblAlma.Text = "Veri Alma:";
            // 
            // tbVeriGonderme
            // 
            this.tbVeriGonderme.Location = new System.Drawing.Point(15, 32);
            this.tbVeriGonderme.Name = "tbVeriGonderme";
            this.tbVeriGonderme.Size = new System.Drawing.Size(256, 20);
            this.tbVeriGonderme.TabIndex = 0;
            // 
            // lblGonderme
            // 
            this.lblGonderme.AutoSize = true;
            this.lblGonderme.Location = new System.Drawing.Point(12, 16);
            this.lblGonderme.Name = "lblGonderme";
            this.lblGonderme.Size = new System.Drawing.Size(59, 13);
            this.lblGonderme.TabIndex = 2;
            this.lblGonderme.Text = "Gönderme:";
            // 
            // ServisAyarlari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 132);
            this.Controls.Add(this.gbServisAdresleri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServisAyarlari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web Servis Ayarları";
            this.gbServisAdresleri.ResumeLayout(false);
            this.gbServisAdresleri.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServisAdresleri;
        private System.Windows.Forms.TextBox tbVeriAlma;
        private System.Windows.Forms.Label lblAlma;
        private System.Windows.Forms.TextBox tbVeriGonderme;
        private System.Windows.Forms.Label lblGonderme;
        private System.Windows.Forms.Button btnKaydet;

    }
}