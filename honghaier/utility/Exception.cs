using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace honghaier.Utility 
{
    class ExpUtil 
    {
        public static string LogExp(Exception exception) 
        {
            return string.Format("{0}+\r\n+{1}", exception.Message, exception.StackTrace);
        }

        public static void LogProc(log4net.ILog log, Exception exception, bool show = true) 
        {
            LogHelper.Exception(log, exception, show);
        }
    }
}
