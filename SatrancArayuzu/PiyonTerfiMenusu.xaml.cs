using SatrancMantigi;
using System.Windows.Controls;
using System.Windows.Input;

namespace SatrancArayuzu
{
    /// <summary>
    /// Interaction logic for PiyonTerfiMenusu.xaml
    /// </summary>
    public partial class PiyonTerfiMenusu : UserControl
    {
        public event Action<SatrancTaslarininCesitleri> YapilanSecim; 
        public PiyonTerfiMenusu(Oyuncu oyuncu)
        {
            InitializeComponent();

            VezirResim.Source = Resimlerr.GoruntuyuAl(oyuncu, SatrancTaslarininCesitleri.Vezir);
            KaleResim.Source = Resimlerr.GoruntuyuAl(oyuncu, SatrancTaslarininCesitleri.Kale);
            FilResim.Source = Resimlerr.GoruntuyuAl(oyuncu, SatrancTaslarininCesitleri.Fil);
            AtResim.Source = Resimlerr.GoruntuyuAl(oyuncu, SatrancTaslarininCesitleri.At);
        }

        private void VezirResim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            YapilanSecim?.Invoke(SatrancTaslarininCesitleri.Vezir);
        }

        private void KaleResim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            YapilanSecim?.Invoke(SatrancTaslarininCesitleri.Kale);
        }

        private void FilResim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            YapilanSecim?.Invoke(SatrancTaslarininCesitleri.Fil);
        }

        private void AtResim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            YapilanSecim?.Invoke(SatrancTaslarininCesitleri.At);
        }
    }
}
