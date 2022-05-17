using System;
using System.Collections.Generic;

namespace FlatRental.DataModel
{
    public partial class Lease
    {
        public int LeaseId { get; set; }
        public int? FlatId { get; set; }
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalSum { get; set; }

        public virtual Flat? Flat { get; set; }
        public virtual User? User { get; set; }
    }
}
