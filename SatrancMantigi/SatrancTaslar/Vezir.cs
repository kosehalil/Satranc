using System;
using System.Collections.Generic;
using System.Linq;

namespace SatrancMantigi
{
    public class Vezir : SatrancTaslar
    {
        // tasin cesitinin vezir oldugu belirlenir.
        public override SatrancTaslarininCesitleri Cesitler => SatrancTaslarininCesitleri.Vezir;

        // sadece rengin okunabildigi degistirilemeyen bir prop ozellik tanimlanir.
        public override Oyuncu Renkler { get; }

        // tasin gidebilecegi yonler belirlenir.
        private static readonly Yonler[] yonlers = new Yonler[]
        {
            Yonler.Dogu,
            Yonler.Bati,
            Yonler.Kuzey,
            Yonler.Guney,
            Yonler.GuneyBati,
            Yonler.GuneyDogu,
            Yonler.KuzeyBati,
            Yonler.KuzeyDogu
        };

        // vezirin rengi parametre olarak alan yapici metodu olusturulur.
        public Vezir(Oyuncu renkler)
        {
            Renkler = renkler;
        }

        public override SatrancTaslar Kopyasi()
        {
            Vezir kopyasi = new Vezir(Renkler);    // yeni bir vezir olusturulur ve bu vezir icin
            kopyasi.tasHareketi = tasHareketi;     // diger renk icinde olacak sekilde aynı ozellikler kopyalanir
            return kopyasi;
        }

        // vezirin mevcut konumlarina haraket etmesi saglanir.
        public override IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon, SatrancTahtasi satrancTahtasi)
        {
            return BelirliYonlerdeHareketEtme(suankiKon, satrancTahtasi, yonlers).Select(yeniKon => new NormalHamleler(suankiKon, yeniKon));
        }
    }
}