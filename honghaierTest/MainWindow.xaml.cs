using Grpc.Core;
using honghaierTest.utility;
using Merlincomm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace honghaierTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timerRead;
        private DispatcherTimer timerWrite;
        private DispatcherTimer timerHeartBeat;

        private Random random;

        private Channel channelH;
        private Channel channelW;
        private gRPCHonghaier.gRPCHonghaierClient clientH;
        private gRPCWukong.gRPCWukongClient clientW;

        private bool isTestWukong = true;

        public MainWindow()
        {
            InitializeComponent();

            InitGRPCComm();

            timerRead = new DispatcherTimer(); // ms
            timerRead.Interval = TimeSpan.FromMilliseconds(500);
            timerRead.Tick += Timer_Read_Elapsed;
            timerRead.Start();

            //timerWrite = new DispatcherTimer(); // ms
            //timerWrite.Interval = TimeSpan.FromMilliseconds(1000);
            //timerWrite.Tick += Timer_Write_Elapsed;
            //// timerWrite.Start();

            //timerHeartBeat = new DispatcherTimer(); // ms
            //timerHeartBeat.Tick += Timer_HeartBeat_Elapsed;
            //timerHeartBeat.Interval = TimeSpan.FromMilliseconds(700);
            //timerHeartBeat.Start();

            random = new Random();
        }

        private void InitGRPCComm() 
        {
            if (isTestWukong == false) 
            {
                string HonghaierServerIP = ConfigurationManager.AppSettings["HonghaierServerIP"];
                int HonghaierServerPort = Int32.Parse(ConfigurationManager.AppSettings["HonghaierServerPort"]);
                channelH = new Channel(HonghaierServerIP + ":" +
                    HonghaierServerPort, ChannelCredentials.Insecure);

                clientH = new gRPCHonghaier.gRPCHonghaierClient(channelH);
            } 
            else {
                string WukongServerIP = ConfigurationManager.AppSettings["WukongServerIP"];
                int WukongServerPort = Int32.Parse(ConfigurationManager.AppSettings["WukongServerPort"]);
                channelW = new Channel(WukongServerIP + ":" +
                    WukongServerPort, ChannelCredentials.Insecure);

                clientW = new gRPCWukong.gRPCWukongClient(channelW);
            }
        }

        private void ListLog_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            LogItemText.Text = listBoxItem.Tag.ToString();
        }

        private void TestGRPC_Click(object sender, RoutedEventArgs e)
        {
            if (isTestWukong == false) {
                var replyH = clientH.Read(new ReadRequest { SrcName = "clientH" });
                Console.WriteLine("来自" + replyH.SrcName);

                channelH.ShutdownAsync().Wait();
                Console.WriteLine("任意键退出...");
            } else {
                var replyW = clientW.ReadRealTimeStates(new ReadRealTimeStatesRequest { SrcName = "clientH" });
                Console.WriteLine("来自" + replyW.SrcName);

                channelH.ShutdownAsync().Wait();
                Console.WriteLine("任意键退出...");
            }
        }

        void Timer_Read_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (isTestWukong == false) {
                    var reply = clientH.Read(FillReadH());
                    ReadText.Text = reply.ToString();
                    UpdateListLog(reply);
                } else {
                    var reply = clientW.ReadRealTimeStates(FillReadW());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private ReadRequest FillReadH()
        {
            ReadRequest readRequest = new ReadRequest();
            readRequest.SrcName = "readClient";
            readRequest.ReqSN = (Int64)random.Next();
            return readRequest;
        }

        private ReadRealTimeStatesRequest FillReadW() {
            ReadRealTimeStatesRequest readRequest = new ReadRealTimeStatesRequest();
            readRequest.SrcName = "readClient";
            readRequest.ReqSN = (Int64)random.Next();
            return readRequest;
        }

        void UpdateListLog(object obj)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = obj.ToString().Split('\n')[0];
            listBoxItem.Tag = obj.ToString();
            listBoxItem.Selected += ListLog_Selected;

            if (ListLog.Items.Count > 10000)
            {
                ListLog.Items.Clear();
            }

            ListLog.Items.Add(listBoxItem);            
        }

        void Timer_Write_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (isTestWukong == false) {
                    var reply = clientH.Write(FillWriteH());
                    UpdateListLog(reply);
                } else {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private WriteRequest FillWriteH()
        {
            WriteRequest writeRequest = new WriteRequest();

            float timeSpan = (float)(Const.MaxTimeSpan * random.NextDouble());
            writeRequest.TimeSpan = timeSpan;

            float[] TempTarget = new float[Const.ARRAY_LENGTH];
            for (int i = 0; i < Const.ARRAY_LENGTH; i++)
            {
                TempTarget[i] = (float)(Const.MaxTemp * random.NextDouble());
            }
            writeRequest.TempTarget.AddRange(TempTarget);
            writeRequest.CtrlMode = 0;

            writeRequest.SrcName = "writeClient";
            writeRequest.ReqSN = (Int64)random.Next();

            return writeRequest;
        }

        void Timer_HeartBeat_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (isTestWukong == false) {
                    var reply = clientH.HeartBeat(FillHeartBeatH());
                    UpdateListLog(reply);
                } else {
                    var reply = clientW.HeartBeat(FillHeartBeatW());
                    UpdateListLog(reply);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private HeartBeatRequestHHR FillHeartBeatH()
        {
            HeartBeatRequestHHR heartBeat = new HeartBeatRequestHHR();
            heartBeat.ReqSN = (Int64)random.Next();
            heartBeat.SrcName = "heartbeatClient";
            heartBeat.TimeOutSpan = (UInt16)random.Next();
            return heartBeat;
        }

        private HeartBeatRequestWk FillHeartBeatW() {
            HeartBeatRequestWk heartBeat = new HeartBeatRequestWk();
            heartBeat.ReqSN = (Int64)random.Next();
            heartBeat.SrcName = "heartbeatClient";
            heartBeat.TimeOutSpan = (UInt16)random.Next();
            return heartBeat;
        }

        private void Amps0_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
