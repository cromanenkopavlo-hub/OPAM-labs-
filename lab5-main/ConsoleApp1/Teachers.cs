namespace CoursesConsoleApp_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Представляє модель викладача.
    /// Містить дані для ідентифікації та авторизації в адміністративній частині.
    /// </summary>
    public class Teachers
    {
        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Teachers"/>.
        /// Порожній конструктор для створення екземпляра класу.
        /// </summary>
        public Teachers()
        {
        }

        /// <summary>
        /// Gets or sets унікальний числовий номер викладача.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets унікальне ім'я користувача (логін) викладача.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets зашифрований пароль для перевірки доступу.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;


        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Teachers"/>.
        /// </summary>
        /// <param name="id">Ідентифікатор.</param>
        /// <param name="user">Логін.</param>
        /// <param name="hash">Хеш пароля.</param>
        public Teachers(int id, string user, string hash)
        {
            this.Id = id;
            this.Username = user ?? string.Empty;
            this.PasswordHash = hash ?? string.Empty;
        }
    }
}
