using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class UserRepository : IRepository<User>
    {
        private FLAT_RENTALContext _flatContext;

        public UserRepository(FLAT_RENTALContext context)
        {
            _flatContext = context;
        }

        public void Create(User item)
        {
            _flatContext.Users.Add(item);
        }

        public void Delete(User item)
        {
            _flatContext.Users.Remove(item);
        }

        public IEnumerable<User> GetAllItems()
        {
            return _flatContext.Users;
        }

        public void Update(User item)
        {
            _flatContext.Users.Update(item);
        }
    }
}
