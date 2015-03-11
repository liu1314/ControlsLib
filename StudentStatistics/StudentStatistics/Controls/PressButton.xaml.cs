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
    public partial class PressButton : UserControl
    {

        public Brush PressBrush
        {
            get { return (Brush)GetValue(PressBrushProperty); }
            set { SetValue(PressBrushProperty, value); }
        }
        public static readonly DependencyProperty PressBrushProperty =
            DependencyProperty.Register("PressBrush", typeof(Brush), typeof(PressButton), new PropertyMetadata(Brushes.Transparent));

        public Brush UnPressBrush
        {
            get { return (Brush)GetValue(UnPressBrushProperty); }
            set { SetValue(UnPressBrushProperty, value); }
        }
        public static readonly DependencyProperty UnPressBrushProperty =
            DependencyProperty.Register("UnPressBrush", typeof(Brush), typeof(PressButton), new PropertyMetadata(Brushes.Transparent));

        public string ViewText
        {
            get { return (string)GetValue(ViewTextProperty); }
            set { SetValue(ViewTextProperty, value); }
        }
        public static readonly DependencyProperty ViewTextProperty =
            DependencyProperty.Register("ViewText", typeof(string), typeof(PressButton), new PropertyMetadata(string.Empty));

        public Brush PressTextBrush
        {
            get { return (Brush)GetValue(PressTextBrushProperty); }
            set { SetValue(PressTextBrushProperty, value); }
        }
        public static readonly DependencyProperty PressTextBrushProperty =
            DependencyProperty.Register("PressTextBrush", typeof(Brush), typeof(PressButton), new PropertyMetadata(null));

        public Brush UnPressTextBrush
        {
            get { return (Brush)GetValue(UnPressTextBrushProperty); }
            set { SetValue(UnPressTextBrushProperty, value); }
        }
        public static readonly DependencyProperty UnPressTextBrushProperty =
            DependencyProperty.Register("UnPressTextBrush", typeof(Brush), typeof(PressButton), new PropertyMetadata(null));

        public event RoutedEventHandler Click
        {
            add { this.viewbutton.Click += value; }
            remove { this.viewbutton.Click -= value; }
        }

        public PressButton()
        {
            InitializeComponent();

            //this.DataContext = this;

            this.Loaded += PressButton_Loaded;
        }

        void PressButton_Loaded(object sender, RoutedEventArgs e)
        {
            SetUnPress();
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            this.viewbutton.Background = this.PressBrush;

            this.viewtextblock.Foreground = this.PressTextBrush == null ? this.Foreground : this.PressTextBrush;
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            SetUnPress();
        }

        public void SetUnPress()
        {
            this.viewbutton.Background = this.UnPressBrush;
            this.viewtextblock.Foreground = this.PressTextBrush == null ? this.Foreground : this.UnPressTextBrush;
        }
    }
}
