using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using UP01.Models;

namespace UP01
{
    public static class BookCreationValidation
    {
        private const int NameMin = 3;
        private const int ContentMin = 15;
        private const int DescMin = 10;
        public static bool CreateBook(string Name, string Desc, string ImagePath, string Content)
        {
            if (!Check(Name, Desc, ImagePath, Content))
            {  return false; }

            
            Core.Context.Books.Add(new Books()
            {
                Name = Name,
                ImagePath = ImagePath,
                Content = Content,
                Description = Desc,
                AuthorID = Auth.cur_user.ID
            });
            Core.Context.SaveChanges();
            return true;
        }

        public static bool ChangeBook(Books book, string Name, string Desc, string ImagePath, string Content)
        {
            if (Check(Name, Desc, ImagePath, Content))
            {
                book.Name = Name;
                book.Description = Desc;
                book.ImagePath = ImagePath;
                book.Content = Content;
                Core.Context.SaveChanges();
                return true;
            }
            return false;
            
        }
        private static bool Check(string Name, string Desc, string ImagePath, string Content)
        {
            if (Name.Length < NameMin)
            {
                MessageBox.Show($"Название должно быть длинее {NameMin} символов");
                return false;
            }

            if (Desc.Length < DescMin)
            {
                MessageBox.Show("Описание слишком короткое");
                return false;
            }
            if (Content.Length < ContentMin)
            {
                MessageBox.Show($"Количество символов в книге должно быть длинее {ContentMin} символов");
                return false;
            }
            if (!ImagePath.StartsWith("http"))
            {
                MessageBox.Show("Ссылка на изображенние некорректная");
                return false;
            }
            return true;
        }
    }
}
