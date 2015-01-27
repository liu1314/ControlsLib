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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Jisons;
using ICESetting.Stage;
using ICESetting.Utils;
using TVM.WPF.Library.Animation;

namespace ICESetting
{
    /// <summary> 分辨率控制控件 </summary>
    public partial class ResolutionControl : UserControl
    {

        private static ImageSource GraduationImageSource = new BitmapImage(new Uri("pack://application:,,,/ICESetting;component/Images/刻度.png", UriKind.RelativeOrAbsolute));

        private List<double> ListX = new List<double>();
        private double traceWidth = 14;
        private int SelectedIndex = 0;

        private List<Devmode> devmodes;
        /// <summary> 当前可设置的分辨率 </summary>
        public List<Devmode> Devmodes
        {
            get
            {
                if (devmodes == null)
                {
                    devmodes = ResolutionHelper.GetSettingDevmodes().ToList();
                    devmodes.Reverse();
                }
                return devmodes;
            }
        }

        /// <summary> 当前分辨率 </summary>
        public Devmode CurrentDevmode
        {
            get { return (Devmode)GetValue(CurrentDevmodeProperty); }
            set { SetValue(CurrentDevmodeProperty, value); }
        }
        public static readonly DependencyProperty CurrentDevmodeProperty =
            DependencyProperty.Register("CurrentDevmode", typeof(Devmode), typeof(ResolutionControl), new PropertyMetadata(null, CurrentDevmodeChangedCallback));
        static void CurrentDevmodeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ResolutionControl;
            control.InitView();
        }

        /// <summary>
        /// 分辨率更改通知
        /// </summary>
        public event EventHandler UpdateResolution;
        protected void UpdateResolutionHandle()
        {
            if (UpdateResolution != null)
            {
                UpdateResolution(this, new EventArgs());
            }
        }

        public ResolutionControl()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Loaded += ResolutionControl_Loaded;
        }

        void ResolutionControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadControl();

            //外部更改分辨率后切换到窗体自动刷新界面动画
            var window = this.FindVisualParent<Window>();
            if (window != null)
            {
                window.Activated += window_Activated;
            }
        }

        void window_Activated(object sender, EventArgs e)
        {
            SetDefaultSelect();
        }

        /// <summary> 加载滚动条内容 </summary>
        private void LoadControl()
        {
            double gap = (this.rootGrid.ActualWidth - Devmodes.Count * traceWidth) / (Devmodes.Count - 1);
            List<Image> ListImage = new List<Image>();
            for (int i = 0; i < Devmodes.Count; i++)
            {
                Image img = new Image() { Width = 14, Height = 22, Source = GraduationImageSource };
                if ((i + 1) != Devmodes.Count)
                {
                    img.Margin = new Thickness(0, 0, gap, 0);
                }
                else
                {
                    img.Margin = new Thickness(0, 0, 0, 0);
                }
                ListImage.Add(img);
                stack.Children.Add(img);
            }

            SetDefaultSelect();

            DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.2) };
            dt.Tick += delegate
            {
                dt.Stop();
                for (int i = 0; i < ListImage.Count; i++)
                {
                    Point pt = ListImage[i].TranslatePoint(new Point(0, 0), this.rootGrid);
                    ListX.Add(pt.X);
                }
                MoveTo();
            };
            dt.Start();
        }

        /// <summary> 设置默认选中分辨率动画 </summary>
        public void SetDefaultSelect()
        {
            var devmode = ResolutionHelper.GetCurrentDevmode();
            var selectdevmode = Devmodes.FirstOrDefault(i => i.Equals(devmode));
            if (devmode != null)
            {
                CurrentDevmode = devmode;

                UpdateResolutionHandle();
            }
        }

        /// <summary> 加载界面动画 </summary>
        private void InitView()
        {
            this.SelectedIndex = this.Devmodes.IndexOf(this.CurrentDevmode);
            MoveTo();
        }

        /// <summary> 分辨率向前选择 </summary>
        public void GoPre()
        {
            if (SelectedIndex != 0)
            {
                SelectedIndex--;
                this.CurrentDevmode = this.Devmodes[SelectedIndex];
            }
            MoveTo();
        }

        /// <summary> 分辨率向后选择 </summary>
        public void GoNext()
        {
            if (SelectedIndex != this.Devmodes.Count - 1)
            {
                SelectedIndex++;
                this.CurrentDevmode = this.Devmodes[SelectedIndex];
            }
            MoveTo();
        }

        /// <summary> 重绘界面动画 </summary>
        private void MoveTo()
        {
            if (ListX.Count <= 0 || this.CurrentDevmode == null)
            {
                return;
            }

            double x = ListX[SelectedIndex];
            double tox = x - showR.Width / 2.0 + 7;
            Utility.PlaySound("clickMove");
            PennerDoubleAnimation daX = new PennerDoubleAnimation() { To = tox, Equation = Equations.QuartEaseOut, Duration = TimeSpan.FromSeconds(0.5) };
            showR.BeginAnimation(Canvas.LeftProperty, daX);
            text.Text = this.CurrentDevmode.PelsWidth.ToString() + "*" + this.CurrentDevmode.PelsHeight.ToString();
            DoubleAnimation daO = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(0.5) };
            text.BeginAnimation(OpacityProperty, daO);
        }

        /// <summary> 设置分辨率 </summary>
        public void SetResolution()
        {
            CurrentDevmode.SetCurrentDevmode();
            Utility.PlaySound("clickDown");
            DoubleAnimation daO = new DoubleAnimation() { From = 1, To = 0.5, AutoReverse = true, Duration = TimeSpan.FromSeconds(0.3) };
            img.BeginAnimation(OpacityProperty, daO);
            text.BeginAnimation(OpacityProperty, daO);
            Utility.INIFILE.SetValue("MAIN", "Resolution", CurrentDevmode.PelsWidth.ToString() + "*" + CurrentDevmode.PelsHeight.ToString());
            UpdateResolutionHandle();
        }

    }
}
