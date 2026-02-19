// <copyright file="Role.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoursesConsoleApp_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Визначає ролі користувачів у системі.
    /// Використовується для контролю доступу до функцій керування даними.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Роль не визначена (користувач не авторизований).
        /// </summary>
        None,

        /// <summary>
        /// Адміністратор з повними правами редагування та видалення.
        /// </summary>
        Admin,

        /// <summary>
        /// Студент із правами перегляду списків, пошуку та фільтрації.
        /// </summary>
        Student,

        /// <summary>
        /// Викладач із доступом до перегляду даних та розрахунку статистики.
        /// </summary>
        Teacher,
    }
}