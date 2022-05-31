using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.Model.Repository;
using FlatRental.View;
using FlatRental.View.UserPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlatRental.ViewModel
{
    public class RentalFormVM : ObservableObject
    {
        private FlatPageVM _flatPage;
        private UnitOfWork _unitOfWork;

        public RentalFormVM() { }

        public RentalFormVM(FlatPageVM flatPage)
        {
            _flatPage = flatPage;

            _unitOfWork = new UnitOfWork();
        }

        //Close app
        private ICommand _closeFormCommand;
        public ICommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand ?? (_closeFormCommand = new RelayCommand(obj =>
                {
                    RentalFormWindow window = obj as RentalFormWindow;
                    window.Close();
                }));
            }
        }

        private int _howManyYears;
        public int HowManyYears
        {
            get { return _howManyYears; }
            set
            {
                _howManyYears = value;
                OnPropertyChanged("HowManyYears");
            }
        }

        private ICommand _sendQueryCommand;
        public ICommand SendQueryCommand
        {
            get
            {
                return _sendQueryCommand ?? (_sendQueryCommand = new RelayCommand(obj =>
                {
                    if (HowManyYears != 0)
                    {
                        RentalFormWindow rentalForm = obj as RentalFormWindow;

                        var item = _unitOfWork.Users.GetUserOrders().Where(t => t.Flat.FlatId == _flatPage.CurrentFlat.FlatId).FirstOrDefault();

                        if (item == null)
                        {
                            

                            Lease lease = new Lease();
                            lease.FlatId = _flatPage.CurrentFlat.FlatId;
                            lease.UserId = CurrentUser.GetInstance().UserId;

                            DateTime startDate = DateTime.Now;
                            DateTime endDate = DateTime.Now.AddYears(HowManyYears);

                            lease.StartDate = startDate;
                            lease.EndDate = endDate;
                            //lease.TotalSum = (endDate - startDate).Days / 30 * _flatPage.CurrentFlat.Price;
                            lease.TotalSum = _flatPage.CurrentFlat.Price;

                            _unitOfWork.Leases.Create(lease);

                            var result = new CustomMessageBox("Заявка отправлена",
                                                MessageType.Success,
                                                MessageButtons.Ok).ShowDialog();
                        }
                        else
                        {
                            var result = new CustomMessageBox("Квартира уже добавлена",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                        }

                        rentalForm.Close();
                    }
                    else
                    {
                        var result = new CustomMessageBox("Выберите срок",
                                    MessageType.Error,
                                    MessageButtons.Ok).ShowDialog();
                    }
                }));
            }
        }
    }
}
