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
    /// AccuracyItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class AccuracyItemControl : UserControl
    {

        public float ViewWidth
        {
            get { return (float)GetValue(ViewWidthProperty); }
            set { SetValue(ViewWidthProperty, value); }
        }
        public static readonly DependencyProperty ViewWidthProperty =
            DependencyProperty.Register("ViewWidth", typeof(float), typeof(AccuracyItemControl), new PropertyMetadata(40f));

        public float ViewHeight
        {
            get { return (float)GetValue(ViewHeightProperty); }
            set { SetValue(ViewHeightProperty, value); }
        }
        public static readonly DependencyProperty ViewHeightProperty =
            DependencyProperty.Register("ViewHeight", typeof(float), typeof(AccuracyItemControl), new PropertyMetadata(440f));

        public Brush ViewBrush
        {
            get { return (Brush)GetValue(ViewBrushProperty); }
            set { SetValue(ViewBrushProperty, value); }
        }
        public static readonly DependencyProperty ViewBrushProperty =
            DependencyProperty.Register("ViewBrush", typeof(Brush), typeof(AccuracyItemControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 242, 112, 110))));

        public float SpanHeight
        {
            get { return (float)GetValue(SpanHeightProperty); }
            set { SetValue(SpanHeightProperty, value); }
        }
        public static readonly DependencyProperty SpanHeightProperty =
            DependencyProperty.Register("SpanHeight", typeof(float), typeof(AccuracyItemControl), new PropertyMetadata(0f));

        public float CompletionRate
        {
            get { return (float)GetValue(CompletionRateProperty); }
            set { SetValue(CompletionRateProperty, value); }
        }
        public static readonly DependencyProperty CompletionRateProperty =
            DependencyProperty.Register("CompletionRate", typeof(float), typeof(AccuracyItemControl), new PropertyMetadata(0.5f, CompletionRateChangedCallback));
        private static void CompletionRateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AccuracyItemControl).RefreshView();
        }

        public string ViewText
        {
            get { return (string)GetValue(ViewTextProperty); }
            set { SetValue(ViewTextProperty, value); }
        }
        public static readonly DependencyProperty ViewTextProperty =
            DependencyProperty.Register("ViewText", typeof(string), typeof(AccuracyItemControl), new PropertyMetadata(string.Empty));

        public AccuracyItemControl()
        {
            InitializeComponent();

            this.Loaded += AccuracyItemControl_Loaded;
        }

        void AccuracyItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            var rate = this.CompletionRate > 1 ? 1 : this.CompletionRate;
            rate = rate < 0 ? 0 : rate;
            this.SpanHeight = (float)(rate * this.ViewHeight);
        }
    }
}
