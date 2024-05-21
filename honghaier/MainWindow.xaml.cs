using honghaier.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TwinCAT.Ads;
using Merlincomm;
using Grpc.Core;
using System.Threading;
using System.Configuration;
using System.Runtime.InteropServices;
using honghaier.Model;
using honghaier.ViewModel;
using honghaier.View;

namespace honghaier
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        private log4net.ILog log = null;
        bool init = true;
        private TcAdsClient adsClient;
        //private System.Timers.Timer timerRead;
        //private System.Timers.Timer timerWrite;
        //private System.Timers.Timer timerHeartBeat;
        private int hPLCReadStruct;
        private int hPLCWriteStruct;
        private int hPLCHeartBeatStruct;
        // private int hA;
        private Random random;
        private int TimeOut = 5;
        private ushort curSNWrite = 0;
        private int curSNRead = -1;
        private ushort maxUnSyncTimes = 5;
        private ushort unSyncTimes = 0;

        public MainWindow()
        {
            ApplyCustomizedConfig();

            log = log4net.LogManager.GetLogger("MainWindow");
            InitializeComponent();
            //Loaded += MainWindow_Loaded;
            InitGRPCServer();
            //InitPLCTest();

            random = new Random();

            //timerRead = new System.Timers.Timer(); // ms
            //timerRead.Interval = 500;
            //timerRead.Elapsed += Timer_Read_Elapsed;
            //timerRead.Start();

            //timerWrite = new System.Timers.Timer(); // ms
            //timerWrite.Interval = 200;
            //timerWrite.Elapsed += Timer_Write_Elapsed;
            //timerWrite.Start();

            //timerHeartBeat = new System.Timers.Timer(); // ms
            //timerHeartBeat.Interval = 500;
            //timerHeartBeat.Elapsed += Timer_HeartBeat_Elapsed;
            //timerHeartBeat.Start();

            InitUI();
        }

        private void InitUI()
        {
            try
            {
                //PIDTableExpander.Content = new PIDTableView();
                /*.Content = new PIDTableView();*/
            }
            catch (Exception exception)
            {
                ExpUtil.LogProc(log, exception, false);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //ApplyCustomizedConfig();
        }

        private void ApplyCustomizedConfig()
        {
            string ConfigPath = ".\\honghaier.customer.config";
            if (System.IO.File.Exists(ConfigPath))
            {
                if (ConfigurationManager.AppSettings.HasKeys())
                {
                    //open customer_config
                    Configuration ConfigCustomer = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = ConfigPath
                    }, ConfigurationUserLevel.None);
                    //set value of the key that exists in both app_config and customer_config
                    foreach (KeyValueConfigurationElement item in ConfigCustomer.AppSettings.Settings)
                    {
                        ConfigurationManager.AppSettings[item.Key] = item.Value;
                    }
                }
            }
        }

        /// <summary>
        /// 直接从Honghaier模块进行PLC测试
        /// </summary>
        private void InitPLCTest()
        {
            try
            {
                adsClient = new TcAdsClient();
                adsClient.Connect(ConfigurationManager.AppSettings["PLCNetID"],
                    Int32.Parse(ConfigurationManager.AppSettings["PLCPort"]));
            }
            catch (Exception exception)
            {
                ExpUtil.LogProc(log, exception, false);
            }
        }

        void Timer_Read_Elapsed(object sender, EventArgs e)
        {
            try
            {
                hPLCReadStruct = adsClient.CreateVariableHandle(".PMA_TC_Input");
                PLCReadStruct readStruct = (PLCReadStruct)adsClient.ReadAny(hPLCReadStruct, typeof(PLCReadStruct));

                UpdateListLog(readStruct);
            }
            catch (Exception exception)
            {
                ExpUtil.LogProc(log, exception, false);
            }
        }

        void UpdateListLog(object obj)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = obj.ToString().Split('\n')[0];
            listBoxItem.Tag = obj.ToString();
            listBoxItem.Selected += ListLog_Selected;
        }

        void Timer_Write_Elapsed(object sender, EventArgs e)
        {
            try
            {
                hPLCWriteStruct = adsClient.CreateVariableHandle(".PMA_TC_Output");
                PLCWriteStruct plcWriteStruct = GetPLCWriteStructFromControls();
                adsClient.WriteAny(hPLCWriteStruct, plcWriteStruct);

                UpdateListLog(plcWriteStruct);
            }
            catch (Exception exception)
            {
                ExpUtil.LogProc(log, exception, false);
            }
        }

        void Timer_HeartBeat_Elapsed(object sender, EventArgs e)
        {
            try
            {
                hPLCHeartBeatStruct = adsClient.CreateVariableHandle(".PMA_TC_Heartbeat");
                PLCHeartBeatStruct readStruct = (PLCHeartBeatStruct)adsClient.ReadAny(
                    hPLCHeartBeatStruct, typeof(PLCHeartBeatStruct));

                if (curSNRead != (ushort)((readStruct.curSNRead - 1) % 3000)) {
                    if (unSyncTimes > maxUnSyncTimes) {
                        throw new Exception("读取plc流水号异常");
                    } else {
                        unSyncTimes++;
                    }
                } else {
                    curSNRead++;
                    unSyncTimes = 0;
                }

                adsClient.WriteAny(hPLCHeartBeatStruct, GetPLCHeartBeatStruct(readStruct));

                UpdateListLog(readStruct);
            }
            catch (Exception exception)
            {
                ExpUtil.LogProc(log, exception, false);
            }
        }

        private PLCHeartBeatStruct GetPLCHeartBeatStruct(PLCHeartBeatStruct readStruct)
        {
            PLCHeartBeatStruct structure = new PLCHeartBeatStruct();
            
            structure.curSNRead = readStruct.curSNRead;
            if (curSNRead == -1) {
                curSNRead = (ushort)((readStruct.curSNRead - 1) % 3000);
            }

            structure.curSNWrite = (ushort)((curSNWrite++) % 32767);
            structure.timeOutSpan = (UInt16)TimeOut;

            return structure;
        }

        private PLCWriteStruct GetPLCWriteStructFromControls()
        {
            PLCWriteStruct structure = new PLCWriteStruct();

            for (int i = 0; i < Const.ARRAY_LENGTH; i++)
            {
                structure.curAmps[i] = (float)random.NextDouble();
                structure.curVoltage[i] = (float)random.NextDouble();
            }

            structure.optCmd = (UInt16)random.Next();

            return structure;
        }

        private void ListLog_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            // LogItemText.Text = listBoxItem.Tag.ToString();
        }

        Server server;
        public void InitGRPCServer()
        {
            string HonghaierServerIP = ConfigurationManager.AppSettings["HonghaierServerIP"];
            int HonghaierServerPort = Int32.Parse(ConfigurationManager.AppSettings["HonghaierServerPort"]);
            //new Thread(() =>
            //{
            try
            {
                server = new Server
                {
                    Services = { gRPCHonghaier.BindService(Singleton<gRPCImpl>.Instance) },
                    Ports = { new ServerPort(HonghaierServerIP, HonghaierServerPort, ServerCredentials.Insecure) }
                };
                server.Start();

                LogHelper.Info("gRPC server listening on port " + HonghaierServerPort, log);
                // Console.WriteLine("任意键退出..");
                // Console.ReadKey();

                //while (true) ;
            }
            catch (Exception exception)
            {
                //ExpUtil.LogProc(log, exception, false);
                log4net.LogManager.Shutdown();
                MessageBox.Show($"Grpc server start failed:\t\n{exception}");
                init = false;
                Environment.Exit(0);
                return;
            }
            init = true;
        }

        private PLCWriteStruct GetResetPLCWriteStructFromControls()
        {
            PLCWriteStruct structure = new PLCWriteStruct();

            for (int i = 0; i < Const.ARRAY_LENGTH; i++)
            {
                structure.curAmps[i] = 0;
            }

            for (int i = 0; i < Const.ARRAY_LENGTH; i++)
            {
                structure.curVoltage[i] = 0;
            }

            structure.optCmd = (UInt16)random.Next();

            return structure;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                if (adsClient == null) return;

                hPLCWriteStruct = adsClient.CreateVariableHandle(".PLCWriteStruct1");
                PLCWriteStruct plcWriteStruct = GetResetPLCWriteStructFromControls();
                adsClient.WriteAny(hPLCWriteStruct, plcWriteStruct);
            }
            catch (Exception exception)
            {
                ExpUtil.LogProc(log, exception, false);
            }
            finally
            {
                log4net.LogManager.Shutdown();
                server?.ShutdownAsync().Wait();
            }
        }

        private void PauseResume_Click(object sender, RoutedEventArgs e)
        {
            if(PauseResume.Content.ToString() == "Pause")
            {
                PauseResume.Content = "Resume";
                Reload.IsEnabled = true;
            }
            else if (PauseResume.Content.ToString() == "Resume")
            {
                PauseResume.Content = "Pause";
                Reload.IsEnabled = false;
            }
            //Singleton<gRPCImpl>.Instance.PauseResume(!Reload.IsEnabled);
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (init) Singleton<gRPCImpl>.Instance.MBTCInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
