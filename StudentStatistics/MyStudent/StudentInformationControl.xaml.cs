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

namespace MyStudent
{
    /// <summary>
    /// StudentInformationControl.xaml 的交互逻辑
    /// </summary>
    public partial class StudentInformationControl : UserControl
    {

        List<string> NumberName = new List<string>() {"ads","sfd","dx","xcsd","ff" };
       

        public double RightRate
        {
            get { return (double)GetValue(RightRateProperty); }
            set { SetValue(RightRateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightRate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightRateProperty =
            DependencyProperty.Register("RightRate", typeof(double), typeof(StudentInformationControl), new PropertyMetadata(0.6d));


        public Brush RedBrush
        {
            get { return (Brush)GetValue(RedBrushProperty); }
            set { SetValue(RedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RedBrushProperty =
            DependencyProperty.Register("RedBrush", typeof(Brush), typeof(StudentInformationControl), new PropertyMetadata(Brushes.Red));


        public Brush BlueBrush
        {
            get { return (Brush)GetValue(BlueBrushProperty); }
            set { SetValue(BlueBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlueBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueBrushProperty =
            DependencyProperty.Register("BlueBrush", typeof(Brush), typeof(StudentInformationControl), new PropertyMetadata(Brushes.Blue));


        public StudentInformationControl()
        {
            InitializeComponent();
            this.Loaded += StudentInformationControl_Loaded;
        }

        void StudentInformationControl_Loaded(object sender, RoutedEventArgs e)
        {
            NumberName.Sort();//按名字排序
            NumberName.Reverse();
            this.txtblock.Text = (this.RightRate * 100).ToString() + "%";
            if (this.RightRate >= 0.6)
            {
                this.txtblock.Foreground = this.BlueBrush;
            }
            else
            {
                this.txtblock.Foreground = this.RedBrush;
            }
        }
    }
}
