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

        public ClassSession(ClassSession pre)
        {
            Id = pre.Id;
            Amount = pre.Amount;
            PointClass = pre.PointClass;
            PointMid = pre.PointMid;
            PointEnd = pre.PointEnd;
            Active = pre.Active;
            BeginDate = pre.BeginDate;
            EndDate = pre.EndDate;
            CommonClass = pre.CommonClass;
            DepartmentId = pre.DepartmentId;
            SubjectId = pre.SubjectId;
            Department = pre.Department;
            Subject = pre.Subject;
            RegistClasses = pre.RegistClasses;
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
