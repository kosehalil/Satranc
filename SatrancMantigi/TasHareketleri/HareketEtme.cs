using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    // HareketEtme adinda bir soyut sinif olusturuyoruz.
    // Base class olarak kullanacagiz türemis sınıflarda kullanmak ve o sınıflara sablon olsun diye 
    // abstrack olarak tanimlamalari yaptik.
    public abstract class HareketEtme        
    {
        public abstract HamleCesitleri hamleCesitleri { get; }

        public abstract Konum suankiKonumdan { get; }      

        public abstract Konum yeniKonuma { get; }  

        public abstract void Calistir(SatrancTahtasi satrancTahtasi);
    }
}
