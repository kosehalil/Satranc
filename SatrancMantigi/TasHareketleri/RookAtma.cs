using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi.TasHareketleri
{
    public class RookAtma : HareketEtme
    {
        public override HamleCesitleri hamleCesitleri { get; }

        public override Konum suankiKonumdan { get; }

        public override Konum yeniKonuma { get; }

        private readonly Yonler sahinHareketYonu;
        private readonly Konum rookSuankiKonum;
        private readonly Konum rookYeniKonum;


        public RookAtma (HamleCesitleri cesit, Konum sahKonum) 
        {
            hamleCesitleri = cesit;
            suankiKonumdan = sahKonum;

            if(hamleCesitleri == HamleCesitleri.SahKanadinaRookAtma)
            {
                sahinHareketYonu = Yonler.Dogu;
                yeniKonuma = new Konum(sahKonum.Satır, 6);
                rookSuankiKonum = new Konum(sahKonum.Satır, 7);
                rookYeniKonum = new Konum(sahKonum.Satır,5);

            }
            else if(cesit == HamleCesitleri.VezirKanadinaRookAtma)
            {
                sahinHareketYonu = Yonler.Bati;
                yeniKonuma = new Konum(sahKonum.Satır, 2);
                rookSuankiKonum = new Konum(sahKonum.Satır, 0);
                rookYeniKonum = new Konum(sahKonum.Satır, 3);
            }
           
        }

        public override bool Calistir(SatrancTahtasi satrancTahtasi)
        {
            new NormalHamleler(suankiKonumdan, yeniKonuma).Calistir(satrancTahtasi);
            new NormalHamleler(rookSuankiKonum, rookYeniKonum).Calistir(satrancTahtasi);

            return false;
        }

        public override bool HamlelerGecerliMi(SatrancTahtasi satrancTahtasi) 
        {
            Oyuncu oyuncu = satrancTahtasi[suankiKonumdan].Renkler;

            if (satrancTahtasi.SahCekiliyorMu(oyuncu))
            {
                return false;
            }

            SatrancTahtasi kopyasi = satrancTahtasi.Kopyasi();
            Konum kingPosInCopy = suankiKonumdan;

            for(int i = 0; i<2; i++)
            {
                new NormalHamleler(kingPosInCopy, kingPosInCopy + sahinHareketYonu).Calistir(kopyasi);
                kingPosInCopy += sahinHareketYonu;

                if (kopyasi.SahCekiliyorMu(oyuncu))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
