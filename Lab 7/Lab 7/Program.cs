using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
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



    static void Main2()
    { 
        public class BankTerminal
        {
        public delegant Action<int> OnMoneyWithdraw
        }
    }

   
}