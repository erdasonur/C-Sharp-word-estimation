using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace odev
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        string rastgele_kelime;
        string[] alfabe = new string[80000];
        int boyut = 0;
        private void Form4_Load(object sender, EventArgs e)
        {
            string dosya_okuma = @"C:\Users\erdasonur\source\repos\odev\sozluk.txt";
            FileStream fs = new FileStream(dosya_okuma, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                alfabe[boyut] = yazi.ToLower();
                yazi = sw.ReadLine();
                boyut++;
            }
            sw.Close();
            fs.Close();
            Random rastgele = new Random();
            int a = boyut - 1;
            int rastgele_sayi = rastgele.Next(a);//0 ile a arasındaki sayıları üretir
            rastgele_kelime = alfabe[rastgele_sayi];
        }
        private void Form4_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string girilen_kelime = textBox1.Text;
            int normal_hesaplama = 0,kartezyen_carpim=0;
            Console.WriteLine(rastgele_kelime);
            for (int i = 0; i < girilen_kelime.Length; i++)
            {
                for (int j = 0; j < rastgele_kelime.Length; j++)
                {
                    if (girilen_kelime.ToCharArray()[i] == rastgele_kelime.ToCharArray()[j])
                    {
                        if (girilen_kelime.IndexOf(girilen_kelime.ToCharArray()[i], 0, i) == -1)
                        {
                            normal_hesaplama++;
                        }
                        kartezyen_carpim++;
                    }
                }
            }
            MessageBox.Show("Normal Hesaplama= " + normal_hesaplama + "\nKartezyen Çarpım= " + kartezyen_carpim,"Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            if (String.Compare(rastgele_kelime,girilen_kelime)==0)
            {
                MessageBox.Show("TEBRİKLER " + rastgele_kelime + " KELİMESİNİ BULDUNUZ!!!", "KELİME TAHMİNİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                if (MessageBox.Show("Yeniden Oynamak İster misiniz?","Oyun Tekrarı",MessageBoxButtons.RetryCancel,MessageBoxIcon.Question)==DialogResult.Retry)
                {
                    Random rastgele = new Random();
                    int a = boyut - 1;
                    int rastgele_sayi = rastgele.Next(a);//0 ile a arasındaki sayıları üretir
                    rastgele_kelime = alfabe[rastgele_sayi];
                }
                else
                {
                    if (MessageBox.Show("Ana menüye dönmek için Tamamı çıkmak içinse İptali seçiniz", "Oyun sonu", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        Form3 f3 = new Form3();
                        f3.Show();
                        this.Hide();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen yeni bir kelime giriniz", "Kelime girişi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Text = "";
            }
            kartezyen_carpim = 0;
            normal_hesaplama = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }
    }
}
