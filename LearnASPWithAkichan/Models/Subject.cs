using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Subject
    {
        public Subject()
        {
            InverseSubjectNavigation = new HashSet<Subject>();
            LopHocPhans = new HashSet<LopHocPhan>();
        }

        public string Id { get; set; } = null!;
        public int? Credits { get; set; }
        public string Name { get; set; } = null!;
        public string? SubjectId { get; set; }
        public string? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Subject? SubjectNavigation { get; set; }
        public virtual ICollection<Subject> InverseSubjectNavigation { get; set; }
        public virtual ICollection<LopHocPhan> LopHocPhans { get; set; }
    }
}
