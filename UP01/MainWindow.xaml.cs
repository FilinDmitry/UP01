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
using UP01.Pages;

namespace UP01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListV_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgBtn.IsChecked == true)
            {
                tt1.Visibility = Visibility.Collapsed;
                tt2.Visibility = Visibility.Collapsed;
                tt3.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt1.Visibility = Visibility.Visible;
                tt2.Visibility = Visibility.Visible;
                tt3.Visibility = Visibility.Visible;
            }
        }

        private void ListV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            ListViewItem lvi =lv.SelectedItem as ListViewItem;
            switch (lvi.Name)
            {
                case ("El1"):
                    MainFrame.NavigationService.Navigate(new AuthorizationPage());
                    break;
            }
        }
    }
}
