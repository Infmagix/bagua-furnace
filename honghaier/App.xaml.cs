using Cytek.Generics;
using SciChart.Charting.Visuals;
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

            SciChartSurface.SetRuntimeLicenseKey("e3deB7gfX+eU3EdsGe2NoZryYACUhN+LFDm+AcVfY97o6DvD3JHB/oPNdq2PUQBe1gj1E9LQw6fZ3+ReYvLz+1Xx6hLdXWOlivNr4k3G+/jPAQGCL3ShvOOjqQFgr0PhdJqHTKJvClrnLgKZ43fsj4Wf7GHvgfwni+lsuI9i5qvH66sDnkgKZv8XDIinHyFBhqIG7/eYYg/5zJKeS5KwwJFrxuHH7wclg9OqDHwYEaN4yqRQAYLAQDWVjQyuEbawk3h3uHcPMgT9KQHGdsGsa5LYKoaKPZHGI1xg+b4NTB3034t/JvxJvTXBx6o/thD37KZkeqnXb2pTrj383z8teM0ecNKrnBU927614eqz4WKfuH1p2FneDZsSIgJZDLACmZMztHWSuYak8FTQoZUigJsOVHfBQmAiUo+3KAWKegnRtLzkfX8UZezKuBwnCa80axRL0DcWscyrMiKWrNhJjxJwRleEB/FKbxvg7aQljjJb8JSN5lm7r4X3Y51hwXySXXjmlHKNybk69CTiufaaHaxmxw==");
        }
    }
}
