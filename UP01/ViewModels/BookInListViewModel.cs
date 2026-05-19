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
        public string Status => GetStatus();
        public string GetStatus()
        {
            ReadingList reading = Core.Context.ReadingList.FirstOrDefault(rl => rl.UserID == Auth.cur_user.ID && rl.BookID == book.ID);
            if (reading == null)
            {
                return string.Empty;
            }
            return reading.BookStatus.Name;
        }
    }
}
