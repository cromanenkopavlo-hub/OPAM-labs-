using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Курси навчання === \n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1. Курс з математики - 1 занятя - 600 грн");
        Console.WriteLine("2. Курс з англiйської мови - 1 занятя - 800 грн");
        Console.WriteLine("3. Курс з програмування - 1 занятя - 1000 грн");
        Console.WriteLine("4. Курс з дизайну - 1 занятя - 1000 грн");
        Console.WriteLine("5. Курс з маркетингу - 1 занятя- - 400 грн");
        Console.WriteLine("!!!Звернiть увагу в нас дiють iндивiдуальнi знижки!!!");
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        // Кiлькiсть курсiв
        Console.Write("Скiльки курсiв з математики ви хочете придбати (0 - не беру): ");
        double math = Convert.ToDouble(Console.ReadLine());

        Console.Write("Скiльки курсiв з англiйської мови ви хочете придбати (0 - не беру): ");
        double english = Convert.ToDouble(Console.ReadLine());

        Console.Write("Скiльки курсiв з програмування ви хочете придбати (0 - не беру): ");
        double programming = Convert.ToDouble(Console.ReadLine());

        Console.Write("Скiльки курсiв з дизайну ви хочете придбати (0 - не беру): ");
        double design = Convert.ToDouble(Console.ReadLine());

        Console.Write("Скiльки курсiв з маркетингу ви хочете придбати (0 - не беру): ");
        double marketing = Convert.ToDouble(Console.ReadLine());
        Console.ResetColor();
        // Цiни
        double priceMath = 600;
        double priceEnglish = 800;
        double priceProgramming = 1000;
        double priceDesign = 1000;
        double priceMarketing = 400;

        double totalPrice = (math * priceMath) + (english * priceEnglish) + (programming * priceProgramming) + (design * priceDesign) + (marketing * priceMarketing);


        double randomValue = new Random().NextDouble() * 10; // 0–10%
        double discount = Math.Round(randomValue, 2);
        double finalPrice = totalPrice - (totalPrice * discount / 100);


        // Вивiд результатiв
        Console.WriteLine("\nЗагальна сума без знижки:" + totalPrice + "грн");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Знижка:" + discount + "%");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Кiнцева сума до оплати:" + Math.Round(finalPrice, 2) + "грн");
        Console.ResetColor();

        Console.WriteLine("Дякуємо за покупку!");




    }
}