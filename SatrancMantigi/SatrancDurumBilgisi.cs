using System.Text;

namespace SatrancMantigi
{
    public class SatrancDurumBilgisi
    {
        private readonly StringBuilder sb = new StringBuilder();    // StringBuilder degistirilen bir satranc dizisi olusturup kullanmak icin olusturduk

        public SatrancDurumBilgisi(Oyuncu suankiOyuncu, SatrancTahtasi satrancTahtasi)
        {
            SatrancTasYerlestirme(satrancTahtasi);
            sb.Append(' ');                                         // Append bu diziye ekleme yapmak icin kullanilir
            SuankiOyuncuyuEkleme(suankiOyuncu);
            sb.Append(' ');
            RookuEkleme(satrancTahtasi);
            sb.Append(' ');
            GecerkenAlmayiEkleme(satrancTahtasi, suankiOyuncu);

        }

        public override string ToString() 
        {
            return sb.ToString();
        } 

        private static char SatrancTasiKarakteri(SatrancTaslar satrancTaslar)
        {
            char c = satrancTaslar.Cesitler switch
            {
                SatrancTaslarininCesitleri.Piyon => 'p',
                SatrancTaslarininCesitleri.At => 'a',
                SatrancTaslarininCesitleri.Kale => 'k',
                SatrancTaslarininCesitleri.Fil => 'f',
                SatrancTaslarininCesitleri.Vezir => 'v',
                SatrancTaslarininCesitleri.Sah => 's',
                _ => ' '
            };

            if(satrancTaslar.Renkler == Oyuncu.beyaz)
            {
                return char.ToUpper(c);
            }
            return c;
        }

        private void SatrancSatirVerileriniEkleme(SatrancTahtasi satrancTahtasi, int satir) 
        {


            int bos = 0;

            for(int c = 0; c < 8; c++)
            {
                if (satrancTahtasi[satir, c] == null)
                {
                    bos++;
                    continue;
                }

                if (bos > 0)
                {
                    sb.Append(bos);
                    bos = 0;
                }

                sb.Append(SatrancTasiKarakteri(satrancTahtasi[satir, c]));

            }

            if (bos > 0)
            {
                sb.Append(bos);
            }

        }


        private void SatrancTasYerlestirme(SatrancTahtasi satrancTahtasi)
        {
            for(int r =0; r<8; r++)
            {
                if(r != 0)
                {
                    sb.Append('/');
                }
                SatrancSatirVerileriniEkleme(satrancTahtasi, r);
            }
        }

        private void SuankiOyuncuyuEkleme(Oyuncu suankiOyuncu)
        {
            if(suankiOyuncu == Oyuncu.beyaz)
            {
                sb.Append('b');
            }
            else 
            {
                sb.Append('s');
            }
        }


        private void RookuEkleme(SatrancTahtasi satrancTahtasi)
        {
            bool rookBeySahKanadina = satrancTahtasi.SahKanadinaRookAtilabilirMi(Oyuncu.beyaz);
            bool rookBeyVezKanadina = satrancTahtasi.VezirKanadinaRookAtilabilirMi(Oyuncu.beyaz);
            bool rookSiySahKanadina = satrancTahtasi.SahKanadinaRookAtilabilirMi(Oyuncu.siyah);
            bool rookSiyVezKanadina = satrancTahtasi.VezirKanadinaRookAtilabilirMi(Oyuncu.siyah);

            if(!(rookBeySahKanadina || rookBeyVezKanadina || rookSiySahKanadina || rookSiyVezKanadina))
            {
                sb.Append('-');
                return;
            }

            if (rookBeySahKanadina)
            {
                sb.Append('S');
            }
            if (rookBeyVezKanadina)
            {
                sb.Append('V');
            }
            if (rookSiySahKanadina)
            {
                sb.Append('s');
            }
            if (rookSiyVezKanadina)
            {
                sb.Append('v');
            }
        }


        private void GecerkenAlmayiEkleme(SatrancTahtasi satrancTahtasi, Oyuncu suankiOyuncu)
        {

            if (!satrancTahtasi.PiyonGecerkenAlmaYapabilirMi(suankiOyuncu))
            {
                sb.Append('-');
                return;
            }

            Konum kon = satrancTahtasi.PiyonGecisKonumunuGoster(suankiOyuncu.Rakibi());
            char sutunBilgisi = (char)('a' + kon.Sutun);
            int satirBilgisi = 8 - kon.Satır;
            sb.Append(sutunBilgisi);
            sb.Append(satirBilgisi);
        }



    }
}
