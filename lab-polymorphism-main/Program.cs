using System;
using System.Collections.Generic;

namespace LabPolymorphism;

class Program
{
    static void Main(string[] args)
    {
        List<Artifact> inventory = new List<Artifact>();

        inventory.Add(new MagicScroll(101));
        inventory.Add(new AncientSword(202));

        Console.WriteLine("--- Аналіз інвентарю ---");

        foreach (var item in inventory)
        {
            item.Identify();
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}