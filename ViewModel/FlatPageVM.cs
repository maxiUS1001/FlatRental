using FlatRental.DataModel;
using FlatRental.Model;
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
        private ObservableCollection<Review> _reviewList;
        public ObservableCollection<Review> ReviewList
        {
            get { return _reviewList; }
            set
            {
                _reviewList = value;
                OnPropertyChanged("ReviewList");
            }
        }

        public Dictionary<User, Review> ReviewDictionary;

        

        public FlatPageVM() { }

        public FlatPageVM(AllFlatsVM allFlats)
        {
            CurrentFlat = allFlats.SelectedFlat;
            ReviewList = new ObservableCollection<Review>(FLAT_RENTALContext.GetContext().Reviews.Where(p => p.FlatId == CurrentFlat.FlatId));
        }

        public User User { get; set; } = CurrentUser.GetInstance();

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

        private string? _comment;
        public string? Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged("Comment");
            }
        }

        private ICommand _addComment;
        public ICommand AddComment
        {
            get
            {
                return _addComment ?? (_addComment = new RelayCommand(obj =>
                {
                    Review review = new Review();
                    review.DateOfReview = DateTime.Now;
                    review.FlatId = CurrentFlat.FlatId;
                    review.UserId = User.UserId;
                    review.Text = Comment;

                    FLAT_RENTALContext.GetContext().Reviews.Add(review);
                    FLAT_RENTALContext.GetContext().SaveChanges();

                    ReviewList = new ObservableCollection<Review>(FLAT_RENTALContext.GetContext().Reviews.Where(p => p.FlatId == CurrentFlat.FlatId));
                }));
            }
        }
    }
}
