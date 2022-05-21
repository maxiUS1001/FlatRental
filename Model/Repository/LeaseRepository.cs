using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class LeaseRepository : ILeaseRepository
    {
        private FLAT_RENTALContext _flatContext;

        public LeaseRepository(FLAT_RENTALContext context)
        {
            _flatContext = new FLAT_RENTALContext();
        }

        public void Create(Lease item)
        {
            _flatContext.Leases.Add(item);
            _flatContext.SaveChanges();
        }

        public void Delete(Lease item)
        {
            _flatContext.Leases.Remove(item);
            _flatContext.SaveChanges();
        }

        public IEnumerable<Lease> GetAllItems()
        {
            return _flatContext.Leases;
        }

        public void Update(Lease item)
        {
            _flatContext.Leases.Update(item);
            _flatContext.SaveChanges();
        }

        public ObservableCollection<UserLease> GetUserLeases()
        {
            var leases = new ObservableCollection<UserLease>();
            _flatContext.Users.Join(_flatContext.Leases, c => c.UserId, u => u.UserId, (c, u) => new UserLease { User = c, Lease = u }).ToList().ForEach(c => leases.Add(c));
            return leases;
        }
    }
}
