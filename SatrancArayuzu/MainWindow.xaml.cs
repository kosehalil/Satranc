using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SatrancMantigi;
using System.Windows.Input;

namespace SatrancArayuzu
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] tasResimleri = new Image[8, 8];                      // bir resmi temsil etmek için kullanilir

        private readonly Rectangle[,] isikizgaralari = new Rectangle[8, 8];            // kare veya dikdörtgen seklinde grafiksel nesneler oluşturmak icin var. 

        private readonly Dictionary<Konum, HareketEtme> HamlelerinTutulmasiSaklanmasi = new Dictionary<Konum, HareketEtme>();

        private SatrancOyunDurumu oyunDurumu;

        private Konum secilenKonum = null;

        public MainWindow()
        {
            InitializeComponent();
            TahtaBaslangiciAyari();

            oyunDurumu = new SatrancOyunDurumu(Oyuncu.beyaz,SatrancTahtasi.TahtaBaslangici());
            TahtaninOlusturulmasi(oyunDurumu.SatrancTahtasi);

        }

        private void TahtaBaslangiciAyari()
        {
            oyunDurumu = new SatrancOyunDurumu(Oyuncu.beyaz, SatrancTahtasi.TahtaBaslangici());

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image tasResim = new Image();
                    tasResimleri[r, c] = tasResim;
                    SatrancKordinatlari.Children.Add(tasResim);      // Add Dictionartnin sınıfının ögesini eklemek icin kullanilan bir metod.

                    Rectangle satrancisigi = new Rectangle();
                    isikizgaralari[r, c] = satrancisigi;
                    Satrancİsigi.Children.Add(satrancisigi);         // Add Dictionartnin sınıfının ögesini eklemek icin kullanilan bir metod.
                }
            }
        }

        private void TahtaninOlusturulmasi(SatrancTahtasi tahta)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    SatrancTaslar taslar = tahta[r, c];
                    tasResimleri[r, c].Source = Resimlerr.GoruntuyuAl(taslar);
                }
            }
        }

        private void SatrancTahtasinaTiklamak(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(Satranc);               // GetPosition C# metodu fare tiklamalari icin kullanilir.
            Konum kon = KareninKonumunuAl(point);                 // Pointte bir C# metodu frameworkudur X-Y kordinatini belirler MainWindowdaki satir = 8 , sütun = 8 kisim.

            if(secilenKonum == null)
            {
                KareKonumuGidebilecegiKonumlar(kon);
            }
            else
            {
                HedeflenenKonumSecildiginde(kon);
            }
         
        }

        private Konum KareninKonumunuAl(Point point)
        {
            double squareSize = Satranc.ActualWidth / 8;
            int satir = (int)(point.Y / squareSize);
            int sutun = (int)(point.X / squareSize);

            return new Konum(satir, sutun);

        }

        // Yani yesil olarak gösterilen kisim oluyor taslarin hareket edebilecegi yerleri gösteriyor..
        private void KareKonumuGidebilecegiKonumlar(Konum kon) 
        {
            IEnumerable<HareketEtme> hareketler = oyunDurumu.TasicinGecerliHamleler(kon);

            if (hareketler.Any())
            {
                secilenKonum = kon;
                GecerliHareketleriTutma(hareketler);
                HamlelerinKonumlarinRenklendirilmesi();
            }
        }

        // Yesil olarak gösterilen konumlardan birine gittiginde de burasi calisiyor konuma gidiyor.
        private void HedeflenenKonumSecildiginde(Konum kon)
        {
            secilenKonum = null;
            HamlelerinKonumlarinRenklendirilmesiniGosterme();

            if(HamlelerinTutulmasiSaklanmasi.TryGetValue(kon, out HareketEtme hareketler))
            {
                HamleYapilsin(hareketler);
            }
        }

        // Burada hamle yapılarak oyun durumuna yansıtılıyor hareket ettirilip tahta yeniden cizdiriliyor.
        private void HamleYapilsin(HareketEtme hareketler)
        {
            oyunDurumu.HareketeGecme(hareketler);
            TahtaninOlusturulmasi(oyunDurumu.SatrancTahtasi);
        }


        private void GecerliHareketleriTutma(IEnumerable<HareketEtme> hareketler)
        {
            HamlelerinTutulmasiSaklanmasi.Clear();

            foreach (HareketEtme hareket in hareketler)
            {
                HamlelerinTutulmasiSaklanmasi[hareket.yeniKonuma] = hareket;
            }

        }

        // Uygun hamleleri renklendirip gösterecek
        private void HamlelerinKonumlarinRenklendirilmesi()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);      // FromArgb coloru kullanmak icin kullanilir yesil rengi temsil eder  (255, 255, 0, 0) mesela bu kirmizi.

            foreach (Konum yeniKon in HamlelerinTutulmasiSaklanmasi.Keys)
            {
                isikizgaralari[yeniKon.Satır, yeniKon.Sutun].Fill = new SolidColorBrush(color);
            }
        }

        // Hamlelerin renklendirilmesini göstermeyerek uygun olmayan hamleleri göstermeyecek böyle
        private void HamlelerinKonumlarinRenklendirilmesiniGosterme()
        {
            foreach (Konum yeniKon in HamlelerinTutulmasiSaklanmasi.Keys)
            {
                isikizgaralari[yeniKon.Satır, yeniKon.Sutun].Fill = Brushes.Transparent;  // Brushes.Transparent seffaf görüntü saglatir.
            }
        }
    }
}

