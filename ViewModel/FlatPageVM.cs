using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View.UserPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class FlatPageVM : ObservableObject
    {
        private ObservableCollection<UserReview> _reviewList;
        public ObservableCollection<UserReview> ReviewList
        {
            get { return _reviewList; }
            set
            {
                _reviewList = value;
                OnPropertyChanged("ReviewList");
            }
        }

        private UnitOfWork _unitOfWork;

        public FlatPageVM() { }

        public FlatPageVM(AllFlatsVM allFlats)
        {
            CurrentFlat = allFlats.SelectedFlat;

            _unitOfWork = new UnitOfWork();

            ReviewList = _unitOfWork.Reviews.GetUserReviewAboutFlat(CurrentFlat);
        }

        private Flat _currentFlat;
        public Flat CurrentFlat
        {
            get { return _currentFlat; }
            set
            {
                _currentFlat = value;
                OnPropertyChanged("CurrentFlat");
            }
        }

        private ICommand _openRentalForm;
        public ICommand OpenRentalForm
        {
            get 
            {
                return _openRentalForm ?? (_openRentalForm = new RelayCommand(obj =>
                {
                    RentalFormWindow rentalForm = new RentalFormWindow();
                    rentalForm.DataContext = new RentalFormVM(this);
                    rentalForm.Show();
                }));
            }
        }

        private string? _review;
        public string? Review
        {
            get { return _review; }
            set
            {
                _review = value;
                OnPropertyChanged("Comment");
            }
        }

        private ICommand _addReview;
        public ICommand AddReview
        {
            get
            {
                return _addReview ?? (_addReview = new RelayCommand(obj =>
                {
                    Review review = new Review();
                    review.DateOfReview = DateTime.Now;
                    review.FlatId = CurrentFlat.FlatId;
                    review.UserId = CurrentUser.GetInstance().UserId;
                    review.Text = Review;

                    _unitOfWork.Reviews.Create(review);
                    _unitOfWork.Save();

                    ReviewList = _unitOfWork.Reviews.GetUserReviewAboutFlat(CurrentFlat);
                    Review = null;
                }));
            }
        }
    }
}
