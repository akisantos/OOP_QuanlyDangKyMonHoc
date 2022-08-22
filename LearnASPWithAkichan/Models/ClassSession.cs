using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class ClassSession
    {
        public ClassSession()
        {
            RegistClasses = new HashSet<RegistClass>();
        }

        public string Id { get; set; } = null!;
        public int Amount { get; set; }
        public double? PointClass { get; set; }
        public double? PointMid { get; set; }
        public double? PointEnd { get; set; }
        public bool Active { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CommonClass { get; set; }
        public string? DepartmentId { get; set; }
        public string? SubjectId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<RegistClass> RegistClasses { get; set; }
    }
}
