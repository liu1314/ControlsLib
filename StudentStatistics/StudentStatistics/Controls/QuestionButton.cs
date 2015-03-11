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
    public class QuestionButton : PressButton
    {

        public static Brush ViewPressBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/StudentStatistics;component/Images/questionnumhalf.png", UriKind.RelativeOrAbsolute)));
            }
        }

        public static Brush ViewUnPressBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/StudentStatistics;component/Images/questionnum.png", UriKind.RelativeOrAbsolute)));
            }
        }

        public QuestionButton()
        {
            base.Width = 142;
            base.Height = 42;
            base.FontSize = 26;
            base.Foreground = new SolidColorBrush(Color.FromArgb(255, 120, 120, 120));

            base.PressBrush = ViewPressBrush;
            base.UnPressBrush = ViewUnPressBrush;
        }
    }
}
