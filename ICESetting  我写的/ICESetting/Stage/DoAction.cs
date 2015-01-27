﻿using ICESetting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using UserMessageBox;

namespace ICESetting.Stage
{
    public partial class SettingStage
    {
        MessageWin CurrentMessageWin;
        public static bool isOrdersValid = true;
        public void DealCmd(string cmd)
        {
            if (!isReadyReboot && isOrdersValid)
            {
                var msg = Utility.ConvertFromStringToMsg(cmd);
                DealMsg(msg);
            }
        }

        /// <summary> 控制执行间隔，以防执行过快 </summary>
        /// <param name="intervialSceond"></param>
        public static void OrdersIntervial(double intervialSceond)
        {
            isOrdersValid = false;
            var dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(intervialSceond) };
            dt.Tick += delegate
            {
                isOrdersValid = true;
                dt.Stop();
            };
            dt.Start();
        }
    }
}
