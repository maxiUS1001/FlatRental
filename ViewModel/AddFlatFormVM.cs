using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.View;
using FlatRental.View.AdminPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;
using FlatRental.Model.Repository;
using System.Text.RegularExpressions;

namespace FlatRental.ViewModel
{
    public class AddFlatFormVM : ObservableObject
    {
        private EditFlatsVM _editFlatsModel;
        private UnitOfWork _unitOfWork;

        public AddFlatFormVM() { }

        public AddFlatFormVM(EditFlatsVM editFlatsModel)
        {
            _editFlatsModel = editFlatsModel;

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
                    AddFlatWindow form = obj as AddFlatWindow;
                    form.Close();
                }));
            }
        }


        //Adding flat
        private ICommand _addFlatCommand;
        public ICommand AddFlatCommand
        {
            get
            {
                return _addFlatCommand ?? (_addFlatCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        AddFlatWindow addFlatForm = obj as AddFlatWindow;

                        Flat flat = new Flat();
                        flat.Metro = Metro;
                        flat.District = District;
                        flat.Мicrodistrict = Мicrodistrict;
                        flat.NumberOfRooms = NumberOfRooms;
                        flat.RentalType = RentalType;
                        flat.Area = Area;
                        flat.Toilet = Toilet;
                        flat.Balcony = Balcony;
                        flat.Floor = Floor;
                        flat.Price = Price;
                        flat.Description = Description;
                        flat.Image = Image;

                        if (Validation.CheckValid(flat))
                        {
                            _unitOfWork.Flats.Create(flat);

                            _editFlatsModel.FlatList = new ObservableCollection<Flat>(_unitOfWork.Flats.GetAllItems());

                            var result = new CustomMessageBox("Квартира добавлена",
                                        MessageType.Success,
                                        MessageButtons.Ok).ShowDialog();

                            addFlatForm.Close();
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
            Regex regex = new Regex(@"^[-A-Za-zA-Яа-я<>%$?№!&_/^*@#()," + "\"" + "+=:;']");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
