using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class PrerequisiteSubject
    {
        public int Id { get; set; }
        public string? SubjectId { get; set; }
        public string? PrerequisiteSubjectId { get; set; }

        public virtual Subject? PrerequisiteSubjectNavigation { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
