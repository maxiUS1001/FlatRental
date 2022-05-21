using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private FLAT_RENTALContext _flatContext;

        public ReviewRepository(FLAT_RENTALContext context)
        {
            _flatContext = context;
        }

        public void Create(Review item)
        {
            _flatContext.Reviews.Add(item);
            _flatContext.SaveChanges();
        }

        public void Delete(Review item)
        {
            _flatContext.Reviews.Remove(item);
            _flatContext.SaveChanges();
        }

        public IEnumerable<Review> GetAllItems()
        {
            return _flatContext.Reviews;
        }

        public ObservableCollection<UserReview> GetUserReviewAboutFlat(Flat flat)
        {
            var comments = new ObservableCollection<UserReview>();
            _flatContext.Reviews.Where(c => c.FlatId == flat.FlatId).Join(_flatContext.Users, c => c.UserId, u => u.UserId, (c, u) => new UserReview { User = u, Review = c }).OrderByDescending(uc => uc.Review.ReviewId).ToList().ForEach(c => comments.Add(c));
            return comments;
        }

        public void Update(Review item)
        {
            _flatContext.Reviews.Update(item);
            _flatContext.SaveChanges();
        }
    }
}
