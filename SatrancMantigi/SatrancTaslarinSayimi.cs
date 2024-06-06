namespace SatrancMantigi
{
    public class SatrancTaslarinSayimi
    {
        // beyaz ve siyah taslarin sayisini kontrol etmek icin tanimlama
        private readonly Dictionary<SatrancTaslarininCesitleri, int> beyazTaslarinSayimi = new();
        private readonly Dictionary<SatrancTaslarininCesitleri, int> siyahTaslarinSayimi = new();

        public int toplamTaslarinSayimi { get; private set; }

        // Dictionary icin her tas türü icin baslangıc degerinin 0 olarak belirlenmesi baslangic degerinin olmasi icin
        public SatrancTaslarinSayimi()
        {
            foreach(SatrancTaslarininCesitleri cesit in Enum.GetValues(typeof(SatrancTaslarininCesitleri)))
            {
                beyazTaslarinSayimi[cesit] = 0;
                siyahTaslarinSayimi[cesit] = 0;
            }
        }

        public void Artis(Oyuncu renkler, SatrancTaslarininCesitleri cesit)
        {
            if(renkler == Oyuncu.beyaz)
            {
                beyazTaslarinSayimi[cesit]++;
            }
            else if(renkler== Oyuncu.siyah)
            {
                siyahTaslarinSayimi[cesit]++;
            }

            toplamTaslarinSayimi++;
        }

        public int Beyaz(SatrancTaslarininCesitleri cesit)
        {
            return beyazTaslarinSayimi[cesit];
        }

        public int Siyah(SatrancTaslarininCesitleri cesit)
        {
            return siyahTaslarinSayimi[cesit];
        }


    }
}
