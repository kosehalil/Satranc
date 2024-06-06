using SatrancMantigi.TasHareketleri;

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

        
        private static bool KaleHareketEttirildiMi(Konum kon,SatrancTahtasi satrancTahtasi)
        {
            if (satrancTahtasi.BosMu(kon))
            {
                return false;
            }
            SatrancTaslar satrancTaslar = satrancTahtasi[kon];
            return satrancTaslar.Cesitler == SatrancTaslarininCesitleri.Kale && !satrancTaslar.tasHareketi;


        }

        private static bool TamamenBosMu(IEnumerable<Konum> konums , SatrancTahtasi satrancTahtasi)
        {
            return konums.All(kon => satrancTahtasi.BosMu(kon));
        }

        private bool SahKanadinaRookYapilabilirMi(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            if (tasHareketi)
            {
                return false;
            }
            Konum rookPos = new Konum(suankiKon.Satır, 7);
            Konum[] betweenPosition = new Konum[] { new(suankiKon.Satır, 5), new(suankiKon.Satır, 6) };

            return KaleHareketEttirildiMi(rookPos, satrancTahtasi) && TamamenBosMu(betweenPosition, satrancTahtasi);
        }

        private bool VezirKanadinaRookYapilabilirMi(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            if (tasHareketi)
            {
                return false;
            }

            Konum rookPos = new Konum(suankiKon.Satır, 0);
            Konum[] betweenPosition = new Konum[] { new(suankiKon.Satır, 1), new(suankiKon.Satır, 2), new(suankiKon.Satır, 3) };

            return KaleHareketEttirildiMi(rookPos, satrancTahtasi) && TamamenBosMu(betweenPosition, satrancTahtasi);
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

            if (SahKanadinaRookYapilabilirMi(suankiKon, satrancTahtasi))
            {
                yield return new RookAtma(HamleCesitleri.SahKanadinaRookAtma, suankiKon);
            }

            if(VezirKanadinaRookYapilabilirMi(suankiKon, satrancTahtasi))
            {
                yield return new RookAtma(HamleCesitleri.VezirKanadinaRookAtma, suankiKon);
            }
        }

        public override bool SahinYoluEngellenebilirMi(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            return HareketEtmePozisyonlar(suankiKon, satrancTahtasi).Any(yeniKon =>
            {
                SatrancTaslar satrancTaslar = satrancTahtasi[yeniKon];
                return satrancTaslar != null && satrancTaslar.Cesitler == SatrancTaslarininCesitleri.Sah;
            });
        }
    }
}
