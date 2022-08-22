using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Subject
    {
        public Subject()
        {
            ClassSessions = new HashSet<ClassSession>();
            PrerequisiteSubjectPrerequisiteSubjectNavigations = new HashSet<PrerequisiteSubject>();
            PrerequisiteSubjectSubjects = new HashSet<PrerequisiteSubject>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Credits { get; set; }
        public string? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<ClassSession> ClassSessions { get; set; }
        public virtual ICollection<PrerequisiteSubject> PrerequisiteSubjectPrerequisiteSubjectNavigations { get; set; }
        public virtual ICollection<PrerequisiteSubject> PrerequisiteSubjectSubjects { get; set; }
    }
}
