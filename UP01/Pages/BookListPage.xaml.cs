using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        int count = 0;
        ListBox LB_ReadingList;
        List<BookInListViewModel> lst_book;
        List<BookInListViewModel> lst_book_readed;
        List<BookInListViewModel> lst_book_planed;
        List<BookInListViewModel> lst_book_reading;
        List<BookInListViewModel> lst_book_dust;
        public BookListPage()
        {
            
            InitializeComponent();
            Genre.ItemsSource = Core.Context.Genre.Select(i => i.Name).ToList();
            lst_book = Core.Context.Books.Select(
                b => new BookInListViewModel()
                {
                    book = b
                }
                ).ToList();

            Update_lists();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            TabControl tab = sender as TabControl;
            TabItem item = tab.SelectedItem as TabItem;
            List<BookInListViewModel> filter = new List<BookInListViewModel>();
            if (LB_ReadingList == null)
            {
                return;
            }
            switch (item.Tag.ToString())
            {
                case ("Все"):
                    filter = lst_book;
                    break;
                case ("Прочитано"):
                    filter = lst_book_readed;
                    break;
                case ("Читаю"):
                    filter = lst_book_reading;
                    break;
                case ("В планах"):
                    filter = lst_book_planed; 
                    break;
                case ("Заброшено"):
                    filter = lst_book_dust;
                    break;
                
            }
            Search.Text = string.Empty;
            Genre.SelectedIndex = -1;
            Sort.SelectedIndex = 0;
            LB_ReadingList.ItemsSource = filter;
        }
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lbx = FindVisualChildByName<ListBox>(this.ListTabControl, "LB_ReadingList");
            LB_ReadingList = lbx;
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

        private void LB_ReadingList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BookInListViewModel bvm = LB_ReadingList.SelectedItem as BookInListViewModel;
            if (bvm != null)
            {
                NavigationService.Navigate(new BookPage(bvm));
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            BookInListViewModel book = cb.DataContext as BookInListViewModel;
            if (book == null)
            {
                return;

            }
            if (cb.SelectedIndex == 0)
            {
                if (book.Status != "")
                {
                    ReadingList rla = book.r_list;
                    Core.Context.ReadingList.Remove(rla);
                    Core.Context.SaveChanges();
                    Update_lists();
                    return;
                }
                
                return;
            }
            
            if (book.Status == "")
            {
                
                ReadingList readingList = new ReadingList()
                {
                    BookID = book.book.ID,
                    UserID = Auth.cur_user.ID,
                    StatusID = Core.Context.BookStatus.First(bs => bs.Name == book.status).ID
                };
                Core.Context.ReadingList.Add(readingList);
                Core.Context.SaveChanges();
                Update_lists();
                return;
            }
            


        }
        private void Update_lists()
        {
            lst_book_dust = lst_book.Where(b => b.Status == "Заброшенно").ToList();
            lst_book_planed = lst_book.Where(b => b.Status == "В планах").ToList();
            lst_book_reading = lst_book.Where(b => b.Status == "Читаю").ToList();
            lst_book_readed = lst_book.Where(b => b.Status == "Прочитано").ToList();
            TabItem item = ListTabControl.SelectedItem as TabItem;
            List<BookInListViewModel> filter = new List<BookInListViewModel>();
            if (item == null)
            { return; }
            switch (item.Tag.ToString())
            {
                case ("Все"):
                    filter = lst_book;
                    break;
                case ("Прочитано"):
                    filter = lst_book_readed;
                    break;
                case ("Читаю"):
                    filter = lst_book_reading;
                    break;
                case ("В планах"):
                    filter = lst_book_planed;
                    break;
                case ("Заброшено"):
                    filter = lst_book_dust;
                    break;

            }
            LB_ReadingList.ItemsSource = FilterList(filter);
        }
        private List<BookInListViewModel> FilterList(List<BookInListViewModel> filter)
        {
            List<BookInListViewModel> list = filter;
            
            if (Genre.SelectedItem != null)
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
            
            return list;
        }

        private void Genre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_lists();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_lists();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            BookInListViewModel book = mi.DataContext as BookInListViewModel;
            if (book == null)
            {
                return;

            }
            if (mi.Header.ToString() == "Без категории")
            {
                if (book.Status != "")
                {
                    ReadingList rla = book.r_list;
                    Core.Context.ReadingList.Remove(rla);
                    Core.Context.SaveChanges();
                    Update_lists();
                    return;
                }
                return;
            }

            if (book.Status == "")
            { 
                ReadingList readingList = new ReadingList()
                {
                    BookID = book.book.ID,
                    UserID = Auth.cur_user.ID
                };
                switch (mi.Header)
                {

                    case "Прочитано":
                        readingList.StatusID = 4;
                        break;
                    case "Читаю":
                        readingList.StatusID = 3;
                        break;
                    case "Заброшено":
                        readingList.StatusID = 1;
                        break;
                    case "В планах":
                        readingList.StatusID = 2;
                        break;
                }
                Core.Context.ReadingList.Add(readingList);
                Core.Context.SaveChanges();
                MessageBox.Show("Успеешно перемещено");
                Update_lists();
                return;
            }
            else
            {
                ReadingList rla = book.r_list;
                switch (mi.Header)
                {
                    
                    case "Прочитано":
                        rla.StatusID = 4;
                        break;
                    case "Читаю":
                        rla.StatusID = 3;
                        break;
                    case "Заброшено":
                        rla.StatusID = 1;
                        break;
                    case "В планах":
                        rla.StatusID = 2;
                        break;
                }
                MessageBox.Show("Успеешно перемещено");
                Core.Context.SaveChanges();
                Update_lists();
                return;
            }
        }
    }
}
