namespace CoursesConsoleApp_1
{
    public class Teachers
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        public Teachers() { }
        public Teachers(int id, string user, string hash)
        {
            Id = id;
            Username = user;
            PasswordHash = hash;
        }
    }
}