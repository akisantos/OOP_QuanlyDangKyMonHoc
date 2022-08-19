using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class RegistClass
    {
        public int? Credits { get; set; }
        public DateTime RegistDate { get; set; }
        public bool Status { get; set; }
        public string StudentId { get; set; } = null!;
        public string ClassSessionId { get; set; } = null!;

        public virtual ClassSession ClassSession { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
