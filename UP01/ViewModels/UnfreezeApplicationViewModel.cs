using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP01.Models;

namespace UP01.ViewModels
{
    
    public class UnfreezeApplicationViewModel
    {
        public ApplicationType type;
        private ApplicationsToUnfreeze application_;
        public ApplicationsToUnfreeze application { get { return application_; } set { application_ = value; SetType(); } }
        public Books book;
        public string group => GetGroup();
        public string Login => application.Users.Login;
        public string Message => application.Message;
        public string Reason => GetReason();

        
        public string GetGroup()
        {
            switch (type)
            {
                case ApplicationType.UnfreezeApplication:
                    return "Пользователи";
                case ApplicationType.BookUnfreeze:
                    return "Книги";
                default:
                    return string.Empty;
            }
        }
        public void SetType()
        {
            if (application_.BookID != null)
            {
                type = ApplicationType.BookUnfreeze;
            }
            else
            {
                type = ApplicationType.UnfreezeApplication;
            }
        }
        public string GetReason()
        {
            string r = string.Empty;
            switch (type)
            {
                case ApplicationType.UnfreezeApplication:
                    r = application.Users.ReasonToFreeze;
                    break;
                case ApplicationType.BookUnfreeze:
                    r = application.Books.ReasonToFreeze;
                    break;
                
                
            }
            if (string.IsNullOrEmpty(r))
            {
                return "Причина не указана";
                    }
            return r;
        }
        public void Unfreeze()
        {
            switch (type)
            {
                case ApplicationType.UnfreezeApplication:
                    application_.Users.isFreeze = true;
                    break;
                case ApplicationType.BookUnfreeze:
                    application_.Books.isFreeze = true;
                    break;


            }
            application_.isClose = true;
            application.Approved = true;
            Core.Context.SaveChanges();
        }
    }
}
