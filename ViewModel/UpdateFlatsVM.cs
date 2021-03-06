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
using System.Text.RegularExpressions;
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
                _price = Math.Round(value ?? 0, 2);
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

                        _flat.Metro = Metro;
                        _flat.District = District;
                        _flat.Мicrodistrict = Мicrodistrict;
                        _flat.NumberOfRooms = NumberOfRooms;
                        _flat.RentalType = RentalType;
                        _flat.Area = Area;
                        _flat.Toilet = Toilet;
                        _flat.Balcony = Balcony;
                        _flat.Floor = Floor;
                        _flat.Price = Price;
                        _flat.Description = Description;
                        _flat.Image = Image;

                        if (Validation.CheckValid(_flat))
                        {
                            _unitOfWork.Flats.Update(_flat);

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

        //Validation
        public void IntValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(char.Parse(e.Text)))
            {
                e.Handled = true;
            }
        }

        public void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[-A-Za-zA-Яа-я<>%$?!&_/^№*@#()," + "\"" + "+=:;']");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
