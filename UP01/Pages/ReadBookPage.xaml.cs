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
using UP01.ViewModels;

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReadBookPage.xaml
    /// </summary>
    public partial class ReadBookPage : Page
    {
        public ReadBookPage(BookViewModel book)
        {
            InitializeComponent();
            TB_Content.Text = book.book.Content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
