using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// StudentAccuracyControl.xaml 的交互逻辑
    /// </summary>
    public partial class StudentAccuracyControl : UserControl
    {

        public ObservableCollection<Info> Items
        {
            get { return (ObservableCollection<Info>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<Info>), typeof(StudentAccuracyControl), new PropertyMetadata(new ObservableCollection<Info>()));

        public StudentAccuracyControl()
        {

            InitializeComponent();

            //this.DataContext = this;

            this.Loaded += StudentAccuracyControl_Loaded;
        }

        void StudentAccuracyControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.TxtTime.Background = new AccuracyItemControl().ViewBrush; 
            Items.Clear();
            Items.Add(new Info() { Value1 = 0.1f });
            Items.Add(new Info() { Value1 = 0.21f });
            Items.Add(new Info() { Value1 = 0.31f });
            Items.Add(new Info() { Value1 = 0.41f });
            Items.Add(new Info() { Value1 = 0.51f });
            Items.Add(new Info() { Value1 = 0.61f });
            Items.Add(new Info() { Value1 = 0.71f });
            Items.Add(new Info() { Value1 = 0.81f });
            Items.Add(new Info() { Value1 = 0.91f });
            Items.Add(new Info() { Value1 = 1.1f });

        }

    }

    public class Info : INotifyPropertyChanged
    {
        public float Value1
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
