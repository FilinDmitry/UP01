using UP01.Models;
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
    /// Логика взаимодействия для BookCatalog.xaml
    /// </summary>
    public partial class BookCatalog : Page
    {
        List<BookViewModel> lst_book;
        public BookCatalog()
        {
            lst_book = Core.Context.Books.Select(
                b => new BookViewModel()
                {
                    book = b
                }
                ).ToList();
            InitializeComponent();
            LB_catalog.ItemsSource = lst_book;
            
            
            Genre.ItemsSource = Core.Context.Genre.Select(i => i.Name).ToList();
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<BookViewModel> list = lst_book;
            if (Genre.SelectedItem == null)
            {
                list = list.Where(b => b.is_genres(Genre.SelectedItem.ToString())).ToList();
            }

            switch (Sort.SelectedItem)
            {
                case ("По рейтингу"):
                    list = list.OrderByDescending(b => b.Avg).ToList();
                    break;
                case ("По имени"):
                    list = list.OrderBy(b => b.Name).ToList();
                    break;
            }
            LB_catalog.ItemsSource = list;
        }

        private void LB_catalog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LB_catalog.SelectedItem != null)
            {
                NavigationService.Navigate(new BookPage(LB_catalog.SelectedItem as BookViewModel));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            BookViewModel bvm = btn.DataContext as BookViewModel;
            NavigationService.Navigate(new BookPage(bvm));
        }
    }
}
