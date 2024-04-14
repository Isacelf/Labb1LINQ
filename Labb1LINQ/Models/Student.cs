using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1LINQ.Models
{
    internal class Student
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
    }
}
