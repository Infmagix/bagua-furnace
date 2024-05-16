using Cytek.Generics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace honghaier
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // 检查进程是否已经运行  
            if (Process.GetProcessesByName("honghaier").Length > 1)
            {
                MessageBox.Show("程序已经运行，请关闭以后再打开。");
                this.Shutdown();
                return;
            }
            // Check UAC priviliage
            AdminRelauncher.RelaunchIfNotAdmin();
        }
    }
}
