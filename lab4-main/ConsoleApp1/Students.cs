using System;

namespace CoursesConsoleApp_1
{
    public class Students
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public Students() { }
        public Students(string user, string pass)
        {
            Username = user ?? "";
            Password = pass ?? "";
        }

        public bool IsEmpty() => string.IsNullOrWhiteSpace(Username);
    }
}
