using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using UP01.Models;

namespace UP01.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewReviewWindow.xaml
    /// </summary>
    public partial class NewReviewWindow : Window
    {
        public bool is_created = false;
        Books book;
        public NewReviewWindow(Books book)
        {
            this.book = book;
            InitializeComponent();
            CB_Rating.ItemsSource = new List<string>()
            { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Rating.SelectedIndex == -1)
            {
                MessageBox.Show("Поставьте оценку");
                return;
            }
            Reviews r = Core.Context.Reviews.FirstOrDefault(i => i.Book == book.ID || i.UserID == Auth.cur_user.ID);
            if (r != null)
            {
                Core.Context.Reviews.Remove(r);
            }
            Core.Context.Reviews.Add(new Reviews()
            {
                Books = book,
                UserID = Auth.cur_user.ID,
                Rating = CB_Rating.SelectedIndex + 1,
                Date = DateTime.Now,
                Message = TB_Message.Text
            });
            Core.Context.SaveChanges();
            is_created = true;
            Close();
        }
    }
}
