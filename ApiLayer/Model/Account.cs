using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class Account
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int AccountType { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }

        public Owner Owner { get; set; }
        public User User { get; set; }
    }
}
