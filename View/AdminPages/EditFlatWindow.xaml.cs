using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlatRental.View.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для EditFlatForm.xaml
    /// </summary>
    public partial class EditFlatWindow : Window
    {
        public EditFlatWindow()
        {
            InitializeComponent();
        }

        private void DragMoveWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
