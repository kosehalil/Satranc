using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SatrancMantigi;

namespace SatrancArayuzu
{
    public static class Resimlerr
    {
        private static readonly Dictionary<SatrancTaslarininCesitleri, ImageSource> Beyazlar = new()
        {
            {SatrancTaslarininCesitleri.Piyon, ResmiYukle("SatrancTaslari/WPawn.png") },
            {SatrancTaslarininCesitleri.At,ResmiYukle("SatrancTaslari/WKnight.png") },
            {SatrancTaslarininCesitleri.Fil,ResmiYukle("SatrancTaslari/WBishop.png") },
            {SatrancTaslarininCesitleri.Kale,ResmiYukle("SatrancTaslari/WRook.png") },
            {SatrancTaslarininCesitleri.Sah, ResmiYukle("SatrancTaslari/WKing.png") },
            {SatrancTaslarininCesitleri.Vezir, ResmiYukle("SatrancTaslari/WQueen.png") }

        };

        private static readonly Dictionary<SatrancTaslarininCesitleri, ImageSource> Siyahlar = new()
        {
            {SatrancTaslarininCesitleri.Piyon, ResmiYukle("SatrancTaslari/BPawn.png") },
            {SatrancTaslarininCesitleri.At,ResmiYukle("SatrancTaslari/BKnight.png") },
            {SatrancTaslarininCesitleri.Fil,ResmiYukle("SatrancTaslari/BBishop.png") },
            {SatrancTaslarininCesitleri.Kale,ResmiYukle("SatrancTaslari/BRook.png") },
            {SatrancTaslarininCesitleri.Sah, ResmiYukle("SatrancTaslari/BKing.png") },
            {SatrancTaslarininCesitleri.Vezir, ResmiYukle("SatrancTaslari/BQueen.png") }

        };

        private static ImageSource ResmiYukle(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        // beyaz - siyah taslar icin o tasların yukarıdaki pnglerini dondurur
        public static ImageSource GoruntuyuAl(Oyuncu Renk, SatrancTaslarininCesitleri cesit)
        {
            return Renk switch
            {
                Oyuncu.beyaz => Beyazlar[cesit],
                Oyuncu.siyah=> Siyahlar[cesit],
                _=> null
            };

        }

        // gelen tas nesnesinin türüne ve rengine bagli olarak ilgili resmi döndürür yoksa null 
        public static ImageSource GoruntuyuAl(SatrancTaslar taslar)
        {
            if (taslar == null)
            {
                return null;
            }

            return GoruntuyuAl(taslar.Renkler, taslar.Cesitler);
        }
    }
}
