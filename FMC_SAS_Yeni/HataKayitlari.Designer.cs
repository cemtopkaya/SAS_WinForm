namespace FMC.Turkiye.SAS
{
    partial class HataKayitlari
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HataKayitlari));
            this.btnTazele = new System.Windows.Forms.Button();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTazele
            // 
            this.btnTazele.AccessibleDescription = "Tazele";
            this.btnTazele.Image = global::FMC.Turkiye.SAS.Properties.Resources.reload;
            this.btnTazele.Location = new System.Drawing.Point(12, 12);
            this.btnTazele.Name = "btnTazele";
            this.btnTazele.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTazele.Size = new System.Drawing.Size(43, 41);
            this.btnTazele.TabIndex = 0;
            this.btnTazele.Tag = global::FMC.Turkiye.SAS.Properties.Resources.fmc_logo;
            this.btnTazele.UseVisualStyleBackColor = true;
            this.btnTazele.Click += new System.EventHandler(this.btnTazele_Click);
            // 
            // dgvLogs
            // 
            this.dgvLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Location = new System.Drawing.Point(12, 59);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.Size = new System.Drawing.Size(450, 349);
            this.dgvLogs.TabIndex = 1;
            // 
            // HataKayitlari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 420);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.btnTazele);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HataKayitlari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EventLogs";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTazele;
        private System.Windows.Forms.DataGridView dgvLogs;
    }
}