namespace SatrancMantigi
{
    // Baslangic icin 3 farklı oyuncu vardır beyaz, siyah veya hic
    public enum Oyuncu
    {
        hicbiri,
        beyaz,
        siyah
    }

    // Satrancta rakip ayrimi icin yapilan kisim 
    // Yani ben Beyaz isim Siyah rakibimdir gibi özellik tasir..
    public static class OyuncuRakibiBelirleme
    {
        public static Oyuncu Rakibi (this Oyuncu Oyuncu)
        {
            switch (Oyuncu)
            {
                case Oyuncu.beyaz:
                    return Oyuncu.siyah;

                case Oyuncu.siyah:
                    return Oyuncu.beyaz;
                default:

                    return Oyuncu.hicbiri;
            }
        }
    }
}
