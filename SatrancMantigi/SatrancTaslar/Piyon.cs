using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class Piyon : SatrancTaslar
    {
        // oncelikle tasin cesitinin yani tasin bir piyon oldugu belirlenir.
        public override SatrancTaslarininCesitleri Cesitler => SatrancTaslarininCesitleri.Piyon;

        // belirlenen tasin rengi belirlenilen sadece get edilebilen bir prop ozellik tanimlanir.
        public override Oyuncu Renkler { get; }

        private readonly Yonler ileri;

        
        // rengi parametre alan bir yapici metod tanimlanir yine
        public Piyon(Oyuncu renkler)
        {
            Renkler = renkler;

            // piyon rengi beyazsa asagida olacagi icin yukari yani (kuzeye) gidecek.
            if (renkler == Oyuncu.beyaz)
            {
                ileri = Yonler.Kuzey;
            }
            // piyon rengi siyahsa yukarida olacagi icin asagi yani (guneye) gidecek.
            else if (renkler == Oyuncu.siyah)
            {
                ileri = Yonler.Guney;
            }
        }

        public override SatrancTaslar Kopyasi()
        {
            Piyon kopyasi = new Piyon(Renkler);        //   piyonun renginden yeni bir piyon yaratiliyor
            kopyasi.tasHareketi = tasHareketi;         //   bu piyonun rengi icinde kullanilan ozellikler kopyalaniyor
            return kopyasi;
        }

        // sasin hareketi icin kontrol etme 
        private static bool HareketEdebilir(Konum kon, SatrancTahtasi satrancTahtasi)
        {
            return SatrancTahtasi.SinirlerinİcindeMi(kon) && satrancTahtasi.BosMu(kon);
        }
        // tasi yiyebilmek icin dolu mu bos mu kontrolü
        private bool TasiYiyebilir(Konum kon,SatrancTahtasi satrancTahtasi)
        {
            if(!SatrancTahtasi.SinirlerinİcindeMi(kon) || satrancTahtasi.BosMu(kon))
            {
                return false;
            }
            return satrancTahtasi[kon].Renkler != Renkler;

        }

        private IEnumerable<HareketEtme> ileriHareketEtme(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            Konum birHamleYapma = suankiKon + ileri;

            if (HareketEdebilir(birHamleYapma, satrancTahtasi))
            {
                yield return new NormalHamleler(suankiKon, birHamleYapma);

                Konum ikiHamleYapma = birHamleYapma + ileri;

                if(!tasHareketi && HareketEdebilir(ikiHamleYapma, satrancTahtasi))
                {
                    yield return new NormalHamleler(suankiKon, ikiHamleYapma);
                }
            }
        }

        // piyonun yemesi icin capraz hamle yaparak yemesi.
        private IEnumerable<HareketEtme> CaprazHamleler(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            foreach(Yonler yonler in new Yonler[] { Yonler.Bati, Yonler.Dogu })
            {
                Konum yeniKon = suankiKon + ileri + yonler;
                if (TasiYiyebilir(yeniKon, satrancTahtasi))
                {
                    yield return new NormalHamleler(suankiKon, yeniKon);
                }

            }
        }
        // secilen konumun dogrultusunda piyonun yapabilecegi hamlelerin hesaplanip yapildigi kisim
        public override IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            return ileriHareketEtme(suankiKon, satrancTahtasi).Concat(CaprazHamleler(suankiKon, satrancTahtasi));
        }
    }
}
