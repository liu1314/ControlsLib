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
    /// CircleControl.xaml 的交互逻辑
    /// </summary>
    public partial class CircleControl : UserControl
    {
        public CircleControl()
        {
            InitializeComponent();
            this.Loaded += CircleControl_Loaded;

        }

        void CircleControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.CompetionRate.Foreground = this.UncompletionBrush;
            this.UncompletionRate.Foreground = this.CompletionBrush;
            this.UncompletionRate.Content =(1 - completion)*100+"%";
            this.CompetionRate.Content = completion * 100 + "%";
            this.CompetionRate.FontSize = 28;
            this.UncompletionRate.FontSize = 18;
            SetView();
            SetLocation();
        }
        private string endcompletion;
        public string complete
        {

            get { return endcompletion; }
            set { endcompletion = value = (completion * 100).ToString() + "%"; }

        }


        public float completion
        {
            get { return (float)GetValue(completionProperty); }
            set { SetValue(completionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for completion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty completionProperty =
            DependencyProperty.Register("completion", typeof(float), typeof(CircleControl), new PropertyMetadata(0.95f, completionPropertyChangedCallback));

        private static void completionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CircleControl).SetView();

        }




        public Brush CompletionBrush
        {
            get { return (Brush)GetValue(CompletionBrushProperty); }
            set { SetValue(CompletionBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CompletionBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompletionBrushProperty =
            DependencyProperty.Register("CompletionBrush", typeof(Brush), typeof(CircleControl), new PropertyMetadata(Brushes.Black));





        public Brush UncompletionBrush
        {
            get { return (Brush)GetValue(UncompletionBrushProperty); }
            set { SetValue(UncompletionBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UncompletionBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UncompletionBrushProperty =
            DependencyProperty.Register("UncompletionBrush", typeof(Brush), typeof(CircleControl), new PropertyMetadata(Brushes.Yellow));

        int Angle = 0;
        public void SetView()
        {
            Angle = (int)(this.completion * 360);
            this.Uncompletion.StartAngle = this.comp.EndAngle = Angle;
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
            UnCompletionLable.X = UnCompletionLable.X - UncompletionRate.ActualWidth / 2;
            UnCompletionLable.Y = UnCompletionLable.Y - UncompletionRate.ActualHeight / 2;
            Canvas.SetTop(this.UncompletionRate, UnCompletionLable.Y);
            Canvas.SetLeft(this.UncompletionRate, UnCompletionLable.X);
            CompletionLable.X = CompletionLable.X - UncompletionRate.ActualWidth / 2;
            CompletionLable.Y = CompletionLable.Y - UncompletionRate.ActualHeight / 2;
            Canvas.SetTop(this.CompetionRate, CompletionLable.Y);
            Canvas.SetLeft(this.CompetionRate, CompletionLable.X);


        }
    }
}
