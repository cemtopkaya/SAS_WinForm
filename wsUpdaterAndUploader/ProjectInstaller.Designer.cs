namespace wsUpdaterAndUploader
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.srvcProcInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.srvcInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // srvcProcInstaller
            // 
            this.srvcProcInstaller.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.srvcInstaller});
            this.srvcProcInstaller.Password = null;
            this.srvcProcInstaller.Username = null;
            // 
            // srvcInstaller
            // 
            this.srvcInstaller.DisplayName = "FMC Türkiye - SAS Windows Servisi";
            this.srvcInstaller.ServiceName = "Service1";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.srvcProcInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller srvcProcInstaller;
        private System.ServiceProcess.ServiceInstaller srvcInstaller;
    }
}