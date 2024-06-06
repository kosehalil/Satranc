using System;

namespace SatrancMantigi
{
    public class SatrancTahtasi
    {
        // Burada o sorgusu yapilan (8 x 8)'lik dizinin tanimlamasi yapiliyor.
        // 8 satir 8 sütundan olusuyor 0-7 degerleri arasindan..
        private readonly SatrancTaslar[,] satranctaslar = new SatrancTaslar[8, 8];

        private readonly Dictionary<Oyuncu, Konum> piyonGecisPozisyonlari = new Dictionary<Oyuncu, Konum>
        {
            {Oyuncu.beyaz, null },
            {Oyuncu.siyah, null }
        };

        // Burada bir get ve set edebilecegimiz bir property yani özellik tanimliyoruz
        public SatrancTaslar this[int satir, int sutun]   
        {
            get { return satranctaslar[satir, sutun]; }      // dizi indeksini satir ve sütunu döndürür.
            set { satranctaslar[satir, sutun] = value; }     // dizi indeksini satir ve sütunu value degerine atar
        }

        // Burada da bir get ve set edebilecegimiz özellik tanimlaniyor.
        public SatrancTaslar this[Konum kon]           
        {
            get { return this[kon.Satır, kon.Sutun]; }      // kısaca tasların konumlarını get ve set edip
            set { this[kon.Satır, kon.Sutun] = value; }     // okuyup degistirebilmemize yarar
        }

        public Konum PiyonGecisKonumunuGoster(Oyuncu oyuncu) 
        {
            return piyonGecisPozisyonlari[oyuncu];
        }

        public void PiyonGecisKonumunuBelirle(Oyuncu oyuncu, Konum kon) 
        {
            piyonGecisPozisyonlari[oyuncu] = kon;
        }

        // Satranc Tahtasinin baslangic durumu olustuluyor ilk konumlar yani.
        public static SatrancTahtasi TahtaBaslangici()
        {
            SatrancTahtasi satranctahtasi = new SatrancTahtasi();
            satranctahtasi.TaslarinBaslangicKonumlari();
            return satranctahtasi;
        }

        /* burada siyah ve beyaz taslar icin dizileri konum olarak kullanarak
           siyah ve beyaz taslari dizilerin konumlarina yerlestiriliyor.   */
        // dizilerin indeksleri 0 dan baslar o yüzden 0-7 arasinda yapiliyor.
        private void TaslarinBaslangicKonumlari()
        {
            this[0, 0] = new Kale(Oyuncu.siyah);
            this[0, 1] = new At(Oyuncu.siyah);
            this[0, 2] = new Fil(Oyuncu.siyah);
            this[0, 3] = new Vezir(Oyuncu.siyah);
            this[0, 4] = new Sah(Oyuncu.siyah);
            this[0, 5] = new Fil(Oyuncu.siyah);
            this[0, 6] = new At(Oyuncu.siyah);
            this[0, 7] = new Kale(Oyuncu.siyah);

            this[7, 0] = new Kale(Oyuncu.beyaz);
            this[7, 1] = new At(Oyuncu.beyaz);
            this[7, 2] = new Fil(Oyuncu.beyaz);
            this[7, 3] = new Vezir(Oyuncu.beyaz);
            this[7, 4] = new Sah(Oyuncu.beyaz);
            this[7, 5] = new Fil(Oyuncu.beyaz);
            this[7, 6] = new At(Oyuncu.beyaz);
            this[7, 7] = new Kale(Oyuncu.beyaz);

            for (int piyonKonumu = 0; piyonKonumu < 8; piyonKonumu++)
            {
                this[1, piyonKonumu] = new Piyon(Oyuncu.siyah);
                this[6, piyonKonumu] = new Piyon(Oyuncu.beyaz);
            }
        }

        // Konumun ( 8 x 8 ) lik sınır icerisinde mi kontrolü yapiliyor.
        public static bool SinirlerinİcindeMi(Konum kon)
        {
            return kon.Satır >= 0 && kon.Satır < 8 && kon.Sutun >= 0 && kon.Sutun < 8;
        }

        // Konumun bos mu dolu mu oldugunu kontrol ediliyor kurallari buna göre tanimlayicaz.
        public bool BosMu(Konum kon)
        {
            return this[kon] == null;
        }

        public IEnumerable<Konum> TasPozisyonlari()
        {
            for (int r = 0; r < 8; r++)
            {
                for(int c = 0; c < 8; c++)
                {
                    Konum kon = new Konum(r, c);

                    if (!BosMu(kon))
                    {
                        yield return kon;
                    }
                }
            }
        }

        public IEnumerable<Konum> TasPozisyonlarinaBagliOlarak(Oyuncu oyuncu)
        {
            return TasPozisyonlari().Where(kon => this[kon].Renkler == oyuncu);
        }

        public bool SahCekiliyorMu (Oyuncu oyuncu)
        {
            return TasPozisyonlarinaBagliOlarak(oyuncu.Rakibi()).Any(kon =>
            {
                SatrancTaslar satrancTaslar = this[kon];
                return satrancTaslar.SahinYoluEngellenebilirMi(kon, this);
            });
        }

        public SatrancTahtasi Kopyasi()
        {
            SatrancTahtasi kopyasi = new SatrancTahtasi();

            foreach(Konum kon in TasPozisyonlari())
            {
                kopyasi[kon] = this[kon].Kopyasi();
            }

            return kopyasi;
        }

        public SatrancTaslarinSayimi TasSayimi()
        {
            SatrancTaslarinSayimi satrancTaslarininSayimi = new SatrancTaslarinSayimi();

            foreach(Konum kon in TasPozisyonlari())
            {
                SatrancTaslar satrancTaslar = this[kon];
                satrancTaslarininSayimi.Artis(satrancTaslar.Renkler, satrancTaslar.Cesitler);
            }
            return satrancTaslarininSayimi;
        }

        public bool YetersizTasKalmasi()
        {
            SatrancTaslarinSayimi satrancTaslarinSayimi = TasSayimi();
            return TahtadaSadeceSahvsSahKaldiginda(satrancTaslarinSayimi) || TahtadaSahFilvsSahKaldiginda(satrancTaslarinSayimi) ||
                   TahtadaSahAtvsSahKaldiginda(satrancTaslarinSayimi) || TahtadaSahFilvsSahFilKaldiginda(satrancTaslarinSayimi);
                   
        }
        
        private static bool TahtadaSadeceSahvsSahKaldiginda(SatrancTaslarinSayimi satrancTaslarinSayimi)
        {
            return satrancTaslarinSayimi.toplamTaslarinSayimi == 2;
        }

        private static bool TahtadaSahFilvsSahKaldiginda(SatrancTaslarinSayimi satrancTaslarinSayimi)
        {
            return satrancTaslarinSayimi.toplamTaslarinSayimi == 3 && (satrancTaslarinSayimi.Beyaz(SatrancTaslarininCesitleri.Fil) == 1 || satrancTaslarinSayimi.Siyah(SatrancTaslarininCesitleri.Fil) == 1);
        }

        private static bool TahtadaSahAtvsSahKaldiginda(SatrancTaslarinSayimi satrancTaslarinSayimi)
        {
            return satrancTaslarinSayimi.toplamTaslarinSayimi == 3 && (satrancTaslarinSayimi.Beyaz(SatrancTaslarininCesitleri.At) == 1 || satrancTaslarinSayimi.Siyah(SatrancTaslarininCesitleri.At) == 1);
        }

        private bool TahtadaSahFilvsSahFilKaldiginda(SatrancTaslarinSayimi satrancTaslarinSayimi)
        {
            if(satrancTaslarinSayimi.toplamTaslarinSayimi != 4)
            {
                return false;
            }
            if(satrancTaslarinSayimi.Beyaz(SatrancTaslarininCesitleri.Fil) !=1 || satrancTaslarinSayimi.Siyah(SatrancTaslarininCesitleri.Fil) != 1)
            {
                return false;
            }

            Konum beyazFilPozisyonu = SecilenTasNedir(Oyuncu.beyaz, SatrancTaslarininCesitleri.Fil);
            Konum siyahFilPozisyonu = SecilenTasNedir(Oyuncu.siyah, SatrancTaslarininCesitleri.Fil);

            return beyazFilPozisyonu.KarelerinRengi() == siyahFilPozisyonu.KarelerinRengi();
        }

        private Konum SecilenTasNedir(Oyuncu renkler, SatrancTaslarininCesitleri cesit)
        {
            return TasPozisyonlarinaBagliOlarak(renkler).First(kon => this[kon].Cesitler == cesit);
        }

        private bool SahVeKaleHareketEttiMi(Konum SahPozisyonu, Konum KalePozisyonu)
        {
            if(BosMu(SahPozisyonu) || BosMu(KalePozisyonu))
            {
                return false; 
            }

            SatrancTaslar sah = this[SahPozisyonu];
            SatrancTaslar kale = this[KalePozisyonu];

            return sah.Cesitler == SatrancTaslarininCesitleri.Sah && kale.Cesitler == SatrancTaslarininCesitleri.Kale &&
                   !sah.tasHareketi && !kale.tasHareketi;

        }

        public bool SahKanadinaRookAtilabilirMi(Oyuncu oyuncu) 
        {
            return oyuncu switch
            {
                Oyuncu.beyaz => SahVeKaleHareketEttiMi(new Konum(7, 4), new Konum(7, 7)),
                Oyuncu.siyah => SahVeKaleHareketEttiMi(new Konum(0, 4), new Konum(0, 7)),
                _ => false
            };
        } 

        public bool VezirKanadinaRookAtilabilirMi(Oyuncu oyuncu)
        {
            return oyuncu switch
            {
                Oyuncu.beyaz => SahVeKaleHareketEttiMi(new Konum(7, 4), new Konum(7, 0)),
                Oyuncu.siyah => SahVeKaleHareketEttiMi(new Konum(0, 4), new Konum(0, 0)),
                _ => false
            };
        }
         
        private bool KonumdaPiyonVarMi(Oyuncu oyuncu , Konum[] piyonPozisyonlari , Konum skipPos)
        {
            foreach(Konum kon in piyonPozisyonlari.Where(SinirlerinİcindeMi))
            {
                SatrancTaslar satrancTaslar = this[kon];
                if(satrancTaslar == null || satrancTaslar.Renkler != oyuncu || satrancTaslar.Cesitler != SatrancTaslarininCesitleri.Piyon)
                {
                    continue;
                }
                GecerkenAlma hareketEtme = new GecerkenAlma(kon, skipPos);
                if (hareketEtme.HamlelerGecerliMi(this))
                {
                    return true;
                }
            }

            return false;
        }

        public bool PiyonGecerkenAlmaYapabilirMi(Oyuncu oyuncu) 
        {
            Konum skipPos = PiyonGecisKonumunuGoster(oyuncu.Rakibi());

            if(skipPos == null)
            {
                return false;
            }

            Konum[] piyonPozisyonlari = oyuncu switch
            {
                Oyuncu.beyaz => new Konum[] { skipPos + Yonler.GuneyBati, skipPos + Yonler.GuneyDogu },
                Oyuncu.siyah => new Konum[] { skipPos + Yonler.KuzeyBati, skipPos + Yonler.KuzeyDogu },
                _ => Array.Empty<Konum>()
            };

            return KonumdaPiyonVarMi(oyuncu, piyonPozisyonlari, skipPos);
        }

    }
}