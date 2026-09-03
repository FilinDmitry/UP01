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
            lst_book = Core.Context.Books.Where(i => !i.isFreeze).Select(
                b => new BookViewModel()
                {
                    book = b
                }
                ).ToList();
            InitializeComponent();
            LB_catalog.ItemsSource = lst_book;
            
            
            List<string> g = Core.Context.Genre.Select(i => i.Name).ToList();
            g.Insert(0, "Все жанры");
            Genre.ItemsSource = g;
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           FilterList();
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

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterList();
        }
        private void FilterList()
        {
            List<BookViewModel> list = lst_book;
            if (Genre.SelectedIndex != 0 && Genre.SelectedIndex != -1)
            {
                list = list.Where(b => b.is_genres(Genre.SelectedItem.ToString())).ToList();
            }

            switch (Sort.SelectedIndex)
            {
                case (0):
                    list = list.OrderByDescending(b => b.Avg_b).ToList();
                    break;
                case (1):
                    list = list.OrderBy(b => b.Name).ToList();
                    break;
            }
            list = list.Where(i => i.Name.Contains(Search.Text) || i.Author.Contains(Search.Text)).ToList();
            LB_catalog.ItemsSource = null;
            LB_catalog.ItemsSource = list;
        }
    }
}
