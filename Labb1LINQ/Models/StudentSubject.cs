using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1LINQ.Models
{
    internal class StudentSubject
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
