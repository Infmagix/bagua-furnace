using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace honghaier.Utility {
    public class LogHelper 
    {
        public static void Debug(string message, log4net.ILog log) 
        {
            if (log == null) return;

            log.Debug(message);
        }
        public static void Info(string message, log4net.ILog log) 
        {
            if (log == null) return;

            log.Info(message);
        }
        public static void Warn(string message, log4net.ILog log) 
        {
            if (log == null) return;

            log.Warn(message);
        }
        public static void Error(string message, log4net.ILog log) 
        {
            if (log == null) return;

            log.Error(message);
        }
        public static void Fatal(string message, log4net.ILog log) 
        {
            if (log == null) return;

            log.Fatal(message);
        }

        public static void Exception(log4net.ILog log, Exception exception, bool show = true) 
        {
            if (log == null) return;

            log.Error(ExpUtil.LogExp(exception));
            if (show == true) 
            {
                MessageBox.Show(ExpUtil.LogExp(exception), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
