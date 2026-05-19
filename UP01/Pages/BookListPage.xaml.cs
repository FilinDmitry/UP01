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
using UP01.Models;
using UP01.ViewModels;

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookListPage.xaml
    /// </summary>
    public partial class BookListPage : Page
    {
        ListBox LB_ReadingList;
        List<BookInListViewModel> lst_book;
        List<BookInListViewModel> lst_book_readed;
        List<BookInListViewModel> lst_book_planed;
        List<BookInListViewModel> lst_book_reading;
        List<BookInListViewModel> lst_book_dust;
        public BookListPage()
        {
            
            InitializeComponent();
            lst_book = Core.Context.Books.Select(
                b => new BookInListViewModel()
                {
                    book = b
                }
                ).ToList();
           
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tab = sender as TabControl;
            TabItem item = tab.SelectedItem as TabItem;
            switch (item.Tag.ToString())
            {
                case ("Все"):
                    break;
                case ("Прочитано"):
                    break;
                case ("Читаю"):
                    break;
                case ("В планах"):
                    break;
                case ("Заброшено"):
                    break;
            }
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lbx = FindVisualChildByName<ListBox>(this.ListTabControl, "LB_ReadingList");
            lbx.ItemsSource = lst_book;
        }

        private T FindVisualChildByName<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            T child = default(T);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var ch = VisualTreeHelper.GetChild(parent, i);
                child = ch as T;
                if (child != null && child.Name == name)
                    break;
                else
                    child = FindVisualChildByName<T>(ch, name);

                if (child != null) break;
            }
            return child;
        }

        // Использование

    }
}
