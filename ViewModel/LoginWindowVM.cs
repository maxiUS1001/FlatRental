using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class LoginWindowVM : ObservableObject
    {
        private UnitOfWork _unitOfWork;

        public LoginWindowVM()
        {
            _unitOfWork = new UnitOfWork();
        }

        private string? _email;
        public string? Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        LoginWindow loginWindow = obj as LoginWindow;

                        IPasswordHasher passwordHasher = new PasswordHasher();
                        var checkUser = _unitOfWork.Users.GetAllItems().Where(u => u.Login == Email).FirstOrDefault();

                        PasswordVerificationResult verification = passwordHasher.VerifyHashedPassword(checkUser.Password, Password);

                        if (verification == PasswordVerificationResult.Success)
                        {
                            CurrentUser.SetInstance(checkUser);

                            if (checkUser.RoleId == (int?)Roles.Admin)
                            {
                                AdminWindow adminWindow = new AdminWindow();
                                adminWindow.Show();
                                loginWindow.Close();
                            }
                            if (checkUser.RoleId == (int?)Roles.User)
                            {
                                UserWindow userWindow = new UserWindow();
                                userWindow.Show();
                                loginWindow.Close();
                            }
                        }
                        else
                        {
                            var result = new CustomMessageBox("Ошибка входа",
                                        MessageType.Error,
                                        MessageButtons.Ok).ShowDialog();
                        }
                    }
                    catch
                    {
                        var result = new CustomMessageBox("Проверьте данные",
                                        MessageType.Error,
                                        MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }

        private ICommand _openRegisterFormCommand;
        public ICommand OpenRegisterFormCommand
        {
            get
            {
                return _openRegisterFormCommand ?? (_openRegisterFormCommand = new RelayCommand(obj =>
                {
                    RegisterWindow registerWindow = new RegisterWindow();
                    registerWindow.Show();
                }));
            }
        }
    }
}
