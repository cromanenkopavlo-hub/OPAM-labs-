namespace CoursesConsoleApp_1
{
    public class Students
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        public Students() { }
        public Students(int id, string email, string hash)
        {
            Id = id;
            Email = email;
            PasswordHash = hash;
        }
    }
}