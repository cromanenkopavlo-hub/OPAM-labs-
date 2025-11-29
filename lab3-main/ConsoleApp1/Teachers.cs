using System;
namespace CoursesConsoleApp_1
{
    
    public class Teachers
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public Teachers() { }
        public Teachers(string user, string pass)
        {
            Username = user ?? "";
            Password = pass ?? "";
        }

        public bool IsEmpty() => string.IsNullOrWhiteSpace(Username);
    }
}
