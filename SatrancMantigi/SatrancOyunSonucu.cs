using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class SatrancOyunSonucu
    {
        public Oyuncu Kazanan { get; }

        public SatrancBitisSekilleri BitisSekilleri { get; } 

        public SatrancOyunSonucu(Oyuncu kazanan , SatrancBitisSekilleri bitisSekilleri)
        {
            Kazanan = kazanan;
            BitisSekilleri = bitisSekilleri;
        }

        public static SatrancOyunSonucu Galibiyet(Oyuncu kazanan)
        {
            return new SatrancOyunSonucu(kazanan, SatrancBitisSekilleri.SahMat);
        }

        public static SatrancOyunSonucu Berabere(SatrancBitisSekilleri bitisSekilleri) 
        {
            return new SatrancOyunSonucu(Oyuncu.hicbiri, bitisSekilleri);
        }

    }
}
