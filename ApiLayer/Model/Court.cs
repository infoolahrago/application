using System;
using System.Collections.Generic;

namespace ApiLayer.Model
{
    public partial class Court
    {
        public long Id { get; set; }
        public long PlaygroundId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CourtType { get; set; }
        public int SizeType { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
