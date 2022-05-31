using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model
{
    public class FlatLease
    {
        public Lease Lease { get; set; }
        public Flat Flat { get; set; }
    }
}
