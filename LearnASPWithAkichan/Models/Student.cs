using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Student
    {
        public Student()
        {
            Accounts = new HashSet<Account>();
            LopHocPhans = new HashSet<LopHocPhan>();
        }

        public string Id { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime Birth { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<LopHocPhan> LopHocPhans { get; set; }
    }
}
