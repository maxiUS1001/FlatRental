using FlatRental.DataModel;
using FlatRental.Model;
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

        public OrdersVM() 
        { 
            LeaseList = new ObservableCollection<Lease>(FLAT_RENTALContext.GetContext().Leases.ToList());

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
