using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class Kale : SatrancTaslar
    {
        // once tasin türünün belirtilir ve bu tas bir kaledir belirlenir.
        public override SatrancTaslarininCesitleri Cesitler => SatrancTaslarininCesitleri.Kale;

        // kalenin rengi belirtilir get edilebilen bir prop ozellik tanimlanir.
        public override Oyuncu Renkler { get; }

        // kalenin gidebilecegi yonler belirlenir bunlar saga sola yukarı ve asagidir 
        // kordinat düzlemindeki kuzey guney dogu batidir
        private static readonly Yonler[] yonlers = new Yonler[]
        {
            Yonler.Guney,
            Yonler.Kuzey,
            Yonler.Bati,
            Yonler.Dogu
        };

        // parametresi renk olan bir yapici metod tanimlanir diger renk tas icinde tanimlamak icin ayni ozellileri.
        public Kale(Oyuncu renkler)
        {
            Renkler = renkler;
        }
        public override SatrancTaslar Kopyasi()
        {
            Kale kopyasi = new Kale(Renkler);        // kalenin rengi kullanilip yeni bir kale olusturuluyor
            kopyasi.tasHareketi = tasHareketi;       // ve bu kalenin ozellikleri diger renk icinde gecerli olacak sekilde kopyalaniyor
            return kopyasi;
        }

        // kalenin mevcut haraketleri sonucunda haraket ettirilir.
        public override IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            return BelirliYonlerdeHareketEtme(suankiKon, satrancTahtasi, yonlers).Select(yeniKon => new NormalHamleler(suankiKon, yeniKon));
        }
    }
}


