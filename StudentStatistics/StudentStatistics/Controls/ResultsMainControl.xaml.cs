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
    /// ResultsMainControl.xaml 的交互逻辑
    /// </summary>
    public partial class ResultsMainControl : UserControl
    {
        public ResultsMainControl()
        {
            InitializeComponent();

            this.Loaded += ResultsMainControl_Loaded;
        }

        void ResultsMainControl_Loaded(object sender, RoutedEventArgs e)
        {

            //this.viewcontrol.Children.Add(new StudentCompletionStatisticsControl());
            this.viewcontrol.Children.Add(new StudentAccuracyControl());
            //
        }

        private void PressButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 成员正确率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Achievement_Click(object sender, RoutedEventArgs e)
        {
            this.viewcontrol.Children.Clear();
            this.viewcontrol.Children.Add(new StudentAchievementControl());
        }

        /// <summary>
        /// 各题正确率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            this.viewcontrol.Children.Clear();
            this.viewcontrol.Children.Add(new StudentAnswerControl());
        }

        /// <summary>
        /// 平均正确率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Completion_Click(object sender, RoutedEventArgs e)
        {
            this.viewcontrol.Children.Clear();
            this.viewcontrol.Children.Add(new StudentCompletionStatisticsControl());
        }

        /// <summary>
        /// 平均用时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accuracy_Click(object sender, RoutedEventArgs e)
        {
            this.viewcontrol.Children.Clear();
            this.viewcontrol.Children.Add(new StudentAccuracyControl());
        }

    }
}
