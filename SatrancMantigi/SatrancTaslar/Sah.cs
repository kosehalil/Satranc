using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class Sah : SatrancTaslar
    {
        // oncelikle tasin bir sah tasi oldugu belirlenir.
        public override SatrancTaslarininCesitleri Cesitler => SatrancTaslarininCesitleri.Sah;

        // bu tasin rengi belirlenir ve sadece get edilebilen bir prop tanimlanir.
        public override Oyuncu Renkler { get; }

        // sahin haraket edebilecegi konumlar 
        private static readonly Yonler[] yonlers = new Yonler[]
        {
            Yonler.Bati,
            Yonler.Dogu,
            Yonler.Kuzey,
            Yonler.Guney,
            Yonler.KuzeyDogu,
            Yonler.KuzeyBati,
            Yonler.GuneyDogu,
            Yonler.GuneyBati
        };

        // sahin rengi parametre alan bir yapici metodu tanimlanir
        public Sah(Oyuncu renkler)
        {
            Renkler = renkler;
        }

        // ayni renk icin islemlerin kopyalanmasi kismi burada da yapilir
        public override SatrancTaslar Kopyasi()
        {
            Sah kopyasi = new Sah(Renkler);      // yeni bir sah olusturulur 
            kopyasi.tasHareketi = tasHareketi;   // ve diger renk icin bu sahin ozelliklerini o da alir
            return kopyasi;
        }

        // sahin mevcut konumdaki hareket edebilecegi pozisyonlarin sinirin icinde mi bos mu o kare dolu mu hesaplanir.
        private IEnumerable<Konum> HareketEtmePozisyonlar(Konum suankiKon,SatrancTahtasi satrancTahtasi)
        {
            foreach (Yonler yonler in yonlers)
            {
                Konum yeniKon = suankiKon + yonler;

                if (!SatrancTahtasi.SinirlerinİcindeMi(yeniKon))
                {
                    continue;
                }
                
                if(satrancTahtasi.BosMu(yeniKon) || satrancTahtasi[yeniKon].Renkler != Renkler)  // renkler farklı mı diye bakar farklıysa yiyebilir o karedeki tasi.
                {
                    yield return yeniKon;
                }
            }
        }
        
        // sahin haraketlerinin hesaplanip yapildigi kisim.
        public override IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            foreach(Konum yeniKon in HareketEtmePozisyonlar(suankiKon, satrancTahtasi))
            {
                yield return new NormalHamleler(suankiKon, yeniKon);
            }
        }
    }
}
