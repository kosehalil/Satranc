namespace SatrancMantigi
{
    public class PiyonTerfisi : HareketEtme
    {
        public override HamleCesitleri hamleCesitleri => HamleCesitleri.PiyonTerfisi; 

        public override Konum suankiKonumdan { get; }

        public override Konum yeniKonuma { get; }

        private readonly SatrancTaslarininCesitleri yeniCesit;

        public PiyonTerfisi(Konum suankiKon, Konum yeniKon, SatrancTaslarininCesitleri yeniCesit)
        {
            suankiKonumdan = suankiKon;
            yeniKonuma = yeniKon;
            this.yeniCesit = yeniCesit;
        }

        private SatrancTaslar TerfiEdilenTasiOlusturmak(Oyuncu renkler)
        {
            return yeniCesit switch
            {
                SatrancTaslarininCesitleri.At => new At(renkler),
                SatrancTaslarininCesitleri.Fil => new Fil(renkler),
                SatrancTaslarininCesitleri.Kale => new Kale(renkler),
                _ => new Vezir(renkler)

            }; 
        }

        // BURAYA TEKRAR DÖN

        public override bool Calistir(SatrancTahtasi satrancTahtasi)
        {
            SatrancTaslar piyon = satrancTahtasi[suankiKonumdan];
            satrancTahtasi[suankiKonumdan] = null;

            SatrancTaslar terfiEdenTas = TerfiEdilenTasiOlusturmak(piyon.Renkler);
            terfiEdenTas.tasHareketi = true;
            satrancTahtasi[yeniKonuma] = terfiEdenTas;

            return true;
        }


    }
}
