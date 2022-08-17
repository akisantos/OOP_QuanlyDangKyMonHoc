using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public string? StudentId { get; set; }

        public virtual Student? Student { get; set; }
    }
}
