using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class TableCore
    {
        public TableCore()
        {
            Regists = new HashSet<Regist>();
        }

        public int Id { get; set; }
        public double? AverageCore { get; set; }
        public double? MidCore { get; set; }
        public double? EndCore { get; set; }

        public virtual ICollection<Regist> Regists { get; set; }
    }
}
