using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SatrancArayuzu
{
    /// <summary>
    /// Interaction logic for SatrancDuraklatmaMenusu.xaml
    /// </summary>
    public partial class SatrancDuraklatmaMenusu : UserControl
    {
        public event Action<MenuAyarlari> YapilanSecim; 
        public SatrancDuraklatmaMenusu()
        {
            InitializeComponent();
        }

        private void DevamEtme(object sender, RoutedEventArgs e)
        {
            YapilanSecim?.Invoke(MenuAyarlari.DevamEt);
        }

        private void TekrarOynama(object sender, RoutedEventArgs e) 
        {
            YapilanSecim?.Invoke(MenuAyarlari.TekrarOyna);
        }
    }
}
