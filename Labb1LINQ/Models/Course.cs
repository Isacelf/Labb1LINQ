using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1LINQ.Models
{
    internal class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
