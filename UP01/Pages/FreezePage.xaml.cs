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

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для FreezePage.xaml
    /// </summary>
    public partial class FreezePage : Page
    {
        public FreezePage()
        {
            this.DataContext = Auth.cur_user;
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationWindow window = new ApplicationWindow(ApplicationType.UnfreezeApplication, Auth.cur_user.ID);
            window.Show();
        }
    }
}
