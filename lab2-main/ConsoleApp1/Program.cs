using System;
using System.Drawing;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        RenderIntro();
        ShowMainMenu();
    }

    public static void RenderIntro()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("===========================================");
        Console.WriteLine("============== Курси навчання =============");
        Console.WriteLine("===========================================");
        Console.ResetColor();
    }

    public static double GetDoubleInput(string prompt = "Введіть число:")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(prompt + " ");
        string input1 = Console.ReadLine();

        if (double.TryParse(input1, out double result) && result >= 0)
        {
            Console.ResetColor();
            return result;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Невірне значення. Введіть невід'ємне число (можна 0).");
        Console.ResetColor();
        return GetDoubleInput(prompt); // рекурсивний виклик
    }

    // Вводить пункти меню
    public static int GetIntInput(string prompt = "Введіть число:")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(prompt + " ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int result))
        {
            Console.ResetColor();
            return result;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ви ввели не число ціле. Спробуйте ще раз.");
        Console.ResetColor();
        return GetIntInput(prompt); 
    }

    public static void ShowMainMenu()
    {
        int attempts = 0;
        const int maxAttempts = 3;

        while (true)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Головне меню:");
            Console.ResetColor();
            Console.WriteLine("1. Товари");
            Console.WriteLine("2. Студенти");
            Console.WriteLine("3. Викладачі");
            Console.WriteLine("4. Замовлення");
            Console.WriteLine("5. Пошук");
            Console.WriteLine("6. Статистика");
            Console.WriteLine("7. Вихід");

            int choice = 0;

            try
            {
                choice = GetIntInput("Виберіть пункт меню:");

                switch (choice)
                {
                    case 1:
                        ShowProductMenu();
                        break;
                    case 2:
                        ShowPupilMenu();
                        break;
                    case 3:
                        ShowTeacherMenu();
                        break;
                    case 4:
                        ShowOrderMenu();
                        break;
                    case 5:
                        Console.WriteLine("Пошук — функція в розробці");
                        break;
                    case 6:
                        Console.WriteLine("Статистика — функція в розробці");
                        break;
                    case 7:
                        Console.WriteLine("Бувай!");
                        Environment.Exit(0);
                        break;
                    default:
                        throw new Exception("Неправильний вибір.");
                }

             
                attempts = 0;

            }
            catch (Exception ex)
            {
                attempts++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка: {ex.Message} Спроба {attempts} з {maxAttempts}.");
                Console.ResetColor();

                if (attempts >= maxAttempts)
                {
                    Console.WriteLine("Ви вичерпали 3 спроби. Програма завершує роботу.");
                    Environment.Exit(0);
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу щоб повернутися в головне меню...");
            Console.ReadKey();
            Console.Clear();
            RenderIntro();
        }
    }


    private static void ShowOrderMenu()
    {
        double priceMath = 600;
        double priceEnglish = 800;
        double priceProgramming = 1000;
        double priceDesign = 1000;
        double priceMarketing = 400;

        Console.Clear();
        Console.WriteLine("=== Курси навчання ===\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"1. Курс з математики - 1 заняття - {priceMath} грн");
        Console.WriteLine($"2. Курс з англійської мови - 1 заняття - {priceEnglish} грн");
        Console.WriteLine($"3. Курс з програмування - 1 заняття - {priceProgramming} грн");
        Console.WriteLine($"4. Курс з дизайну - 1 заняття - {priceDesign} грн");
        Console.WriteLine($"5. Курс з маркетингу - 1 заняття - {priceMarketing} грн");
        Console.WriteLine("!!! Зверніть увагу — діють індивідуальні знижки !!!");
        Console.ResetColor();
        Console.WriteLine();

        double math = GetDoubleInput("Скільки занять з математики ви хочете придбати (0 - не беру): ");
        double english = GetDoubleInput("Скільки занять з англійської мови ви хочете придбати (0 - не беру): ");
        double programming = GetDoubleInput("Скільки занять з програмування ви хочете придбати (0 - не беру): ");
        double design = GetDoubleInput("Скільки занять з дизайну ви хочете придбати (0 - не беру): ");
        double marketing = GetDoubleInput("Скільки занять з маркетингу ви хочете придбати (0 - не беру): ");

        double totalMath = math * priceMath;
        double totalEnglish = english * priceEnglish;
        double totalProgramming = programming * priceProgramming;
        double totalDesign = design * priceDesign;
        double totalMarketing = marketing * priceMarketing;

        double totalPrice = totalMath + totalEnglish + totalProgramming + totalDesign + totalMarketing;

        double discountPercent = 0;
        if (totalPrice > 10000) discountPercent = 30;
        else if (totalPrice > 5000) discountPercent = 20;
        else if (totalPrice > 2000) discountPercent = 10;

        double discountAmount = Math.Round(totalPrice * (discountPercent / 100.0), 2);
        double finalPrice = Math.Round(totalPrice - discountAmount, 2);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine();
        Console.WriteLine("=== Підсумок замовлення ===");
        Console.WriteLine($"Математика: {math} зан., вартість: {totalMath} грн.");
        Console.WriteLine($"Англійська: {english} зан., вартість: {totalEnglish} грн.");
        Console.WriteLine($"Програмування: {programming} зан., вартість: {totalProgramming} грн.");
        Console.WriteLine($"Дизайн: {design} зан., вартість: {totalDesign} грн.");
        Console.WriteLine($"Маркетинг: {marketing} зан., вартість: {totalMarketing} грн.");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"\nЗагальна сума без знижки: {totalPrice} грн.");
        Console.WriteLine($"Знижка: {discountPercent}% (економія: {discountAmount} грн.)");
        Console.WriteLine($"Сума до оплати: {finalPrice} грн.");
        Console.ResetColor();

        Console.WriteLine("\nДякуємо за покупку!");
    }

    private static void ShowPupilMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Меню студентів ===");
        Console.WriteLine("Функція в розробці");
    }

    private static void ShowTeacherMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Меню викладачів ===");
        Console.WriteLine("Функція в розробці");
    }

    private static void ShowProductMenu()
    {
        Console.Clear();
        Console.WriteLine("========= МЕНЮ ТОВАРІВ =========");
        Console.WriteLine("1. Додати новий товар");
        Console.WriteLine("2. Переглянути всі товари");
        Console.WriteLine("3. Редагувати товар");
        Console.WriteLine("4. Видалити товар");
        Console.WriteLine("5. Пошук товару за назвою");
        Console.WriteLine("6. Сортувати за ціною / кількістю");
        Console.WriteLine("7. Повернутись у головне меню");
        Console.WriteLine("--------------------------------");

        int choice = GetIntInput("Виберіть дію (1-7):");

        switch (choice)
        {
            case 1:
            case 3:
            case 4:
            case 5:
            case 6:
                Console.WriteLine("Функція в розробці");
                break;
            case 2:
                Showgoods();
                break;
            case 7:
                ShowMainMenu();
                return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неправильний пункт меню");
                Console.ResetColor();
                break;
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися у меню товарів...");
        Console.ReadKey();
        ShowProductMenu();
    }

    private static void Showgoods()
    {
        double priceMath = 600;
        double priceEnglish = 800;
        double priceProgramming = 1000;
        double priceDesign = 1000;
        double priceMarketing = 400;

        Console.Clear();
        Console.WriteLine("=== Товари / Курси навчання ===\n");
        Console.WriteLine($"1. Курс з математики - 1 заняття - {priceMath} грн");
        Console.WriteLine($"2. Курс з англійської мови - 1 заняття - {priceEnglish} грн");
        Console.WriteLine($"3. Курс з програмування - 1 заняття - {priceProgramming} грн");
        Console.WriteLine($"4. Курс з дизайну - 1 заняття - {priceDesign} грн");
        Console.WriteLine($"5. Курс з маркетингу - 1 заняття - {priceMarketing} грн");
        Console.WriteLine("\n!!! Зверніть увагу — діють індивідуальні знижки !!!");
    }
}
