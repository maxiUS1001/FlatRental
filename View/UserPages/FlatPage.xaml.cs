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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlatRental.View.UserPages
{
    /// <summary>
    /// Логика взаимодействия для FlatPage.xaml
    /// </summary>
    public partial class FlatPage : Page
    {
        public FlatPage()
        {
            InitializeComponent();
        }

        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            ScrollViewer scroll = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                if (scroll.VerticalOffset - e.Delta <= scroll.ExtentHeight - scroll.ViewportHeight)
                {
                    scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
                }
                else
                {
                    scroll.ScrollToBottom();
                }
            }
            else
            {
                if (scroll.VerticalOffset + e.Delta > 0)
                {
                    scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
                }
                else
                {
                    scroll.ScrollToTop();
                }
            }
            e.Handled = true;
        }
    }
}
