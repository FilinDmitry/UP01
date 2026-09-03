using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using UP01.Models;

namespace UP01
{
    static class Auth
    {
        static public bool is_reg = true;
        static public Users cur_user = Core.Context.Users.First(i => i.ID == 1);
        private static List<Users> lst_users = Core.Context.Users.ToList();
        /// <summary>
        /// Функция проверки строки, на то является ли она корректным email
        /// </summary>
        /// <param name="email">Строка которую необходимо проверить</param>
        /// <returns><see langword="true"/> если email является корректным, иначе <see langword="false"/></returns>
        private static bool Emailvalidation(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
        /// <summary>
        /// Метод который проверяет параметры регистрации на корректность. <br/>
        /// Если принимаемые параметры корректны, то вызывает <see cref="Update(Users)"></see> а также <see cref="Check_user(string, string)"/> для входа в аккаунт. 
        /// Иначе выдатет окно с предупреждение о некорректном действии 
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="name">Отображаемое имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="email">email пользователя</param>
        /// <returns><see langword="true"/> если полученные параметры корректы, <see langword="false"/> если хотя бы один из параметров не соответствует условиям</returns>
        static public bool New_user(string login, string name, string password, string email)
        {
            if (login.Contains(" ") || password.Contains(" "))
            {
                MessageBox.Show("Логин и/или пароль не могут содержать пробелы");
                return false;
            }

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Заполните все поля");
                return false;
            }

            if (!Emailvalidation(email))
            {
                MessageBox.Show("Введен некорректный email");
                return false;
            }
            Users u = lst_users.Where(i => i.Login == login).FirstOrDefault();
            if (u != null)
            {
                MessageBox.Show("Такой пользователь уже существует");
                return false;
            }

            Users user = new Users()
            {
                Login = login,
                Name = name,
                Password = password,
                Email = email
            };

            Update(user);
            Check_user(login, password);
            return true;
        }
        /// <summary>
        /// Функция для входа в аккаунт
        /// </summary>
        /// <param name="login_">Значения логина для входа</param>
        /// <param name="password">Значения пароля для входа</param>
        /// <returns><see langword="true"/> если вход успешно выполнене, <see langword="false"/> если введены некорректные данные</returns>
        static public bool Check_user(string login_, string password)
        {
            Users u = lst_users.Where(i => i.Login == login_ && i.Password == password).FirstOrDefault();
            if (u == null)
            {
                MessageBox.Show("Введен неверный логин или пароль");
                return false;
            }
            else
            {
                MessageBox.Show("Вход успешно выполнен");
                is_reg = true;
                cur_user = u;
                return true;
            }
        }
        /// <summary>
        /// Создает запись в пользователя в БД
        /// </summary>
        /// <param name="user"></param>

        static private void Update(Users user)
        {
            Core.Context.Users.Add(user);
            Core.Context.SaveChanges();
            lst_users = Core.Context.Users.ToList();
        }
    }
}

