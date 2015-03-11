using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentStatistics
{
    /// <summary>
    /// StudentAnswerControl.xaml 的交互逻辑
    /// </summary>
    public partial class StudentInfo : UserControl
    {

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Username.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(string), typeof(StudentInfo), new PropertyMetadata("hahshh"));


        public double RightRate
        {
            get { return (double)GetValue(RightRateProperty); }
            set { SetValue(RightRateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightRate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightRateProperty =
            DependencyProperty.Register("RightRate", typeof(double), typeof(StudentInfo), new PropertyMetadata(null));


        public Brush RedBrush
        {
            get { return (Brush)GetValue(RedBrushProperty); }
            set { SetValue(RedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RedBrushProperty =
            DependencyProperty.Register("RedBrush", typeof(Brush), typeof(StudentInfo), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 242, 112, 110))));

        public Brush BlueBrush
        {
            get { return (Brush)GetValue(BlueBrushProperty); }
            set { SetValue(BlueBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlueBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueBrushProperty =
            DependencyProperty.Register("BlueBrush", typeof(Brush), typeof(StudentInfo), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(216, 255, 0))));

        public Brush PressBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/StudentStatistics;component/Images/peoplebottomclick.png", UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush UnPressBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/StudentStatistics;component/Images/peoplebottom.png", UriKind.RelativeOrAbsolute)));
            }
        }
        public StudentInfo()
        {
            InitializeComponent();
            this.Loaded += StudentInfo_Loaded;
            this.Margin = new Thickness(0, 0, -2, 0);
        }

        public StudentInfo(string name, double rightrate)
           : this()
        {
            this.Username = name;
            this.RightRate = rightrate;
        }

        void StudentInfo_Loaded(object sender, RoutedEventArgs e)
        {
            this.StudentRightRate.Text = (this.RightRate * 100).ToString() + "%";
            this.StuedentName.Text = this.Username;
            if (this.RightRate >= 0.6)
            {
                this.StudentRightRate.Foreground = this.BlueBrush;
            }
            else
            {
                this.StudentRightRate.Foreground = this.RedBrush;
            }
            SetUnPress();
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            this.viewbutton.Background = this.PressBrush;

            //this.viewtextblock.Foreground = this.PressTextBrush == null ? this.Foreground : this.PressTextBrush;
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            SetUnPress();
        }

        public void SetUnPress()
        {
            this.viewbutton.Background = this.UnPressBrush;
            //this.viewtextblock.Foreground = this.PressTextBrush == null ? this.Foreground : this.UnPressTextBrush;
        }

        private void viewbutton_Click(object sender, RoutedEventArgs e)
        {
            ResultsMainControl RealControl = new ResultsMainControl();
            RealControl.viewcontrol.Children.Clear();
            RealControl.viewcontrol.Children.Add(new StudentAnswerControl());
        }

        private void viewbutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResultsMainControl RealControl = new ResultsMainControl();
            RealControl.viewcontrol.Children.Clear();
            RealControl.viewcontrol.Children.Add(new StudentAnswerControl());
        }
    }
}
