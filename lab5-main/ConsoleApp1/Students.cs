using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesConsoleApp_1
{
    /// <summary>
    /// Представляє модель студента.
    /// Містить дані для ідентифікації та безпечного входу в систему.
    /// </summary>
    public class Students
    {
        /// <summary>Унікальний ідентифікатор студента у базі даних.</summary>
        public int Id { get; set; }
        /// <summary>Електронна пошта студента, що використовується як логін.</summary>
        public string Email { get; set; } = "";
        /// <summary>Зашифрований пароль (хеш), що зберігається для безпеки.</summary>
        public string PasswordHash { get; set; } = "";
        /// <summary>
        /// Конструктор за замовчуванням.
        /// Використовується для створення порожнього об'єкта студента.
        /// </summary>
        public Students() { }
        /// <summary>
        /// Конструктор з параметрами для ініціалізації даних студента.
        /// </summary>
        /// <param name="id">Унікальний номер</param>
        /// <param name="email">Електронна адреса</param>
        /// <param name="hash">Хешований пароль</param>
        public Students(int id, string email, string hash)
        {
            Id = id;
            Email = email;
            PasswordHash = hash;
        }
    }
}