namespace SatrancMantigi
{
    public class Konum
    {
        public int Satır { get; }
        public int Sutun { get; }

        public Konum(int satir, int sutun)
        {
            Satır = satir;
            Sutun = sutun;
        }

        // burada karenin renginin belirlenme işi yapılıyor.
        // eğer satır ve sütun numaralarının toplamının 2'ye bölümü
        // sıfıra(0) esitse yani cift sayi ise == KARE RENGİ BEYAZDIR.
        // sıfıra esit degilse yani tek sayi ise == KARE RENGİ SİYAHTIR.
        public Oyuncu KarelerinRengi()
        {
            if ((Satır + Sutun) % 2 == 0)
            {
                return Oyuncu.beyaz;
            }
            return Oyuncu.siyah;
        }

        // equals karsilastirma icin kullanilir burada da satir - sutun karsilastirilip
        // satir ve sutun esitse bu nesneler esit kabul edilir bu yuzden kullanildi
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Konum konum = (Konum)obj;
            return Satır == konum.Satır && Sutun == konum.Sutun;
        }


        // satir ve sutunun birlestirilmesi ile olusan ozeti yapiyor (hashcode)
        public override int GetHashCode()
        {
            return HashCode.Combine(Satır, Sutun);
        }

        // (referanceequals) nesnelerin(sag-sol) ayni yeri isaret edip etmedigini kontrol edip true-false döndürür 
        public static bool operator ==(Konum sol, Konum sag)
        {
            if (object.ReferenceEquals(sol, sag))
            {
                return true;
            }
            if (object.ReferenceEquals(sol, null) || object.ReferenceEquals(sag, null))
            {
                return false;
            }
            return sol.Equals(sag);
        }
        
        public static bool operator !=(Konum sol, Konum sag)
        {
           return !(sol == sag);
        }

        //Tahtanin sinirlari icerisinde mi yani (8 x 8) icerisinde mi diye kontrol ediliyor.
        public bool SinirlarinİcindeMi()
        {
            return Satır >= 0 && Satır < 8 && Sutun >= 0 && Sutun < 8;
        }
        // iki konum toplanıp yeni konum elde edilir 
        public static Konum operator +(Konum konum, Yonler yon)
        {
            return new Konum(konum.Satır + yon.SatırKısımı, konum.Sutun + yon.SutunKısımı);
        }

    }
}