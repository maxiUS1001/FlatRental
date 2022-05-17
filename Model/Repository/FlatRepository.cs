using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class FlatRepository : IRepository<Flat>
    {
        private FLAT_RENTALContext _flatContext;

        public FlatRepository(FLAT_RENTALContext context)
        {
            _flatContext = context;
        }

        public void Create(Flat item)
        {
            _flatContext.Flats.Add(item);
        }

        public void Delete(Flat item)
        {
            _flatContext.Flats.Remove(item);
        }

        public IEnumerable<Flat> GetAllItems()
        {
            return _flatContext.Flats;
        }

        public void Update(Flat item)
        {
            _flatContext.Flats.Update(item);
        }
    }
}
