using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP01.Models;

namespace UP01.ViewModels
{
    public class BookInListViewModel : BookViewModel
    {
        public List<string> lists_ = new List<string>()
        {
            "", "Прочитано", "Читаю", "В планах", "Заброшенно"
        };
        public List<string> lists => lists_;
        public string status;
        public string Status {
            get { return GetStatus(); }
            set { status = value; }
        }
        public ReadingList r_list => GetReadingList();
        public string GetStatus()
        {
            ReadingList reading = GetReadingList(); 
            if (reading == null)
            {
                return string.Empty;
            }
            if (reading.BookStatus == null)
            {  return string.Empty; }
            return reading.BookStatus.Name;
        }
        private ReadingList GetReadingList()
        {
            return Core.Context.ReadingList.FirstOrDefault(rl => rl.UserID == Auth.cur_user.ID && rl.BookID == book.ID);
        }
    }
}
