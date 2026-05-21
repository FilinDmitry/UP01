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
        /// <summary>
        /// Вызывает <see cref="Check(string, string, string, string)"/> если проверка входных параметров прошла успешно, то создает новую книгу
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Desc"></param>
        /// <param name="ImagePath"></param>
        /// <param name="Content"></param>
        /// <returns><see langword="true"/> если книга создана, иначе <see langword="false"/></returns>
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
        /// <summary>
        /// Вызывает <see cref="Check(string, string, string, string)"/> если проверка входных параметров прошла успешно, то изменяет существующую книгу
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Desc"></param>
        /// <param name="ImagePath"></param>
        /// <param name="Content"></param>
        /// <param name="book">Книга для изменения</param>
        /// <returns><see langword="true"/> если книга изменена, иначе <see langword="false"/></returns>
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
        /// <summary>
        /// Проверяет данные для создания/изменения пользователя, если есть ошибка выводит окно с сообщением
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Desc"></param>
        /// <param name="ImagePath"></param>
        /// <param name="Content"></param>
        /// <returns><see langword="true"/> если все введеные параметры корректны, иначе <see langword="false"/></returns>
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
