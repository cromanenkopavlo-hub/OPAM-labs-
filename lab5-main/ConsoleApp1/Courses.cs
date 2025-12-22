using System;

namespace CoursesConsoleApp_1
{
    public class Course
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";
        public double Price { get; set; } = 0;

        public Course() { }
        public Course(int id, string name, double price)
        {
            Id = id;
            Name = name ?? "";
            Price = price;
        }
    }
}