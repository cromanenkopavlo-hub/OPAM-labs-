// <copyright file="Courses.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CoursesConsoleApp_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Представляє модель навчального курсу.
    /// Містить дані про ідентифікатор, назву та вартість.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Course"/>.
        /// </summary>
        public Course()
        {
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Course"/>.
        /// </summary>
        /// <param name="id">Ідентифікатор курсу.</param>
        /// <param name="name">Назва курсу.</param>
        /// <param name="price">Вартість курсу.</param>
        public Course(int id, string name, double price)
        {
            this.Id = id;
            this.Name = name ?? string.Empty;
            this.Price = price;
        }

        /// <summary>
        /// Gets or sets унікальний номер курсу.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets назву навчальної програми.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets вартість навчання для даного курсу.
        /// </summary>
        public double Price { get; set; } = 0;
    }
}