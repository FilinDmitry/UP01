using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UP01.Models;
namespace UP01.ViewModels
{
    public class BookViewModel
    {
        public Books book;
        public string Avg => Get_Rating();
        public string Name => book.Name;
        public string ImagePath => book.ImagePath;
        public string Author => book.Users.Name;
        public string Description => book.Description;

        public string Rating => Rating_Book_Page();

        public string Genres => Get_Genres();
        public bool is_genres(string Genre)
        {
            return book.Genre.Where(g => g.Name == Genre).FirstOrDefault() != null;
        }
        private string Get_Rating()
        {
            ICollection<Reviews> reviews = book.Reviews;
            if (reviews.Count == 0)
            {
                return "0 оценок";
            }
            else
            {
                return reviews.Average(r => r.Rating).ToString("F1") + "/10";
            }

        }

        private string Get_Genres()
        {
            return string.Join(", ", book.Genre.Select(g => g.Name).ToList());
        }
        private string Rating_Book_Page()
        {
            string rate = Get_Rating();
            if (rate[0] == '0')
            {
                return "К этой книге 0 отзывов";
            }
            else
            {
                return rate;
            }
        }
    }
}
