using System;
using System.Collections.Generic;

namespace LearnASPWithAkichan.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? Role { get; set; }
        public string? StudentId { get; set; }

        public virtual Student? Student { get; set; }

        public Account()
        {
            Id=0;
            Username = "AkiChan";
            Password = "0096";
            Role = 0;
            StudentId = "21748010340042";
;       }
    }
}
