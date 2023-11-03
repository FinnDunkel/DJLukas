using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppToolBar.Pages
{
    public partial class Transportkosten : Page
    {
        public Transportkosten()
        {
            InitializeComponent();
        }

        private void btn_weiter_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB0C4DE");

            System.Windows.Media.SolidColorBrush brush = new System.Windows.Media.SolidColorBrush(color);

            btn_weiter.Background = brush;
            btn_weiter.Cursor = Cursors.Hand;

            NavigationService.Navigate(new Uri("/Pages/Montagekosten.xaml", UriKind.Relative));
        }
    }
}
