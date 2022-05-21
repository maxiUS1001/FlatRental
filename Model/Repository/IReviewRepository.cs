﻿using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model.Repository
{
    interface IReviewRepository : IRepository<Review>
    {
        ObservableCollection<UserReview> GetUserReviewAboutFlat(Flat item);
    }
}
