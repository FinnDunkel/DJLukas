using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppToolBar.Pages
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();

        }         
        
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedPicture = e.OriginalSource as NavigationSeite2;

            NavigationService.Navigate(ClickedPicture.NavUri); 
        }
    }
    }


