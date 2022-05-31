using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private FLAT_RENTALContext _flatContext;

        public UserRepository(FLAT_RENTALContext context)
        {
            _flatContext = context;
        }

        public void Create(User item)
        {
            _flatContext.Users.Add(item);
            _flatContext.SaveChanges();
        }

        public void Delete(User item)
        {
            _flatContext.Users.Remove(item);
            _flatContext.SaveChanges();
        }

        public IEnumerable<User> GetAllItems()
        {
            return _flatContext.Users;
        }

        public bool IsExistUser(string login)
        {
            return _flatContext.Users.Any(t => t.Login == login);     
        }

        public ObservableCollection<FlatLease> GetUserOrders()
        {
            var orders = new ObservableCollection<FlatLease>();
            _flatContext.Leases.Where(t => t.UserId == CurrentUser.GetInstance().UserId).Join(_flatContext.Flats, c => c.FlatId, u => u.FlatId, (c, u) => new FlatLease { Lease = c, Flat = u}).ToList().ForEach(c => orders.Add(c));
            return orders;
        }

        public void Update(User item)
        {
            _flatContext.Users.Update(item);
            _flatContext.SaveChanges();
        }
    }
}
