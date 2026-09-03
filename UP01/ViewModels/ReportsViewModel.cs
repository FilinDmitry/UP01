using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UP01.Models;
using UP01.Pages;

namespace UP01.ViewModels
{
    public class ReportsViewModel
    {
        public ReportType type;
        private Reports report_;
        public Reports report { get { return report_; } set { type = (ReportType)value.ReportType; report_ = value; } }
        public string info => GetInfo();

        public string group => GetGroup();
        public string comment => "Причина: " + report_.Description;
        public string Author => report_.Users.Name;
        private string GetInfo()
        {
            switch (type)
            {
                case ReportType.BookReport:
                    return "Жалоба на книгу: " + report_.Books.Name;
                case ReportType.UserReport:
                    return "Жалоба на пользователя: " + report_.Users1.Name;
                case ReportType.ReviewReport:
                    return "Комментарий: " + report_.Reviews.Message;
            }
            return "Информация недоступна";
        }
        private string GetGroup()
        {
            switch (type)
            {
                case ReportType.BookReport:
                    return "Книги";
                case ReportType.UserReport:
                    return "Пользователи";
                case ReportType.ReviewReport:
                    return "Комментарии";
            }
            return string.Empty;
        }

        public void Freeze()
        {
            switch (type)
            {
                case ReportType.BookReport:
                    report.Books.isFreeze = true;
                    break;
                case ReportType.UserReport:
                    report.Users1.isFreeze = true;
                    break;
                case ReportType.ReviewReport:
                    report.Reviews.IsFreeze = true;
                    break;
            }
            report_.isClose = true;
            report.Approved = true;
            Core.Context.SaveChanges();
        }

        public Page NavigateTo()
        {
            switch (type)
            {
                case ReportType.BookReport:
                    return new BookPage(new BookViewModel() { book = report.Books });
                case ReportType.UserReport:
                    
                    return null;
                case ReportType.ReviewReport:
                    return new BookPage(new BookViewModel() { book = report.Reviews.Books });
            }
            return null;
        }
        
    }
}
