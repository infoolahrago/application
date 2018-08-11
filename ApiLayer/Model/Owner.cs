using System;
using System.Collections.Generic;

namespace ApiLayer.Model
{
    public partial class Owner
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? IdentityType { get; set; }
        public int? IdentityNumber { get; set; }
        public string Address { get; set; }

        public Account Account { get; set; }
    }
}
