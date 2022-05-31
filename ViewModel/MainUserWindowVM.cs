using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class MainUserWindowVM : ObservableObject
    {
        private UnitOfWork _unitOfWork;

        private Page _allFlatsPage = new View.UserPages.AllFlatsPage();
        private Page _userOrdersPage = new View.UserPages.UserOrders();
        private Page _profilePage = new View.ProfilePage();

        private AllFlatsVM _allFlatsVM;

        private UserOrdersVM _userOrdersVM;

        public MainUserWindowVM()
        {
            _unitOfWork = new UnitOfWork();

            CurrentPage = _allFlatsPage;

            _allFlatsVM = new AllFlatsVM(this);
            _allFlatsPage.DataContext = _allFlatsVM;

            _userOrdersVM = new UserOrdersVM();
            _userOrdersPage.DataContext = _userOrdersVM;


            SearchText = "";
        }

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private ICommand _allFlatsPageCommand;
        public ICommand AllFlatsPageCommand
        {
            get
            {
                return _allFlatsPageCommand ?? (_allFlatsPageCommand = new RelayCommand(obj =>
                {
                    CurrentPage = _allFlatsPage;
                }));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }


        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new RelayCommand(obj =>
                {
                    UserWindow userWindow = obj as UserWindow;

                    CurrentUser.ClearInstance();
                    
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();

                    userWindow.Close();
                }));
            }
        }

        private ICommand _profilePageCommand;
        public ICommand ProfilePageCommand
        {
            get
            {
                return _profilePageCommand ?? (_profilePageCommand = new RelayCommand(obj =>
                {
                    CurrentPage = _profilePage;
                }));
            }
        }

        private ICommand _userOrdersPageCommand;
        public ICommand UserOrdersPageCommand
        {
            get
            {
                return _userOrdersPageCommand ?? (_userOrdersPageCommand = new RelayCommand(obj =>
                {
                    _userOrdersVM.UserFlat = new ObservableCollection<FlatLease>(_unitOfWork.Users.GetUserOrders());

                    CurrentPage = _userOrdersPage;
                }));
            }
        }

        //Close app
        private ICommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                return _closeAppCommand ?? (_closeAppCommand = new RelayCommand(obj =>
                {
                    UserWindow window = obj as UserWindow;
                    window.Close();
                }));
            }
        }

        //Minimize app
        private ICommand _minimizeAppCommand;
        public ICommand MinimizeAppCommand
        {
            get
            {
                return _minimizeAppCommand ?? (_minimizeAppCommand = new RelayCommand(obj =>
                {
                    UserWindow window = obj as UserWindow;
                    window.WindowState = WindowState.Minimized;
                }));
            }
        }

        //Maximize app
        private ICommand _maximizeAppCommand;
        public ICommand MaximizeAppCommand
        {
            get
            {
                return _maximizeAppCommand ?? (_maximizeAppCommand = new RelayCommand(obj =>
                {
                    UserWindow window = obj as UserWindow;

                    if (window.WindowState == WindowState.Normal)
                    {
                        window.WindowState = WindowState.Maximized;
                    }
                    else if (window.WindowState == WindowState.Maximized)
                    {
                        window.WindowState = WindowState.Normal;
                    }
                }));
            }
        }

        //Search
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        Regex regex = new Regex(SearchText, RegexOptions.IgnoreCase);
                        var searchResult = _unitOfWork.Flats.GetAllItems().Where(t => regex.Match(t.Description).Success);
                        _allFlatsVM.FlatList = new ObservableCollection<Flat>(searchResult);
                    }
                    catch
                    {
                        var result = new CustomMessageBox("Недопустимый символ в строке поиска",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }

        //Show filter panel
        private ICommand _filterPanelCommand;
        public ICommand FilterPanelCommand
        {
            get
            {
                return _filterPanelCommand ?? (_filterPanelCommand = new RelayCommand(obj =>
                {
                    var border = obj as Border;
                    FilterPanelAnimation.ShowOrHidePanel(border);
                }));
            }
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

        private string? _numberOfRooms;
        public string? NumberOfRooms
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

        private string? _areaFrom;
        public string? AreaFrom
        {
            get
            {
                return _areaFrom;
            }
            set
            {
                _areaFrom = value;
                OnPropertyChanged("AreaFrom");
            }
        }

        private string? _areaTo;
        public string? AreaTo
        {
            get
            {
                return _areaTo;
            }
            set
            {
                _areaTo = value;
                OnPropertyChanged("AreaTo");
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

        private string? _floor;
        public string? Floor
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

        private string? _priceTo;
        public string? PriceTo
        {
            get
            {
                return _priceTo;
            }
            set
            {
                _priceTo = value;
                OnPropertyChanged("PriceTo");
            }
        }

        private string? _priceFrom;
        public string? PriceFrom
        {
            get
            {
                return _priceFrom;
            }
            set
            {
                _priceFrom = value;
                OnPropertyChanged("PriceFrom");
            }
        }

        private ICommand _filterFlatsCommand;
        public ICommand FilterFlatsCommand
        {
            get
            {
                return _filterFlatsCommand ?? (_filterFlatsCommand = new RelayCommand(obj =>
                {
                    UserWindow userWindow = obj as UserWindow;
                    userWindow.FlatsRadioButton.IsChecked = true;

                    CurrentPage = _allFlatsPage;

                    var flatList = _unitOfWork.Flats.GetAllItems().ToList();

                    if (!string.IsNullOrWhiteSpace(Metro))
                    {
                        if (Metro != "Любое")
                        {
                            flatList = flatList.Where(p => p.Metro == Metro).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(District))
                    {
                        if (District != "Любой")
                        {
                            flatList = flatList.Where(p => p.District == District).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Мicrodistrict))
                    {
                        if (Мicrodistrict != "Любой")
                        {
                            flatList = flatList.Where(p => p.Мicrodistrict == Мicrodistrict).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(NumberOfRooms))
                    {
                        flatList = flatList.Where(p => p.NumberOfRooms == int.Parse(NumberOfRooms)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(RentalType))
                    {
                        if (RentalType != "Любой")
                        {
                            flatList = flatList.Where(p => p.RentalType == RentalType).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Toilet))
                    {
                        if (Toilet != "Любой")
                        {
                            flatList = flatList.Where(p => p.Toilet == Toilet).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Balcony))
                    {
                        if (Balcony != "Любой")
                        {
                            flatList = flatList.Where(p => p.Balcony == Balcony).ToList();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Floor))
                    {
                        flatList = flatList.Where(p => p.Floor == int.Parse(Floor)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(PriceFrom) & !string.IsNullOrWhiteSpace(PriceTo))
                    {
                        flatList = flatList.Where(p => p.Price >= decimal.Parse(PriceFrom) & p.Price <= decimal.Parse(PriceTo)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(PriceTo) & string.IsNullOrWhiteSpace(PriceFrom))
                    {
                        flatList = flatList.Where(p => p.Price <= decimal.Parse(PriceTo)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(PriceFrom) & string.IsNullOrWhiteSpace(PriceTo))
                    {
                        flatList = flatList.Where(p => p.Price >= decimal.Parse(PriceFrom)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(AreaFrom) & !string.IsNullOrWhiteSpace(AreaTo))
                    {
                        flatList = flatList.Where(p => p.Area >= decimal.Parse(AreaFrom) & p.Area <= decimal.Parse(AreaTo)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(AreaTo) & string.IsNullOrWhiteSpace(AreaFrom))
                    {
                        flatList = flatList.Where(p => p.Area <= decimal.Parse(AreaTo)).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(AreaFrom) & string.IsNullOrWhiteSpace(AreaTo))
                    {
                        flatList = flatList.Where(p => p.Area >= decimal.Parse(AreaFrom)).ToList();
                    }

                    _allFlatsVM.FlatList = new ObservableCollection<Flat>(flatList);

                    FilterPanelAnimation.ShowOrHidePanel(userWindow.FilterPanel);
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

        public void SearchValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[-<>%$?!&_/^~`№*@#()" + "\"" + "+=:;']");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
