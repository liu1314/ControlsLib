using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StudentStatistics
{
    public class TittleButton : PressButton
    {

        public static Brush ViewPressBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/StudentStatistics;component/Images/topbuttonclick.png", UriKind.RelativeOrAbsolute)));
            }
        }

        public static Brush ViewUnPressBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/StudentStatistics;component/Images/topbutton.png", UriKind.RelativeOrAbsolute)));
            }
        }

        public TittleButton()
        {
            base.Width = 202;
            base.Height = 60;
            base.FontSize = 26;

            base.PressBrush = ViewPressBrush;
            base.UnPressBrush = ViewUnPressBrush;

            base.PressTextBrush = Brushes.Black;
            base.UnPressTextBrush = Brushes.White;
        }
    }
}
