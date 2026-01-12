using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesConsoleApp_1
{/// <summary>
 /// Представляє модель навчального курсу.
 /// Містить дані про ідентифікатор, назву та вартість.
 /// </summary>
    public class Course
    {/// <summary>Унікальний номер курсу.</summary>
        public int Id { get; set; }
        /// <summary>Назва навчальної програми.</summary>
        public string Name { get; set; } = "";
        /// <summary>Вартість навчання для даного курсу.</summary>
        public double Price { get; set; } = 0;
        /// <summary>
        /// Конструктор за замовчуванням. 
        /// Необхідний для створення порожніх об'єктів.
        /// </summary>
        public Course() { }

        /// <summary>
        /// Конструктор з параметрами для створення нового курсу.
        /// </summary>
        /// <param name="id">Ідентифікатор курсу</param>
        /// <param name="name">Назва курсу</param>
        /// <param name="price">Вартість курсу</param>
        public Course(int id, string name, double price)
        {
            Id = id;
            Name = name ?? "";
            Price = price;
        }
    }
}