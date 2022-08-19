using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Student
    {
        public Student()
        {
            RegistClasses = new HashSet<RegistClass>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime Birth { get; set; }
        public string Address { get; set; } = null!;
        public string HomeTown { get; set; } = null!;
        public string? DepartmentId { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<RegistClass> RegistClasses { get; set; }
    }
}
