using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public abstract class SatrancTaslar
    {
        // satranc tasinin cesitini döndüren abstract soyut bir prop ozellik diger alt siniflarda kullanicagimiz.
        public abstract SatrancTaslarininCesitleri Cesitler { get; }
        // satranc tasinini rengini belirtecek diger siniflarda cagiracagimiz soyut prop.
        public abstract Oyuncu Renkler { get; }
        public bool tasHareketi { get; set; } = false;

        // satranc taslarinin kopyasini olusturup ozellikleri diger tas icin kopyalamak icin kullanilan kisim.
        public abstract SatrancTaslar Kopyasi();

        // belirtilen secilen tasin haraket edebilecegi mecvut konumlari yaptirmak icin kullanilan soyuf metod
        public abstract IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon, SatrancTahtasi satrancTahtasi);

        // belirtilen secilen tasin belirli bir yonde edebilecegi hareketlerin bos mu dolu mu tahta sinirlarinin icinde mi diye kontrol edilip belirlenen kisim
        protected IEnumerable<Konum> PozisyonYonundeHareketEtme(Konum suankiKon, SatrancTahtasi satrancTahtasi, Yonler yon)
        {
            for (Konum kon = suankiKon + yon ; SatrancTahtasi.SinirlerinİcindeMi(kon); kon += yon)
            {
                if (satrancTahtasi.BosMu(kon))
                {
                    yield return kon;
                    continue;
                }

                SatrancTaslar satrancTaslar = satrancTahtasi[kon];

                
                    if (satrancTaslar.Renkler != Renkler)
                    {
                        yield return kon;
                    }
                    yield break; 
                
            }
        }
        // belirlenen tasin edebilecegi hareketleri hesaplanir ve hareket edilir.
        protected IEnumerable<Konum> BelirliYonlerdeHareketEtme(Konum suankiKon, SatrancTahtasi satrancTahtasi, Yonler[] yonlers)
        {
            return yonlers.SelectMany(yonler => PozisyonYonundeHareketEtme(suankiKon, satrancTahtasi, yonler));
        }
    }
}