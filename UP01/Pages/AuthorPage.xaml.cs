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
using UP01.Windows;
using UP01.ViewModels;
using UP01.Models;

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        public AuthorPage()
        {
            
            InitializeComponent();
            lst_books.ItemsSource = Auth.cur_user.Books.Where(b => !b.isFreeze).Select(
                b => new BookViewModel()
                {
                    book = b
                }
                ).ToList();
            lst_freeze.ItemsSource = Auth.cur_user.Books.Where(b => b.isFreeze).Select(
                b => new BookViewModel()
                {
                    book = b,
                }
                ).ToList();
        }

        private void Unfreeze_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            BookViewModel bvm = btn.DataContext as BookViewModel;
            ApplicationWindow window = new ApplicationWindow(Auth.cur_user.ID, bvm.book.ID);
            window.Show();
        }

        private void Edit_book_click(object sender, RoutedEventArgs e)
        {
            if (Auth.cur_user.isFreeze)
            {
                MessageBox.Show("Ваш аккаунт был заморожен");
                return;
            }
            Button btn = sender as Button;
            BookViewModel bvm = btn.DataContext as BookViewModel;
            NewBookWindow newBook = new NewBookWindow(bvm.book);
            newBook.ShowDialog();
            if (newBook.is_created)
            {
                lst_books.ItemsSource = Auth.cur_user.Books.Where(b => !b.isFreeze).Select(
                                b => new BookViewModel()
                                {
                                    book = b
                                }
                                ).ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.cur_user.isFreeze)
            {
                MessageBox.Show("Ваш аккаунт был заморожен");
                return;
            }
            NewBookWindow newBook = new NewBookWindow(null);
            newBook.ShowDialog();
            if (newBook.is_created)
            {
                lst_books.ItemsSource = Auth.cur_user.Books.Where(b => !b.isFreeze).Select(
                                b => new BookViewModel()
                                {
                                    book = b
                                }
                                ).ToList();
            }
        }
    }
}
