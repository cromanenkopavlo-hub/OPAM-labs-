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

        static void AuthMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===  КУРСИ ===");
            Console.ResetColor();
            Console.WriteLine("1.Адмін \n2.Студент \n3.Викладач \n0.Вихід");
            int choice = GetInt();
            if (choice == 0) Environment.Exit(0);

            string login = GetStr("Логін/Email:");
            string pass = GetStr("Пароль:");

            if (choice == 1 && login == "admin" && pass == "admin123") { CurrentRole = Role.Admin; CurrentUser = "Admin"; }
            else HandleAuth(choice == 2 ? Role.Student : Role.Teacher, login, pass);
        }

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

            if (exists) { CurrentRole = role; CurrentUser = login; }
            else
            {
                Console.WriteLine("Невірно. Зареєструвати? (1-Так)");
                if (GetInt() == 1)
                {
                    string path = (role == Role.Student) ? FileDataHandler.StudentsPath : FileDataHandler.TeachersPath;
                    File.AppendAllLines(path, new[] { $"{FileDataHandler.GetNextId(path)},{login},{hash}" });
                    CurrentRole = role; CurrentUser = login;
                }
            }
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Користувач: {CurrentUser} [{CurrentRole}]");
            Console.WriteLine("1. КУРСИ \n2. СТУДЕНТИ \n3. ВИКЛАДАЧІ \n4. Вихід");
            switch (GetInt())
            {
                case 1: EntityMenu("Курси"); break;
                case 2: EntityMenu("Студенти"); break;
                case 3: EntityMenu("Викладачі"); break;
                case 4: CurrentRole = Role.None; break;
            }
        }

        static void EntityMenu(string type)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- УПРАВЛІННЯ: {type.ToUpper()} ---");
                Console.WriteLine("1. Список \n2. Пошук \n3. Сортування \n4. Статистика");
                if (CurrentRole == Role.Admin) Console.WriteLine("5. Додати \n6. Видалити");
                Console.WriteLine("0. Назад");

                int ch = GetInt();
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
                case 2:
                    string q = GetStr("Пошук за назвою:").ToLower();
                    List<Course> filtered = new List<Course>();
                    foreach (var c in list) if (c.Name.ToLower().Contains(q)) filtered.Add(c);
                    ShowCourses(filtered);
                    break;
                case 3:
                    Console.WriteLine("1. За назвою \n2. За ціною");
                    if (GetInt() == 1) list.Sort((a, b) => a.Name.CompareTo(b.Name));
                    else list.Sort((a, b) => a.Price.CompareTo(b.Price));
                    ShowCourses(list);
                    break;
                case 4:
                    if (list.Count == 0) return;
                    double sum = 0, max = 0;
                    foreach (var c in list) { sum += c.Price; if (c.Price > max) max = c.Price; }
                    Console.WriteLine($"Всього: {list.Count} | Сер. ціна: {sum / list.Count:F2} | Max: {max}");
                    break;
                case 5: if (CurrentRole == Role.Admin) AddCourse(); break;
                case 6: if (CurrentRole == Role.Admin) DeleteEntity("Курси"); break;
            }
        }

        static void HandleStudentActions(int ch)
        {
            var list = FileDataHandler.ReadStudents();
            switch (ch)
            {
                case 1: ShowStudents(list); break;
                case 2:
                    string q = GetStr("Пошук Email:").ToLower();
                    List<Students> filtered = new List<Students>();
                    foreach (var s in list) if (s.Email.ToLower().Contains(q)) filtered.Add(s);
                    ShowStudents(filtered);
                    break;
                case 3:
                    list.Sort((a, b) => a.Email.CompareTo(b.Email));
                    ShowStudents(list);
                    break;
            }
        }

        static void HandleTeacherActions(int ch)
        {
            var list = FileDataHandler.ReadTeachers();
            switch (ch)
            {
                case 1: ShowTeachers(list); break;
                case 2:
                    string q = GetStr("Пошук за логіном:").ToLower();
                    List<Teachers> filtered = new List<Teachers>();
                    foreach (var t in list) if (t.Username.ToLower().Contains(q)) filtered.Add(t);
                    ShowTeachers(filtered);
                    break;
                case 3:
                    list.Sort((a, b) => a.Username.CompareTo(b.Username));
                    ShowTeachers(list);
                    break;
            }
        }

        static void ShowCourses(List<Course> l)
        {
            Console.WriteLine("\n{0,-5} | {1,-20} | {2,-10}", "ID", "Назва", "Ціна");
            foreach (var c in l) Console.WriteLine("{0,-5} | {1,-20} | {2,10:F2}", c.Id, c.Name, c.Price);
        }
        static void ShowStudents(List<Students> l) { foreach (var s in l) Console.WriteLine($"ID: {s.Id} | Email: {s.Email}"); }
        static void ShowTeachers(List<Teachers> l) { foreach (var t in l) Console.WriteLine($"ID: {t.Id} | Логін: {t.Username}"); }

        static void AddCourse()
        {
            string n = GetStr("Назва:"); double p = GetDouble("Ціна:");
            File.AppendAllLines(FileDataHandler.CoursesPath, new[] { $"{FileDataHandler.GetNextId(FileDataHandler.CoursesPath)},{n},{p}" });
        }

        static void DeleteEntity(string type)
        {
            int id = GetInt("Введіть ID для видалення:");
            if (type == "Курси")
            {
                List<Course> all = FileDataHandler.ReadCourses();
                List<Course> updated = new List<Course>();
                foreach (var c in all) if (c.Id != id) updated.Add(c);
                FileDataHandler.SaveCourses(updated);
            }
        }
        static string GetStr(string p) { Console.Write(p + " "); return Console.ReadLine() ?? ""; }
        static int GetInt(string p = "Дія:") { Console.Write(p + " "); int.TryParse(Console.ReadLine(), out int r); return r; }
        static double GetDouble(string p) { Console.Write(p + " "); double.TryParse(Console.ReadLine(), out double r); return r; }
    }
}