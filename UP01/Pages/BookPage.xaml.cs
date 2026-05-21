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
using UP01.Windows;

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        bool IsUserAdmin = false;

        public Visibility visibility
        {
            get {
                if (IsUserAdmin)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            set
            { }
        }
        public BookPage(BookViewModel book)
        {
            IsUserAdmin = Auth.cur_user.RoleID == 3;
            this.DataContext = book;
            InitializeComponent();
            
            LB_Reviews.ItemsSource = book.book.Reviews.Where(i => !i.IsFreeze).ToList();
            
        }

        private void Menu_Author_Report(object sender, RoutedEventArgs e)
        {
            BookViewModel bvm = this.DataContext as BookViewModel;
            ReportWindow reportWindow = new ReportWindow(ReportType.UserReport, bvm.book.AuthorID, Auth.cur_user.ID);
            reportWindow.Show();
        }

        private void Menu_Book_Report(object sender, RoutedEventArgs e)
        {
            BookViewModel bvm = this.DataContext as BookViewModel;
            ReportWindow reportWindow = new ReportWindow(ReportType.BookReport, bvm.book.ID, Auth.cur_user.ID);
            reportWindow.Show();
        }

        private void Review_Report_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Reviews review = btn.DataContext as Reviews;
            ReportWindow reportWindow = new ReportWindow(ReportType.ReviewReport, review.ID, Auth.cur_user.ID);
            reportWindow.Show();
        }

        private void Freezer_Click(object sender, RoutedEventArgs e)
        {
            BookViewModel bookView = this.DataContext as BookViewModel;
            Button button = sender as Button;
            Reviews review = button.DataContext as Reviews;
            review.IsFreeze = true;

            LB_Reviews.ItemsSource = null;
            LB_Reviews.ItemsSource = bookView.book.Reviews.Where(i => !i.IsFreeze).ToList();
        }

        private void CreateReview_Click(object sender, RoutedEventArgs e)
        {
            BookViewModel bookView = this.DataContext as BookViewModel;
            if (Auth.cur_user.isFreeze)
            {
                MessageBox.Show("Ваш аккаунт был заморожен");
                return;
            }
            NewReviewWindow newReview = new NewReviewWindow(bookView.book);
            newReview.ShowDialog();
            if (newReview.is_created)
            {
                MessageBox.Show("Отзыв успешно создан");
                LB_Reviews.ItemsSource = null;
                LB_Reviews.ItemsSource = bookView.book.Reviews.ToList();
            }
        }

        private void ReadBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReadBookPage(this.DataContext as BookViewModel));
        }
    }
}
