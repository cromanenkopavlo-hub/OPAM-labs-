using System;
using System.Collections.Generic;

interface Refuelable
{
    void Refill();
}

abstract class Vehicle
{
    public string brand;
    public int speed;

    public abstract void Move();
}

class Bicycle : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Велосипед їде");
    }
}

class Airplane : Vehicle, Refuelable
{
    public override void Move()
    {
        Console.WriteLine("Літак літає");
    }

    public void Refill()
    {
        Console.WriteLine("Літак заправляється паливом");
    }
}

class Car : Vehicle, Refuelable
{
    public override void Move()
    {
        Console.WriteLine("Машина їде");
    }

    public void Refill()
    {
        Console.WriteLine("Машина заправляється паливом");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        List<Vehicle> vehicles = new List<Vehicle>();

        vehicles.Add(new Car());
        vehicles.Add(new Bicycle());
        vehicles.Add(new Airplane());

        foreach (Vehicle vehicle in vehicles)
        {
            vehicle.Move();

            if (vehicle is Refuelable refuelable)
            {
                refuelable.Refill();
            }
        }

  
    }
}