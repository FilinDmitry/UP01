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
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        ReportType type;
        int ID;
        int AuthorID;
        public ReportWindow(ReportType type, int ID, int AuthorID)
        {
            this.type = type;
            this.ID = ID;
            this.AuthorID = AuthorID;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string comment = TB_Comment.Text;
            switch (type)
            {
                case ReportType.UserReport:
                    break;
                case ReportType.BookReport:
                    break;
                case ReportType.ReviewReport:
                    break;
            }
        }
    }
}
