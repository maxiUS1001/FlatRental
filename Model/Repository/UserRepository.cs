using FlatRental.DataModel;
using System;
using System.Collections.Generic;
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

        public void Update(User item)
        {
            _flatContext.Users.Update(item);
            _flatContext.SaveChanges();
        }
    }
}
