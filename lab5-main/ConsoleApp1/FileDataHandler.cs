using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CoursesConsoleApp_1
{/// <summary>
    /// Статичний клас для керування файловими операціями.
    /// Забезпечує ініціалізацію, зчитування та збереження даних у форматі CSV.
    /// </summary>
    public static class FileDataHandler
    {
        private const string FolderName = "data";
        public static readonly string CoursesPath = Path.Combine(FolderName, "courses.csv");
        public static readonly string StudentsPath = Path.Combine(FolderName, "students.csv");
        public static readonly string TeachersPath = Path.Combine(FolderName, "teachers.csv");

        public static void InitializeFiles()
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
            EnsureFile(CoursesPath, "Id,Name,Price");
            EnsureFile(StudentsPath, "Id,Email,PasswordHash");
            EnsureFile(TeachersPath, "Id,Username,PasswordHash");
        }
        /// <summary>
        /// Створює папку "data" та перевіряє наявність усіх необхідних файлів.
        /// Якщо файли відсутні, створює їх із відповідними заголовками.
        /// </summary>
        private static void EnsureFile(string path, string header)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
                File.WriteAllLines(path, new[] { header }, Encoding.UTF8);
        }
        /// <summary>
        /// Перетворює звичайний текст пароля на захищений SHA256 хеш.
        /// </summary>
        /// <param name="password">Пароль у відкритому вигляді</param>
        /// <returns>Рядок зашифрованого пароля у форматі Base64</returns>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Генерація ID 
        /// <summary>
        /// Розраховує наступний доступний ID для нового запису у файлі.
        /// </summary>
        /// <param name="path">Шлях до CSV-файлу</param>
        /// <returns>Число — новий вільний ідентифікатор</returns>
        public static int GetNextId(string path)
        {
            if (!File.Exists(path)) return 1;
            try
            {
                string[] lines = File.ReadAllLines(path);
                int max = 0;
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (int.TryParse(parts[0], out int id))
                    {
                        if (id > max) max = id;
                    }
                }
                return max + 1;
            }
            catch { return 1; }
        }

        // Читання курсів 
        /// <summary>
        /// Зчитує файл courses.csv та перетворює його рядки на список об'єктів Course.
        /// </summary>
        /// <returns>Список об'єктів курсів</returns>
        public static List<Course> ReadCourses()
        {
            List<Course> list = new List<Course>();
            if (!File.Exists(CoursesPath)) return list;
            string[] lines = File.ReadAllLines(CoursesPath);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] p = lines[i].Split(',');
                    if (p.Length == 3)
                        list.Add(new Course(int.Parse(p[0]), p[1], double.Parse(p[2])));
                }
                catch { continue; }
            }
            return list;
        }
        /// <summary>
        /// Зчитує дані студентів для авторизації.
        /// </summary>
        /// <returns>Список об'єктів студентів</returns>
        public static List<Students> ReadStudents()
        {
            List<Students> list = new List<Students>();
            if (!File.Exists(StudentsPath)) return list;
            string[] lines = File.ReadAllLines(StudentsPath);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] p = lines[i].Split(',');
                    if (p.Length == 3)
                        list.Add(new Students(int.Parse(p[0]), p[1], p[2]));
                }
                catch { continue; }
            }
            return list;
        }
        /// <summary>
        /// Виконує повний перезапис файлу teachers.csv актуальними даними.
        /// Використовується для синхронізації змін після редагування або реєстрації.
        /// </summary>
        /// <param name="list">Список об'єктів викладачів для збереження</param>
        public static List<Teachers> ReadTeachers()
        {
            List<Teachers> list = new List<Teachers>();
            if (!File.Exists(TeachersPath)) return list;
            string[] lines = File.ReadAllLines(TeachersPath);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] p = lines[i].Split(',');
                    if (p.Length == 3)
                        list.Add(new Teachers(int.Parse(p[0]), p[1], p[2]));
                }
                catch { continue; }
            }
            return list;
        }

        // Збереження даних 
        /// <summary>
        /// Повністю перезаписує файл курсів новими даними із пам'яті.
        /// </summary>
        /// <param name="list">Актуальний список курсів</param>
        public static void SaveCourses(List<Course> list)
        {
            List<string> lines = new List<string> { "Id,Name,Price" };
            foreach (var c in list) lines.Add($"{c.Id},{c.Name},{c.Price}");
            File.WriteAllLines(CoursesPath, lines, Encoding.UTF8);
        }

        public static void SaveStudents(List<Students> list)
        {
            List<string> lines = new List<string> { "Id,Email,PasswordHash" };
            foreach (var s in list) lines.Add($"{s.Id},{s.Email},{s.PasswordHash}");
            File.WriteAllLines(StudentsPath, lines, Encoding.UTF8);
        }

        public static void SaveTeachers(List<Teachers> list)
        {
            List<string> lines = new List<string> { "Id,Username,PasswordHash" };
            foreach (var t in list) lines.Add($"{t.Id},{t.Username},{t.PasswordHash}");
            File.WriteAllLines(TeachersPath, lines, Encoding.UTF8);
        }
    }
}