using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1LINQ.Data;
using Microsoft.EntityFrameworkCore;

namespace Labb1LINQ
{
    internal class Methods
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n[1] Show math teachers");
                Console.WriteLine("[2] Show students with their respective class supervisors");
                Console.WriteLine("[3] Check if the subject Programming 1 exists or not");
                Console.WriteLine("[4] Update the subject Programming 2 to OOP");
                Console.WriteLine("[5] Update students with class supervisor Anas to Reidar");
                Console.WriteLine("[6] Exit\n");
                Console.Write("Pick a number: ");

                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Methods.GetMathTeachers();
                        break;
                    case "2":
                        Methods.GetStudentsWithTeachers();
                        break;
                    case "3":
                        Methods.CheckSubject();
                        break;
                    case "4":
                        Methods.ChangeSubject();
                        break;
                    case "5":
                        Methods.ChangeTeacher();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nYou must choose a number between 1-6!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }
        // Retrieves all math teachers from the database
        public static void GetMathTeachers()
        {
            SchoolDbContext context = new SchoolDbContext();

            Console.Clear();

            Console.WriteLine($"\nNumber of teachers: {context.Teachers.Count()}");
            Console.WriteLine($"Number of subjects: {context.Subjects.Count()}");
            Console.WriteLine($"Number of TeacherSubjects: {context.teacherSubjects.Count()}");

            // Searches for math teachers
            var mathTeachers = context.teacherSubjects
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Where(ts => ts.Subject.SubjectName == "Mathematics")
                .Select(ts => ts.Teacher.TeacherName)
                .Distinct()
                .ToList();

            Console.WriteLine($"\nMath teachers found: {mathTeachers.Count}\n");
            if (mathTeachers.Count == 0)
            {
                Console.WriteLine("\nNo math teachers found");
            }
            else
            {
                foreach (var teacherName in mathTeachers)
                {
                    Console.WriteLine(teacherName);
                }
            }

            Console.ReadLine();
        }

        // Retrieves students and their assigned teachers
        public static void GetStudentsWithTeachers()
        {
            SchoolDbContext context = new SchoolDbContext();

            Console.Clear();

            // Searches for students and their teachers
            var studentsWithTeachers = context.Students
                .Include(s => s.Teacher)
                .Select(s => new
                {
                    studentName = s.StudentName,
                    teacherName = s.Teacher.TeacherName
                })
                .ToList();

            if (studentsWithTeachers.Count == 0)
            {
                Console.WriteLine("\nNo students found.");
            }
            else
            {
                foreach (var item in studentsWithTeachers)
                {
                    Console.WriteLine($"\nStudent: {item.studentName}, Teacher: {item.teacherName}");
                }
            }

            Console.ReadLine();
        }

        // Checks if the subject "Programming 1" exists in the database
        public static void CheckSubject()
        {
            SchoolDbContext context = new SchoolDbContext();

            Console.Clear();

            // Checks for existence
            bool exists = context.Subjects.Any(s => s.SubjectName == "Programming 1");

            // Prints out the result of the check
            if (!exists)
            {
                Console.WriteLine("Programming 1 does not exist..");
            }
            else
            {
                Console.WriteLine("Programming 1 exists!");
            }

            Console.ReadLine();
        }

        // Updates the name of the subject "Programming 2" to "OOP"
        public static void ChangeSubject()
        {
            SchoolDbContext context = new SchoolDbContext();

            Console.Clear();

            // Finds the subject to update
            var subjectToUpdate = context.Subjects
                .FirstOrDefault(s => s.SubjectName == "Programming 2");

            // Checks if the subject exists and updates it
            if (subjectToUpdate != null)
            {
                subjectToUpdate.SubjectName = "OOP";
                context.SaveChanges();
                Console.WriteLine("The subject has been updated!");
            }
            else
            {
                Console.WriteLine("Programming 2 was not found and therefore cannot be updated..");
            }

            Console.ReadLine();
        }

        // Changes the class supervisor for students from Anas to Reidar
        public static void ChangeTeacher()
        {
            SchoolDbContext context = new SchoolDbContext();

            Console.Clear();

            // Finds students to change class supervisor
            var changeTeacher = context.Students
                .Where(s => s.TeacherId == 1)
                .ToList();

            // Changes class supervisor and saves changes
            if (changeTeacher.Count > 0)
            {
                foreach (var student in changeTeacher)
                {
                    student.TeacherId = 2;
                }

                context.SaveChanges();
                Console.WriteLine("Class supervisor has been changed for students with Anas to Reidar");
            }
            else
            {
                Console.WriteLine("No students with Anas as class supervisor were found..");
            }

            Console.ReadLine();
        }
    }
}




