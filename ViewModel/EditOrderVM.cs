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
    public class EditOrderVM : ObservableObject
    {
        private OrdersVM _ordersModel;
        private UserLease _order;
        private UnitOfWork _unitOfWork;

        public EditOrderVM() { }

        public EditOrderVM(OrdersVM ordersModel, UserLease order)
        {
            _ordersModel = ordersModel;
            _order = order;
            _unitOfWork = new UnitOfWork();

            StartTime = order.Lease.StartDate;
            EndTime = order.Lease.EndDate;
        }

        private ICommand _closeFormCommand;
        public ICommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand ?? (_closeFormCommand = new RelayCommand(obj =>
                {
                    EditOrderWindow form = obj as EditOrderWindow;
                    form.Close();
                }));
            }
        }

        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        private decimal? _totalPrice;
        public decimal? TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private ICommand _editOrderCommand;
        public ICommand EditOrderCommand
        {
            get
            {
                return _editOrderCommand ?? (_editOrderCommand = new RelayCommand(obj =>
                {
                    EditOrderWindow editOrder = obj as EditOrderWindow;

                    if (EndTime > StartTime)
                    {
                        _order.Lease.StartDate = StartTime;
                        _order.Lease.EndDate = EndTime;

                        _unitOfWork.Leases.Update(_order.Lease);

                        _ordersModel.LeaseList = new ObservableCollection<UserLease>(_unitOfWork.Leases.GetUserLeases());

                        var result = new CustomMessageBox("Заказ изменен",
                                                MessageType.Success,
                                                MessageButtons.Ok).ShowDialog();

                        editOrder.Close();
                    }
                    else
                    {
                        var result = new CustomMessageBox("Проверьте даты",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }
    }
}
