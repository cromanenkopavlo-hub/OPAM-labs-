using System;
using System.Collections.Generic;

/*interface Refuelable
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
}*/
abstract class Food
{
    public string name;
    public double caloriesPer100g;

    public abstract double CalculateCalories(double grams);
}

class Fruit : Food
{
    public override double CalculateCalories(double grams)
    {
        return (caloriesPer100g / 100) * grams;
    }
}
class Meat : Food
{
    public override double CalculateCalories(double grams)
    {
        return (caloriesPer100g / 100) * grams;
    }
}
class Dairy : Food
{
    public override double CalculateCalories(double grams)
    {
        return (caloriesPer100g / 100) * grams;
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Food> foods = new List<Food>();

        Fruit apple = new Fruit();
        apple.name = "Яблуко";
        apple.caloriesPer100g = 52;

        Meat chicken = new Meat();
        chicken.name = "Курятина";
        chicken.caloriesPer100g = 239;

        Dairy milk = new Dairy();
        milk.name = "Молоко";
        milk.caloriesPer100g = 60;

        foods.Add(apple);
        foods.Add(chicken);
        foods.Add(milk);

        foreach (Food food in foods)
        {
            double calories = food.CalculateCalories(100);
            Console.WriteLine($"{food.name} (100 г) = {calories} ккал");
        }
    }
}
