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

namespace ICESetting.SettingControls
{
    /// <summary>
    /// ResolutionContorl.xaml 的交互逻辑
    /// </summary>
    public partial class ResolutionContorl : UserControl
    {

        public event EventHandler UpdateResolution {

            add { this.resolutionUI.UpdateResolution += value; }
            remove { this.resolutionUI.UpdateResolution -= value; }
        }  


        public ResolutionContorl()
        {
            InitializeComponent();
        }

        public void SetResolution()
        { }

        public void GoPre()
        { }

        public void GoNext()
        { }
    }
}
