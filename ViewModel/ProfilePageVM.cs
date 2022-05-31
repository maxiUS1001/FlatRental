using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class ProfilePageVM : ObservableObject
    {
        private UnitOfWork _unitOfWork;

        public ProfilePageVM()
        {
            _unitOfWork = new UnitOfWork();

            Login = CurrentUser.GetInstance().Login;
            Name = CurrentUser.GetInstance().Name;
            PhoneNumber = CurrentUser.GetInstance().PhoneNumber;

            _realPassword = CurrentUser.GetInstance().Password;
            _realEmail = CurrentUser.GetInstance().Login;

            CurrentName = CurrentUser.GetInstance().Name;
        }

        private string? _currentName;
        public string? CurrentName
        {
            get { return _currentName; }
            set
            {
                _currentName = value;
                OnPropertyChanged("CurrentName");
            }
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

        private string? _fakePassword = "1234";
        private string? _realPassword;

        private string? _fakeEmail = "qwerty1234@gmail.com";
        private string? _realEmail;

        private ICommand _saveProfileInfoCommand;
        public ICommand SaveProfileInfoCommand
        {
            get
            {
                return _saveProfileInfoCommand ?? (_saveProfileInfoCommand = new RelayCommand(onj =>
                {
                    try
                    {
                        CurrentUser.GetInstance().Name = Name;
                        CurrentUser.GetInstance().PhoneNumber = PhoneNumber;
                        CurrentUser.GetInstance().Password = _fakePassword;
                        CurrentUser.GetInstance().Login = _fakeEmail;

                        if (Validation.CheckValid(CurrentUser.GetInstance()))
                        {
                            CurrentUser.GetInstance().Login = _realEmail;
                            CurrentUser.GetInstance().Password = _realPassword;
                            _unitOfWork.Users.Update(CurrentUser.GetInstance());
                            CurrentName = CurrentUser.GetInstance().Name;

                            var result = new CustomMessageBox("Пользователь изменен",
                                                MessageType.Success,
                                                MessageButtons.Ok).ShowDialog();
                        }
                        else
                        {
                            var result = new CustomMessageBox("Пользователь не изменен",
                                        MessageType.Error,
                                        MessageButtons.Ok).ShowDialog();
                        }
                    }
                    catch
                    {
                        var result = new CustomMessageBox("Пользователь не изменен",
                                        MessageType.Error,
                                        MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }

        //Validation
        public void LettersValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9<>%$?№!&_./^*@#()," + "\"" + "+=:;']");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
