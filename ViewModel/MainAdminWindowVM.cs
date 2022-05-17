using FlatRental.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
#pragma warning disable CS8600
#pragma warning disable CS8612
#pragma warning disable CS8767

namespace FlatRental.ViewModel
{
    public class MainAdminWindowVM : ObservableObject
    {
        private Page _flatsBDPage = new View.AdminPages.FlatsBDPage();
        private Page _ordersPage = new View.AdminPages.OrdersPage();

        public MainAdminWindowVM()
        {
            CurrentPage = _flatsBDPage;
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
    }
}
