
namespace Student_System
{
    using System;
    using Data;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;

    public class Program
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            using (var db = new StudentsystemDBContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                //SeedData(db);

                PrintAllStudentsAndHomework(db);
            }
        }

        private static void PrintAllStudentsAndHomework(StudentsystemDBContext db)
        {
            var data = db
                .Students
                .Select(s => new {s.Name, s.Homeworks})
                .ToList();
            foreach (var KVPair in data)
            {
                Console.WriteLine($"{KVPair.Name}");
                for (int i = 0; i < KVPair.Homeworks.Count; i++)
                {
                    var homework = KVPair.Homeworks[i];
                    Console.WriteLine(homework.ContentType);
                    Console.WriteLine(homework.Content);
                }
            }
        }

        private static void SeedData(StudentsystemDBContext db)
        {
            const int totalStudents = 25;
            const int totalCourses = 10;
            DateTime currentDate = DateTime.Now;

            //Students
            Console.WriteLine("Adding students");
            for (int i = 0; i < totalStudents; i++)
            {
                db.Students.Add(new Student()
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate.AddDays(i),
                    Birthday = currentDate.AddYears(-20).AddDays(i),
                    PhoneNumber = $"Random Phone {i}"
                });
            }

            db.SaveChanges();

            //Courses
            Console.WriteLine("Adding Courses");
            var addedCourses = new List<Course>();
            for (int i = 0; i < totalCourses; i++)
            {
                var course = new Course()
                {
                    Name = $"Course {i}",
                    Description = $"Course Details {i}",
                    Price = 100 * i,
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(20 + i)

                };

                

                addedCourses.Add(course);
                db.Courses.Add(course);
            }

            db.SaveChanges();
            //Student in Courses

            Console.WriteLine("Connecting Students and Courses");
            
            var studentIds = db
                .Students
                .Select(s => s.Id)
                .ToList();


            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];
                var studentsInCourse = random.Next(2, totalStudents / 2);
                Console.WriteLine("new");
                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studentIds[random.Next(0, studentIds.Count)];
                    Console.WriteLine($"Check : {studentId}");
                    if (!currentCourse.Students.Any(s => s.StudentId == studentId))
                    {
                        Console.WriteLine($"{currentCourse.Id} : {studentId}");
                        currentCourse.Students.Add(new StudentCourses()
                        {
                            StudentId = studentId,
                            CourseId = currentCourse.Id
                        });
                    }
                    else
                    {
                        j--;
                    }
                    
                }

                //Resources
                Console.WriteLine("Adding Resources");
                var resourcesInCourse = random.Next(2, 20);
                var types = new[] {0, 1, 2, 999};
                currentCourse.Resources.Add(new Resource()
                {
                    Name = $"Resource {i}",
                    URL = $"URL {i}",
                    Type = (ResourceType)types[random.Next(0,types.Length)]
                });
            }

            db.SaveChanges();

            //Homeworks

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];

                var studentInCourseIds = currentCourse
                    .Students
                    .Select(s => s.StudentId)
                    .ToList();
                for (int j = 0; j < studentInCourseIds.Count; j++)
                {
                    var totalHomeworks = random.Next(2, 5);

                    for (int k = 0; k < totalHomeworks; k++)
                    {
                        db.Homeworks.Add(new Homework()
                        {
                            Content = $"Content Homework {i}",
                            SubmissionDate = currentDate.AddDays(-i),
                            ContentType = ContentType.Zip,
                            StudentId = studentInCourseIds[j],
                            CourseId = currentCourse.Id
                        });
                    }
                }

                db.SaveChanges();

            }

            
        }
    }
}
