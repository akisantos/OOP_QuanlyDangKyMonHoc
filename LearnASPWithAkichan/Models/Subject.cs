using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Subject
    {
        public Subject()
        {
            ClassSessions = new HashSet<ClassSession>();
            PrerequisiteSubjects = new HashSet<Subject>();
            Subjects = new HashSet<Subject>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Credits { get; set; }
        public string? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<ClassSession> ClassSessions { get; set; }

        public virtual ICollection<Subject> PrerequisiteSubjects { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
