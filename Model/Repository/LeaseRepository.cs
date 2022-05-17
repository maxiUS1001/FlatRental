using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class LeaseRepository : IRepository<Lease>
    {
        private FLAT_RENTALContext _flatContext;

        public LeaseRepository(FLAT_RENTALContext context)
        {
            _flatContext = new FLAT_RENTALContext();
        }

        public void Create(Lease item)
        {
            _flatContext.Leases.Add(item);
        }

        public void Delete(Lease item)
        {
            _flatContext.Leases.Remove(item);
        }

        public IEnumerable<Lease> GetAllItems()
        {
            return _flatContext.Leases;
        }

        public void Update(Lease item)
        {
            _flatContext.Leases.Update(item);
        }
    }
}
