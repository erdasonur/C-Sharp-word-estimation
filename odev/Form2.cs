using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private void Form2_Load_1(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(450, 150);
            label3.Text = "Rastgele Kelime: " + rastgele_kelime;
            pictureBox1.Image = ımageList1.Images[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int kartezyen_carpim = 3, normal_hesaplama = 3, eleman = 0;
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Normal Hesaplamayı ve Kartezyen Çarpımını Giriniz!!!!","Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                    string[] temp = new string[d_boyut];
                    int normal_toplam = 0, kartezyen_toplam = 0;
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
                                if (rastgele_kelime.IndexOf(aranan, 0, i) == -1)
                                {
                                    normal_toplam += adetler[j];
                                }
                                kartezyen_toplam += adetler[j];
                            }
                            Array.Clear(adetler, 0, adetler.Length);
                        }
                        //Console.WriteLine(normal_hesaplama.ToString() + " " + kartezyen_carpim + " " + normal_toplam + " " + kartezyen_toplam.ToString());
                        if (normal_hesaplama == normal_toplam && kartezyen_carpim == kartezyen_toplam)
                        {
                            temp[eleman] = value;
                            //Console.WriteLine(temp[eleman].ToString());
                            eleman++;
                        }
                        normal_toplam = 0;
                        kartezyen_toplam = 0;
                    }
                    function(temp,eleman);//kalan elemanları sözlük dizine atıyor..
                    Array.Clear(temp, 0, d_boyut);  
                }
                }
            if (alfabe[1] != null && alfabe[2]!=null)
            {
                rastgele_kelime = "";
                Random rastgele = new Random();
                Console.WriteLine(eleman.ToString());
                int rastgele_sayi = rastgele.Next(eleman);//0 ile sözlük boyutu arasındaki sayıları üretir
                MessageBox.Show("Sözlükte kalan kelime sayısı: "+eleman.ToString(),"Sözlük Bilisi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                rastgele_kelime = alfabe[rastgele_sayi];
                label3.Text = rastgele_kelime;
                MessageBox.Show("Yeni Kartezyen Çarpım ve Normal Hesaplama Değerlerini Giriniz","Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Tuttuğunuz Kelime : " + alfabe[0],"Kelime Bulundu!!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                for (int i = 0; i < alfabe.Length; i++)
                {
                    Console.WriteLine(alfabe[i]);
                }
            }
                eleman = 0;
            
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
        }
    }
}
