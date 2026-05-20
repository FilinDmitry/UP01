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
        public string Avg => String_Rating();
        public string Name => book.Name;
        public double Avg_b => Get_Rating(); 
        public string ImagePath => book.ImagePath;
        public string Author => book.Users.Name;
        public string Description => book.Description;
        public string Rating => Rating_Book_Page();

        public string Genres => Get_Genres();
        public bool is_genres(string Genre)
        {
            return book.Genre.Where(g => g.Name == Genre).FirstOrDefault() != null;
        }
        private double Get_Rating()
        {
            ICollection<Reviews> reviews = book.Reviews;
            if (reviews.Count == 0)
            {
                return 0;
            }
            else
            {
                return reviews.Average(r => r.Rating);
            }
        }
        private string String_Rating()
        {
            double rating = Get_Rating();
            if (rating == 0)
            {
                return "0 оценок";
            }
            else
            {
                return rating.ToString("F1") + "/10";
            }

        }

        private string Get_Genres()
        {
            return string.Join(", ", book.Genre.Select(g => g.Name).ToList());
        }
        private string Rating_Book_Page()
        {
            string rate = Get_Rating().ToString("F1");
            if (rate[0] == '0')
            {
                return "0 оценок";
            }
            else
            {
                return "Оценка " + rate + "/10";
            }
        }
    }
}
