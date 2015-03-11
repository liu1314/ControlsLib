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
    /// CompletionRateStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class StudentCompletionStatisticsControl : UserControl
    {

        public float CompletionRate
        {
            get { return (float)GetValue(CompletionRateProperty); }
            set { SetValue(CompletionRateProperty, value); }
        }
        public static readonly DependencyProperty CompletionRateProperty =
            DependencyProperty.Register("CompletionRate", typeof(float), typeof(StudentCompletionStatisticsControl), new PropertyMetadata(0.7f, CompletionRateChangedCallback));
        private static void CompletionRateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StudentCompletionStatisticsControl).RefreshView();
        }

        public Brush CompletionBrush
        {
            get { return (Brush)GetValue(CompletionBrushProperty); }
            set { SetValue(CompletionBrushProperty, value); }
        }
        public static readonly DependencyProperty CompletionBrushProperty =
            DependencyProperty.Register("CompletionBrush", typeof(Brush), typeof(StudentCompletionStatisticsControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 242, 112, 110))));

        public Brush DisCompletionBrush
        {
            get { return (Brush)GetValue(DisCompletionBrushProperty); }
            set { SetValue(DisCompletionBrushProperty, value); }
        }
        public static readonly DependencyProperty DisCompletionBrushProperty =
            DependencyProperty.Register("DisCompletionBrush", typeof(Brush), typeof(StudentCompletionStatisticsControl), new PropertyMetadata(Brushes.White));

        public StudentCompletionStatisticsControl()
        {
            InitializeComponent();
            this.Loaded += CompletionRateStatisticsControl_Loaded;
        }

        void CompletionRateStatisticsControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.TxtRight.Background = DisCompletionBrush;
            this.TxtWrong.Background = CompletionBrush;
            this.CompetionLable.Foreground = this.DisCompletionBrush;
            this.UncompletionLable.Foreground = this.CompletionBrush;
            this.UncompletionLable.Content = (1 - CompletionRate) * 100 + "%";
            this.CompetionLable.Content = CompletionRate * 100 + "%";
            this.TxtRightRate.Text = CompletionRate * 100 + "%";
            this.CompetionLable.FontSize = 28;
            this.UncompletionLable.FontSize = 18;
            RefreshView();
            SetLocation();

        }
        int Angle = 0;
        private void RefreshView()
        {
            Angle = (int)(this.CompletionRate * 360);
            this.completionarc.StartAngle = 0;
            this.discompletionarc.EndAngle = 360;
            this.completionarc.EndAngle = this.discompletionarc.StartAngle = Angle;
        }


        /// <summary>
        /// 计算lable的方位
        /// </summary>
        public void SetLocation()
        {
            Point CircleHeart = new Point(200, 200);  //设置圆心的坐标
            Point CompletionLable = new Point();
            Point UnCompletionLable = new Point();
            int R = 200;
            if (Angle <= 180)
            {
                CompletionLable.X = R + (R / 2) * Math.Sin(Angle / 2 * Math.PI / 180);
                CompletionLable.Y = R - (R / 2) * Math.Cos(Angle / 2 * Math.PI / 180);
            }

            else
            {
                CompletionLable.X = Math.Sin((180 - Angle / 2) * (Math.PI / 180)) * (R / 2) + R;
                CompletionLable.Y = Math.Cos((180 - Angle / 2) * (Math.PI / 180)) * (R / 2) + R;
            }
            UnCompletionLable.X = 2 * R - CompletionLable.X;
            UnCompletionLable.Y = 2 * R - CompletionLable.Y;
            UnCompletionLable.X = UnCompletionLable.X - UncompletionLable.ActualWidth / 2;
            UnCompletionLable.Y = UnCompletionLable.Y - UncompletionLable.ActualHeight / 2;
            Canvas.SetTop(this.UncompletionLable, UnCompletionLable.Y);
            Canvas.SetLeft(this.UncompletionLable, UnCompletionLable.X);
            CompletionLable.X = CompletionLable.X - UncompletionLable.ActualWidth / 2;
            CompletionLable.Y = CompletionLable.Y - UncompletionLable.ActualHeight / 2;
            Canvas.SetTop(this.CompetionLable, CompletionLable.Y);
            Canvas.SetLeft(this.CompetionLable, CompletionLable.X);


        }
    }
}
