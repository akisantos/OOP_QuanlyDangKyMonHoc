using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Regist
    {
        public DateTime OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public DateTime RegistTime { get; set; }
        public string StudentId { get; set; } = null!;
        public string SubjectId { get; set; } = null!;
        public int TableCoreId { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual TableCore TableCore { get; set; } = null!;
    }
}
