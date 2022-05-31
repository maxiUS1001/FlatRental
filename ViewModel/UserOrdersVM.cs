using FlatRental.Model;
using FlatRental.Model.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class UserOrdersVM : ObservableObject
    {
        private UnitOfWork _unitOfWork;

        private ObservableCollection<FlatLease> _userFlat;
        public ObservableCollection<FlatLease> UserFlat
        {
            get { return _userFlat; }
            set
            {
                _userFlat = value;
                OnPropertyChanged("UserFlat");
            }
        }

        public UserOrdersVM()
        {
            _unitOfWork = new UnitOfWork();

            UserFlat = new ObservableCollection<FlatLease>(_unitOfWork.Users.GetUserOrders());
        }
        
        private ICommand _cancelOrderCommand;
        public ICommand CancelOrderCommand
        {
            get
            {
                return _cancelOrderCommand ?? (_cancelOrderCommand = new RelayCommand(obj =>
                {
                    FlatLease flatLease = obj as FlatLease;

                    _unitOfWork.Leases.Delete(flatLease.Lease);
                    UserFlat = new ObservableCollection<FlatLease>(_unitOfWork.Users.GetUserOrders());
                }));
            }
        }
    }
}
