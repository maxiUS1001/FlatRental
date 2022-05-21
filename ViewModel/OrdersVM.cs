using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using FlatRental.View.AdminPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class OrdersVM : ObservableObject
    {
        private ObservableCollection<UserLease> _leaseList;
        public ObservableCollection<UserLease> LeaseList
        {
            get
            {
                return _leaseList;
            }
            set
            {
                _leaseList = value;
                OnPropertyChanged("LeaseList");
            }
        }

        private UnitOfWork _unitOfWork;

        public OrdersVM()
        {
            _unitOfWork = new UnitOfWork();

            LeaseList = new ObservableCollection<UserLease>(_unitOfWork.Leases.GetUserLeases());
        }

        private UserLease _selectedOrder;
        public UserLease SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }


        private ICommand _deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get
            {
                return _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        _unitOfWork.Leases.Delete(SelectedOrder.Lease);

                        LeaseList = new ObservableCollection<UserLease>(_unitOfWork.Leases.GetUserLeases());

                        var result = new CustomMessageBox("Заказ удален",   
                                            MessageType.Success,
                                            MessageButtons.Ok).ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        var result = new CustomMessageBox("Выберите заказ для удаления",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }

        private ICommand _openEditOrderForm;
        public ICommand OpenEditOrderFormCommand
        {
            get
            {
                return _openEditOrderForm ?? (_openEditOrderForm = new RelayCommand(onj =>
                {
                    EditOrderWindow editOrder = new EditOrderWindow();
                    editOrder.DataContext = new EditOrderVM(this, SelectedOrder);
                    editOrder.Show();
                }));
            }
        }
    }
}
