using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNet.Identity;
using FlatRental.Model.Repository;
using System.Text.RegularExpressions;

namespace FlatRental.ViewModel
{
    public class RegisterWindowVM : ObservableObject
    {
        private UnitOfWork _unitOfWork;

        public RegisterWindowVM() 
        {
            _unitOfWork = new UnitOfWork();
        }

        private string? _login;
        public string? Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string? _phoneNumber;
        public string? PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }


        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                return _addUserCommand ?? (_addUserCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        RegisterWindow registerWindow = obj as RegisterWindow;

                        User user = new User();
                        user.Login = Login;
                        user.Name = Name;
                        user.PhoneNumber = PhoneNumber;
                        user.Password = Password;
                        user.RoleId = (int?)Roles.User;

                        
                        if (Validation.CheckValid(user))
                        {
                            if (!_unitOfWork.Users.IsExistUser(user.Login))
                            {
                                IPasswordHasher passwordHasher = new PasswordHasher();
                                user.Password = passwordHasher.HashPassword(user.Password);

                                _unitOfWork.Users.Create(user);

                                var result = new CustomMessageBox("Вы зарегистрировались",
                                            MessageType.Success,
                                            MessageButtons.Ok).ShowDialog();

                                registerWindow.Close();
                            }
                            else
                            {
                                var result = new CustomMessageBox("Пользователь уже существует",
                                        MessageType.Error,
                                        MessageButtons.Ok).ShowDialog();
                            }
                        }                     
                    }
                    catch
                    {
                        var result = new CustomMessageBox("Проверьте введенные данные",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }                
                }));
            }
        }

        //Validation
        public void LettersValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[<>%$?№!&_/^.*@#()," + "\"" + "+=:;']");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
