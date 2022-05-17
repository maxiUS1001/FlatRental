using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.ViewModel
{
    public class OrdersVM : ObservableObject
    {
        private ObservableCollection<Lease> _leaseList;
        public ObservableCollection<Lease> LeaseList
        {
            get
            {
                return _leaseList;
            }
            set
            {
                _leaseList = value;
                OnPropertyChanged("FlatList");
            }
        }

        private UnitOfWork _unitOfWork;

        public OrdersVM() 
        {
            _unitOfWork = new UnitOfWork();

            LeaseList = new ObservableCollection<Lease>(_unitOfWork.Leases.GetAllItems());

            if(LeaseList.Count != 0)
                SelectedOrder = LeaseList.First();
        }

        private Lease _selectedOrder;
        public Lease SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged("SelectedItem");
            }
        }


    }
}
