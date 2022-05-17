﻿using FlatRental.DataModel;
using FlatRental.Model;
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

        public EditFlatsVM() 
        {
            FlatList = new ObservableCollection<Flat>(FLAT_RENTALContext.GetContext().Flats.ToList());
           
            if (FlatList.Count != 0)
                SelectedFlat = FlatList.First();
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
                        FLAT_RENTALContext.GetContext().Flats.Remove(SelectedFlat);
                        FLAT_RENTALContext.GetContext().SaveChanges();
                        FlatList = new ObservableCollection<Flat>(FLAT_RENTALContext.GetContext().Flats.ToList());

                        if (FlatList.Count != 0)
                            SelectedFlat = FlatList.First();

                        var result = new CustomMessageBox("Квартира удалена",
                                            MessageType.Success,
                                            MessageButtons.Ok).ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        var result = new CustomMessageBox(ex.Message,
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
                    try
                    {
                        EditFlatWindow editFlatWindow = new EditFlatWindow();
                        editFlatWindow.DataContext = new UpdateFlatsVM(this, SelectedFlat);
                        editFlatWindow.Show();
                    }
                    catch (Exception ex)
                    {
                        var result = new CustomMessageBox(ex.Message,
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }
    }
}