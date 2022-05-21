using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class UnitOfWork
    {
        private FLAT_RENTALContext db = new FLAT_RENTALContext();
        private FlatRepository _flatRepository;
        private UserRepository _userRepository;
        private LeaseRepository _leaseRepository;
        private ReviewRepository _reviewRepository;

        public FlatRepository Flats
        {
            get
            {
                if (_flatRepository == null)
                    _flatRepository = new FlatRepository(db);
                return _flatRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(db);
                return _userRepository;
            }
        }

        public LeaseRepository Leases
        {
            get
            {
                if (_leaseRepository == null)
                    _leaseRepository = new LeaseRepository(db);
                return _leaseRepository;
            }
        }

        public ReviewRepository Reviews
        {
            get
            {
                if (_reviewRepository == null)
                    _reviewRepository = new ReviewRepository(db);
                return _reviewRepository;
            }
        }
    }
}
