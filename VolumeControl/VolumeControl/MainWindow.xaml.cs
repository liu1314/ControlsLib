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
            this.currenttxt.Text = SPVolumeControl.Instance.VolumeCurrent.ToString();
            this.systxt.Text = SPVolumeControl.Instance.VolumeSystem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SPVolumeControl.Instance.VolumeCurrent = (float.Parse(this.currenttxt.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SPVolumeControl.Instance.VolumeSystem = (float.Parse(this.systxt.Text));
        }
    }
}
