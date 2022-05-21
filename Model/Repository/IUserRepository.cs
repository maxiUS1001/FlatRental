using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    interface IUserRepository : IRepository<User>
    {
        bool IsExistUser(string login);
    }
}
