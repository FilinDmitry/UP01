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
using UP01.Windows;

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.DataContext = Auth.cur_user;
            InitializeComponent();
            if (Auth.cur_user.isFreeze)
            {
                SP_Freeze.Visibility = Visibility.Visible;
            }
            LB_comments.ItemsSource = Auth.cur_user.Reviews.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FreezePage());
        }

        private void Became_author_click(object sender, RoutedEventArgs e)
        {
            ApplicationWindow window = new ApplicationWindow(ApplicationType.BecomeAuthorApplication, Auth.cur_user.ID);
            window.Show();
        }
    }
}
