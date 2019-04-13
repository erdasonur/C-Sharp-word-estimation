using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace odev
{
    public partial class Form1 : Form
    {
        int boyut = 0;
        string[] alfabe = new string[80000];
        Form2 f2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            //dosyadan veri okuma işlemi
            string dosya_okuma = @"C:\Users\erdasonur\source\repos\odev\sozluk.txt";
            FileStream fs = new FileStream(dosya_okuma, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                alfabe[boyut] = yazi;
                yazi = sw.ReadLine();
                boyut++;
            }
            sw.Close();
            fs.Close();
            f2.alfabe = alfabe;
            f2.d_boyut = boyut;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                //girilen değer türkçe kelime mi diye kontrol et (sözlükte varlığının kontrolü)
                f2.rastgele_kelime = Interaction.InputBox("Girilecek kelime:", "Kelime Belirleme", "", 400, 300);
                int kelime_tespiti = 0,i;
                for (i = 0; i < boyut; i++)
                {
                    if(String.Compare(alfabe[i], f2.rastgele_kelime) == 0)
                    {
                        //kelime sözlükte var
                        kelime_tespiti = 1;
                        break;//döngüyü bitir kelime bulundu
                    }
                }
                if (kelime_tespiti == 0)//mesaj ile diziyi karşılaştır kelime dizi de yoksa eklemek ister misin diye sorma işlemi..
                {
                    MessageBox.Show("Bu kelime sözlükte yer almıyor.. Türkçe değil ya da eklenmemiş.","Sözlük Kontrolü",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    
                    if (MessageBox.Show(f2.rastgele_kelime + "kelimesini sözlüğe eklememi ister misin ? E/H", "Sözlüğe Kelime Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        //dosyaya yazma işlemi
                        string dosya_yolu = @"C:\Users\erdasonur\source\repos\odev\sozluk.txt";
                        FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(f2.rastgele_kelime);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        alfabe[boyut++] = f2.rastgele_kelime;//diziye de yazıldı
                        MessageBox.Show("Seçiminiz üzerine kelime sözlüğe eklendi..","Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Seçiminiz üzerine kelime sözlüğe eklenmedi..","Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                //Form2'ye dizinin boyutu (kaç kelime olduğu) gidiyor..
                f2.Show();
                this.Hide();
            }
            else
            {
                //random kelime alma işlemi
                Random rastgele = new Random();
                int a = boyut - 1;
                int rastgele_sayi = rastgele.Next(a);//0 ile 50 arasındaki sayıları üretir
                f2.rastgele_kelime = f2.alfabe[rastgele_sayi];
                MessageBox.Show("Rastgele gelen kelime: " + f2.rastgele_kelime, "Seçilen Kelime", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f2.Show();
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "----KELİME SEÇİMİ----";
            timer1.Start();
            timer1.Interval = 300;
            this.DesktopLocation = new Point(450, 150);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
        }
    }
}
