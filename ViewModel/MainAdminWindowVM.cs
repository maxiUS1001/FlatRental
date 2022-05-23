using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
#pragma warning disable CS8600
#pragma warning disable CS8612
#pragma warning disable CS8767

namespace FlatRental.ViewModel
{
    public class MainAdminWindowVM : ObservableObject
    {
        private Page _flatsBDPage = new View.AdminPages.FlatsBDPage();
        private Page _ordersPage = new View.AdminPages.OrdersPage();

        private EditFlatsVM _editFlatsVM = new EditFlatsVM();
        private OrdersVM _ordersVM = new OrdersVM();

        private UnitOfWork _unitOfWork;

        private bool _filterBool = true;
        public SineEase easingFunction1 = new SineEase();

        public MainAdminWindowVM()
        {
            _unitOfWork = new UnitOfWork();

            CurrentPage = _flatsBDPage;

            _flatsBDPage.DataContext = _editFlatsVM;
            _ordersPage.DataContext = _ordersVM;

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

        //Search
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(obj =>
                {
                    if (CurrentPage == _flatsBDPage)
                    {
                        try
                        {
                            Regex regex = new Regex(SearchText, RegexOptions.IgnoreCase);
                            var searchResult = _unitOfWork.Flats.GetAllItems().Where(t => regex.Match(t.Description).Success);
                            _editFlatsVM.FlatList = new ObservableCollection<Flat>(searchResult);
                        }
                        catch
                        {
                            var result = new CustomMessageBox("Недопустимый символ в строке поиска",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                        }
                    }
                    if (CurrentPage == _ordersPage)
                    {
                        try
                        {
                            Regex regex = new Regex(SearchText);
                            var searchResult = _unitOfWork.Leases.GetUserLeases().Where(t => regex.Match(t.User.PhoneNumber).Success);
                            _ordersVM.LeaseList = new ObservableCollection<UserLease>(searchResult);
                        }
                        catch
                        {
                            var result = new CustomMessageBox("Введите телефон без символа '+'",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                        }
                    }
                }));
            }
        }


        //Open orders page
        private ICommand _ordersPageCommand;
        public ICommand OrdersPageCommand
        {
            get
            {
                return _ordersPageCommand ?? (_ordersPageCommand = new RelayCommand(obj =>
                {
                    CurrentPage = _ordersPage;
                }));
            }
        }

        //Open flats bd page
        private ICommand _flatsBDPageCommand;
        public ICommand FlatsBDPageCommand
        {
            get
            {
                return _flatsBDPageCommand ?? (_flatsBDPageCommand = new RelayCommand(obj =>
                {
                    CurrentPage = _flatsBDPage;
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
                        AdminWindow window = obj as AdminWindow;
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
                    AdminWindow window = obj as AdminWindow;
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
                    AdminWindow window = obj as AdminWindow;

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

        //Show filter panel
        private ICommand _filterPanelCommand;
        public ICommand FilterPanelCommand
        {
            get
            {
                return _filterPanelCommand ?? (_filterPanelCommand = new RelayCommand(obj =>
                {
                    var border = obj as Border;
                    ThicknessAnimation OpenMn = new ThicknessAnimation();
                    if (_filterBool == true)
                    {
                        OpenMn.From = new Thickness(10, -340, 0, 0);
                        OpenMn.To = new Thickness(10, 0, 0, 0);
                        _filterBool = false;
                    }
                    else
                    {
                        OpenMn.From = new Thickness(10, 0, 0, 0);
                        OpenMn.To = new Thickness(10, -340, 0, 0);
                        _filterBool = true;
                    }

                    OpenMn.Duration = TimeSpan.FromSeconds(0.5);
                    easingFunction1.EasingMode = EasingMode.EaseOut;
                    OpenMn.EasingFunction = easingFunction1;
                    Storyboard strbrdmn = new Storyboard();
                    Storyboard.SetTargetName(OpenMn, border.Name);
                    Storyboard.SetTargetProperty(OpenMn, new PropertyPath("Margin"));
                    strbrdmn.Children.Add(OpenMn);
                    strbrdmn.Begin(border);
                }));
            }
        }
    }
}
