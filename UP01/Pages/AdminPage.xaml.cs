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
using UP01.Models;
using UP01.ViewModels;

namespace UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
           
            InitializeComponent();
            Update();

            

        }

        private void Change_user_role(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            UserViewModel user = cb.DataContext as UserViewModel;
            if (user == null)
            {
                return;

            }
            user.user.Roles = Core.Context.Roles.First(r => r.Name == user.new_role_);
            Core.Context.SaveChanges();
            Update();
        }

        private void AuthorApproveBtnClicl(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ApplicationsToBecomeAuthor becomeAuthor = btn.DataContext as ApplicationsToBecomeAuthor;
            becomeAuthor.isClose = true;
            becomeAuthor.Approved = true;
            becomeAuthor.Users.RoleID = 2;
            Core.Context.SaveChanges();
            Update();
        }

        private void AuthorRegectBtnClicl(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ApplicationsToBecomeAuthor becomeAuthor = btn.DataContext as ApplicationsToBecomeAuthor;
            becomeAuthor.isClose = true;
            becomeAuthor.Approved = false;
            Core.Context.SaveChanges();
            Update();
        }

        

        private void Update()
        {
            List<UnfreezeApplicationViewModel> ask_list = Core.Context.ApplicationsToUnfreeze.Where(i => !i.isClose).Select(i => new UnfreezeApplicationViewModel()
            {
                application = i

            }).ToList();
            CollectionView view_ask = (CollectionView)CollectionViewSource.GetDefaultView(ask_list);
            PropertyGroupDescription groupDescription_ask = new PropertyGroupDescription("group");
            view_ask.GroupDescriptions.Add(groupDescription_ask);
            view_ask.SortDescriptions.Add(new SortDescription("group", ListSortDirection.Ascending));
            LB_Unfreeze.ItemsSource = null;
            LB_Unfreeze.ItemsSource = view_ask;

            List<ReportsViewModel> reports_list = Core.Context.Reports.Where(i => !i.isClose).Select(i => new ReportsViewModel()
            {
                report = i,
            }).ToList();
            CollectionView view_rep = (CollectionView)CollectionViewSource.GetDefaultView(reports_list);
            PropertyGroupDescription groupDescription_rep = new PropertyGroupDescription("group");
            view_rep.GroupDescriptions.Add(groupDescription_rep);
            view_rep.SortDescriptions.Add(new SortDescription("group", ListSortDirection.Ascending));
            LB_reports.ItemsSource = null;
            LB_reports.ItemsSource = view_rep;

            var freeze_list = Core.Context.Users.Where(i => i.isFreeze).Select(u => new FreezeViewModel()
            {
                freezeType = freezeType.User,
                user = u
            }).ToList().Union(Core.Context.Books.Where(i => i.isFreeze).Select(u => new FreezeViewModel()
            {
                freezeType = freezeType.Book,
                user = u.Users,
                book = u

            }).ToList()).ToList();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(freeze_list);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("group");
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(new SortDescription("group", ListSortDirection.Ascending));
            LB_freeze.ItemsSource = null;
            LB_freeze.ItemsSource = view;



            LB_Author.ItemsSource = null;
            LB_Users.ItemsSource = null;
            LB_Users.ItemsSource = Core.Context.Users.Where(u => u.ID != Auth.cur_user.ID).Select(u =>
                new UserViewModel()
                { user = u }
                ).ToList();
            LB_Author.ItemsSource = Core.Context.ApplicationsToBecomeAuthor.Where(i => !i.isClose).ToList();
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FreezeViewModel freeze = btn.DataContext as FreezeViewModel;
            if (freeze.GetGroup() == "Книги")
            {
                NavigationService.Navigate(new BookPage(new BookViewModel() { book = freeze.book }));
            }
            else
            {
                MessageBox.Show("Данная функция в разработке");
            }
        }

        private void UnfreezeBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FreezeViewModel freeze = btn.DataContext as FreezeViewModel;
            freeze.UnFreeze();
            Update();
            MessageBox.Show("Разморожен");
        }

        private void TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            UserViewModel user = txt.DataContext as UserViewModel;
            switch (txt.Name)
            {
                case "TB_P":
                    user.user.Password = txt.Text;
                    break;
                case "TB_N":
                    user.user.Name = txt.Text;
                    break;
            }
            Core.Context.SaveChanges();
        }

        private void RerortApprove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ReportsViewModel rvm = btn.DataContext as ReportsViewModel;
            rvm.Freeze();
            Update();
            MessageBox.Show("Заморожено");
        }

        private void ReportRegret_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ReportsViewModel rvm = btn.DataContext as ReportsViewModel;
            rvm.report.isClose = true;
            rvm.report.Approved = false;
            Core.Context.SaveChanges();
            Update();
            MessageBox.Show("Жалоба отколонена");
        }

        private void AboutReport_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ReportsViewModel rvm = btn.DataContext as ReportsViewModel;
            Page p = rvm.NavigateTo();
            if (p != null)
            {
                NavigationService.Navigate(p);
            }
            else
            {
                MessageBox.Show("В разработке");
            }
        }

        private void AskUnfreezeApprove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            UnfreezeApplicationViewModel uavm = btn.DataContext as UnfreezeApplicationViewModel;
            uavm.Unfreeze();
            Update();
        }

        private void AskUnfreezeRegret_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            UnfreezeApplicationViewModel uavm = btn.DataContext as UnfreezeApplicationViewModel;
            uavm.application.isClose = true;
            uavm.application.Approved = false;
            Core.Context.SaveChanges();
            Update();
        }
    }
}
