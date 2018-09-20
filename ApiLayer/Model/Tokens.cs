using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class Tokens
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
