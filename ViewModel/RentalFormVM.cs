using FlatRental.DataModel;
using FlatRental.Model;
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

        public RentalFormVM() { }

        public RentalFormVM(FlatPageVM flatPage)
        {
            _flatPage = flatPage;
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set 
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private decimal? _sum;
        public decimal? Sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = (StartDate.Day - EndDate.Day) * _flatPage.CurrentFlat.Price;
                OnPropertyChanged("Sum");
            }
        }

        private ICommand _sendQueryCommand;
        public ICommand SendQueryCommand
        {
            get
            {
                return _sendQueryCommand ?? (_sendQueryCommand = new RelayCommand(obj =>
                {
                    RentalFormWindow rentalForm = obj as RentalFormWindow;

                    Lease lease = new Lease();
                    lease.FlatId = _flatPage.CurrentFlat.FlatId;
                    lease.UserId = CurrentUser.GetInstance().UserId;
                    lease.StartDate = StartDate;
                    lease.EndDate = EndDate;
                    lease.TotalSum = (EndDate.Day - StartDate.Day) * _flatPage.CurrentFlat.Price;

                    FLAT_RENTALContext.GetContext().Leases.Add(lease);
                    FLAT_RENTALContext.GetContext().SaveChanges();

                    rentalForm.Close();
                }));
            }
        }
    }
}
