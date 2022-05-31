using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class ReviewPageVM : ObservableObject
    {
        private ObservableCollection<Review> _reviewList;
        public ObservableCollection<Review> ReviewList
        {
            get
            {
                return _reviewList;
            }
            set
            {
                _reviewList = value;
                OnPropertyChanged("ReviewList");
            }
        }

        private UnitOfWork _unitOfWork;

        public ReviewPageVM() 
        {
            _unitOfWork = new UnitOfWork();

            ReviewList = new ObservableCollection<Review>(_unitOfWork.Reviews.GetAllItems());
        }

        private Review _selectedReview;
        public Review SelectedReview
        {
            get { return _selectedReview; }
            set
            {
                _selectedReview = value;
                OnPropertyChanged("SelectedReview");
            }
        }

        //Delete review
        private ICommand _deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get
            {
                return _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        _unitOfWork.Reviews.Delete(SelectedReview);

                        ReviewList = new ObservableCollection<Review>(_unitOfWork.Reviews.GetAllItems());

                        if (ReviewList.Count != 0)
                            SelectedReview = ReviewList.First();

                        var result = new CustomMessageBox("Комментарий удален",
                                            MessageType.Success,
                                            MessageButtons.Ok).ShowDialog();
                    }
                    catch
                    {
                        var result = new CustomMessageBox("Выберите комментарий для удаления",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }

    }
}
