using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class Balance
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public double Balance1 { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
