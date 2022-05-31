using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlatRental.ViewModel
{
    public class AllFlatsVM : ObservableObject
    {
        private ObservableCollection<Flat> _flatList;
        public ObservableCollection<Flat> FlatList
        {
            get { return _flatList; }
            set
            {
                _flatList = value;
                OnPropertyChanged("FlatList");
            }
        }
        
        private MainUserWindowVM _mainUser;
        private UnitOfWork _unitOfWork;

        private Page _flatPage; 

        public AllFlatsVM() { }

        public AllFlatsVM(MainUserWindowVM mainUser)
        {          
            _unitOfWork = new UnitOfWork();

            FlatList = new ObservableCollection<Flat>(_unitOfWork.Flats.GetAllItems());
            
            _flatPage = new View.UserPages.FlatPage();

            _mainUser = mainUser;
        }

        private Flat _selectedFlat;
        public Flat SelectedFlat
        {
            get
            {
                return _selectedFlat;
            }
            set
            {
                _selectedFlat = value;
                OnPropertyChanged("SelectedFlat");

                _flatPage.DataContext = new FlatPageVM(this);
                _mainUser.CurrentPage = _flatPage;
            }
        }
    }
}
