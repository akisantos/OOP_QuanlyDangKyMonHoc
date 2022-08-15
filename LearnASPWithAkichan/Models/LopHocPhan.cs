using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class LopHocPhan
    {
        public string StudentId { get; set; } = null!;
        public string SubjectId { get; set; } = null!;
        public DateTime? Time { get; set; }
        public double? AverageCore { get; set; }
        public double? MidCore { get; set; }
        public double? EndCore { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
