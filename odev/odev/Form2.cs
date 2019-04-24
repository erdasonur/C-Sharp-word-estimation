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
        public string[] Alfabe { get; set; }
        public string Rastgele_kelime { get; set; }
        public int D_boyut { get; set; }
        public Form2()
        {
            InitializeComponent();
        }
        char[] harfler= {'a','b','c','ç','d','e','f','g','ğ','h','ı','i','j','k','l','m','n','o','ö','p','r','s','ş','t','u','ü','v','y','z'};
        private void Form2_Load_1(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(450, 150);
            label3.Text = "Rastgele Kelime:   "+ Rastgele_kelime;
            for (int i = 0; i < harfler.Length; i++)
            {
                listBox1.Items.Add(harfler[i]);
            }
            label4.Text = "Sözlükte kalan kelime sayısı: " + D_boyut.ToString();
        }
        private void Button1_Click(object sender, EventArgs e)
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
                    Loop_function(normal_hesaplama, kartezyen_carpim);
                }
            }

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Function(string[] array, int dimension)
        {
            Array.Clear(Alfabe, 0, dimension);
            for (int i = 0; i < array.Length; i++)
            {
                Alfabe[i] = array[i];
            }
            Letter_checking(array);
        }
        string[] anagrams = new string[10];
        int anagram_counter = 0;
        private void Letter_checking(string[] array)
        {
            char[] olanharfler = new char[29];
            int olanharfler_counter = 0;
            for (int i = 0; i < harfler.Length; i++)
            {
                char aranan = harfler[i];
                foreach (string word in Alfabe)
                {
                    if (string.IsNullOrEmpty(word))
                    {
                        break;
                    }
                    else if (word.IndexOf(aranan, 0) != -1)//sözlükte olan harfleri olan harfler dizine atma işlemi
                    {
                            olanharfler[olanharfler_counter] = aranan;
                            olanharfler_counter++;
                            break;
                    }
                }
            }
            int bayrak_biti = 0,olmayanharfler_counter=0;
            char[] olmayan_harfler = new char[29];
            for (int i = 0; i < harfler.Length; i++)
            {
                for (int j = 0; j < olanharfler.Length; j++)
                {
                    if (harfler[i] == olanharfler[j])//olan harf varsa bayrak biti 1 olur 
                    {                                //bayrak biti 1 ise sözlükte olan harf
                        bayrak_biti = 1;             //bayrak biti 0 ise olmayan olur 
                        break;
                    }
                }
                if (bayrak_biti == 0)//burada bayrak biti 0 ise olmayan harfler dizine harfleri atıyoruz
                {
                    olmayan_harfler[olmayanharfler_counter] = harfler[i];
                    olmayanharfler_counter++;
                }
                bayrak_biti = 0;
            }
            Array.Clear(harfler, 0, harfler.Length);//Diziyi boşaltma işlemi
            for (int i = 0; i < olanharfler.Length; i++)
            {
                harfler[i] = olanharfler[i];//olan harfleri harfler dizine atıyoruz
            }
            listBox1.Items.Clear();
            for (int i = 0; i < harfler.Length; i++)//canlı olarak olan harfleri görmek için listboxa ekliyoruz
            {
                listBox1.Items.Add(harfler[i]);
            }
            for (int i = 0; i < olmayan_harfler.Length; i++)//canlı olarak olmayan harfleri görmek için listbox2ye ekliyoruz
            {
                listBox2.Items.Add(olmayan_harfler[i]);
            }
        }
        private void Loop_function(int nh, int kc)
        {
            int normal_toplam = 0, kartezyen_toplam = 0,eleman=0;
            string[] temp = new string[D_boyut];
            foreach (string value in Alfabe)
            {
                if (string.IsNullOrEmpty(value))
                {
                    break;
                }
                /*for (int i = 0; i < rastgele_kelime.Length; i++)//string karşılaştırma 1.yol
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
                }*/
                for (int i = 0; i < Rastgele_kelime.Length; i++)//string karşılaştırma 2.yol
                {
                    for (int j = 0; j < value.Length; j++)
                    {
                        if (Rastgele_kelime.ToCharArray()[i] == value.ToCharArray()[j])
                        {
                            if (Rastgele_kelime.IndexOf(Rastgele_kelime.ToCharArray()[i], 0, i) == -1)
                            {
                                normal_toplam++;
                            }
                            kartezyen_toplam++;
                        }
                    }
                }

                if (nh == normal_toplam && kc == kartezyen_toplam)//anagram kelime kontrolü
                {
                    if (Rastgele_kelime.Length == value.Length && Rastgele_kelime!=value)
                    {
                        int i;
                        for (i = 0; i < value.Length; i++)
                        {
                            char aranan = Rastgele_kelime.ToCharArray()[i];
                            if (value.IndexOf(aranan,0)==-1)
                            {
                                break;
                            }
                        }
                        if (value.Length != i)
                        {
                            temp[eleman] = value;
                            eleman++;
                        }
                        else
                        {
                            anagrams[anagram_counter] = value;
                            anagram_counter++;
                        }
                        
                    }
                    else
                    {
                        temp[eleman] = value;
                        eleman++;
                    }
                }

                normal_toplam = 0;
                kartezyen_toplam = 0;
            }
            Function(temp, eleman);//kalan elemanları sözlük dizine atıyor..
            Array.Clear(temp, 0, D_boyut);
            if (Alfabe[1] != null)
            {
                listBox3.Items.Add(Rastgele_kelime);
                listBox4.Items.Add(nh.ToString() + "  --  " + kc.ToString());
                int bayrak = 0;
                do
                {
                    bayrak = 0;
                    Random_word(eleman);
                    for (int i = 0; i < listBox3.Items.Count; i++)
                    {
                        if (Rastgele_kelime == listBox3.Items[i].ToString())
                        {
                            bayrak = 1;
                        }
                    }
                } while (bayrak == 1);//Burada üst üste aynı kelime gelmesi engelleniyor.
                label3.Text = "Rastgele kelime:" + Rastgele_kelime;
                MessageBox.Show("Yeni Kartezyen Çarpım ve Normal Hesaplama Değerlerini Giriniz", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
                Array.Clear(anagrams, 0, anagrams.Length);
                anagram_counter = 0;
            }
            else
            {
                if (Alfabe[0] == null)
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
                        if (MessageBox.Show("Ana menüye dönmek için Tamamı ,kalip incelemek için İptali , çıkmak içinse çarpı tuşun seçiniz", "Oyun sonu", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                        {
                            Form3 f3 = new Form3();
                            f3.Show();
                            this.Hide();
                        }
                        else
                        {
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            label1.Visible = false;
                            label2.Visible = false;
                            button1.Visible = false;
                            listBox3.Items.Add(Rastgele_kelime);
                            listBox4.Items.Add(nh.ToString() + "-" + kc.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seçiminiz üzere kelime sözlüğe eklenmedi", "Bilgi Ekranı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (MessageBox.Show("Ana menüye dönmek için Tamamı ,kalıp incelemek için İptali , çıkmak içinse çarpı tuşun seçiniz", "Oyun sonu", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                        {
                            Form3 f3 = new Form3();
                            f3.Show();
                            this.Hide();
                        }
                        else
                        {
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            label1.Visible = false;
                            label2.Visible = false;
                            button1.Visible = false;
                            listBox3.Items.Add(Rastgele_kelime);
                            listBox4.Items.Add(nh.ToString() + "-" + kc.ToString());
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Tuttuğunuz Kelime : " + Alfabe[0], "Kelime Bulundu!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    for (int i = 0; i < anagrams.Length; i++)
                    {
                        if (string.IsNullOrEmpty(anagrams[i])){
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Bulunan kelimenin olası "+(i+1) +". anagramı= " + anagrams[i],"Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        }
                    }
                    if (MessageBox.Show("Ana menüye dönmek için Tamamı ,kalıp incelemek için İptali , çıkmak içinse çarpı tuşun seçiniz", "Oyun sonu", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        Form3 f3 = new Form3();
                        f3.Show();
                        this.Hide();
                    }
                    else
                    {
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        label1.Visible = false;
                        label2.Visible = false;
                        button1.Visible = false;
                        listBox3.Items.Add(Rastgele_kelime);
                        listBox4.Items.Add(nh.ToString() + " - " + kc.ToString());
                        label4.Text = "Kalan kelime sayısı:  " + eleman;
                    }
                }
            }
            eleman = 0;
        }
        public void Random_word(int elemann)
        {
            Rastgele_kelime = "";
            Random rastgele = new Random();
            int rastgele_sayi = rastgele.Next(elemann);//0 ile sözlük boyutu arasındaki sayıları üretir
            label4.Text = "Sözlükte kalan kelime sayısı: " + elemann.ToString();
            Rastgele_kelime = Alfabe[rastgele_sayi];
        }
    }
}
