﻿using System;
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

namespace FlatRental.View.UserPages
{
    /// <summary>
    /// Логика взаимодействия для RentalFormWindow.xaml
    /// </summary>
    public partial class RentalFormWindow : Window
    {
        public RentalFormWindow()
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
