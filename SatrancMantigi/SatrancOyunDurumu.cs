using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class SatrancOyunDurumu
    {
        // Satranc tahtasini okunabilir ama yazılamaz bir prop yani özellik olarak tanimladik.
        public SatrancTahtasi SatrancTahtasi { get; }        

        public Oyuncu SuankiOyuncu { get; private set; }     // sadece oyuncu sınıfının icinden erisilebilsin diye private set yazma olarak kullanılıyor.

        public SatrancOyunSonucu OyunSonucu { get; private set; } = null;

        private int gecerkenAlmaYemeHareketi = 0;  

        private string stringDurumBilgisi;

        private readonly Dictionary<string, int> gecmisStringDurumBilgisi = new Dictionary<string, int>();

        // SatrancOyunDurumunun oyuncuyu ve satranctahtasini parametre alan kurucu metodunu tanimladik
        public SatrancOyunDurumu(Oyuncu oyuncu,SatrancTahtasi satranctahtasi)
        {
            SuankiOyuncu = oyuncu;
            SatrancTahtasi = satranctahtasi;

            stringDurumBilgisi = new SatrancDurumBilgisi(SuankiOyuncu, satranctahtasi).ToString();
            gecmisStringDurumBilgisi[stringDurumBilgisi] = 1;
        }

        // IEnumerable sıralama sorgu tarzı islemler yapmak icin kullandik.
        // Burada da konumda bir tas var mi diye ve tas varsa
        // bu tasin mevcut oyuncunun renginde mi diye kontrol ediyor.
        public IEnumerable<HareketEtme> TasicinGecerliHamleler(Konum kon)
        {
            if(SatrancTahtasi.BosMu(kon) || SatrancTahtasi[kon].Renkler != SuankiOyuncu)
            {
                return Enumerable.Empty<HareketEtme>();
            }

            SatrancTaslar satrancTaslar = SatrancTahtasi[kon];
            IEnumerable<HareketEtme> hamleSecenekleri = satrancTaslar.HamleleriYap(kon, SatrancTahtasi);
            return hamleSecenekleri.Where(hareketEtme => hareketEtme.HamlelerGecerliMi(SatrancTahtasi));
        }

        // hamlenin sirayla degistigi yer yani;
        // beyazdan-siyaha veya siyahtan-beyaza geciyor hamle
        public void HareketeGecme(HareketEtme hareketEtme)
        {
            SatrancTahtasi.PiyonGecisKonumunuBelirle(SuankiOyuncu, null);
            bool captureOrPawn = hareketEtme.Calistir(SatrancTahtasi);

            if (captureOrPawn)
            {
                gecerkenAlmaYemeHareketi = 0;
                gecmisStringDurumBilgisi.Clear();
            }
            else
            {
                gecerkenAlmaYemeHareketi++;
            }

            SuankiOyuncu = SuankiOyuncu.Rakibi();
            DurumBilgisiniGuncelleme();
            SahMatOlupOyunBittiMi();

        }

        public IEnumerable<HareketEtme> ButunGecerliHamleler(Oyuncu oyuncu) 
        {
            IEnumerable<HareketEtme> hamleSecenekleri = SatrancTahtasi.TasPozisyonlarinaBagliOlarak(oyuncu).SelectMany(kon =>
            {
                SatrancTaslar satrancTaslar = SatrancTahtasi[kon];
                return satrancTaslar.HamleleriYap(kon, SatrancTahtasi);
            });

            return hamleSecenekleri.Where(hareketEtme => hareketEtme.HamlelerGecerliMi(SatrancTahtasi));
        }

        private void SahMatOlupOyunBittiMi() 
        {
            if (!ButunGecerliHamleler(SuankiOyuncu).Any())
            {
                if (SatrancTahtasi.SahCekiliyorMu(SuankiOyuncu))
                {
                    OyunSonucu = SatrancOyunSonucu.Galibiyet(SuankiOyuncu.Rakibi());
                }
                else
                {
                    OyunSonucu = SatrancOyunSonucu.Berabere(SatrancBitisSekilleri.OynanacakHamleninOlmamasiBerabere);
                }
            }
            else if (SatrancTahtasi.YetersizTasKalmasi())
            {
                OyunSonucu = SatrancOyunSonucu.Berabere(SatrancBitisSekilleri.SatrancTaslarininMatIcinYeterliOlmamasi);
            }
            else if (ElliHamleKurali())
            {
                OyunSonucu = SatrancOyunSonucu.Berabere(SatrancBitisSekilleri.ElliHamleKurali);
            }
            else if (AyniHamleyiUcKereOynamak())
            {
                OyunSonucu = SatrancOyunSonucu.Berabere(SatrancBitisSekilleri.AyniHamleyiUcKereOynamak);
            }

        }

        public bool OyunBittiMi() 
        {
            return OyunSonucu != null;
        }

        private bool ElliHamleKurali()
        {
            int butunHamleler = gecerkenAlmaYemeHareketi / 2;
            return butunHamleler == 50;
        }

        private void DurumBilgisiniGuncelleme()
        {
            stringDurumBilgisi = new SatrancDurumBilgisi(SuankiOyuncu, SatrancTahtasi).ToString();

            if (!gecmisStringDurumBilgisi.ContainsKey(stringDurumBilgisi))   // ContainsKey ile birseyin olup olmadigi sorugu yapiliyor (durum bilgisinin)
            {
                gecmisStringDurumBilgisi[stringDurumBilgisi] = 1;
            }
            else
            {
                gecmisStringDurumBilgisi[stringDurumBilgisi]++;
            }
        }

        private bool AyniHamleyiUcKereOynamak()
        {
            return gecmisStringDurumBilgisi[stringDurumBilgisi] == 3;
        }
    }
}
