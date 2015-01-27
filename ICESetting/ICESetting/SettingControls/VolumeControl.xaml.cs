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
using Helpers;
using ICESetting.Stage;
using ICESetting.Utils;

namespace ICESetting
{
    /// <summary> 音量控制控件 </summary>
    public partial class VolumeControl : UserControl
    {

        /// <summary> 当前系统音量 </summary>
        public double VolumeValue
        {
            get { return (double)GetValue(VolumeValueProperty); }
            set { SetValue(VolumeValueProperty, value); }
        }
        public static readonly DependencyProperty VolumeValueProperty =
            DependencyProperty.Register("VolumeValue", typeof(double), typeof(VolumeControl), new PropertyMetadata((double)VolumeHelper.Instance.VolumeSystem, VolumeValueChangedCallback));

        /// <summary> 音量增减量  与slider控件的value相互绑定 </summary>
        public int VolumeSpan
        {
            get { return (int)GetValue(VolumeSpanProperty); }
            set { SetValue(VolumeSpanProperty, value); }
        }
        public static readonly DependencyProperty VolumeSpanProperty =
            DependencyProperty.Register("VolumeSpan", typeof(int), typeof(VolumeControl), new PropertyMetadata(3));

        static void VolumeValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (double)e.NewValue;
            int volume = (int)(value * 100);
            if (volume != (int)VolumeHelper.Instance.VolumeSystem)
            {
                VolumeHelper.Instance.VolumeSystem = (float)(volume / 100);
            }
            var control = d as VolumeControl;
            control.InitView();

        }
        public VolumeControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += VolumeControl_Loaded;
            VolumeHelper.Instance.OnVolumeNotification += Instance_OnVolumeNotification;
        }

        void VolumeControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitView();
        }
        /// <summary>外部系统更改音量时同步显示 </summary>
        void Instance_OnVolumeNotification(CoreAudioApi.AudioVolumeNotificationData data)
        {
            this.Dispatcher.BeginInvoke((Action)(() => { VolumeValue = VolumeHelper.Instance.VolumeSystem; }));
        }

        /// <summary> 音量降低 </summary>
        public void DecreaseVolumn()
        {
            SettingStage.OrdersIntervial(0.2);
            var volume = VolumeValue - VolumeSpan;
            VolumeValue = volume <= 0 ? 0 : volume;
        }

        /// <summary> 音量增加 </summary>
        public void IncreaseVolumn()
        {
            SettingStage.OrdersIntervial(0.2);
            var volume = VolumeValue + VolumeSpan;
            VolumeValue = volume >= 100 ? 100 : volume;
        }

        /// <summary> 刷新界面显示 </summary>
        private void InitView()
        {
            volumeimg.Width = 318 * VolumeValue / 100d;
            Utility.INIFILE.SetValue("MAIN", "SystemVolumn", VolumeValue.ToString());
        }

    }
}
