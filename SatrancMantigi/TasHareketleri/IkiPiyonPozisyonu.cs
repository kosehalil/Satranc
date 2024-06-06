using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class İkiPiyonPozisyonu : HareketEtme
    {
        public override HamleCesitleri hamleCesitleri => HamleCesitleri.IkiPiyonPozisyonu;

        public override Konum suankiKonumdan { get; }

        public override Konum yeniKonuma { get; }

        private readonly Konum gelinenKonum;

        public İkiPiyonPozisyonu(Konum suankiKon, Konum yeniKon)
        {
            suankiKonumdan = suankiKon;
            yeniKonuma = yeniKon;
            gelinenKonum = new Konum((suankiKon.Satır + yeniKon.Satır) / 2, suankiKon.Sutun);
        }

        public override bool Calistir(SatrancTahtasi satrancTahtasi)
        {
            Oyuncu oyuncu = satrancTahtasi[suankiKonumdan].Renkler;
            satrancTahtasi.PiyonGecisKonumunuBelirle(oyuncu, gelinenKonum);
            new NormalHamleler(suankiKonumdan, yeniKonuma).Calistir(satrancTahtasi);

            return true;
        }
    }
}
