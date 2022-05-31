using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FlatRental.Model
{
    public static class FilterPanelAnimation
    {
        private static bool _filterBool = true;
        public static SineEase easingFunction1 = new SineEase();

        public static void ShowOrHidePanel(Border border)
        {
            ThicknessAnimation OpenMn = new ThicknessAnimation();
            if (_filterBool == true)
            {
                OpenMn.From = new Thickness(10, -340, 0, 0);
                OpenMn.To = new Thickness(10, 0, 0, 0);
                _filterBool = false;
            }
            else
            {
                OpenMn.From = new Thickness(10, 0, 0, 0);
                OpenMn.To = new Thickness(10, -340, 0, 0);
                _filterBool = true;
            }

            OpenMn.Duration = TimeSpan.FromSeconds(0.5);
            easingFunction1.EasingMode = EasingMode.EaseOut;
            OpenMn.EasingFunction = easingFunction1;
            Storyboard strbrdmn = new Storyboard();
            Storyboard.SetTargetName(OpenMn, border.Name);
            Storyboard.SetTargetProperty(OpenMn, new PropertyPath("Margin"));
            strbrdmn.Children.Add(OpenMn);
            strbrdmn.Begin(border);
        }
    }
}
