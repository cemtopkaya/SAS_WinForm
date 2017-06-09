using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FMC.Turkiye.CSharp.Extensions;

namespace SeriUretec
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            Application.Run(new SeriNoUretec());
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            bool bLoglandi = false;

            Exception ex = (Exception)args.ExceptionObject;
            try
            {
                bLoglandi = ex.LogException("Unhandled Exception Occured");
            }
            catch (Exception exx)
            {
                MessageBox.Show(ex.Message
                                + Environment.NewLine
                                + "İstisnası kayıt edilirken hata oluştu"
                                + Environment.NewLine + exx.Message);
            }
            if (!bLoglandi)
            {
                string s = "Mesaj:" + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace;
                s.LogToTempFile("Unhandled Exception kayıt altına alınırken hata oluştu.");
            }
        }
    }
}
