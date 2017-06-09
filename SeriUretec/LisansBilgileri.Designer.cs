namespace SeriUretec
{
    partial class LisansBilgileri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LisansBilgileri));
            this.gbLisansBilgileri = new System.Windows.Forms.GroupBox();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnAktiveEt = new System.Windows.Forms.Button();
            this.tbSeriNo = new System.Windows.Forms.TextBox();
            this.lblSeriNo = new System.Windows.Forms.Label();
            this.tbAnahtar = new System.Windows.Forms.TextBox();
            this.lblAnahtar = new System.Windows.Forms.Label();
            this.gbLisansBilgileri.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLisansBilgileri
            // 
            this.gbLisansBilgileri.Controls.Add(this.btnIptal);
            this.gbLisansBilgileri.Controls.Add(this.btnAktiveEt);
            this.gbLisansBilgileri.Controls.Add(this.tbSeriNo);
            this.gbLisansBilgileri.Controls.Add(this.lblSeriNo);
            this.gbLisansBilgileri.Controls.Add(this.tbAnahtar);
            this.gbLisansBilgileri.Controls.Add(this.lblAnahtar);
            this.gbLisansBilgileri.Location = new System.Drawing.Point(7, 8);
            this.gbLisansBilgileri.Name = "gbLisansBilgileri";
            this.gbLisansBilgileri.Size = new System.Drawing.Size(284, 191);
            this.gbLisansBilgileri.TabIndex = 3;
            this.gbLisansBilgileri.TabStop = false;
            this.gbLisansBilgileri.Text = "Lisans Bilgileri";
            // 
            // btnIptal
            // 
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.Image = global::SeriUretec.Properties.Resources.Close_2_icon_1_;
            this.btnIptal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIptal.Location = new System.Drawing.Point(15, 163);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(80, 23);
            this.btnIptal.TabIndex = 8;
            this.btnIptal.Text = "İPTAL";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnAktiveEt
            // 
            this.btnAktiveEt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAktiveEt.Image = global::SeriUretec.Properties.Resources.Actions_dialog_ok_apply_icon_1_;
            this.btnAktiveEt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAktiveEt.Location = new System.Drawing.Point(101, 163);
            this.btnAktiveEt.Name = "btnAktiveEt";
            this.btnAktiveEt.Size = new System.Drawing.Size(170, 23);
            this.btnAktiveEt.TabIndex = 7;
            this.btnAktiveEt.Text = "AKTİVASYONU TAMAMLA";
            this.btnAktiveEt.UseVisualStyleBackColor = true;
            this.btnAktiveEt.Click += new System.EventHandler(this.btnAktiveEt_Click);
            // 
            // tbSeriNo
            // 
            this.tbSeriNo.Location = new System.Drawing.Point(15, 130);
            this.tbSeriNo.Name = "tbSeriNo";
            this.tbSeriNo.Size = new System.Drawing.Size(256, 20);
            this.tbSeriNo.TabIndex = 5;
            // 
            // lblSeriNo
            // 
            this.lblSeriNo.AutoSize = true;
            this.lblSeriNo.Location = new System.Drawing.Point(12, 114);
            this.lblSeriNo.Name = "lblSeriNo";
            this.lblSeriNo.Size = new System.Drawing.Size(42, 13);
            this.lblSeriNo.TabIndex = 4;
            this.lblSeriNo.Text = "Seri No";
            // 
            // tbAnahtar
            // 
            this.tbAnahtar.Location = new System.Drawing.Point(15, 32);
            this.tbAnahtar.Multiline = true;
            this.tbAnahtar.Name = "tbAnahtar";
            this.tbAnahtar.Size = new System.Drawing.Size(256, 79);
            this.tbAnahtar.TabIndex = 3;
            // 
            // lblAnahtar
            // 
            this.lblAnahtar.AutoSize = true;
            this.lblAnahtar.Location = new System.Drawing.Point(12, 16);
            this.lblAnahtar.Name = "lblAnahtar";
            this.lblAnahtar.Size = new System.Drawing.Size(44, 13);
            this.lblAnahtar.TabIndex = 2;
            this.lblAnahtar.Text = "Anahtar";
            // 
            // LisansBilgileri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 207);
            this.Controls.Add(this.gbLisansBilgileri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LisansBilgileri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lisanslama Bilgileri";
            this.gbLisansBilgileri.ResumeLayout(false);
            this.gbLisansBilgileri.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLisansBilgileri;
        private System.Windows.Forms.TextBox tbSeriNo;
        private System.Windows.Forms.Label lblSeriNo;
        private System.Windows.Forms.TextBox tbAnahtar;
        private System.Windows.Forms.Label lblAnahtar;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnAktiveEt;
    }
}