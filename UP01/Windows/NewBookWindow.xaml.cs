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
using System.Windows.Shapes;
using UP01.Models;

namespace UP01.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewBookWindow.xaml
    /// </summary>
    public partial class NewBookWindow : Window
    {
        Books book;
        public bool is_created = false;
        public NewBookWindow(Books book)
        {
            this.book = book;
            InitializeComponent();
            if (book != null )
            {
                btn.Content = "Изменить";
                TB_Name.Text = book.Name;
                TB_Image.Text = book.ImagePath;
                TB_Desc.Text = book.Description;
                TB_Content.Text = book.Content;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (book == null)
            {
                if (BookCreationValidation.CreateBook(TB_Name.Text, TB_Desc.Text, TB_Image.Text, TB_Content.Text))
                {
                    Window w = GetWindow(this);
                    w.Close();
                    MessageBox.Show("Книга успешно созданна");
                    is_created = true;
                }
            }
            else
            {
                if(BookCreationValidation.ChangeBook(book, TB_Name.Text, TB_Desc.Text, TB_Image.Text, TB_Content.Text))
                {
                    Window w = GetWindow(this);
                    w.Close();
                    MessageBox.Show("Книга успешно изменена");
                    is_created = true;
                }
            }
        }
    }
}
