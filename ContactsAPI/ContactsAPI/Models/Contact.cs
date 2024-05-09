using System;
using System.Collections.Generic;

namespace ContactsAPI.Models
{
    public partial class Contact
    {
        public Guid Id { get; set; }
        public string? Fullname { get; set; }
        public long Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Nickname { get; set; }
    }
}
