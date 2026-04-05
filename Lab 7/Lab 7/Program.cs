using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

/*class Program1
{
    static void Main()
    {
        // створюємо список дій
        List<Action> actions = new List<Action>();


        // виконуємо всі дії
        foreach (var action in actions)
        {
            action();
        }


        for (int i = 1; i <= 5; i++)
        {
            int number = i;
            actions.Add(() => Console.WriteLine(number));
        }

        foreach (var action in actions)
        {
            action();
        }
    }
}*/
/*class Program3
{
    static void Main()
    {

        Func<double, double> discountCalculator = null;


        discountCalculator += price => price * 0.95; // -5%
        discountCalculator += price => price * 0.90; // -10%
        discountCalculator += price => price - 100;  // -100 грн


        double result = discountCalculator(1000);

        Console.WriteLine($"Результат: {result}");
    }
}*/



class Program4
{
    static void Main()
    {
        var discounts = new List<Func<double, double>>
        {
            price => price * 0.95, // -5%
            price => price * 0.90, // -10%
            price => price - 100   // -100 грн
        };

        double price = 1000;
        foreach (var discount in discounts)
        {
            price = discount(price); 
        }

        Console.WriteLine($"Ціна: {price}");
    }
}