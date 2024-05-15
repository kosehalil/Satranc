using System;

namespace SatrancMantigi
{
    public class SatrancTahtasi
    {
        // Burada o sorgusu yapilan (8 x 8)'lik dizinin tanimlamasi yapiliyor.
        // 8 satir 8 sütundan olusuyor 0-7 degerleri arasindan..
        private readonly SatrancTaslar[,] satranctaslar = new SatrancTaslar[8, 8];

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

    }
}