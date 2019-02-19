using System;
using System.Collections.Generic;
using System.Text;

namespace myB2B.Domain
{
    public class ApplicationUser : ApplicationEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public Company UserCompany { get; set; }
    }
}
