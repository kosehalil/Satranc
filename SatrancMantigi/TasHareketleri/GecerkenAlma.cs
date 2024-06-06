namespace SatrancMantigi
{
    public class GecerkenAlma : HareketEtme
    {
        public override HamleCesitleri hamleCesitleri => HamleCesitleri.GecerkenAlma;

        public override Konum suankiKonumdan { get; }

        public override Konum yeniKonuma { get; }

        private readonly Konum tasYenilenKonum;

        public GecerkenAlma(Konum suankiKon, Konum yeniKon)
        {
            suankiKonumdan = suankiKon;
            yeniKonuma = yeniKon;
            tasYenilenKonum = new Konum(suankiKon.Satır, yeniKon.Sutun);          
        }

        public override bool Calistir(SatrancTahtasi satrancTahtasi)
        {
            new NormalHamleler(suankiKonumdan, yeniKonuma).Calistir(satrancTahtasi);
            satrancTahtasi[tasYenilenKonum] = null;

            return true;
        }







    }
}
