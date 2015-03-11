using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StudentStatistics;

namespace StudentStatistics
{
    /// <summary>
    /// StudentAchievementControl.xaml 的交互逻辑
    /// </summary>
    public partial class StudentAchievementControl : UserControl
    {

        public ObservableCollection<StudentData> Items
        {
            get { return (ObservableCollection<StudentData>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<StudentData>), typeof(StudentAchievementControl), new PropertyMetadata(new ObservableCollection<StudentData>()));

        ObservableCollection<StudentData> orderItem = new ObservableCollection<StudentData>();

        public StudentAchievementControl()
        {
            InitializeComponent();
            this.orderItem.Add(new StudentData() { Name = "aji2", Rate = 0.8d });
            this.orderItem.Add(new StudentData() { Name = "bi1", Rate = 0.7d });
            this.orderItem.Add(new StudentData() { Name = "cji2", Rate = 0.3d });
            this.Items = new ObservableCollection<StudentData>(orderItem);
            this.Loaded += StudentAchievementControl_Loaded;
        }


        void StudentAchievementControl_Loaded(object sender, RoutedEventArgs e)
        {

            //for (int i = 0; i < 10; i++)
            //{
            //      this.grid.Children.Add(new StudentInfo("asaas",0.5));
            //}


        }

        private void StudentInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ResultsMainControl RealControl = new ResultsMainControl();
            //RealControl.viewcontrol.Children.Clear();
            //RealControl.viewcontrol.Children.Add(new StudentAnswerControl());
        }

        private void OrderBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var items = orderItem.OrderBy<StudentData, double>((Func<StudentData, double>)((i) => { return 1d; }));
            this.Items = new ObservableCollection<StudentData>(items);

        }

        private void OrderBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        bool aa = false;

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (aa)
            {
                var items = orderItem.OrderBy((Func<StudentData, double>)((i) => { return i.Rate; }));
                this.Items = new ObservableCollection<StudentData>(items);
            }
            else
            {
                var items = orderItem.OrderBy((Func<StudentData, string>)((i) => { return i.Name; }));
                this.Items = new ObservableCollection<StudentData>(items);
            }

            aa = !aa;
        }
    }

    public class StudentData
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double rate;

        public double Rate
        {
            get { return rate; }
            set { rate = value; }
        }




    }


}
