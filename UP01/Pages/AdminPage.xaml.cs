using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            /*
                List<Seans> seans_lst_d = Core.Context.Seans.Where(i => i.Film_ID == movie.ID).ToList();



            var seans_lst = seans_lst_d.Select(i => new Seans_Info
            {
                ID = i.ID,
                Date = i.StartTime.Date,
                Time = i.StartTime.TimeOfDay,
                Kinozal_ID = i.Kinozal_ID,
                Lenght = i.Lenght
            }
            ).ToList();


            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(seans_lst);
            PropertyGroupDescription groupDescription
                    = new PropertyGroupDescription("Date");
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));

            DataContext = movie;
            InitializeComponent();
            */
            InitializeComponent();
        }
    }
}
