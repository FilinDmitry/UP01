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
            ReadingList rl = book.r_list;
            rl.BookStatus.ID = Core.Context.BookStatus.First(bs => bs.Name == book.status).ID;
            Update_lists();


        }
        private void Update_lists()
        {
            lst_book_dust = lst_book.Where(b => b.Status == "Заброшенно").ToList();
            lst_book_planed = lst_book.Where(b => b.Status == "В планах").ToList();
            lst_book_reading = lst_book.Where(b => b.Status == "Читаю").ToList();
            lst_book_readed = lst_book.Where(b => b.Status == "Прочитано").ToList();
        }
    }
}
