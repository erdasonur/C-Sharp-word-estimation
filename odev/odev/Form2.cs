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
using Microsoft.VisualBasic;

namespace odev
{
    public partial class Form2 : Form
    {
        public string[] alfabe { get; set; }
        public string rastgele_kelime { get; set; }
        public int d_boyut { get; set; }
        public Form2()
        {
            InitializeComponent();
        }
        char[] harfler= {'a','b','c','ç','d','e','f','g','ğ','h','ı','i','j','k','l','m','n','o','ö','p','r','s','ş','t','u','ü','v','y','z'};
        private void Form2_Load_1(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(450, 150);
            label3.Text = "Rastgele Kelime:   "+ rastgele_kelime;
            pictureBox1.Image = ımageList1.Images[0];
            for (int i = 0; i < harfler.Length; i++)
            {
                listBox1.Items.Add(harfler[i]);
            }
            label4.Text = "Sözlükte kalan kelime sayısı: " + d_boyut.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int kartezyen_carpim = 3, normal_hesaplama = 3;
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Normal Hesaplamayı ve Kartezyen Çarpımını Giriniz!!!!", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                normal_hesaplama = int.Parse(textBox1.Text);
                kartezyen_carpim = int.Parse(textBox2.Text);
                if (kartezyen_carpim < normal_hesaplama)
                {
                    MessageBox.Show("Kartezyen Çarpım , Normal Toplamdan küçük olamaz!!", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    normal_hesaplama = int.Parse(textBox1.Text);
                    kartezyen_carpim = int.Parse(textBox2.Text);
                    loop_function(normal_hesaplama, kartezyen_carpim);
                }
            }

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void function(string[] array, int dimension)
        {
            Array.Clear(alfabe, 0, dimension);
            for (int i = 0; i < array.Length; i++)
            {
                alfabe[i] = array[i];
            }
            letter_checking(array);
        }
        private void letter_checking(string[] array)
        {
            char[] olanharfler = new char[29];
            int k = 0;
            for (int i = 0; i < harfler.Length; i++)
            {
                char aranan = harfler[i];
                foreach (string word in alfabe)
                {
                    if (string.IsNullOrEmpty(word))
                    {
                        break;
                    }
                    else if (word.IndexOf(aranan, 0) != -1)
                    {
                            olanharfler[k] = aranan;
                            k++;
                            break;
                    }
                }
            }
            int bayrak_biti = 0,l=0;
            char[] olmayan_harfler = new char[29];
            for (int i = 0; i < harfler.Length; i++)
            {
                for (int j = 0; j < olanharfler.Length; j++)
                {
                    if (harfler[i] == olanharfler[j])
                    {
                        bayrak_biti = 1;
                        break;
                    }
                }
                if (bayrak_biti == 0)
                {
                    olmayan_harfler[l] = harfler[i];
                    l++;
                }
                bayrak_biti = 0;
            }
            Array.Clear(harfler, 0, harfler.Length);
            for (int i = 0; i < olanharfler.Length; i++)
            {
                harfler[i] = olanharfler[i];
                Console.Write(olanharfler[i].ToString());
            }
            listBox1.Items.Clear();
            for (int i = 0; i < harfler.Length; i++)
            {
                listBox1.Items.Add(harfler[i]);
            }
            for (int i = 0; i < olmayan_harfler.Length; i++)
            {
                listBox2.Items.Add(olmayan_harfler[i]);
            }
        }
        private void loop_function(int nh, int kc)
        {
            int normal_toplam = 0, kartezyen_toplam = 0,eleman=0;
            string[] temp = new string[d_boyut];
            foreach (string value in alfabe)
            {
                if (string.IsNullOrEmpty(value))
                {
                    break;
                }
                for (int i = 0; i < rastgele_kelime.Length; i++)
                {
                    char aranan = rastgele_kelime.ToCharArray()[i];
                    int[] adetler = Enumerable.Range(0, value.Length).Select(x => value.Substring(x, 1).Count(p => p == aranan)).ToArray();
                    for (int j = 0; j < adetler.Length; j++)
                    {
                        if (rastgele_kelime.IndexOf(aranan, 0, i) == -1)//harf  daha önce geldi mi?
                        {
                            normal_toplam += adetler[j];
                        }
                        kartezyen_toplam += adetler[j];
                    }
                    Array.Clear(adetler, 0, adetler.Length);
                }

                if (nh == normal_toplam && kc == kartezyen_toplam)
                {
                    temp[eleman] = value;
                    eleman++;
                }

                normal_toplam = 0;
                kartezyen_toplam = 0;
            }
            function(temp, eleman);//kalan elemanları sözlük dizine atıyor..
            Array.Clear(temp, 0, d_boyut);
            if (alfabe[1] != null && alfabe[2] != null)
            {
                listBox3.Items.Add(rastgele_kelime);
                rastgele_kelime = "";
                Random rastgele = new Random();
                Console.WriteLine(eleman.ToString());
                int rastgele_sayi = rastgele.Next(eleman);//0 ile sözlük boyutu arasındaki sayıları üretir
                label4.Text="Sözlükte kalan kelime sayısı: " + eleman.ToString();
                rastgele_kelime = alfabe[rastgele_sayi];
                label3.Text = "Rastgele kelime:" + rastgele_kelime;
                MessageBox.Show("Yeni Kartezyen Çarpım ve Normal Hesaplama Değerlerini Giriniz", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                if (alfabe[0] == null)
                {
                    if (MessageBox.Show("Girdiğiniz kelime sözlükte bulunamadı..\nKelimeyi sözlüğe eklememi ister misiniz?", "Bilgi Ekranı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string yazilacak_kelime = Interaction.InputBox("Tuttuğunuz kelimeyi giriniz..", "Kelime girişi","", 400, 300);
                        string dosya_yolu = @"C:\Users\erdasonur\source\repos\odev\sozluk.txt";
                        FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(yazilacak_kelime);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        MessageBox.Show("Seçiminiz üzerine kelime sözlüğe eklendi..", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    else
                    {
                        MessageBox.Show("Seçiminiz üzere kelime sözlüğe eklenmedi", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Tuttuğunuz Kelime : " + alfabe[0], "Kelime Bulundu!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            eleman = 0;
        }
    }
}
