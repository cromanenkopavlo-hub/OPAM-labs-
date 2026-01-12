using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesConsoleApp_1
{
    /// <summary>
    /// Визначає ролі користувачів у системі.
    /// Використовується для контролю доступу до функцій керування даними.
    /// </summary>
    public enum Role
    {
        /// <summary>Роль не визначена (користувач не авторизований).</summary>
        None,
        /// <summary>Адміністратор з повними правами редагування.</summary>
        Admin,
        /// <summary>Студент із правами перегляду та пошуку.</summary>
        Student,
        /// <summary>Викладач із доступом до списків та статистики.</summary>
        Teacher
    }
}