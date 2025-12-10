using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoursesConsoleApp_1
{
    enum Role { None, Admin, Student, Teacher }

    class UserSession
    {
        public Role Role { get; set; } = Role.None;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    class Program
    {
        static UserSession CurrentSession = new UserSession();

        const string ADMIN_USERNAME = "admin";
        const string ADMIN_PASSWORD = "admin123";

        #region Data
        static List<Course> courses = new List<Course>();
        static List<Students> students = new List<Students>();
        static List<Teachers> teachers = new List<Teachers>();
        #endregion

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            RenderIntro();
            AskRoleAndAuthenticate();
            ShowMainMenu();
        }

        #region Console helpers
        static int GetIntInput(string prompt = "Введіть число:")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt + " ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int res))
            {
                Console.ResetColor();
                return res;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ви ввели не ціле число. Спробуйте ще раз.");
            Console.ResetColor();
            return GetIntInput(prompt);
        }

        static double GetDoubleInput(string prompt = "Введіть число:")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt + " ");
            string input = Console.ReadLine();
            if (double.TryParse(input, out double res) && res >= 0)
            {
                Console.ResetColor();
                return res;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Невірне значення. Введіть невід'ємне число (можна 0).");
            Console.ResetColor();
            return GetDoubleInput(prompt);
        }

        static string GetStringInput(string prompt = "Введіть текст:")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt + " ");
            string input = Console.ReadLine();
            Console.ResetColor();
            return input ?? "";
        }

        static void RenderIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("===========================================");
            Console.WriteLine("============== Курси навчання =============");
            Console.WriteLine("===========================================");
            Console.ResetColor();
        }
        #endregion

        #region Role & Auth
        static void AskRoleAndAuthenticate()
        {
            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Оберіть роль для входу:");
                Console.ResetColor();
                Console.WriteLine("1. Адміністратор");
                Console.WriteLine("2. Студент");
                Console.WriteLine("3. Викладач");
                Console.WriteLine("4. Вихід");
                int rc = GetIntInput("Виберіть роль (1-4):");
                switch (rc)
                {
                    case 1:
                        if (AuthenticateAdmin())
                        {
                            CurrentSession.Role = Role.Admin;
                            CurrentSession.Username = ADMIN_USERNAME;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Успішний вхід як Адмін.");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Невірний логін/пароль для адміністратора.");
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        if (AuthenticateAccount(Role.Student)) return;
                        break;
                    case 3:
                        if (AuthenticateAccount(Role.Teacher)) return;
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static bool AuthenticateAdmin()
        {
            string login = GetStringInput("Логін:");
            string pass = GetStringInput("Пароль:");
            return login == ADMIN_USERNAME && pass == ADMIN_PASSWORD;
        }

        static bool AuthenticateAccount(Role role)
        {
            string roleName = role == Role.Student ? "Студент" : "Викладач";
            Console.WriteLine($"\n=== Авторизація: {roleName} ===");
            string login = GetStringInput("Логін:");
            string pass = GetStringInput("Пароль:");

            var list = role == Role.Student ? (List<object>)students.Cast<object>().ToList() : teachers.Cast<object>().ToList();
            var acc = list.FirstOrDefault(x => (role == Role.Student ? ((Students)x).Username : ((Teachers)x).Username) == login);

            if (acc != null)
            {
                string storedPass = role == Role.Student ? ((Students)acc).Password : ((Teachers)acc).Password;
                if (storedPass == pass)
                {
                    CurrentSession.Username = login;
                    CurrentSession.Role = role;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Успішний вхід як {roleName} ({login}).");
                    Console.ResetColor();
                    return true;
                }
            }

            // реєстрація нового акаунта
            Console.WriteLine($"Акаунт не знайдено. Створити новий акаунт? (1-Так, 0-Ні)");
            int choice = GetIntInput();
            if (choice == 1)
            {
                if (role == Role.Student) students.Add(new Students(login, pass));
                else teachers.Add(new Teachers(login, pass));
                CurrentSession.Username = login;
                CurrentSession.Role = role;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Акаунт створено як {roleName} ({login}).");
                Console.ResetColor();
                return true;
            }
            return false;
        }
        #endregion

        #region Main Menu
        static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Головне меню:");
                Console.ResetColor();
                Console.WriteLine("1. Курси");
                Console.WriteLine("2. Студенти");
                Console.WriteLine("3. Викладачі");
                Console.WriteLine("4. Змінити користувача / Вийти");
                int choice = GetIntInput("Виберіть пункт меню:");
                switch (choice)
                {
                    case 1: ShowCourseMenu(); break;
                    case 2: ShowStudentMenu(); break;
                    case 3: ShowTeacherMenu(); break;
                    case 4:
                        CurrentSession = new UserSession();
                        RenderIntro();
                        AskRoleAndAuthenticate();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір.");
                        Console.ResetColor();
                        break;
                }
            }
        }
        #endregion
        #region Courses
        static void ShowCourseMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========= МЕНЮ КУРСИ =========");
                Console.ResetColor();
                Console.WriteLine("1. Додати курс");
                Console.WriteLine("2. Вивести всі курси");
                Console.WriteLine("3. Пошук курсу");
                Console.WriteLine("4. Видалити курс");
                Console.WriteLine("5. Сортування курсів");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Повернутись у головне меню");

                int choose = GetIntInput("Виберіть дію:");

                switch (choose)
                {
                    case 1: AddCourses(); break;
                    case 2: PrintAllCourses(); break;
                    case 3: SearchCourseByName(); break;
                    case 4: DeleteCourse(); break;
                    case 5: SortCoursesMenu(); break;
                    case 6: CourseStatistics(); break;
                    case 7: return;
                    default: Console.WriteLine("Невірний пункт"); break;
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу щоб повернутись...");
                Console.ReadKey();
            }
        }

        static void AddCourses()
        {
            while (true)
            {
                string name = GetStringInput("Назва курсу:");
                double price = GetDoubleInput("Ціна:");
                courses.Add(new Course(name, price));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Курс додано!");
                Console.ResetColor();

                int c = GetIntInput("Додати ще? (1-Так, 0-Ні):");
                if (c == 0) break;
            }
        }

        static void PrintAllCourses()
        {
            Console.WriteLine("\n=== Курси ===");
            if (courses.Count == 0)
            {
                Console.WriteLine("Немає курсів.");
                return;
            }

            Console.WriteLine("{0,-5}{1,-30}{2,10}", "№", "Назва", "Ціна");
            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine("{0,-5}{1,-30}{2,10}", i + 1, courses[i].Name, courses[i].Price);
            }
        }

        static void SearchCourseByName()
        {
            string name = GetStringInput("Введіть назву курсу для пошуку:");
            bool found = false;
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{i + 1}. {courses[i].Name} - {courses[i].Price} грн");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Курс не знайдено.");
        }

        static void DeleteCourse()
        {
            PrintAllCourses();
            int index = GetIntInput("Введіть номер курсу для видалення:") - 1;
            if (index >= 0 && index < courses.Count)
            {
                courses.RemoveAt(index);
                Console.WriteLine("Курс видалено.");
            }
            else
            {
                Console.WriteLine("Невірний індекс.");
            }
        }

        static void SortCoursesMenu()
        {
            Console.WriteLine("1. Сортування за назвою (алфавіт)");
            Console.WriteLine("2. Сортування за ціною");
            Console.WriteLine("3. Бульбашкове сортування за ціною");
            int choose = GetIntInput("Виберіть тип сортування:");
            switch (choose)
            {
                case 1:
                    courses.Sort((a, b) => a.Name.CompareTo(b.Name));
                    Console.WriteLine("Сортування за назвою виконано.");
                    break;
                case 2:
                    courses.Sort((a, b) => a.Price.CompareTo(b.Price));
                    Console.WriteLine("Сортування за ціною виконано.");
                    break;
                case 3:
                    BubbleSortCourses();
                    Console.WriteLine("Бульбашкове сортування виконано.");
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        static void BubbleSortCourses()
        {
            for (int i = 0; i < courses.Count - 1; i++)
            {
                for (int j = 0; j < courses.Count - i - 1; j++)
                {
                    if (courses[j].Price > courses[j + 1].Price)
                    {
                        var temp = courses[j];
                        courses[j] = courses[j + 1];
                        courses[j + 1] = temp;
                    }
                }
            }
        }

        static void CourseStatistics()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Немає курсів для статистики.");
                return;
            }

            double min = courses.Min(c => c.Price);
            double max = courses.Max(c => c.Price);
            double sum = courses.Sum(c => c.Price);
            double avg = courses.Average(c => c.Price);

            Console.WriteLine($"Кількість курсів: {courses.Count}");
            Console.WriteLine($"Мінімальна ціна: {min}");
            Console.WriteLine($"Максимальна ціна: {max}");
            Console.WriteLine($"Сума цін: {sum}");
            Console.WriteLine($"Середня ціна: {avg}");
        }
        #endregion

        #region Students
        static void ShowStudentMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========= МЕНЮ СТУДЕНТИ =========");
                Console.ResetColor();
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Вивести всіх студентів");
                Console.WriteLine("3. Пошук студента");
                Console.WriteLine("4. Видалити студента");
                Console.WriteLine("5. Сортування студентів");
                Console.WriteLine("6. Повернутись у головне меню");

                int ch = GetIntInput("Виберіть дію:");
                switch (ch)
                {
                    case 1: AddStudents(); break;
                    case 2: PrintAllStudents(); break;
                    case 3: SearchStudent(); break;
                    case 4: DeleteStudent(); break;
                    case 5: SortStudentsMenu(); break;
                    case 6: return;
                    default: Console.WriteLine("Невірний пункт"); break;
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу щоб повернутись...");
                Console.ReadKey();
            }
        }

        static void AddStudents()
        {
            while (true)
            {
                string username = GetStringInput("Логін студента:");
                string pass = GetStringInput("Пароль:");
                students.Add(new Students(username, pass));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Студент доданий!");
                Console.ResetColor();

                int c = GetIntInput("Додати ще? (1-Так, 0-Ні):");
                if (c == 0) break;
            }
        }

        static void PrintAllStudents()
        {
            Console.WriteLine("\n=== Студенти ===");
            if (students.Count == 0)
            {
                Console.WriteLine("Немає студентів.");
                return;
            }
            Console.WriteLine("{0,-5}{1,-20}", "№", "Логін");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine("{0,-5}{1,-20}", i + 1, students[i].Username);
            }
        }

        static void SearchStudent()
        {
            string name = GetStringInput("Введіть логін студента для пошуку:");
            bool found = false;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].Username.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{i + 1}. {students[i].Username}");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Студент не знайдено.");
        }

        static void DeleteStudent()
        {
            PrintAllStudents();
            int index = GetIntInput("Введіть номер студента для видалення:") - 1;
            if (index >= 0 && index < students.Count)
            {
                students.RemoveAt(index);
                Console.WriteLine("Студента видалено.");
            }
            else Console.WriteLine("Невірний індекс.");
        }

        static void SortStudentsMenu()
        {
            Console.WriteLine("1. Сортування за логіном");
            int ch = GetIntInput("Виберіть тип сортування:");
            if (ch == 1)
            {
                students.Sort((a, b) => a.Username.CompareTo(b.Username));
                Console.WriteLine("Сортування виконано.");
            }
            else Console.WriteLine("Невірний вибір.");
        }
        #endregion

        #region Teachers
        static void ShowTeacherMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========= МЕНЮ ВИКЛАДАЧІ =========");
                Console.ResetColor();
                Console.WriteLine("1. Додати викладача");
                Console.WriteLine("2. Вивести всіх викладачів");
                Console.WriteLine("3. Пошук викладача");
                Console.WriteLine("4. Видалити викладача");
                Console.WriteLine("5. Сортування викладачів");
                Console.WriteLine("6. Повернутись у головне меню");

                int ch = GetIntInput("Виберіть дію:");
                switch (ch)
                {
                    case 1: AddTeachers(); break;
                    case 2: PrintAllTeachers(); break;
                    case 3: SearchTeacher(); break;
                    case 4: DeleteTeacher(); break;
                    case 5: SortTeachersMenu(); break;
                    case 6: return;
                    default: Console.WriteLine("Невірний пункт"); break;
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу щоб повернутись...");
                Console.ReadKey();
            }
        }

        static void AddTeachers()
        {
            while (true)
            {
                string username = GetStringInput("Логін викладача:");
                string pass = GetStringInput("Пароль:");
                teachers.Add(new Teachers(username, pass));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Викладача додано!");
                Console.ResetColor();

                int c = GetIntInput("Додати ще? (1-Так, 0-Ні):");
                if (c == 0) break;
            }
        }

        static void PrintAllTeachers()
        {
            Console.WriteLine("\n=== Викладачі ===");
            if (teachers.Count == 0)
            {
                Console.WriteLine("Немає викладачів.");
                return;
            }
            Console.WriteLine("{0,-5}{1,-20}", "№", "Логін");
            for (int i = 0; i < teachers.Count; i++)
            {
                Console.WriteLine("{0,-5}{1,-20}", i + 1, teachers[i].Username);
            }
        }

        static void SearchTeacher()
        {
            string name = GetStringInput("Введіть логін викладача для пошуку:");
            bool found = false;
            for (int i = 0; i < teachers.Count; i++)
            {
                if (teachers[i].Username.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{i + 1}. {teachers[i].Username}");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Викладача не знайдено.");
        }

        static void DeleteTeacher()
        {
            PrintAllTeachers();
            int index = GetIntInput("Введіть номер викладача для видалення:") - 1;
            if (index >= 0 && index < teachers.Count)
            {
                teachers.RemoveAt(index);
                Console.WriteLine("Викладача видалено.");
            }
            else
            {
                Console.WriteLine("Невірний індекс.");
            }
        }

        static void SortTeachersMenu()
        {
            Console.WriteLine("1. Сортування за логіном");
            int ch = GetIntInput("Виберіть тип сортування:");
            if (ch == 1)
            {
                teachers.Sort((a, b) => a.Username.CompareTo(b.Username));
                Console.WriteLine("Сортування виконано.");
            }
            else
            {
                Console.WriteLine("Невірний вибір.");
            }
        }
        #endregion
    }
}

