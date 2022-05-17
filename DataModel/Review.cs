using System;
using System.Collections.Generic;

namespace FlatRental.DataModel
{
    public partial class Review
    {
        public int? ReviewId { get; set; }
        public string? Text { get; set; }
        public DateTime? DateOfReview { get; set; }
        public int? UserId { get; set; }
        public int? FlatId { get; set; }

        public virtual Flat? Flat { get; set; }
        public virtual User? User { get; set; }
    }
}
