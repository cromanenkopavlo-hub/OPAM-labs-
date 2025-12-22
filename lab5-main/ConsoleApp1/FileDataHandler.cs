using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CoursesConsoleApp_1
{
    public static class FileDataHandler
    {
        // Назва папки 
        private const string FolderName = "data";

        // Шляхи до файлів
        public static readonly string CoursesPath = Path.Combine(FolderName, "courses.csv");
        public static readonly string StudentsPath = Path.Combine(FolderName, "students.csv");
        public static readonly string TeachersPath = Path.Combine(FolderName, "teachers.csv");

        public static void InitializeFiles()
        {
            // Створює папку якщо вона не існує
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }

            EnsureFile(CoursesPath, "Id,Name,Price");
            EnsureFile(StudentsPath, "Id,Email,PasswordHash");
            EnsureFile(TeachersPath, "Id,Username,PasswordHash");
        }

        private static void EnsureFile(string path, string header)
        {
            // Перевірка існування та запис 
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
                File.WriteAllLines(path, new[] { header }, Encoding.UTF8);
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static int GetNextId(string path)
        {
            try
            {
                var lines = File.ReadAllLines(path).Skip(1);
                return lines.Any() ? lines.Max(l => int.Parse(l.Split(',')[0])) + 1 : 1;
            }
            catch { return 1; }
        }

        public static List<Course> ReadCourses() => File.ReadAllLines(CoursesPath).Skip(1)
            .Select(l => l.Split(',')).Where(p => p.Length == 3)
            .Select(p => new Course(int.Parse(p[0]), p[1], double.Parse(p[2]))).ToList();

        public static List<Students> ReadStudents() => File.ReadAllLines(StudentsPath).Skip(1)
            .Select(l => l.Split(',')).Where(p => p.Length == 3)
            .Select(p => new Students(int.Parse(p[0]), p[1], p[2])).ToList();

        public static List<Teachers> ReadTeachers() => File.ReadAllLines(TeachersPath).Skip(1)
            .Select(l => l.Split(',')).Where(p => p.Length == 3)
            .Select(p => new Teachers(int.Parse(p[0]), p[1], p[2])).ToList();

        public static void SaveCourses(List<Course> list) => File.WriteAllLines(CoursesPath, new[] { "Id,Name,Price" }.Concat(list.Select(c => $"{c.Id},{c.Name},{c.Price}")), Encoding.UTF8);
        public static void SaveStudents(List<Students> list) => File.WriteAllLines(StudentsPath, new[] { "Id,Email,PasswordHash" }.Concat(list.Select(s => $"{s.Id},{s.Email},{s.PasswordHash}")), Encoding.UTF8);
        public static void SaveTeachers(List<Teachers> list) => File.WriteAllLines(TeachersPath, new[] { "Id,Username,PasswordHash" }.Concat(list.Select(t => $"{t.Id},{t.Username},{t.PasswordHash}")), Encoding.UTF8);
    }
}