using Jisons;
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

namespace VolumeControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           this.currenttxt.Value = SPVolumeControl.Instance.VolumeCurrent;
           this.systxt.Value= SPVolumeControl.Instance.VolumeSystem;
        }

       

        public double SystemValue
        {
            get { return (double)GetValue(SystemValueProperty); }
            set { SetValue(SystemValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SystemValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SystemValueProperty =
            DependencyProperty.Register("SystemValue", typeof(double), typeof(MainWindow), new PropertyMetadata(0d,PropertyChangedCallback));



        static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var de = (double)e.NewValue;
            SPVolumeControl.Instance.VolumeSystem = (float)de;
        }
        public double CurrentVolumeValue
        {
            get { return (double)GetValue(CurrentVolumeValueProperty); }
            set { SetValue(CurrentVolumeValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentVolumeValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentVolumeValueProperty =
            DependencyProperty.Register("CurrentVolumeValue", typeof(double), typeof(MainWindow), new PropertyMetadata(0d,PropertyChangedCallback));


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SPVolumeControl.Instance.VolumeCurrent = (float.Parse(this.currenttxt.Value.ToString()));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SPVolumeControl.Instance.VolumeSystem = (float.Parse(this.systxt.Value.ToString()));
        }
    }
}
