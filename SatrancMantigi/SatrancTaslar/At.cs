namespace SatrancMantigi
{
    public class At : SatrancTaslar
        {
            // tasin türü belirtilir ilk olarak bu tas attir.
            public override SatrancTaslarininCesitleri Cesitler => SatrancTaslarininCesitleri.At;
            
            // atin rengi belirtilir bir prop tanimlanir
            public override Oyuncu Renkler { get; }     // 
            
            // belirtilen atin rengini parametre olarak alan bir yapici metod tanimladik ki koptasi icin kullanabilelim (beyaz-siyah) ortak kullanim icin.
            public At(Oyuncu renkler)       // oyuncunun oynadigi taslarin rengi neyse atinin rengide o olur degistiremez.
            {
                Renkler = renkler;
            }


            public override SatrancTaslar Kopyasi()
            {
                At kopyasi = new At(Renkler);          // atin rengini kullanarak yeni at örnegi olusturuluyor (siyah - beyaz) icin
                kopyasi.tasHareketi = tasHareketi;    // atin hareketlerini kopyaliyor
                return kopyasi;
            }
            
            // atin potansiyel konumlari hesaplanir (tahta üzerinde hangi konumlara gidebilecegi hesaplanir.)
            private static IEnumerable<Konum> PotansiyelPozisyonlar(Konum suankiKonum)
            {
               foreach (Yonler dikeydekiYonler in new Yonler[] {Yonler.Kuzey, Yonler.Guney })         // atin asagi ve yukari olarak hareket edebildigi 
               {
                  foreach(Yonler yataydakiYonler in new Yonler[] { Yonler.Bati, Yonler.Dogu })        // ve yine atin saga ve sola hareket edebildigi anlamina gelir
                  {
                    yield return suankiKonum + 2 * dikeydekiYonler + yataydakiYonler;                 // bulundugu konumun dizi degerini 2 ile carpar ve 2 birim uzagindaki 
                    yield return suankiKonum + 2 * yataydakiYonler + dikeydekiYonler;                 // sag ve sol olacak sekilde potansiyel konumunu bulur
                  }
               }

            }
            
            // atin potansiyel konumlarina gore bu sefer haraket etme konumlarini hesaplanir.
            private IEnumerable<Konum> HareketEtmePozisyonlar(Konum suankiKon, SatrancTahtasi satrancTahtasi)
            {
                  return PotansiyelPozisyonlar(suankiKon).Where(kon => SatrancTahtasi.SinirlerinİcindeMi(kon)
                              && (satrancTahtasi.BosMu(kon) || satrancTahtasi[kon].Renkler != Renkler));
            }
            
            // ve bu potansiyeller sonrasinda artik hamlelerin yapilacak oldugu kisim.
            public override IEnumerable<HareketEtme> HamleleriYap(Konum suankiKon, SatrancTahtasi satrancTahtasi)
            {
            return HareketEtmePozisyonlar(suankiKon, satrancTahtasi).Select(yeniKon => new NormalHamleler(suankiKon, yeniKon));
            }
           
    }
}

