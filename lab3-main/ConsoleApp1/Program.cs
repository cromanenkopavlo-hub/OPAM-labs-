using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

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
        // Адмін
        const string ADMIN_USERNAME = "admin";
        const string ADMIN_PASSWORD = "admin123";

        // 5 слотів для курсів
        private static Course c1 = new Course();
        private static Course c2 = new Course();
        private static Course c3 = new Course();
        private static Course c4 = new Course();
        private static Course c5 = new Course();

        // 5 слотів для студентів
        private static Students s1 = new Students();
        private static Students s2 = new Students();
        private static Students s3 = new Students();
        private static Students s4 = new Students();
        private static Students s5 = new Students();

        // 5 слотів для викладачів
        private static Teachers t1 = new Teachers();
        private static Teachers t2 = new Teachers();
        private static Teachers t3 = new Teachers();
        private static Teachers t4 = new Teachers();
        private static Teachers t5 = new Teachers();

        static UserSession CurrentSession = new UserSession();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            SeedEmptyData();
            RenderIntro();
            AskRoleAndAuthenticate();
            ShowMainMenu();
        }

        static void SeedEmptyData()
        {
            c1 = new Course();
            c2 = new Course();
            c3 = new Course();
            c4 = new Course();
            c5 = new Course();

            s1 = new Students();
            s2 = new Students();
            s3 = new Students();
            s4 = new Students();
            s5 = new Students();

            t1 = new Teachers();
            t2 = new Teachers();
            t3 = new Teachers();
            t4 = new Teachers();
            t5 = new Teachers();
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
                        if (AuthenticateAccount(Role.Student))
                        {
                            CurrentSession.Role = Role.Student;
                            return;
                        }
                        break;
                    case 3:
                        if (AuthenticateAccount(Role.Teacher))
                        {
                            CurrentSession.Role = Role.Teacher;
                            return;
                        }
                        break;
                    case 4:
                        Console.WriteLine("Бувай!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір ролі. Спробуйте ще раз.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static bool AuthenticateAdmin()
        {
            Console.WriteLine("\n=== Авторизація: Адміністратор ===");
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


            var (foundId, acc) = FindAccountByUsername(role, login);



            if (acc != null && !IsAccountEmpty(acc, role) && GetPassword(acc, role) == pass)
            {
                CurrentSession.Username = login;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Успішний вхід як {roleName} ({login}).");
                Console.ResetColor();
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Акаунт не знайдено або невірний пароль.");
            Console.ResetColor();


            int freeSlot = FirstEmptyAccountSlot(role);
            if (freeSlot == -1)
            {
                Console.WriteLine("Немає вільних слотів для реєстрації. Зверніться до адміністратора.");
                return false;
            }

            Console.WriteLine($"Бажаєте зареєструвати новий акаунт у вільному слоті #{freeSlot}? (1 - Так, 0 - Ні)");
            int choice = GetIntInput("Ваш вибір (1/0):");
            if (choice == 1)
            {
                RegisterAccountInSlot(role, freeSlot, login, pass);
                CurrentSession.Username = login;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Акаунт створено як {roleName} ({login}).");
                Console.ResetColor();
                return true;
            }

            return false;
        }



        static (int, object) FindAccountByUsername(Role role, string username)
        {
            if (role == Role.Student)
            {
                if (!s1.IsEmpty() && s1.Username == username) return (1, s1);
                if (!s2.IsEmpty() && s2.Username == username) return (2, s2);
                if (!s3.IsEmpty() && s3.Username == username) return (3, s3);
                if (!s4.IsEmpty() && s4.Username == username) return (4, s4);
                if (!s5.IsEmpty() && s5.Username == username) return (5, s5);
            }
            else
            {
                if (!t1.IsEmpty() && t1.Username == username) return (1, t1);
                if (!t2.IsEmpty() && t2.Username == username) return (2, t2);
                if (!t3.IsEmpty() && t3.Username == username) return (3, t3);
                if (!t4.IsEmpty() && t4.Username == username) return (4, t4);
                if (!t5.IsEmpty() && t5.Username == username) return (5, t5);
            }
            return (-1, null);
        }

        static bool IsAccountEmpty(object acc, Role role)
        {
            if (acc == null) return true;
            return role == Role.Student ? ((Students)acc).IsEmpty() : ((Teachers)acc).IsEmpty();
        }

        static string GetPassword(object acc, Role role)
        {
            if (acc == null) return "";
            return role == Role.Student ? ((Students)acc).Password : ((Teachers)acc).Password;
        }

        static int FirstEmptyAccountSlot(Role role)
        {
            if (role == Role.Student)
            {
                if (s1.IsEmpty()) return 1;
                if (s2.IsEmpty()) return 2;
                if (s3.IsEmpty()) return 3;
                if (s4.IsEmpty()) return 4;
                if (s5.IsEmpty()) return 5;
            }
            else
            {
                if (t1.IsEmpty()) return 1;
                if (t2.IsEmpty()) return 2;
                if (t3.IsEmpty()) return 3;
                if (t4.IsEmpty()) return 4;
                if (t5.IsEmpty()) return 5;
            }
            return -1;
        }

        static object GetAccountById(Role role, int id)
        {
            if (role == Role.Student)
            {
                return id switch
                {
                    1 => s1,
                    2 => s2,
                    3 => s3,
                    4 => s4,
                    5 => s5,
                    _ => null
                };
            }
            else
            {
                return id switch
                {
                    1 => t1,
                    2 => t2,
                    3 => t3,
                    4 => t4,
                    5 => t5,
                    _ => null
                };
            }
        }

        static void SetAccountById(Role role, int id, object acc)
        {
            if (role == Role.Student)
            {
                switch (id)
                {
                    case 1: s1 = (Students)acc; break;
                    case 2: s2 = (Students)acc; break;
                    case 3: s3 = (Students)acc; break;
                    case 4: s4 = (Students)acc; break;
                    case 5: s5 = (Students)acc; break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1: t1 = (Teachers)acc; break;
                    case 2: t2 = (Teachers)acc; break;
                    case 3: t3 = (Teachers)acc; break;
                    case 4: t4 = (Teachers)acc; break;
                    case 5: t5 = (Teachers)acc; break;
                }
            }
        }

        static void RegisterAccountInSlot(Role role, int id, string username, string password)
        {
            if (role == Role.Student)
                SetAccountById(role, id, new Students(username, password));
            else
                SetAccountById(role, id, new Teachers(username, password));
        }

        static void PrintAccounts(Role role)
        {
            Console.WriteLine(role == Role.Student ? "\n=== Студенти ===" : "\n=== Викладачі ===");
            for (int i = 1; i <= 5; i++)
            {
                var a = GetAccountById(role, i);
                if (a != null && !IsAccountEmpty(a, role))
                {
                    string username = role == Role.Student ? ((Students)a).Username : ((Teachers)a).Username;
                    Console.WriteLine($"{i}. {username}");
                }
            }
        }
        #endregion

        #region Course slot helpers
        static int FirstEmptyCourseSlot()
        {
            if (c1.IsEmpty()) return 1;
            if (c2.IsEmpty()) return 2;
            if (c3.IsEmpty()) return 3;
            if (c4.IsEmpty()) return 4;
            if (c5.IsEmpty()) return 5;
            return -1;
        }

        static Course GetCourseById(int id)
        {
            return id switch
            {
                1 => c1,
                2 => c2,
                3 => c3,
                4 => c4,
                5 => c5,
                _ => null
            };
        }

        static void SetCourseById(int id, Course course)
        {
            switch (id)
            {
                case 1: c1 = course; break;
                case 2: c2 = course; break;
                case 3: c3 = course; break;
                case 4: c4 = course; break;
                case 5: c5 = course; break;
            }
        }

        static void PrintAllCourses()
        {
            Console.WriteLine("\n=== Курси ===");
            if (!c1.IsEmpty()) Console.WriteLine($"1. {c1.Name} - {c1.Price} грн");
            if (!c2.IsEmpty()) Console.WriteLine($"2. {c2.Name} - {c2.Price} грн");
            if (!c3.IsEmpty()) Console.WriteLine($"3. {c3.Name} - {c3.Price} грн");
            if (!c4.IsEmpty()) Console.WriteLine($"4. {c4.Name} - {c4.Price} грн");
            if (!c5.IsEmpty()) Console.WriteLine($"5. {c5.Name} - {c5.Price} грн");
        }
        #endregion

        #region Menus
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
                Console.WriteLine("4. Замовлення");
                Console.WriteLine("5. Пошук");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Змінити користувача / Вийти");
                int choice = GetIntInput("Виберіть пункт меню:");
                switch (choice)
                {
                    case 1: ShowCourseMenu(); break;
                    case 2: ShowStudentMenu(); break;
                    case 3: ShowTeacherMenu(); break;
                    case 4: ShowOrderMenu(); break;
                    case 5: ShowSearch(); break;
                    case 6: ShowStatistics(); break;
                    case 7:
                        Console.WriteLine();
                        Console.WriteLine("1. Змінити користувача");
                        Console.WriteLine("2. Вийти");
                        int sub = GetIntInput("Виберіть (1-2):");
                        if (sub == 1)
                        {
                            CurrentSession = new UserSession();
                            RenderIntro();
                            AskRoleAndAuthenticate();
                        }
                        else Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        #region Courses menu
        static void ShowCourseMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========= МЕНЮ КУРСИ =========");
                Console.ResetColor();
                Console.WriteLine("1. Переглянути всі курси");
                if (CurrentSession.Role == Role.Admin)
                {
                    Console.WriteLine("2. Додати новий курс");
                    Console.WriteLine("3. Редагувати курс");
                    Console.WriteLine("4. Видалити курс");
                    Console.WriteLine("5. Пошук курсу за назвою");
                    Console.WriteLine("6. Повернутись у головне меню");
                }
                else
                {
                    Console.WriteLine("2. Пошук курсу за назвою");
                    Console.WriteLine("3. Повернутись у головне меню");
                }
                int ch = GetIntInput("Виберіть дію:");
                if (CurrentSession.Role == Role.Admin)
                {
                    switch (ch)
                    {
                        case 1: PrintAllCourses(); break;
                        case 2: AskForCourse(); break;
                        case 3: EditCourse(); break;
                        case 4: DeleteCourse(); break;
                        case 5: SearchCourseByName(); break;
                        case 6: return;
                        default: Console.WriteLine("Невірний пункт"); break;
                    }
                }
                else
                {
                    switch (ch)
                    {
                        case 1: PrintAllCourses(); break;
                        case 2: SearchCourseByName(); break;
                        case 3: return;
                        default: Console.WriteLine("Невірний пункт"); break;
                    }
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу щоб повернутись...");
                Console.ReadKey();
            }
        }

        static void AskForCourse()
        {
            while (true)
            {
                string name = GetStringInput("Введіть назву курсу:");
                double price = GetDoubleInput("Введіть ціну (за заняття) (грн):");
                int slot = FirstEmptyCourseSlot();
                if (slot == -1)
                {
                    Console.WriteLine("Немає місця для курсів (всі 5 слотів зайняті).");
                    break;
                }
                SetCourseById(slot, new Course(name, price));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Курс додано в слот #{slot}.");
                Console.ResetColor();
                Console.WriteLine("Додати ще? 1 - Так, 0 - Ні");
                int c = GetIntInput("Ваш вибір (1/0):");
                if (c == 1) continue;
                else break;
            }
        }

        static void EditCourse()
        {
            Console.WriteLine("В розробці");
        }

        static void DeleteCourse()
        {
            Console.WriteLine("В розробці");
        }

        static void SearchCourseByName()
        {
            Console.WriteLine("В розробці");
        }
        #endregion

        #region Students menu
        static void ShowStudentMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========= МЕНЮ СТУДЕНТИ =========");
                Console.ResetColor();
                Console.WriteLine("1. Переглянути список студентів");
                if (CurrentSession.Role == Role.Admin)
                {
                    Console.WriteLine("2. Додати студента (в слот)");
                    Console.WriteLine("3. Видалити студента");
                    Console.WriteLine("4. Повернутись у головне меню");
                }
                else
                {
                    Console.WriteLine("2. Повернутись у головне меню");
                }
                int ch = GetIntInput("Виберіть дію:");
                if (CurrentSession.Role == Role.Admin)
                {
                    switch (ch)
                    {
                        case 1: PrintAccounts(Role.Student); break;
                        case 2: AdminAddAccount(Role.Student); break;
                        case 3: AdminRemoveAccount(Role.Student); break;
                        case 4: return;
                        default: Console.WriteLine("Невірний вибір"); break;
                    }
                }
                else
                {
                    switch (ch)
                    {
                        case 1: PrintAccounts(Role.Student); break;
                        case 2: return;
                        default: Console.WriteLine("Невірний вибір"); break;
                    }
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        static void AdminAddAccount(Role role)
        {
            int free = FirstEmptyAccountSlot(role);
            if (free == -1)
            {
                Console.WriteLine("Немає вільних слотів.");
                return;
            }
            string login = GetStringInput("Логін для нового акаунта:");
            string pass = GetStringInput("Пароль:");
            RegisterAccountInSlot(role, free, login, pass);
            Console.WriteLine($"Акаунт додано у слот #{free}.");
        }

        static void AdminRemoveAccount(Role role)
        {
            Console.WriteLine("В розробці");
        }
        #endregion

        #region Teachers menu
        static void ShowTeacherMenu()
        {
            while (true)
            {
                Console.Clear();
                RenderIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========= МЕНЮ ВИКЛАДАЧІ =========");
                Console.ResetColor();
                Console.WriteLine("1. Переглянути список викладачів");
                if (CurrentSession.Role == Role.Admin)
                {
                    Console.WriteLine("2. Додати викладача (в слот)");
                    Console.WriteLine("3. Видалити викладача");
                    Console.WriteLine("4. Повернутись у головне меню");
                }
                else
                {
                    Console.WriteLine("2. Повернутись у головне меню");
                }
                int ch = GetIntInput("Виберіть дію:");
                if (CurrentSession.Role == Role.Admin)
                {
                    switch (ch)
                    {
                        case 1: PrintAccounts(Role.Teacher); break;
                        case 2: AdminAddAccount(Role.Teacher); break;
                        case 3: AdminRemoveAccount(Role.Teacher); break;
                        case 4: return;
                        default: Console.WriteLine("Невірний вибір"); break;
                    }
                }
                else
                {
                    switch (ch)
                    {
                        case 1: PrintAccounts(Role.Teacher); break;
                        case 2: return;
                        default: Console.WriteLine("Невірний вибір"); break;
                    }
                }
                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }
        #endregion

        #region Orders (uses course slots)
        static void ShowOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Курси навчання (Замовлення) ===\n");
            var available = new List<Course>();
            if (!c1.IsEmpty()) available.Add(c1);
            if (!c2.IsEmpty()) available.Add(c2);
            if (!c3.IsEmpty()) available.Add(c3);
            if (!c4.IsEmpty()) available.Add(c4);
            if (!c5.IsEmpty()) available.Add(c5);

            if (available.Count == 0)
            {
                Console.WriteLine("Немає доступних курсів.");
                return;
            }

            for (int i = 0; i < available.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{i + 1}. {available[i].Name} - 1 заняття - {available[i].Price} грн");
                Console.ResetColor();
            }

            Console.WriteLine("!!! Зверніть увагу — діють індивідуальні знижки !!!\n");
            double total = 0;
            var quantities = new double[available.Count];

            for (int i = 0; i < available.Count; i++)
            {
                quantities[i] = GetDoubleInput($"Скільки занять з '{available[i].Name}' ви хочете придбати (0 - не беру): ");
                total += quantities[i] * available[i].Price;
            }

            double discount = 0;
            if (total > 10000) discount = 30;
            else if (total > 5000) discount = 20;
            else if (total > 2000) discount = 10;

            double discountAmount = Math.Round(total * (discount / 100.0), 2);
            double finalPrice = Math.Round(total - discountAmount, 2);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n=== Підсумок замовлення ===");
            for (int i = 0; i < available.Count; i++)
                if (quantities[i] > 0)
                    Console.WriteLine($"{available[i].Name}: {quantities[i]} зан., вартість: {quantities[i] * available[i].Price} грн");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\nЗагальна сума без знижки: {total} грн.");
            Console.WriteLine($"Знижка: {discount}% (економія: {discountAmount} грн.)");
            Console.WriteLine($"Сума до оплати: {finalPrice} грн.");
            Console.ResetColor();
            Console.WriteLine("\nДякуємо за покупку!");
        }
        #endregion

        #region Statistics
        static void ShowStatistics()
        {
            Console.Clear();
            RenderIntro();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========= СТАТИСТИКА КУРСІВ =========");
            Console.ResetColor();

            var courses = new List<Course>();
            if (!c1.IsEmpty()) courses.Add(c1);
            if (!c2.IsEmpty()) courses.Add(c2);
            if (!c3.IsEmpty()) courses.Add(c3);
            if (!c4.IsEmpty()) courses.Add(c4);
            if (!c5.IsEmpty()) courses.Add(c5);

            if (courses.Count == 0)
            {
                Console.WriteLine("Немає курсів для розрахунку статистики.");
                return;
            }

            double totalSum = courses.Sum(x => x.Price);

           
            double avgPrice = Math.Round(courses.Average(x => x.Price), 2);

            
            double avgCountPerSlot = Math.Round((double)courses.Count / 5.0, 2);

            
            int countGreater500 = courses.Count(x => x.Price > 500);

           
            double minPrice = courses.Min(x => x.Price);
            double maxPrice = courses.Max(x => x.Price);

            Console.WriteLine($"Кількість курсів: {courses.Count}");
            Console.WriteLine($"Загальна сума (всі ціни): {totalSum} грн");
            Console.WriteLine($"Середня ціна: {avgPrice} грн");
            Console.WriteLine($"Середня кількість курсів на слот (із 5): {avgCountPerSlot}");
            Console.WriteLine($"Кількість курсів з ціною > 500 грн: {countGreater500}");
            Console.WriteLine($"Мінімальна ціна: {minPrice} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice} грн");

            Console.WriteLine("\nНатисніть будь-яку клавішу щоб повернутись у головне меню...");
            Console.ReadKey();
        }
        #endregion

        #region Search
        static void ShowSearch()
        {
            Console.WriteLine("В розробці");
        }
        #endregion
    }
    #endregion       
}
