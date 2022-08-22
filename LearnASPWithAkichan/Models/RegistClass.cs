using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class RegistClass
    {
        public int Id { get; set; }
        public int? Credits { get; set; }
        public DateTime RegistDate { get; set; }
        public bool? Status { get; set; }
        public string? StudentId { get; set; }
        public string? ClassSessionId { get; set; }

        public virtual ClassSession? ClassSession { get; set; }
        public virtual Student? Student { get; set; }
    }
}
