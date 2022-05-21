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

namespace FlatRental.ViewModel
{
    public class RegisterWindowVM
    {
        private UnitOfWork _unitOfWork;

        public RegisterWindowVM() 
        {
            _unitOfWork = new UnitOfWork();
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
                        user.Login = registerWindow.EmailTextBox.Text;
                        user.Name = registerWindow.UsernameTextBox.Text;
                        user.PhoneNumber = registerWindow.PhoneTextBox.Text;
                        user.Password = registerWindow.PasswordTextBox.Password;
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
