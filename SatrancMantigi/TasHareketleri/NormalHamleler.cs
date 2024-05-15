using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatrancMantigi
{
    public class NormalHamleler : HareketEtme
    {
        // oncelikle yapilan hamlenin normal bir hamle oldugunu sectik
        public override HamleCesitleri hamleCesitleri => HamleCesitleri.NormalHamleler;
        // konumunun degistirilemeyecegi bir prop ozellik tanimladik sadece get edilebilir
        public override Konum suankiKonumdan { get; } 

        public override Konum yeniKonuma { get; }     

        // normal hamlelerin suanki ve yeni konumu parametre alan bir yapici kurucu metodu
        public NormalHamleler(Konum suankiKon, Konum yeniKon)
        {
            suankiKonumdan = suankiKon;
            yeniKonuma = yeniKon;
        }


        // normal hareketler burada gerceklesiyor
        public override void Calistir(SatrancTahtasi satrancTahtasi) 
        {
            SatrancTaslar satrancTaslar = satrancTahtasi[suankiKonumdan];  // ilk önce suan bulundugu konumdaki tası buluyor 
            satrancTahtasi[yeniKonuma] = satrancTaslar;                   // secilen bulunan tas yeni konuma tasınıyor
            satrancTahtasi[suankiKonumdan] = null;                       // mevcut konuma artık o tas geliniyor tas varsa siliniyor yeme islemi gerceklesmis oluyor
            satrancTaslar.tasHareketi = true;                           // burası da hamlelerin ilk durumlarının tutulması oluyor kısaca (tasHareketi
                                                                       // rok atmadan önce sah'in veya kalenin hareket edip etmedigini kontrol etmek icin
        }
    }
}
