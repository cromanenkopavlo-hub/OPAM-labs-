using System;

namespace CoursesConsoleApp_1
{
    public class Course
    {
        public string Name { get; set; } = "";
        public double Price { get; set; } = 0;

        public Course() { }
        public Course(string name, double price)
        {
            Name = name ?? "";
            Price = price;
        }

        public bool IsEmpty() => string.IsNullOrWhiteSpace(Name);
    }
}
