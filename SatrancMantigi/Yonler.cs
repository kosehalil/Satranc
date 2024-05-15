namespace SatrancMantigi
{
    public class Yonler
    {
        // Yönleri tanimladik x-y kordinat düzlemindeki birim cember gibi düsünebilirsiniz.
        public readonly static Yonler Kuzey = new Yonler(-1, 0);
        public readonly static Yonler Guney = new Yonler(1, 0);
        public readonly static Yonler Dogu = new Yonler(0, 1);
        public readonly static Yonler Bati = new Yonler(0, -1);

        // Yönleri tanimladik yine burada fil,vezir,sah gibi capraz gidebilen taslar icin.
        public readonly static Yonler KuzeyDogu = Kuzey + Dogu;
        public readonly static Yonler KuzeyBati = Kuzey + Bati;
        public readonly static Yonler GuneyDogu = Guney + Dogu;
        public readonly static Yonler GuneyBati = Guney + Bati;


        public int SatırKısımı { get;  }                   

        public int SutunKısımı { get;  } 


        public Yonler(int satırKısımı, int sutunKısımı)
        {
            SatırKısımı = satırKısımı;                 // yonler nesnesi olusturuldugunda satırKısmı 
            SutunKısımı = sutunKısımı;                 // ve sutunKısmı kullanilacak 
        }

        // Burada Yonler nesnesinin toplanmasi saglanir kuzeye, güneye, doguya, batiya hareketi saglatir.
        public static Yonler operator +(Yonler yon1, Yonler yon2)
        {
            return new Yonler(yon1.SatırKısımı + yon2.SatırKısımı, yon1.SutunKısımı + yon2.SutunKısımı);
        }

        // hareket icin sabit bir degerle tasin hamlelerini hesaplamak icin carpim yapilir.
        public static Yonler operator *(int carpan , Yonler yon)
        {
            return new Yonler(carpan * yon.SatırKısımı, carpan * yon.SutunKısımı);
        }
    }
}
