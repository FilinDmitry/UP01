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
    /// Логика взаимодействия для ApplicationWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : Window
    {
        ApplicationType type;
        int UserID; 
        public ApplicationWindow(ApplicationType type, int UserID)
        {
            this.type = type;
            this.UserID = UserID;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case ApplicationType.UnfreezeApplication:
                    if (TB_Comment.Text == null)
                    {
                        MessageBox.Show("введите причину почему мы должны вас разблокировать");
                        return;
                    }
                    ApplicationsToUnfreeze application_f = new ApplicationsToUnfreeze()
                    {
                        UserID = UserID,
                        Message = TB_Comment.Text
                    };
                    Core.Context.ApplicationsToUnfreeze.Add(application_f);
                    Core.Context.SaveChanges();
                    break;
                case ApplicationType.BecomeAuthorApplication:
                    ApplicationsToBecomeAuthor application_a = new ApplicationsToBecomeAuthor()
                    {
                        UserID = UserID,
                        Message = TB_Comment.Text
                    };
                    Core.Context.ApplicationsToBecomeAuthor.Add(application_a);
                    Core.Context.SaveChanges();
                    break;
            }
        }
    }
}
