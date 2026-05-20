using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP01.Models;

namespace UP01.ViewModels
{
    public enum freezeType 
    {
        User,
        Book,
        Review
    }
    public class FreezeViewModel
    {
        public freezeType freezeType;
        public string group => GetGroup();
        public string name => GetName();
        public string reason => GetReason();

        public Users user;
        public Books book;
        public Reviews reviews;
        public string GetGroup()
        {
            switch (freezeType)
            {
                case freezeType.User:
                    return "Пользователи";
                case freezeType.Book:
                    return "Книги";
                default:
                    return "Отзывы";
            }
        }
        public string GetReason()
        {
            switch (freezeType)
            {
                case freezeType.User:
                    return user.ReasonToFreeze;
                case freezeType.Book:
                    return book.ReasonToFreeze;
                default:
                    return "Гойда";
            }
        }
        public string GetName()
        {
            switch (freezeType)
            {
                case freezeType.User:
                    return user.Login;
                case freezeType.Book:
                    return book.Name;
                default:
                    return "Отзывы";
            }
        }

        public void UnFreeze()
        {
            switch (freezeType)
            {
                case freezeType.User:
                    user.isFreeze = false;
                    break;
                case freezeType.Book:
                    book.isFreeze = false;
                    break;
                default:
                    reviews.IsFreeze = false;
                    break;
            }
            Core.Context.SaveChanges();
        }

    }
}
