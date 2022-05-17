using FlatRental.DataModel;
using FlatRental.Model;
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
        public ObservableCollection<Flat> FlatList { get; set; }
        private MainUserWindowVM _mainUser;

        private Page _flatPage; 

        public AllFlatsVM() { }

        public AllFlatsVM(MainUserWindowVM mainUser)
        {          
            FlatList = new ObservableCollection<Flat>(FLAT_RENTALContext.GetContext().Flats);
            
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
