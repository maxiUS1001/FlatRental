using FlatRental.Model;
using FlatRental.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class MainUserWindowVM : ObservableObject
    {
        private Page _allFlatsPage = new View.UserPages.AllFlatsPage();

        public MainUserWindowVM() 
        {
            CurrentPage = _allFlatsPage;
            _allFlatsPage.DataContext = new AllFlatsVM(this);
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
    }
}
