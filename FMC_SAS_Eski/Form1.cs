using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int baslatma_butonu = 0;
        int baglanti = 0;
        int port_sec = 0;
        int rx_data = 0, rx_data1 = 0;
        int i = 0, j = 0;
        int alarm_sayisi;
        int alarm_kodu;
        int dezenfeksiyon_sayisi;
        int calisma_saati;
        int kayit_no_buyuk,kayit_no_kucuk,tur;
        int kayit_sayisi;
        int[] int_data = new int[17];
        int[] ay_string = new int[1000];
        int[] gun_string = new int[1000];
        int[] saat_string = new int[1000];
        int[] sicaklik_string = new int[1000];
        int[] giris_iletkenlik = new int[1000];
        int[] uretim_iletkenlik = new int[1000];
        float[] verim = new float[1000];
        string text = "";
        StreamWriter yaz;// = new StreamWriter("d:\\deneme.txt");
        Graphics graph;
        Pen pen;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                comboBox1.Items.Add(s);

            serialPort1.ReadTimeout = 2000;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (baglanti == 1)
            {
                if (baslatma_butonu == 0)
                {
                    anaekran_textBox.Text = "CALISIYOR";
                    button1.Text = "DURDUR";
                    baslatma_butonu = 1;
                    try { serialPort1.Write("x"); }
                    catch (Exception)
                    { baglanti_kesildi(); }
                }
                else if (baslatma_butonu == 1)
                {
                    anaekran_textBox.Text = "BEKLEME";
                    button1.Text = "CALISTIR";
                    baslatma_butonu = 0;
                    try { serialPort1.Write("y"); }
                    catch (Exception)
                    { baglanti_kesildi(); }
                }
            }
            else
            {
                anaekran_textBox.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            pen = new Pen(Color.Red, 1);

            if (baglanti == 1)
            {
                try { serialPort1.Write("?"); }  //Kayit Sayisi
                catch (Exception)
                { baglanti_kesildi(); }

                try { kayit_no_kucuk = serialPort1.ReadChar(); } //Kayit_no_kucuk
                catch (Exception)
                { baglanti_kesildi(); }

                try { kayit_no_buyuk = serialPort1.ReadChar(); } //Kayit_no_buyuk
                catch (Exception)
                { baglanti_kesildi(); }
                kayit_no_buyuk -= 32;

                try { tur = serialPort1.ReadChar(); } //Tur
                catch (Exception)
                { baglanti_kesildi(); }

                if (tur == 1)
                { kayit_sayisi = 8192; }
                else
                { kayit_sayisi = (kayit_no_buyuk * 256 + kayit_no_kucuk) / 8; }
            }
            
            if (baglanti == 1)
            {
                //yaz = File.AppendText("d:\\deneme.txt");
                yaz = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FMCWTSLOG.txt");
                yaz.Write("Cihaz Numarası: ");

                reportViewer1.LocalReport.Refresh();
                try { serialPort1.Write("0"); }  //Seri numarası   
                catch (Exception)
                { baglanti_kesildi(); }
                text = "";
                for (i = 0; i < 4; i++)
                {
                    if (baglanti == 1)
                    {
                        try { rx_data = serialPort1.ReadChar(); }
                        catch (Exception)   
                        { baglanti_kesildi(); }
                        int_data[i] = rx_data + 48;
                    }
                }
                for (i = 0; i < 4; i++)
                {
                    text += Convert.ToChar(int_data[i]);
                }
                yaz.WriteLine(text);
            }

            if (baglanti == 1)
            {
                //yaz = File.AppendText("d:\\deneme.txt");
                yaz.Write("Kayıt Tarihi: ");
                yaz.WriteLine(DateTime.Now);
                yaz.WriteLine("");
                yaz.WriteLine("Anlık Değerler: ");
                yaz.Write("Giris Suyu İletkenliği: ");

                try { serialPort1.Write("1"); }  //Giris Suyu İletkenliği   
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                giris_iletkenligi_textBox.Text = rx_data.ToString();
                yaz.WriteLine(rx_data + "uS");
            }


            if (baglanti == 1)
            {
                yaz.Write("Üretim Suyu İletkenliği: ");
                try { serialPort1.Write("2"); }  //Üretim Suyu İletkenliği 
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                uretim_iletkenligi_textBox.Text = rx_data.ToString();
                yaz.WriteLine(rx_data + "uS");
            }

            if (baglanti == 1)
            {
                yaz.Write("Su Sıcaklığı: ");
                try { serialPort1.Write("3"); }  //Sıcaklık 
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                su_sicakliği_textBox.Text = rx_data.ToString();
                yaz.WriteLine(rx_data + "°C");
            }

            if (baglanti == 1)
            {
                yaz.Write("Çalışma Saati: ");
                try { serialPort1.Write("4"); } //Çalışma Saati 
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                calisma_saati_textBox.Text = rx_data.ToString();
                calisma_saati = rx_data;
                yaz.WriteLine(rx_data + " Saat");
            }

            if (baglanti == 1)
            {
                yaz.Write("Alarm Sayısı: ");
                try { serialPort1.Write("5"); }  //Alarm Sayisi 
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data1 * 256 + rx_data;
                alarm_sayisi_textBox.Text = rx_data.ToString();
                alarm_sayisi = rx_data;
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Dezenfeksiyon Sayısı: ");
                try { serialPort1.Write("6"); }//Dezenfeksiyon Sayisi  
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data1 * 256 + rx_data;
                dezenfeksiyonsayisi_textBox.Text = rx_data.ToString();
                dezenfeksiyon_sayisi = rx_data;
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.WriteLine("");
                yaz.WriteLine("Ayarlanan Değerler:");
                yaz.Write("Üretim İletkenlik Limiti: ");
                try { serialPort1.Write("7"); }//Üretim İletkenlik Limit Değeri
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Giriş İletkenlik Limiti: ");
                try { serialPort1.Write("8"); }//Giriş İletkenlik Limiti
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Dezenfeksiyon Süresi:");
                try { serialPort1.Write("9"); }//Dezenfeksiyon Süresi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Durulama Süresi:");
                try { serialPort1.Write(":"); }//Durulama Süresi            
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Temizleme Süresi:");
                try { serialPort1.Write(";"); }//Temizleme Süresi            
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                
                yaz.WriteLine("");
                yaz.WriteLine("Alarm Kayıtları:");

                try { serialPort1.Write("<"); }//Alarm Log
                catch (Exception)
                { baglanti_kesildi(); }

                if (alarm_sayisi > 0)
                {
                    if (alarm_sayisi > 10)
                        alarm_sayisi = 10;

                    for (j = 0; j < alarm_sayisi; j++)
                    {
                        text = "";
                        for (i = 0; i < 16; i++)
                        {
                            if (    baglanti == 1)
                            {
                                try { rx_data = serialPort1.ReadChar(); }
                                catch (Exception)
                                { baglanti_kesildi(); }
                                int_data[i] = rx_data;
                            }
                        }
                        alarm_kodu = int_data[0];
                        for (i = 1; i < 15; i++)
                        {
                            text += Convert.ToChar(int_data[i]);
                        }

                        switch (alarm_kodu)
                        {
                            case 49:
                                {
                                    text += " Yüksek İletkenlik Alarmı";
                                    break;
                                }

                            case 50:
                                {
                                    text += " Şebeke Basınç Alarmı";
                                    break;
                                }

                            case 51:
                                {
                                    text += " Seviye Sensörü Alarmı";
                                    break;
                                }

                            case 52:
                                {
                                    text += " Yüksek Sıcaklık Alarmı";
                                    break;
                                }

                            case 53:
                                {
                                    text += " Termik Alarmı";
                                    break;
                                }

                            case 54:
                                {
                                    text += " Yüksek Giriş İletkenlik Alarmı";
                                    break;
                                }
                        }
                        listBox1.Items.Add(text);
                        yaz.WriteLine(text);
                    }
                }
                else
                { listBox1.Items.Add("Kayıt Yok"); }
                
            }
            if (baglanti == 1)
            {
                if (kayit_sayisi > 0)
                {
                    try { serialPort1.Write("="); }//Tüm kayıtlar
                    catch (Exception)
                    { baglanti_kesildi(); }
                    
                    for (i = 0; i < kayit_sayisi; i++)
                    {
                        try { rx_data = serialPort1.ReadByte(); }//gun
                        catch (Exception)
                        { baglanti_kesildi(); }
                        ay_string[i]=rx_data;
                        
                        try { rx_data = serialPort1.ReadByte(); }//ay
                        catch (Exception)
                        { baglanti_kesildi(); }
                        gun_string[i]=rx_data;
                        
                        try { rx_data = serialPort1.ReadByte(); }//saat
                        catch (Exception)
                        { baglanti_kesildi(); }
                        saat_string[i]=rx_data;
                        
                        try { rx_data = serialPort1.ReadByte(); }//giris iletkenlik buyuk
                        catch (Exception)
                        { baglanti_kesildi(); }
                        giris_iletkenlik[i]=rx_data;
                        
                        try { rx_data = serialPort1.ReadByte(); }//giris iletkenlik kucuk
                        catch (Exception)
                        { baglanti_kesildi(); }
                        giris_iletkenlik[i] =giris_iletkenlik[i]*256+rx_data;
                        
                        try { rx_data = serialPort1.ReadByte(); }//uretim iletkenlik
                        catch (Exception)
                        { baglanti_kesildi(); }
                        uretim_iletkenlik[i] = rx_data;

                        try { rx_data = serialPort1.ReadByte(); }//sicaklik
                        catch (Exception)
                        { baglanti_kesildi(); }
                        sicaklik_string[i] = rx_data;

                        try { rx_data = serialPort1.ReadByte(); }//# dur biti
                        catch (Exception)
                        { baglanti_kesildi(); }
                        if(rx_data!=35)
                        {baglanti_kesildi();}
                    }
                    yaz.Close();
                    yaz.Dispose();
                   /* try { serialPort1.Write(">"); }// Hand-shake protocol
                    catch (Exception)
                    { baglanti_kesildi(); }
                    try { rx_data = serialPort1.ReadByte(); }//hand-shake return
                    catch (Exception)
                    { baglanti_kesildi(); }
                   */
                    rx_data = 62;
                    if ((baglanti == 1)&&(rx_data==62))
                    {
                        SqlCeConnection myCon = new SqlCeConnection();
                        myCon.ConnectionString = @"Data Source = C:\Users\Erkan\Desktop\WindowsFormsApplication1\WindowsFormsApplication1\Database1.sdf;" +
                        "Password = q1w2e3r4.";

                        myCon.Open();
                        SqlCeTransaction myTrans;
                        myTrans = myCon.BeginTransaction();

                        try
                        {
                            string mySqlCode = "delete from reportTable ";
                            SqlCeCommand myCommand = new SqlCeCommand(mySqlCode, myCon);

                            for (i = 0; i < kayit_sayisi; i++)
                            {

                                verim[i] = ((giris_iletkenlik[i] - uretim_iletkenlik[i]) * 100 / (giris_iletkenlik[i] + 1));
                                mySqlCode = "insert into reportTable (girisIletkenlik,uretimIletkenlik,sicaklik,verim,saat,gun,ay ) values (" + giris_iletkenlik[i].ToString() + "," + uretim_iletkenlik[i].ToString() + "," + sicaklik_string[i].ToString() + "," + verim[i].ToString() + "," + saat_string[i].ToString() + "," + gun_string[i].ToString() + "," + ay_string[i].ToString() + ")";
                                myCommand = new SqlCeCommand(mySqlCode, myCon);
                                myCommand.Transaction = myTrans;
                                myCommand.ExecuteNonQuery();

                            }

                            myTrans.Commit();
                            this.reportTableTableAdapter.Fill(this.database1DataSet.reportTable);
                            reportViewer1.LocalReport.Refresh();
                        }

                        catch (Exception)
                        {
                            // myTrans.Rollback();
                        }
                        finally
                        {
                            myCon.Close();
                            this.reportTableTableAdapter.Fill(this.database1DataSet.reportTable);
                            reportViewer1.LocalReport.Refresh();
                            reportViewer1.RefreshReport();
                        }
                    }
                }       
                else
                {
                    yaz.WriteLine("Kayıt Yok");
                    yaz.Close();
                    yaz.Dispose();
                }
            }
            else
            {
                calisma_saati_textBox.Clear();
                giris_iletkenligi_textBox.Clear();
                uretim_iletkenligi_textBox.Clear();
                su_sicakliği_textBox.Clear();
                alarm_sayisi_textBox.Clear();
                dezenfeksiyonsayisi_textBox.Clear();
                yaz.Close();
                yaz.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (port_sec == 0)
            {
                baglanti = 0;
                radioButton1.Checked = false;
                radioButton1.Text = "Baglantı Yok";
            }
            else
            {
                if (baglanti == 0)
                {

                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.Open();

                    if (serialPort1.IsOpen)
                    {
                        radioButton1.Text = "Bekleyin...";

                        try { serialPort1.Write("z"); }
                        catch (Exception)
                        { baglanti_kesildi(); }

                        try { rx_data = serialPort1.ReadByte(); }
                        catch (Exception)
                        { baglanti_kesildi(); }

                        if (rx_data == 10)
                        {
                            baglanti = 1;
                            comboBox1.Enabled = false;
                            radioButton1.Checked = true;
                            radioButton1.Text = "Bağlandi";
                            button3.Text = "Bağlantı Kes";
                        }
                        else
                        { baglanti_kesildi(); }

                    }
                    else
                    { baglanti_kesildi(); }
                }
                else
                { baglanti_kesildi(); }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            port_sec = 1;
        }

        private void baglanti_kesildi()
        {
            serialPort1.Close();
            baglanti = 0;
            radioButton1.Checked = false;
            radioButton1.Text = "Bağlantı Yok";
            comboBox1.Enabled = true;
            button3.Text = "Bağlan";
            anaekran_textBox.Text = "";
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                comboBox1.Items.Add(s);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.reportTableTableAdapter.Fill(this.database1DataSet.reportTable);
            this.reportViewer1.RefreshReport();
        }



    }
}
//okan:3443175
/*

            pictureBox1.Refresh();
            pictureBox2.Refresh();

            pen = new Pen(Color.Red, 1);

            graph = pictureBox1.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for(i=0;i<599;i++)
            {
                graph.DrawLine(pen,i,Dizi[i]%20,i+1,Dizi[i+1]%20);
            }

            graph = pictureBox2.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for (i = 0; i < 599; i++)
            {
                graph.DrawLine(pen, i, (Dizi[i] % 20), i + 1, (Dizi[i + 1] % 20));
            }

            graph = pictureBox3.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for (i = 0; i < 599; i++)
            {
                graph.DrawLine(pen, i, (Dizi[i] % 20), i + 1, (Dizi[i + 1] % 20));
            }

            graph = pictureBox4.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for (i = 0; i < 599; i++)
            {
                graph.DrawLine(pen, i, (Dizi[i] % 20), i + 1, (Dizi[i + 1] % 20));
            }


            pen = new Pen(Color.Black, 1);

            graph = pictureBox1.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 1999, 99);

            graph = pictureBox2.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 999, 99);
            
            graph = pictureBox3.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 999, 99);

            graph = pictureBox4.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 999, 99);
 */


/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data.SqlServerCe;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int baslatma_butonu = 0;
        int baglanti = 0;
        int port_sec = 0;
        int rx_data=0,rx_data1=0;
        int i = 0,j=0;
        int alarm_sayisi;
        int alarm_kodu;
        int dezenfeksiyon_sayisi;
        int calisma_saati;
        int[] int_data=new int[17];
        int[] sicaklik_string = new int[1000];
        int[] giris_iletkenlik = new int[1000];
        int[] uretim_iletkenlik = new int[1000];
        int[] verim = new int[1000];
        string text = "";
        StreamWriter yaz;// = new StreamWriter("d:\\deneme.txt");
        //Graphics graph;
        //Pen pen;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                comboBox1.Items.Add(s);
           serialPort1.ReadTimeout = 2000;
           baglanti = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (baglanti == 1)
            {
                if (baslatma_butonu == 0)
                {
                    anaekran_textBox.Text = "CALISIYOR";
                    button1.Text = "DURDUR";
                    baslatma_butonu = 1;
                    try { serialPort1.Write("0"); }
                    catch (Exception)
                    { baglanti_kesildi(); }                   
                }
                else if (baslatma_butonu == 1)
                {
                    anaekran_textBox.Text = "BEKLEME";
                    button1.Text = "CALISTIR";
                    baslatma_butonu = 0;
                    try { serialPort1.Write("1"); }
                    catch (Exception)
                    { baglanti_kesildi(); }                    
                }                   
            }
            else
            {
                anaekran_textBox.Text = "";
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (yaz !=null)
            {
                yaz.Close();
                yaz.Dispose();               
            }

            //baglanti_kesildi();
                //baglan();

            listBox1.Items.Clear();

            if (baglanti == 1)
            {
                //yaz = File.AppendText("d:\\deneme.txt");
                yaz = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FMCWTSLOG.txt");
                yaz.Write("Cihaz Numarası: ");

                try { serialPort1.Write("C"); }  //Giris Suyu İletkenliği
                catch (Exception)
                { baglanti_kesildi(); }
                text = "";
                for (i = 0; i < 4; i++)
                {
                    if (baglanti == 1)
                    {
                        try { rx_data = serialPort1.ReadChar(); }
                        catch (Exception)
                        { baglanti_kesildi(); }
                        int_data[i] = rx_data+48;
                    }
                }
                for (i = 0; i < 4; i++)
                {
                    text += Convert.ToChar(int_data[i]);
                }
                yaz.WriteLine(text);
            }

            if (baglanti == 1)
            {
                //yaz = File.AppendText("d:\\deneme.txt");
                yaz.Write("Kayıt Tarihi: ");
                yaz.WriteLine(DateTime.Now);
                yaz.WriteLine("");
                yaz.WriteLine("Anlık Değerler: ");
                yaz.Write("Giris Suyu İletkenliği: ");

                try { serialPort1.Write("2"); }  //Giris Suyu İletkenliği
                catch(Exception)
                {baglanti_kesildi();}
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                giris_iletkenligi_textBox.Text = rx_data.ToString();
                yaz.WriteLine(rx_data + "uS");
            }


            if (baglanti == 1)
            {
                yaz.Write("Üretim Suyu İletkenliği: ");
                try { serialPort1.Write("3"); }  //Üretim Suyu İletkenliği
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                uretim_iletkenligi_textBox.Text = rx_data.ToString();
                yaz.WriteLine(rx_data + "uS");
            }

            if (baglanti == 1)
            {
                yaz.Write("Su Sıcaklığı: ");
                try { serialPort1.Write("4"); }  //Sıcaklık
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                su_sicakliği_textBox.Text = rx_data.ToString();
                yaz.WriteLine(rx_data + "°C");
            }

            if (baglanti == 1)
            {
                yaz.Write("Çalışma Saati: ");
                try { serialPort1.Write("5"); } //Çalışma Saati
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                calisma_saati_textBox.Text = rx_data.ToString();
                calisma_saati = rx_data;
                yaz.WriteLine(rx_data + " Saat");
            }

            if (baglanti == 1)
            {
                yaz.Write("Alarm Sayısı: ");
                try { serialPort1.Write("6"); }  //Alarm Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data1 * 256 + rx_data;
                alarm_sayisi_textBox.Text = rx_data.ToString();
                alarm_sayisi = rx_data;
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Dezenfeksiyon Sayısı: ");
                try { serialPort1.Write("8"); }//Dezenfeksiyon Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                dezenfeksiyonsayisi_textBox.Text = rx_data.ToString();
                dezenfeksiyon_sayisi = rx_data;
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.WriteLine("");
                yaz.WriteLine("Ayarlanan Değerler:");
                yaz.Write("Üretim İletkenlik Limiti: ");
                try { serialPort1.Write(">"); }//Dezenfeksiyon Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Giriş İletkenlik Limiti: ");
                try { serialPort1.Write("?"); }//Dezenfeksiyon Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data1 = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                rx_data = rx_data * 256 + rx_data1;
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Dezenfeksiyon Süresi:");
                try { serialPort1.Write("@"); }//Dezenfeksiyon Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Durulama Süresi:");
                try { serialPort1.Write("A"); }//Dezenfeksiyon Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.Write("Temizleme Süresi:");
                try { serialPort1.Write("B"); }//Dezenfeksiyon Sayisi
                catch (Exception)
                { baglanti_kesildi(); }
                try { rx_data = serialPort1.ReadByte(); }
                catch (Exception)
                { baglanti_kesildi(); }
                yaz.WriteLine(rx_data);
            }

            if (baglanti == 1)
            {
                yaz.WriteLine("");
                yaz.WriteLine("Alarm Kayıtları:");
                
                try { serialPort1.Write("7"); }//Alarm Log
                catch (Exception)
                { baglanti_kesildi(); }

                if (alarm_sayisi > 0)
                {
                    if (alarm_sayisi > 10)
                        alarm_sayisi = 10;

                    for (j = 0; j < alarm_sayisi; j++)
                    {
                        text = "";
                        for (i = 0; i < 17; i++)
                        {
                            if (baglanti == 1)
                            {
                                try { rx_data = serialPort1.ReadChar(); }
                                catch (Exception)
                                { baglanti_kesildi(); }
                                int_data[i] = rx_data;
                            }
                        }
                        alarm_kodu = int_data[0];
                        for (i = 1; i < 15; i++)
                        {
                            text += Convert.ToChar(int_data[i]);
                        }

                        switch (alarm_kodu)
                        {
                            case 49:
                                {
                                    text += " Yüksek İletkenlik Alarmı";
                                    break;
                                }

                            case 50:
                                {
                                    text += " Şebeke Basınç Alarmı";
                                    break;
                                }

                            case 51:
                                {
                                    text += " Seviye Sensörü Alarmı";
                                    break;
                                }

                            case 52:
                                {
                                    text += " Yüksek Sıcaklık Alarmı";
                                    break;
                                }

                            case 53:
                                {
                                    text += " Termik Alarmı";
                                    break;
                                }

                            case 54:
                                {
                                    text += " Yüksek Giriş İletkenlik Alarmı";
                                    break;
                                }
                        }
                        listBox1.Items.Add(text);
                        yaz.WriteLine(text);
                    }
                }
                else
                { listBox1.Items.Add("Kayıt Yok"); }
            
                    
            }
            if (baglanti == 1)
            {
                yaz.WriteLine("");
                yaz.WriteLine("");
                yaz.WriteLine("Sıcaklık Kayıtları:");
                //yaz.WriteLine("");
                if (calisma_saati > 0)
                {
                    try { serialPort1.Write(":"); }//Sıcaklık Kayıtları
                    catch (Exception)
                    { baglanti_kesildi(); }

                    for (i = 0; i < calisma_saati; i++)
                    {
                        try { rx_data = serialPort1.ReadByte(); }
                        catch (Exception)
                        { baglanti_kesildi(); }
                        sicaklik_string[i] = rx_data;
                        yaz.Write(rx_data+" ");
                    }
                }
                else
                {
                    yaz.WriteLine("Kayıt Yok");
                }
            }
            if (baglanti == 1)
            {
                yaz.WriteLine("");
                yaz.WriteLine("");
                yaz.WriteLine("Üretim İletkenlik Kayıtları:");
                //yaz.WriteLine("");
                if (calisma_saati > 0)
                {
                    try { serialPort1.Write(";"); }//Üretim iletkenik Kayıtları
                    catch (Exception)
                    { baglanti_kesildi(); }

                    for (i = 0; i < calisma_saati; i++)
                    {
                        try { rx_data = serialPort1.ReadByte(); }
                        catch (Exception)
                        { baglanti_kesildi(); }
                        uretim_iletkenlik[i] = rx_data;
                        yaz.WriteLine(rx_data+" ");
                    }

                }
                else
                {
                    yaz.WriteLine("Kayıt Yok");
                }
            }
            if (baglanti == 1)
            {
                yaz.WriteLine("");
                yaz.WriteLine("");
                yaz.WriteLine("Giriş İletkenlik Kayıtları:");
                //yaz.WriteLine("");
                if (calisma_saati > 0)
                {
                    try { serialPort1.Write("<"); }//Giriş İletkenlik Kayıtları
                    catch (Exception)
                    { baglanti_kesildi(); }
                    
                    for (i = 0; i < calisma_saati; i++)
                    {
                       
                        try { rx_data = serialPort1.ReadByte(); }
                        catch (Exception)
                        { baglanti_kesildi(); }
                        try { rx_data1 = serialPort1.ReadByte(); }                      
                        catch (Exception)
                        { baglanti_kesildi(); }
                      
                        giris_iletkenlik[i] = rx_data*256+rx_data1;
                        yaz.WriteLine(giris_iletkenlik[i].ToString() + " ");
   
                    
                            verim[i] = (giris_iletkenlik[i] - uretim_iletkenlik[i]) / giris_iletkenlik[i] * 100;
                      
                 
                    }
                    
                    SqlCeConnection myCon = new SqlCeConnection();
                    //  myCon.ConnectionString = @"Data Source=|DataDirectory|\Database1.sdf;"+ "providerName=Microsoft.SqlServerCe.Client.3.5";

                    myCon.ConnectionString = @"Data Source = C:\Users\Erkan\Desktop\WindowsFormsApplication1\WindowsFormsApplication1\Database1.sdf;" +
                  "Password = q1w2e3r4.";

                    myCon.Open();
                    SqlCeTransaction myTrans;
                    myTrans = myCon.BeginTransaction();

                    try
                    {

                        string mySqlCode = "delete from reportTable ";
                        SqlCeCommand myCommand = new SqlCeCommand(mySqlCode, myCon);
                        //myCommand.Transaction = myTrans;



                        mySqlCode = "insert into reportTable (girisIletkenlik) values (" + calisma_saati.ToString() + ")";
                        myCommand = new SqlCeCommand(mySqlCode, myCon);
                        myCommand.Transaction = myTrans;

                        myCommand.ExecuteNonQuery();
                       // myTrans.Commit();
                        for (i = 0; i < calisma_saati; i++)
                        {
                        
                        mySqlCode = "insert into reportTable (girisIletkenlik) values (" + giris_iletkenlik[i].ToString() + ")";
                        myCommand = new SqlCeCommand(mySqlCode, myCon);
                        myCommand.Transaction = myTrans;

                        myCommand.ExecuteNonQuery();
                        //myTrans.Commit();
                        }
                         //mySqlCode = "insert into reportTable (girisIletkenlik) values (90)";
                        //"update reportTable set girisiletkenlik=110 where saat=5"
                        //string mySqlCode = "delete from reportTable ";
                        myTrans.Commit();

                        //myCommand.ExecuteNonQuery();
                        //myTrans.Commit();
                    }
                    catch (Exception)
                    {
                       // myTrans.Rollback();
                    }
                    finally
                    {
                        myCon.Close();
                        reportViewer1.LocalReport.Refresh();
                    } 

                }
                else
                {
                    yaz.WriteLine("Kayıt Yok");
                }
                yaz.Close();
                yaz.Dispose();

            }
            else
            {
                calisma_saati_textBox.Clear();
                giris_iletkenligi_textBox.Clear();
                uretim_iletkenligi_textBox.Clear();
                su_sicakliği_textBox.Clear();
                alarm_sayisi_textBox.Clear();
                dezenfeksiyonsayisi_textBox.Clear();
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (port_sec == 0)
            {
                baglanti = 0;
                radioButton1.Checked = false;
                radioButton1.Text = "Baglantı Yok";
            }
            else
            {
                baglan();
            }
        }

        private void baglan()
        {
            if (baglanti == 0)
            {

                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();

                if (serialPort1.IsOpen)
                {
                    radioButton1.Text = "Bekleyin...";

                    try { serialPort1.Write("="); }
                    catch (Exception)
                    { baglanti_kesildi(); }

                    try { rx_data = serialPort1.ReadByte(); }
                    catch (Exception)
                    { baglanti_kesildi(); }

                    if (rx_data == 10)
                    {
                        baglanti = 1;
                        comboBox1.Enabled = false;
                        radioButton1.Checked = true;
                        radioButton1.Text = "Bağlandi";
                        button3.Text = "Bağlantı Kes";
                    }
                    else
                    { baglanti_kesildi(); }

                }
                else
                { baglanti_kesildi(); }
            }
            else
            { baglanti_kesildi(); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            port_sec = 1;
        }

        private void baglanti_kesildi()
        {
            serialPort1.Close();
            baglanti = 0;
            radioButton1.Checked = false;
            radioButton1.Text = "Bağlantı Yok";
            comboBox1.Enabled = true;
            button3.Text = "Bağlan";
            anaekran_textBox.Text = "";
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                comboBox1.Items.Add(s);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.reportTable' table. You can move, or remove it, as needed.
            this.reportTableTableAdapter.Fill(this.database1DataSet.reportTable);

            this.reportViewer1.RefreshReport();
          
        }     

    }



}
//okan:3443175


            pictureBox1.Refresh();
            pictureBox2.Refresh();

            pen = new Pen(Color.Red, 1);

            graph = pictureBox1.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for(i=0;i<599;i++)
            {
                graph.DrawLine(pen,i,Dizi[i]%20,i+1,Dizi[i+1]%20);
            }

            graph = pictureBox2.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for (i = 0; i < 599; i++)
            {
                graph.DrawLine(pen, i, (Dizi[i] % 20), i + 1, (Dizi[i + 1] % 20));
            }

            graph = pictureBox3.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for (i = 0; i < 599; i++)
            {
                graph.DrawLine(pen, i, (Dizi[i] % 20), i + 1, (Dizi[i + 1] % 20));
            }

            graph = pictureBox4.CreateGraphics();
            Sayi = RandomNumberGenerator.Create();
            Sayi.GetBytes(Dizi);
            for (i = 0; i < 599; i++)
            {
                graph.DrawLine(pen, i, (Dizi[i] % 20), i + 1, (Dizi[i + 1] % 20));
            }


            pen = new Pen(Color.Black, 1);

            graph = pictureBox1.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 1999, 99);

            graph = pictureBox2.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 999, 99);
            
            graph = pictureBox3.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 999, 99);

            graph = pictureBox4.CreateGraphics();
            graph.DrawLine(pen, 0, 0, 0, 99);
            graph.DrawLine(pen, 0, 99, 999, 99);
 */