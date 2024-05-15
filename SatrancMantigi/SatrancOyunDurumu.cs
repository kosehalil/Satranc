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

        // SatrancOyunDurumunun oyuncuyu ve satranctahtasini parametre alan kurucu metodunu tanimladik
        public SatrancOyunDurumu(Oyuncu oyuncu,SatrancTahtasi satranctahtasi)
        {
            SuankiOyuncu = oyuncu;
            SatrancTahtasi = satranctahtasi;
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
            return satrancTaslar.HamleleriYap(kon, SatrancTahtasi);
        }

        // hamlenin sirayla degistigi yer yani;
        // beyazdan-siyaha veya siyahtan-beyaza geciyor hamle
        public void HareketeGecme(HareketEtme hareketEtme)
        {
            hareketEtme.Calistir(SatrancTahtasi);
            SuankiOyuncu = SuankiOyuncu.Rakibi();

        }
    }
}
