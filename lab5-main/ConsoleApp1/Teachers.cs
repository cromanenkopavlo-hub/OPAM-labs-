using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesConsoleApp_1
{
    /// <summary>
    /// Представляє модель викладача.
    /// Містить дані для ідентифікації та авторизації в адміністративній частині.
    /// </summary>
    public class Teachers
    {
        /// <summary>Унікальний числовий номер викладача.</summary>
        public int Id { get; set; }
        /// <summary>Унікальне ім'я користувача (логін) викладача.</summary>
        public string Username { get; set; } = "";
        /// <summary>Зашифрований пароль для перевірки доступу.</summary>
        public string PasswordHash { get; set; } = "";
        /// <summary>
        /// Порожній конструктор для створення екземпляра класу.
        /// </summary>
        public Teachers() { }
        /// <summary>
        /// Конструктор для ініціалізації викладача з конкретними даними.
        /// </summary>
        /// <param name="id">Ідентифікатор</param>
        /// <param name="user">Логін</param>
        /// <param name="hash">Хеш пароля</param>
        public Teachers(int id, string user, string hash)
        {
            Id = id;
            Username = user;
            PasswordHash = hash;
        }
    }
}