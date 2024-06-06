using SatrancMantigi;
using System.Windows;
using System.Windows.Controls;

namespace SatrancArayuzu
{
    /// <summary>
    /// Interaction logic for OyunBitisMenusu.xaml
    /// </summary>
    public partial class OyunBitisMenusu : UserControl
    {


        public event Action<MenuAyarlari> YapilanSecim;
        public OyunBitisMenusu(SatrancOyunDurumu satrancOyunDurumu)
        {       
            InitializeComponent();

            SatrancOyunSonucu oyunSonucu = satrancOyunDurumu.OyunSonucu;
            KazananOyuncu.Text = KazananOyuncuYazisi(oyunSonucu.Kazanan);
            KazanmaSekili.Text = KazananOyuncuKazanmaSekli(oyunSonucu.BitisSekilleri, satrancOyunDurumu.SuankiOyuncu);
        }

        public static string KazananOyuncuYazisi(Oyuncu kazanan)
        {
            return kazanan switch
            {
                Oyuncu.beyaz => "Beyaz Oyuncu Kazandi",
                Oyuncu.siyah => "Siyah Oyuncu Kazandi",
                _ => "Berabere Kaldiniz"
            };
        }


        private static string OyuncuDurum(Oyuncu oyuncu)
        {
            return oyuncu switch
            {
                Oyuncu.beyaz => "Beyaz",
                Oyuncu.siyah => "Siyah",
                _ => ""
            };
        }


        private static string KazananOyuncuKazanmaSekli(SatrancBitisSekilleri bitisNedeni, Oyuncu suankiOyuncu)
        {
            return bitisNedeni switch
            {
                SatrancBitisSekilleri.OynanacakHamleninOlmamasiBerabere => $"PAT - {OyuncuDurum(suankiOyuncu)} Hamle yapamiyor",
                SatrancBitisSekilleri.SahMat => $"SAHMAT - {OyuncuDurum(suankiOyuncu)} Hamle yapamiyor",
                SatrancBitisSekilleri.ElliHamleKurali => "ELLİ-HAMLE KURALİ",
                SatrancBitisSekilleri.SatrancTaslarininMatIcinYeterliOlmamasi => "MAT İCİN YETERLİ TAS YOK",
                SatrancBitisSekilleri.AyniHamleyiUcKereOynamak => "ÜC KEZ AYNİ HAMLE TEKRARİ",
                _ => ""

            };
        }
        private void TekrarOynayaBasmak(object sender, RoutedEventArgs e)  
        {

            YapilanSecim?.Invoke(MenuAyarlari.TekrarOyna);     // Invoke ile metodu cagiriyoruz

        }

        private void CikisaBasmak(object sender, RoutedEventArgs e)  
        {

            YapilanSecim?.Invoke(MenuAyarlari.Cikis);    // Invoke ile metodu cagiriyoruz

        }

      
    }
}
