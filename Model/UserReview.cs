using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model
{
    public class UserReview
    {
        public User User { get; set; }
        public Review Review { get; set; }
    }
}
