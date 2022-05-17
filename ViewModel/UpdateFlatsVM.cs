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
#pragma warning disable CS8602
#pragma warning disable CS8600

namespace FlatRental.ViewModel
{
    public class UpdateFlatsVM : ObservableObject
    {
        private Flat _flat;
        private EditFlatsVM _editFlatsModel;

        private UnitOfWork _unitOfWork;

        public UpdateFlatsVM() { }

        public UpdateFlatsVM(EditFlatsVM editFlatsModel, Flat flat)
        {
            _flat = flat;
            _editFlatsModel = editFlatsModel;

            Metro = flat.Metro;
            District = flat.District;
            Мicrodistrict = flat.Мicrodistrict;
            NumberOfRooms = flat.NumberOfRooms;
            RentalType = flat.RentalType;
            Area = flat.Area;
            Toilet = flat.Toilet;
            Balcony = flat.Balcony;
            Floor = flat.Floor;
            Price = flat.Price;
            Description = flat.Description;
            Image = flat.Image;

            _unitOfWork = new UnitOfWork();
        }

        private string? _metro;
        public string? Metro
        {
            get 
            {
                return _metro;
            }
            set 
            {
                _metro = value;
                OnPropertyChanged("Metro");
            }
        }

        private string? _district;
        public string? District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
                OnPropertyChanged("District");
            }
        }

        private string? _microdistrict;
        public string? Мicrodistrict
        {
            get
            {
                return _microdistrict;
            }
            set
            {
                _microdistrict = value;
                OnPropertyChanged("Microdistrict");
            }
        }

        private int? _numberOfRooms;
        public int? NumberOfRooms
        {
            get
            {
                return _numberOfRooms;
            }
            set
            {
                _numberOfRooms = value;
                OnPropertyChanged("NumberOfRooms");
            }
        }

        private string? _rentalType;
        public string? RentalType
        {
            get
            {
                return _rentalType;
            }
            set
            {
                _rentalType = value;
                OnPropertyChanged("RentalType");
            }
        }

        private decimal? _area;
        public decimal? Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
                OnPropertyChanged("Area");
            }
        }

        private string? _toilet;
        public string? Toilet
        {
            get
            {
                return _toilet;
            }
            set
            {
                _toilet = value;
                OnPropertyChanged("Toilet");
            }
        }

        private string? _balcony;
        public string? Balcony
        {
            get
            {
                return _balcony;
            }
            set
            {
                _balcony = value;
                OnPropertyChanged("Balcony");
            }
        }

        private int? _floor;
        public int? Floor
        {
            get
            {
                return _floor;
            }
            set
            {
                _floor = value;
                OnPropertyChanged("Floor");
            }
        }

        private decimal? _price;
        public decimal? Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private string? _description;
        public string? Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string? _image;
        public string? Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        //Close form
        private ICommand _closeFormCommand;
        public ICommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand ?? (_closeFormCommand = new RelayCommand(obj =>
                {
                    EditFlatWindow form = obj as EditFlatWindow;
                    form.Close();
                }));
            }
        }

        //Update DB
        private ICommand _editFlatCommand;
        public ICommand EditFlatCommand
        {
            get
            {
                return _editFlatCommand ?? (_editFlatCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        EditFlatWindow editFlatWindow = obj as EditFlatWindow;

                        _flat.Metro = editFlatWindow.MetroTextBox.Text;
                        _flat.District = editFlatWindow.DistrictTextBox.Text;
                        _flat.Мicrodistrict = editFlatWindow.MicrodistrictTextBox.Text;
                        _flat.NumberOfRooms = int.Parse(editFlatWindow.RoomsTextBox.Text);
                        _flat.RentalType = editFlatWindow.RentalTypeTextBox.Text;
                        _flat.Area = decimal.Parse(editFlatWindow.AreaTextBox.Text.Replace(".", ","));
                        _flat.Toilet = editFlatWindow.ToiletTextBox.Text;
                        _flat.Balcony = editFlatWindow.BalconyTextBox.Text;
                        _flat.Floor = int.Parse(editFlatWindow.FloorTextBox.Text);
                        _flat.Price = decimal.Parse(editFlatWindow.PriceTextBox.Text.Replace(".", ","));
                        _flat.Description = editFlatWindow.DescriptionTextBox.Text;
                        _flat.Image = editFlatWindow.ImageTextBox.Text;

                        if (Validation.CheckValid(_flat))
                        {
                            _unitOfWork.Flats.Update(_flat);
                            _unitOfWork.Save();

                            _editFlatsModel.FlatList = new ObservableCollection<Flat>(_unitOfWork.Flats.GetAllItems());
                            var result = new CustomMessageBox("Квартира изменена",
                                            MessageType.Success,
                                            MessageButtons.Ok).ShowDialog();
                            editFlatWindow.Close();
                        }
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
