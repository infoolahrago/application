using System;
using System.Collections.Generic;

namespace ApiLayer.Model
{
    public partial class Account
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int AccountType { get; set; }
        public int Status { get; set; }

        public Owner Owner { get; set; }
        public User User { get; set; }
    }
}
