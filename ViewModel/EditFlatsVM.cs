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
    public class EditFlatsVM : ObservableObject
    {
        private ObservableCollection<Flat> _flatList;
        public ObservableCollection<Flat> FlatList
        {
            get
            {
                return _flatList;
            }
            set
            {
                _flatList = value;
                OnPropertyChanged("FlatList");
            }
        }

        private UnitOfWork _unitOfWork;

        public EditFlatsVM() 
        {
            _unitOfWork = new UnitOfWork();

            FlatList = new ObservableCollection<Flat>(_unitOfWork.Flats.GetAllItems());
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
                OnPropertyChanged("SelectedItem");
            }
        }

        //Delete flat
        private ICommand _deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get 
            {
                return _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(obj => 
                {
                    try
                    {
                        _unitOfWork.Flats.Delete(SelectedFlat);

                        FlatList = new ObservableCollection<Flat>(_unitOfWork.Flats.GetAllItems());

                        if (FlatList.Count != 0)
                            SelectedFlat = FlatList.First();

                        var result = new CustomMessageBox("Квартира удалена",
                                            MessageType.Success,
                                            MessageButtons.Ok).ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        var result = new CustomMessageBox("Выберите квартиру для удаления",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }             
                }));
            }
        }

        //Add flat
        private ICommand _openAddFlatFormCommand;
        public ICommand OpenAddFlatFormCommand
        {
            get
            {
                return _openAddFlatFormCommand ?? (_openAddFlatFormCommand = new RelayCommand(obj =>
                {
                    AddFlatWindow addFlatForm = new AddFlatWindow();
                    addFlatForm.DataContext = new AddFlatFormVM(this);
                    addFlatForm.Show();
                }));
            }
        }

        //Edit flat
        private ICommand _openEditFlatFormCommand;
        public ICommand OpenEditFlatFormCommand
        {
            get
            {
                return _openEditFlatFormCommand ?? (_openEditFlatFormCommand = new RelayCommand(obj =>
                {
                    if (SelectedFlat != null)
                    {
                        EditFlatWindow editFlatWindow = new EditFlatWindow();
                        editFlatWindow.DataContext = new UpdateFlatsVM(this, SelectedFlat);
                        editFlatWindow.Show();
                    }
                    else
                    {
                        var result = new CustomMessageBox("Выберите квартиру для изменения",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }
    }
}
