using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
