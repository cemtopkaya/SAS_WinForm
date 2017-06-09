namespace SeriUretec
{
    partial class ServisBilgileri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServisBilgileri));
            this.gbServisAdresleri = new System.Windows.Forms.GroupBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.tbYonetim = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.gbServisAdresleri.Controls.Add(this.tbYonetim);
            this.gbServisAdresleri.Controls.Add(this.label1);
            this.gbServisAdresleri.Controls.Add(this.tbVeriAlma);
            this.gbServisAdresleri.Controls.Add(this.lblAlma);
            this.gbServisAdresleri.Controls.Add(this.tbVeriGonderme);
            this.gbServisAdresleri.Controls.Add(this.lblGonderme);
            this.gbServisAdresleri.Location = new System.Drawing.Point(6, 7);
            this.gbServisAdresleri.Name = "gbServisAdresleri";
            this.gbServisAdresleri.Size = new System.Drawing.Size(284, 186);
            this.gbServisAdresleri.TabIndex = 3;
            this.gbServisAdresleri.TabStop = false;
            this.gbServisAdresleri.Text = "Web Servis Adresleri";
            // 
            // btnKaydet
            // 
            this.btnKaydet.Image = global::SeriUretec.Properties.Resources.Save_icon_1_;
            this.btnKaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKaydet.Location = new System.Drawing.Point(181, 143);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(90, 33);
            this.btnKaydet.TabIndex = 3;
            this.btnKaydet.Text = "K&AYDET";
            this.btnKaydet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // tbYonetim
            // 
            this.tbYonetim.Location = new System.Drawing.Point(15, 117);
            this.tbYonetim.Name = "tbYonetim";
            this.tbYonetim.Size = new System.Drawing.Size(256, 20);
            this.tbYonetim.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Yönetim";
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
            this.lblAlma.Size = new System.Drawing.Size(51, 13);
            this.lblAlma.TabIndex = 4;
            this.lblAlma.Text = "Veri Alma";
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
            this.lblGonderme.Size = new System.Drawing.Size(56, 13);
            this.lblGonderme.TabIndex = 2;
            this.lblGonderme.Text = "Gönderme";
            // 
            // ServisBilgileri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 200);
            this.Controls.Add(this.gbServisAdresleri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServisBilgileri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servis & Kullanıcı Bilgileri";
            this.gbServisAdresleri.ResumeLayout(false);
            this.gbServisAdresleri.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServisAdresleri;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.TextBox tbVeriAlma;
        private System.Windows.Forms.Label lblAlma;
        private System.Windows.Forms.TextBox tbVeriGonderme;
        private System.Windows.Forms.Label lblGonderme;
        private System.Windows.Forms.TextBox tbYonetim;
        private System.Windows.Forms.Label label1;
    }
}