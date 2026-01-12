using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CoursesConsoleApp_1
{
    class Program
    {
        static Role CurrentRole = Role.None;
        static string CurrentUser = "";

        /// <summary>
        /// Точка входу в програму. Налаштовує кодування консолі, 
        /// ініціалізує файлову систему та запускає нескінченний цикл роботи програми.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            FileDataHandler.InitializeFiles();
            while (true)
            {
                if (CurrentRole == Role.None) AuthMenu();
                else MainMenu();
            }
        }

        /// <summary>
        /// Відображає початкове меню для вибору ролі (Адмін, Студент, Викладач) 
        /// та зчитує облікові дані для авторизації.
        /// </summary>
        static void AuthMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===  КУРСИ НАВЧАННЯ ===");
            Console.ResetColor();
            Console.WriteLine("1.Адмін \n2.Студент \n3.Викладач \n0.Вихід");
            int choice = GetInt("Оберіть пункт (0-3):", 0, 3);
            if (choice == 0) Environment.Exit(0);

            string login = GetStr("Логін/Email:");
            string pass = GetStr("Пароль:");

            if (choice == 1 && login == "a" && pass == "1")
            {
                CurrentRole = Role.Admin;
                CurrentUser = "Admin";
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n[СИСТЕМА]: Ви успішно увійшли як Адміністратор!");
                Console.ResetColor();
                Console.WriteLine("Натисніть Enter, щоб продовжити...");
                Console.ReadLine();
            }
            else HandleAuth(choice == 2 ? Role.Student : Role.Teacher, login, pass);
        }

        /// <summary>
        /// Виконує перевірку введених даних користувача. Порівнює хеш пароля 
        /// із записами у CSV-файлах та пропонує реєстрацію, якщо юзера не знайдено.
        /// </summary>
        static void HandleAuth(Role role, string login, string pass)
        {
            string hash = FileDataHandler.HashPassword(pass);
            bool exists = false;

            if (role == Role.Student)
            {
                foreach (var s in FileDataHandler.ReadStudents())
                    if (s.Email == login && s.PasswordHash == hash) { exists = true; break; }
            }
            else
            {
                foreach (var t in FileDataHandler.ReadTeachers())
                    if (t.Username == login && t.PasswordHash == hash) { exists = true; break; }
            }

            if (exists)
            {
                CurrentRole = role;
                CurrentUser = login;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n[СИСТЕМА]: Вітаємо, {login}! Ви успішно увійшли.");
                Console.ResetColor();
                Console.WriteLine("Натисніть Enter, щоб продовжити...");
                Console.ReadLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[ПОМИЛКА]: Користувача не знайдено або пароль невірний.");
                Console.ResetColor();
                Console.WriteLine("Бажаєте зареєструвати новий акаунт? (1 - Так, 2 - Ні)");
                if (GetInt("Дія (1-2):", 1, 2) == 1)
                {
                    string path = (role == Role.Student) ? FileDataHandler.StudentsPath : FileDataHandler.TeachersPath;
                    File.AppendAllLines(path, new[] { $"{FileDataHandler.GetNextId(path)},{login},{hash}" });
                    CurrentRole = role; CurrentUser = login;
                    Console.WriteLine("Реєстрація успішна! Вхід виконано.");
                    Console.WriteLine("Натисніть Enter, щоб продовжити...");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Головне меню авторизованого користувача. Дозволяє обрати категорію 
        /// для роботи (Курси, Студенти, Викладачі) або вийти з акаунта.
        /// </summary>
        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Користувач: {CurrentUser} [{CurrentRole}]");
            Console.WriteLine("1. КУРСИ \n2. СТУДЕНТИ \n3. ВИКЛАДАЧІ \n4. Вихід");
            switch (GetInt("Оберіть пункт (1-4):", 1, 4))
            {
                case 1: EntityMenu("Курси"); break;
                case 2: EntityMenu("Студенти"); break;
                case 3: EntityMenu("Викладачі"); break;
                case 4: CurrentRole = Role.None; break;
            }
        }

        /// <summary>
        /// Універсальне меню для роботи з конкретним типом даних. 
        /// Відображає доступні дії залежно від прав доступу.
        /// </summary>
        static void EntityMenu(string type)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- УПРАВЛІННЯ: {type.ToUpper()} ---");
                Console.WriteLine("1. Список \n2. Пошук (текст/ID) \n3. Сортування \n4. Статистика \n5. Фільтрація");

                int maxVal = 5;
                if (CurrentRole == Role.Admin)
                {
                    Console.WriteLine("6. Додати \n7. Видалити \n8. Редагувати");
                    maxVal = 8;
                }
                Console.WriteLine("0. Назад");

                int ch = GetInt($"Оберіть пункт (0-{maxVal}):", 0, maxVal);
                if (ch == 0) break;

                if (type == "Курси") HandleCourseActions(ch);
                else if (type == "Студенти") HandleStudentActions(ch);
                else if (type == "Викладачі") HandleTeacherActions(ch);
                Console.WriteLine("\nНатисніть Enter..."); Console.ReadLine();
            }
        }

        static void HandleCourseActions(int ch)
        {
            var list = FileDataHandler.ReadCourses();
            switch (ch)
            {
                case 1: ShowCourses(list); break;
                case 2: SearchEntity("Курси"); break;
                case 3:
                    Console.WriteLine("1. За назвою \n2. За ціною");
                    if (GetInt("Варіант (1-2):", 1, 2) == 1) list.Sort((a, b) => a.Name.CompareTo(b.Name));
                    else list.Sort((a, b) => a.Price.CompareTo(b.Price));
                    ShowCourses(list);
                    break;
                case 4:
                    if (list.Count == 0) return;
                    double sum = 0, max = 0;
                    foreach (var c in list) { sum += c.Price; if (c.Price > max) max = c.Price; }
                    Console.WriteLine($"Всього: {list.Count} | Сер. ціна: {sum / list.Count:F2} | Max: {max}");
                    break;
                case 5: FilterEntity("Курси"); break;
                case 6: if (CurrentRole == Role.Admin) AddEntity("Курси"); break;
                case 7: if (CurrentRole == Role.Admin) DeleteEntity("Курси"); break;
                case 8: if (CurrentRole == Role.Admin) UpdateEntity("Курси"); break;
            }
        }

        static void HandleStudentActions(int ch)
        {
            var list = FileDataHandler.ReadStudents();
            switch (ch)
            {
                case 1: ShowStudents(list); break;
                case 2: SearchEntity("Студенти"); break;
                case 3:
                    list.Sort((a, b) => a.Email.CompareTo(b.Email));
                    ShowStudents(list);
                    break;
                case 5: FilterEntity("Студенти"); break;
                case 6: if (CurrentRole == Role.Admin) AddEntity("Студенти"); break;
                case 7: if (CurrentRole == Role.Admin) DeleteEntity("Студенти"); break;
                case 8: if (CurrentRole == Role.Admin) UpdateEntity("Студенти"); break;
            }
        }

        static void HandleTeacherActions(int ch)
        {
            var list = FileDataHandler.ReadTeachers();
            switch (ch)
            {
                case 1: ShowTeachers(list); break;
                case 2: SearchEntity("Викладачі"); break;
                case 3:
                    list.Sort((a, b) => a.Username.CompareTo(b.Username));
                    ShowTeachers(list);
                    break;
                case 5: FilterEntity("Викладачі"); break;
                case 6: if (CurrentRole == Role.Admin) AddEntity("Викладачі"); break;
                case 7: if (CurrentRole == Role.Admin) DeleteEntity("Викладачі"); break;
                case 8: if (CurrentRole == Role.Admin) UpdateEntity("Викладачі"); break;
            }
        }

        /// <summary>
        /// Виконує пошук за текстом або за ID та виводить помилку, якщо нічого не знайдено.
        /// </summary>
        static void SearchEntity(string type)
        {
            Console.WriteLine("1. Пошук за текстом \n2. Пошук за ID");
            int mode = GetInt("Режим:", 1, 2);
            bool found = false;

            if (type == "Курси")
            {
                var list = FileDataHandler.ReadCourses();
                List<Course> res = new List<Course>();
                if (mode == 1) { string q = GetStr("Назва:").ToLower(); foreach (var c in list) if (c.Name.ToLower().Contains(q)) res.Add(c); }
                else { int id = GetInt("ID:"); foreach (var c in list) if (c.Id == id) res.Add(c); }
                if (res.Count > 0) { ShowCourses(res); found = true; }
            }
            else if (type == "Студенти")
            {
                var list = FileDataHandler.ReadStudents();
                List<Students> res = new List<Students>();
                if (mode == 1) { string q = GetStr("Email:").ToLower(); foreach (var s in list) if (s.Email.ToLower().Contains(q)) res.Add(s); }
                else { int id = GetInt("ID:"); foreach (var s in list) if (s.Id == id) res.Add(s); }
                if (res.Count > 0) { ShowStudents(res); found = true; }
            }
            else
            {
                var list = FileDataHandler.ReadTeachers();
                List<Teachers> res = new List<Teachers>();
                if (mode == 1) { string q = GetStr("Логін:").ToLower(); foreach (var t in list) if (t.Username.ToLower().Contains(q)) res.Add(t); }
                else { int id = GetInt("ID:"); foreach (var t in list) if (t.Id == id) res.Add(t); }
                if (res.Count > 0) { ShowTeachers(res); found = true; }
            }

            if (!found) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Помилка: Запис не знайдено."); Console.ResetColor(); }
        }

        /// <summary>
        /// Фільтрує записи за першою літерою або за ціною (для курсів) через звичайні цикли.
        /// </summary>
        static void FilterEntity(string type)
        {
            Console.WriteLine("1. За першою літерою \n2. За параметром (ціна)");
            int opt = GetInt("Вибір:", 1, 2);
            bool found = false;

            if (opt == 1)
            {
                string letter = GetStr("Введіть літеру:").ToLower();
                if (letter == "") return;
                string target = letter.Substring(0, 1);

                if (type == "Курси") { List<Course> r = new List<Course>(); foreach (var c in FileDataHandler.ReadCourses()) if (c.Name.ToLower().StartsWith(target)) r.Add(c); if (r.Count > 0) { ShowCourses(r); found = true; } }
                else if (type == "Студенти") { List<Students> r = new List<Students>(); foreach (var s in FileDataHandler.ReadStudents()) if (s.Email.ToLower().StartsWith(target)) r.Add(s); if (r.Count > 0) { ShowStudents(r); found = true; } }
                else { List<Teachers> r = new List<Teachers>(); foreach (var t in FileDataHandler.ReadTeachers()) if (t.Username.ToLower().StartsWith(target)) r.Add(t); if (r.Count > 0) { ShowTeachers(r); found = true; } }
            }
            else
            {
                if (type == "Курси") { double p = GetDouble("Ціна до:"); List<Course> r = new List<Course>(); foreach (var c in FileDataHandler.ReadCourses()) if (c.Price <= p) r.Add(c); if (r.Count > 0) { ShowCourses(r); found = true; } }
                else { string d = GetStr("Домен (напр. @gmail):").ToLower(); if (type == "Студенти") { List<Students> r = new List<Students>(); foreach (var s in FileDataHandler.ReadStudents()) if (s.Email.ToLower().Contains(d)) r.Add(s); if (r.Count > 0) { ShowStudents(r); found = true; } } }
            }

            if (!found) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Нічого не знайдено."); Console.ResetColor(); }
        }

        /// <summary>
        /// Форматує та виводить у консоль таблицю курсів із вирівнюванням колонок.
        /// </summary>
        static void ShowCourses(List<Course> l)
        {
            Console.WriteLine("\n{0,-5} | {1,-20} | {2,-10}", "ID", "Назва", "Ціна");
            foreach (var c in l) Console.WriteLine("{0,-5} | {1,-20} | {2,10:F2}", c.Id, c.Name, c.Price);
        }

        /// <summary>
        /// Виводить список студентів.
        /// </summary>
        static void ShowStudents(List<Students> l) { foreach (var s in l) Console.WriteLine($"ID: {s.Id} | Email: {s.Email}"); }

        /// <summary>
        /// Виводить список викладачів.
        /// </summary>
        static void ShowTeachers(List<Teachers> l) { foreach (var t in l) Console.WriteLine($"ID: {t.Id} | Логін: {t.Username}"); }

        /// <summary>
        /// Універсальний метод для додавання нових записів до відповідних файлів.
        /// </summary>
        static void AddEntity(string type)
        {
            if (type == "Курси") { string n = GetStr("Назва:"); double p = GetDouble("Ціна:", 0); File.AppendAllLines(FileDataHandler.CoursesPath, new[] { $"{FileDataHandler.GetNextId(FileDataHandler.CoursesPath)},{n},{p}" }); }
            else if (type == "Студенти") { string e = GetStr("Email:"); string h = FileDataHandler.HashPassword(GetStr("Пароль:")); File.AppendAllLines(FileDataHandler.StudentsPath, new[] { $"{FileDataHandler.GetNextId(FileDataHandler.StudentsPath)},{e},{h}" }); }
            else { string u = GetStr("Логін:"); string h = FileDataHandler.HashPassword(GetStr("Пароль:")); File.AppendAllLines(FileDataHandler.TeachersPath, new[] { $"{FileDataHandler.GetNextId(FileDataHandler.TeachersPath)},{u},{h}" }); }
            Console.WriteLine("Успішно додано!");
        }

        /// <summary>
        /// Оновлює дані існуючого запису за його ID без використання LINQ.
        /// </summary>
        static void UpdateEntity(string type)
        {
            int id = GetInt("ID для редагування:", 1);
            bool found = false;

            if (type == "Курси") { List<Course> list = FileDataHandler.ReadCourses(); for (int i = 0; i < list.Count; i++) if (list[i].Id == id) { found = true; string n = GetStr("Нова назва:"); if (n != "") list[i].Name = n; list[i].Price = GetDouble("Ціна:", 0); break; } if (found) FileDataHandler.SaveCourses(list); }
            else if (type == "Студенти") { List<Students> list = FileDataHandler.ReadStudents(); for (int i = 0; i < list.Count; i++) if (list[i].Id == id) { found = true; string e = GetStr("Новий Email:"); if (e != "") list[i].Email = e; break; } if (found) FileDataHandler.SaveStudents(list); }
            else { List<Teachers> list = FileDataHandler.ReadTeachers(); for (int i = 0; i < list.Count; i++) if (list[i].Id == id) { found = true; string u = GetStr("Новий логін:"); if (u != "") list[i].Username = u; break; } if (found) FileDataHandler.SaveTeachers(list); }

            if (found) Console.WriteLine("Дані оновлено!");
            else Console.WriteLine("Не знайдено.");
        }

        /// <summary>
        /// Видаляє запис із бази даних за вказаним ID. Повністю переписує CSV-файл.
        /// </summary>
        static void DeleteEntity(string type)
        {
            int id = GetInt("ID для видалення:", 1);
            bool found = false;

            if (type == "Курси") { List<Course> all = FileDataHandler.ReadCourses(); List<Course> upd = new List<Course>(); foreach (var c in all) if (c.Id != id) upd.Add(c); else found = true; FileDataHandler.SaveCourses(upd); }
            else if (type == "Студенти") { List<Students> all = FileDataHandler.ReadStudents(); List<Students> upd = new List<Students>(); foreach (var s in all) if (s.Id != id) upd.Add(s); else found = true; FileDataHandler.SaveStudents(upd); }
            else { List<Teachers> all = FileDataHandler.ReadTeachers(); List<Teachers> upd = new List<Teachers>(); foreach (var t in all) if (t.Id != id) upd.Add(t); else found = true; FileDataHandler.SaveTeachers(upd); }

            if (found) Console.WriteLine("Видалено!");
            else Console.WriteLine("ID не знайдено.");
        }

        /// <summary>
        /// Отримує текстовий рядок від користувача через консоль.
        /// </summary>
        static string GetStr(string p) { Console.Write(p + " "); return Console.ReadLine() ?? ""; }

        /// <summary>
        /// Запитує ціле число у користувача та виконує спробу його перетворення з перевіркою діапазону.
        /// </summary>
        static int GetInt(string p = "Дія:", int min = int.MinValue, int max = int.MaxValue)
        {
            int r;
            while (true) { Console.Write(p + " "); if (int.TryParse(Console.ReadLine(), out r) && r >= min && r <= max) return r; Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Помилка! Введіть число від {min} до {max}."); Console.ResetColor(); }
        }

        /// <summary>
        /// Отримує від користувача дробове число з перевіркою на формат і мінімум.
        /// </summary>
        static double GetDouble(string p, double min = 0)
        {
            double r;
            while (true) { Console.Write(p + " "); string input = Console.ReadLine()?.Replace('.', ',') ?? ""; if (double.TryParse(input, out r) && r >= min) return r; Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Помилка! Введіть коректне число (мін: {min})."); Console.ResetColor(); }
        }
    }
}
