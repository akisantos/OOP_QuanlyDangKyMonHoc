using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Account
    {
        public Account()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public bool Role { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
