using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class TransactionBooking
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long CourtId { get; set; }
        public string BookingNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int BookingStatus { get; set; }
        public int BookingType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
