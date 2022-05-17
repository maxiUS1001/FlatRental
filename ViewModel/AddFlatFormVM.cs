using FlatRental.DataModel;
using FlatRental.Model;
using FlatRental.View;
using FlatRental.View.AdminPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;


using System.Diagnostics;

namespace FlatRental.ViewModel
{
    public class AddFlatFormVM : ObservableObject
    {
        private EditFlatsVM _editFlatsModel;

        public AddFlatFormVM() { }

        public AddFlatFormVM(EditFlatsVM editFlatsModel) 
        {
            _editFlatsModel = editFlatsModel;
        }

        //Close form
        private ICommand _closeFormCommand;
        public ICommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand ?? (_closeFormCommand = new RelayCommand(obj =>
                {
                    AddFlatWindow form = obj as AddFlatWindow;
                    form.Close();
                }));
            }
        }


        //Adding flat
        private ICommand _addFlatCommand;
        public ICommand AddFlatCommand
        {
            get
            {
                return _addFlatCommand ?? (_addFlatCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        AddFlatWindow addFlatForm = obj as AddFlatWindow;

                        Flat flat = new Flat();
                        flat.Metro = addFlatForm.MetroTextBox.Text;
                        flat.District = addFlatForm.DistrictTextBox.Text;
                        flat.Мicrodistrict = addFlatForm.MicrodistrictTextBox.Text;
                        flat.NumberOfRooms = int.Parse(addFlatForm.RoomsTextBox.Text);
                        flat.RentalType = addFlatForm.RentalTypeTextBox.Text;
                        flat.Area = decimal.Parse(addFlatForm.AreaTextBox.Text.Replace(".", ","));
                        flat.Toilet = addFlatForm.ToiletTextBox.Text;
                        flat.Balcony = addFlatForm.BalconyTextBox.Text;
                        flat.Floor = int.Parse(addFlatForm.FloorTextBox.Text);
                        flat.Price = decimal.Parse(addFlatForm.PriceTextBox.Text.Replace(".", ","));
                        flat.Description = addFlatForm.DescriptionTextBox.Text;
                        flat.Image = addFlatForm.ImageTextBox.Text;

                        if (Validation.CheckValid(flat))
                        {
                            FLAT_RENTALContext.GetContext().Flats.Add(flat);
                            FLAT_RENTALContext.GetContext().SaveChanges();
                            
                            _editFlatsModel.FlatList = new ObservableCollection<Flat>(FLAT_RENTALContext.GetContext().Flats.ToList());                          
                            
                            var result = new CustomMessageBox("Квартира добавлена",
                                        MessageType.Success,
                                        MessageButtons.Ok).ShowDialog();
                           
                            addFlatForm.Close();
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
