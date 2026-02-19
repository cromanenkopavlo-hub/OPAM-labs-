namespace CoursesConsoleApp_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Представляє модель студента.
    /// Містить дані для ідентифікації та безпечного входу в систему.
    /// </summary>
    public class Students
    {
        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Students"/>.
        /// Конструктор за замовчуванням.
        /// </summary>
        public Students()
        {
        }

        /// <summary>
        /// Gets or sets унікальний ідентифікатор студента у базі даних.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets електронну пошту студента, що використовується як логін.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets зашифрований пароль (хеш), що зберігається для безпеки.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Students"/>.
        /// </summary>
        /// <param name="id">Унікальний номер.</param>
        /// <param name="email">Електронна адреса.</param>
        /// <param name="hash">Хешований пароль.</param>
        public Students(int id, string email, string hash)
        {
            this.Id = id;
            this.Email = email ?? string.Empty;
            this.PasswordHash = hash ?? string.Empty;
        }
    }
}
