using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class Fil : SatrancTaslar
    {
        // tasin türü belirtilir bu tas bir fil tasidir.
        public override SatrancTaslarininCesitleri Cesitler => SatrancTaslarininCesitleri.Fil;

        // filin rengi belirtilir get edilebilen bir prop ozellik tanimlanir.
        public override Oyuncu Renkler { get; }

        // filin gidebilecegi yonler tanimlanir 
        private static readonly Yonler[] yonlers = new Yonler[]
        {
            Yonler.KuzeyBati,
            Yonler.KuzeyDogu,
            Yonler.GuneyBati,
            Yonler.GuneyDogu
        };

        // filin rengini parametre olarak alan yapici olusturulur
        public Fil(Oyuncu renkler)
        {
            Renkler = renkler;
        }

        // 
        public override SatrancTaslar Kopyasi()
        {
            Fil kopyasi = new Fil(Renkler);       // filin rengi kullanilarak yeni fil olusturulur beyazsa - siyah icin
            kopyasi.tasHareketi = tasHareketi;    // ayni ozellikler diger tas rengi icinde kopyalanir
            return kopyasi;
        }

         // filin mevcut hamlelerini secilip yapilacak oldugu kisim.
         public override IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon,SatrancTahtasi satrancTahtasi)
         {
            return BelirliYonlerdeHareketEtme(suankiKon, satrancTahtasi, yonlers).Select(yeniKon => new NormalHamleler(suankiKon, yeniKon));
         }
    }
}
