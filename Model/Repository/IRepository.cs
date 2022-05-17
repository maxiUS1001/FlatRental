using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllItems();
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(T item); // удаление объекта по id
    }
}
