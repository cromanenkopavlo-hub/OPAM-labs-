namespace Test1
    {
        class Program
        {
            static void Main(string[] args)
            {
                string[] student = { "Романенко", "Яльч","Скатов", "Панющик", "Бурч", "Німець", "Чаварга", "Куцик", "Левчинко", "Бурикін","Савко", "Губський", "Дронов", "Куцик" };
                Random rnd = new Random();
                List<string> used = new List<string>(); 
                Console.WriteLine("Виведення 35 разів:");
                for (int i = 1; i <= 35; i++)
                {
                    int A = rnd.Next(0, student.Length);
                    string randomStudent = student[A];
                    Console.WriteLine(student[A]);
                    if (!used.Contains(randomStudent))
                    {
                        Console.WriteLine("Вибрано студента: " + randomStudent);
                        used.Add(randomStudent);
                    }
                }

                Console.ReadKey();
            }
        }
    }