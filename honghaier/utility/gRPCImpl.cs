using Grpc.Core;
using honghaier.Model;
using Merlincomm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TwinCAT.Ads;

namespace honghaier.Utility
{
    internal class gRPCImpl : gRPCHonghaier.gRPCHonghaierBase 
    {
        private log4net.ILog log = null;

        //System.Timers.Timer timerWrite;
        //WriteRequest request;
        PLCWriteStruct _writeStruct = new PLCWriteStruct();
        PLCReadStruct _readStruct = new PLCReadStruct();
        IDbProvider _dbProvider;

        //private TcAdsClient adsClient = new TcAdsClient();
        public gRPCImpl()
        {
            //adsClient.Connect(ConfigurationManager.AppSettings["PLCNetID"],
            //  Int32.Parse(ConfigurationManager.AppSettings["PLCPort"]));

            MBTCInitialize();
            InitGRPCComm();
            log = log4net.LogManager.GetLogger("gRPCImpl");

            // Initailize db provider
            if (Const.TimeScaleDBEnable)
            {
                _dbProvider = Singleton<TimeScaleDBHelper>.Instance;
            }
            else if (Const.InfluxDBEnable)
            {
                //_dbProvider = Singleton<InfluxDBHelper>.Instance;
            }
        }

        [DllImport(@"MBTC.dll", EntryPoint = "MBTC_load_config", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MBTC_load_config(string configFile);

        public void MBTCInitialize()
        {
            //PIDClear();
            try
            {
                string configFileFullName = @".\Config\SimplePIDConfig.xml";
                MBTC_load_config(configFileFullName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //[DllImport(@"MBTC.dll", EntryPoint = "MBTC_reload_config", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int MBTC_reload_config(string configFile);

        //public void MBTCReload()
        //{
        //    //PIDClear();
        //    try
        //    {
        //        string configFileFullName = @".\Config\SimplePIDConfig.xml";
        //        MBTC_load_config(configFileFullName);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        public override Task<ReadReply> Read(ReadRequest request, ServerCallContext context)
        {
            try
            {
                //int hPLCReadStruct = adsClient.CreateVariableHandle("MAIN.PLCReadStruct1");
                //PLCReadStruct readStruct = (PLCReadStruct)adsClient.ReadAny(hPLCReadStruct, typeof(PLCReadStruct));
                //PLCReadStruct readStruct = new PLCReadStruct();
                ReadReply readReply = FillReadReplyByPLCReadStruct(_readStruct);
                return Task.FromResult(readReply);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private ReadReply FillReadReplyByPLCReadStruct(PLCReadStruct readStruct)
        {
            ReadReply readReply = new ReadReply();
            //readStruct.curTCTemper[0] += 0.123456f;

            readReply.Amps.AddRange(readStruct.curAmps);
            readReply.Voltage.AddRange(readStruct.curVoltage);
            readReply.TemperStep.AddRange(readStruct.curTempStep);
            readReply.TemperOnWafer.AddRange(readStruct.curWaferOnTemper);
            readReply.TemperOffWafer.AddRange(readStruct.curWaferOffTemper);
            readReply.TemperTc.AddRange(readStruct.curTCTemper);

            return readReply;
        }

        public override Task<WriteReply> Write(WriteRequest request, ServerCallContext context)
        {
            //this.request = request;
            WriteReply writeReply = new WriteReply { SrcName = "WriteRequest" };
            if (request == null) return Task.FromResult(writeReply);

            try
            {
                MBTCUpdate(request);
                if (_writeStruct != null)
                {
                    writeReply.Current.AddRange(_writeStruct.curAmps);
                }
            }
            catch (Exception ex)
            {
                ExpUtil.LogProc(log, ex, false);
                MessageBox.Show(ex.Message);
            }
            return Task.FromResult(writeReply);
        }

        private Random random;
        private Channel channel;
        //private gRPCWukong.gRPCWukongClient client;
        private void InitGRPCComm()
        {
            random = new Random();

            string WukongServerIP = ConfigurationManager.AppSettings["WukongServerIP"];
            int WukongServerPort = Int32.Parse(ConfigurationManager.AppSettings["WukongServerPort"]);
            channel = new Channel(WukongServerIP + ":" +
                WukongServerPort, ChannelCredentials.Insecure);

            //client = new gRPCWukong.gRPCWukongClient(channel);

            //timerWrite = new System.Timers.Timer(); // ms
            //timerWrite.Interval = interval;
            //timerWrite.Elapsed += Timer_Write_Elapsed;
            //timerWrite.Start();
        }

        //public void PauseResume(bool setState)
        //{
        //    if(setState == true)
        //    {
        //        timerWrite.Start();
        //    }
        //    else
        //    {
        //        timerWrite.Stop();
        //    }
        //}

        //void Timer_Write_Elapsed(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if(request == null) return;

        //        _plcWriteStruct = new PLCWriteStruct();
        //        MBTCUpdate(request, ref _plcWriteStruct);

        //        //int hPLCWriteStruct = adsClient.CreateVariableHandle("MAIN.PLCWriteStruct1");
        //        //adsClient.WriteAny(hPLCWriteStruct, _plcWriteStruct);
        //    }
        //    catch (Exception exception)
        //    {
        //        ExpUtil.LogProc(log, exception, false);
        //    }
        //}

        /// <summary>
        /// 更新pid写入结构
        /// </summary>
        /// <param name="writeRequest"></param>
        /// <param name="plcWriteStruct"></param>
        /// 
        //public void PIDInitialize(float curTemp=450, float targetTemp=451, float timeDuration=60)//PID使用前必须初始化,时间跨度单位是秒
        //{
        //    if(simpledPidController==null)
        //    {
        //        simpledPidController = new SimpledPID(Const.PID_P, Const.PID_I, Const.PID_I, Const.Max_Amp);//P、I、D、和最大电流、
        //    }
        //    simpledPidController.Clear();
        //    simpledPidController.HeatingPathPlan(curTemp, targetTemp, timeDuration);
        //}

        //public void PIDClear()//加热结束请调用Clear;
        //{
        //    if (simpledPidController != null)
        //    {
        //        simpledPidController.Clear();
        //    }
        //}

        public void MBTCUpdate(WriteRequest writeRequest)
        {
            // Update read struct for grpc feedback query
            var readStruct = new PLCReadStruct();
            readStruct.curAmps[0] = writeRequest.Current[0];
            readStruct.curAmps[1] = writeRequest.Current[1];
            readStruct.curAmps[2] = writeRequest.Current[2];
            readStruct.curAmps[3] = writeRequest.Current[3];
            readStruct.curVoltage[0] = writeRequest.Voltage[0];
            readStruct.curVoltage[1] = writeRequest.Voltage[1];
            readStruct.curVoltage[2] = writeRequest.Voltage[2];
            readStruct.curVoltage[3] = writeRequest.Voltage[3];
            readStruct.curWaferOffTemper[0] = writeRequest.TemperOffWafer[0];
            readStruct.curWaferOffTemper[1] = writeRequest.TemperOffWafer[1];
            readStruct.curWaferOffTemper[2] = writeRequest.TemperOffWafer[2];
            readStruct.curWaferOffTemper[3] = writeRequest.TemperOffWafer[3];
            readStruct.curWaferOnTemper[0] = writeRequest.TemperOnWafer[0];
            readStruct.curWaferOnTemper[1] = writeRequest.TemperOnWafer[1];
            readStruct.curWaferOnTemper[2] = writeRequest.TemperOnWafer[2];
            readStruct.curWaferOnTemper[3] = writeRequest.TemperOnWafer[3];
            readStruct.curTCTemper[0] = writeRequest.TemperTc[0];
            readStruct.curTCTemper[1] = writeRequest.TemperTc[1];
            readStruct.curTCTemper[2] = writeRequest.TemperTc[2];
            readStruct.curTCTemper[3] = writeRequest.TemperTc[3];
            _readStruct = readStruct;
            PyroTempDiffLog();
            

            // MBTC algorithm invoke
            // simpledPidController.Update(writeRequest.TempCurrent[0], ref plcWriteStruct.curAmps[0]);
            var outDat = CalcMBTC(writeRequest);
            _readStruct.curTempStep[0] = (float)outDat.zone_temp_step.zone_a;
            _readStruct.curTempStep[1] = (float)outDat.zone_temp_step.zone_b;
            _readStruct.curTempStep[2] = (float)outDat.zone_temp_step.zone_c;
            _readStruct.curTempStep[3] = (float)outDat.zone_temp_step.zone_d;

            var writeStruct = new PLCWriteStruct();
            writeStruct.curAmps[0] = (float)outDat.zone_current_step.zone_1;
            writeStruct.curAmps[1] = (float)outDat.zone_current_step.zone_2;
            writeStruct.curAmps[2] = (float)outDat.zone_current_step.zone_3;
            writeStruct.curAmps[3] = (float)outDat.zone_current_step.zone_4;
            _writeStruct = writeStruct;

            // 存库
            if (Const.TimeScaleDBEnable || Const.InfluxDBEnable)
            {
                var dict = TransOutputParas(outDat);
                _dbProvider?.WriteDict(dict);
            }
        }

        private void PyroTempDiffLog()
        {
            var max = _readStruct.curWaferOnTemper.Max();
            if (max > 450)
            {
                var min = _readStruct.curWaferOnTemper.Where(x => x != 0 && x != 450).Min();
                if (Math.Abs(max - min) > 80)
                    LogHelper.Debug($"HHR Pyro On Wafer Temp Diff > 80: {min}, {max}", log);
            }

            max = _readStruct.curWaferOffTemper.Max();
            if (max > 450)
            {
                var min = _readStruct.curWaferOffTemper.Where(x => x != 0 && x != 450).Min();
                if (Math.Abs(max - min) > 80)
                    LogHelper.Debug($"HHR Pyro Off Wafer Temp Diff > 80: {min}, {max}", log);
            }
        }

        [DllImport(@"MBTC.dll", EntryPoint = "MBTC_simple_run", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MBTC_simple_run(IntPtr inData, IntPtr outData);

        //[DllImport(@"MBTC.dll", EntryPoint = "MPC_run", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int MPC_run(IntPtr inData, IntPtr outData);

        MBTC_OUTPUT_STRUCT CalcMBTC(WriteRequest writeRequest)
        {
            MBTC_INPUT_STRUCT inDat = new MBTC_INPUT_STRUCT();
            inDat.ctrl_mode = (SYS_CTRL_MODE_ENUM)writeRequest.CtrlMode; // SYS_CTRL_MODE_ENUM.MODE_MPC_SPEED_PRIOR;
            inDat.ctrl_variable = (SYS_CTRL_VARIABLE_ENUM)writeRequest.CtrlVar; //SYS_CTRL_VARIABLE_ENUM.VARIABLE_PYRO_SUSC_TOP_CTRL;
            inDat.sys_enable = 1;
            /* 0:Ramp start from feedback  1:Ramp start from target */
            inDat.parameter = writeRequest.Parameter;

            inDat.start_time_stamp = writeRequest.StartTime;
            inDat.ramp_time = writeRequest.TimeSpan == 0 ? 0.001 : writeRequest.TimeSpan;

            inDat.zone_temp_target.zone_a = writeRequest.TempTarget[0];
            inDat.zone_temp_target.zone_b = writeRequest.TempTarget[1];
            inDat.zone_temp_target.zone_c = writeRequest.TempTarget[2];
            inDat.zone_temp_target.zone_d = writeRequest.TempTarget[3];

            inDat.zone_voltage_feedback.zone_1 = writeRequest.Voltage[0];
            inDat.zone_voltage_feedback.zone_2 = writeRequest.Voltage[1];
            inDat.zone_voltage_feedback.zone_3 = writeRequest.Voltage[2];
            inDat.zone_voltage_feedback.zone_4 = writeRequest.Voltage[3];

            //inDat.tc_temp.tc_1 = 1.1;
            //inDat.tc_temp.tc_2 = 1.2;
            //int hPLCReadStruct = adsClient.CreateVariableHandle(".PMA_TC_Input");
            //PLCReadStruct readStruct = (PLCReadStruct)adsClient.ReadAny(hPLCReadStruct, typeof(PLCReadStruct));
            //inDat.tc_temp.tc_1 = readStruct.curTCTemper[0];
            //inDat.tc_temp.tc_2 = readStruct.curTCTemper[1];
            inDat.tc_temp.tc_1 = writeRequest.TemperTc[0];
            inDat.tc_temp.tc_2 = writeRequest.TemperTc[1];
            inDat.tc_temp.tc_3 = writeRequest.TemperTc[2];
            inDat.tc_temp.tc_4 = writeRequest.TemperTc[3];

            inDat.zone_current_feedback.zone_1 = writeRequest.Current[0];
            inDat.zone_current_feedback.zone_2 = writeRequest.Current[1];
            inDat.zone_current_feedback.zone_3 = writeRequest.Current[2];
            inDat.zone_current_feedback.zone_4 = writeRequest.Current[3];
            inDat.zone_temp_feedback.susceptor_top_a = writeRequest.TemperOffWafer[0];
            inDat.zone_temp_feedback.susceptor_top_b = writeRequest.TemperOffWafer[1];
            inDat.zone_temp_feedback.susceptor_top_c = writeRequest.TemperOffWafer[2];
            inDat.zone_temp_feedback.susceptor_top_d = writeRequest.TemperOffWafer[3];
            inDat.zone_temp_feedback.wafer_bottom_a = writeRequest.TemperOnWafer[0];
            inDat.zone_temp_feedback.wafer_bottom_b = writeRequest.TemperOnWafer[1];
            inDat.zone_temp_feedback.wafer_bottom_c = writeRequest.TemperOnWafer[2];
            inDat.zone_temp_feedback.wafer_bottom_d = writeRequest.TemperOnWafer[3];
            //inDat.zone_temp_feedback.wafer_top_a = writeRequest.TemperOnWafer[4];
            //inDat.zone_temp_feedback.wafer_top_b = writeRequest.TemperOnWafer[5];
            //inDat.zone_temp_feedback.wafer_top_c = writeRequest.TemperOnWafer[6];
            //inDat.zone_temp_feedback.wafer_top_d = writeRequest.TemperOnWafer[7];

            IntPtr _inputBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MBTC_INPUT_STRUCT)));
            IntPtr _outputBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MBTC_OUTPUT_STRUCT)));
            Marshal.StructureToPtr(inDat, _inputBuffer, false);
            MBTC_simple_run(_inputBuffer, _outputBuffer);
            //MPC_run(_inputBuffer, _outputBuffer);
            inDat = (MBTC_INPUT_STRUCT)Marshal.PtrToStructure(
                _inputBuffer, typeof(MBTC_INPUT_STRUCT));
            MBTC_OUTPUT_STRUCT outDat = (MBTC_OUTPUT_STRUCT)Marshal.PtrToStructure(
                _outputBuffer, typeof(MBTC_OUTPUT_STRUCT));
            return outDat;
        }

        //private SimpledPID simpledPidController;
        //private class SimpledPID
        //{
        //    public SimpledPID(float p = 0.01f, float i = 0.01f, float d = 0.01f, float curThreshold = 60)
        //    {
        //        startTime = DateTime.Now;
        //        lastRoundTime = DateTime.Now;
        //        Kp = p;
        //        Ki = i;
        //        Kd = d;
        //        maxCurrent = curThreshold;
        //    }

        //    private float Kp;
        //    private float Ki;
        //    private float Kd;

        //    private float maxCurrent//最大电流,单位是安培；
        //    {
        //        get;
        //        set;
        //    }
        //    private float heatingSlope; //升温的斜率

        //    private float startTemperature;//开始时候的温度
        //    public float nowSetpointTemp;//现在设定温度点
        //    private float targetTemperature;

        //    private float presentError;//当前误差值
        //    private float lastError; //上一轮的误差值


        //    private float proportionalTerm;//比例项
        //    private float integralTerm;//积分项
        //    private float derivativeTerm;//微分项

        //    private float maxTimeSpan;

        //    private DateTime startTime;
        //    private DateTime lastRoundTime;
        //    private DateTime nowTime;


        //    public void Update(float inputTemperature, ref float outputCurrent)
        //    {
        //        nowTime = DateTime.Now;
        //        float deltaTime = (float)(nowTime - lastRoundTime).TotalSeconds;

        //        float timeCost = (float)(nowTime - startTime).TotalSeconds;
        //        if (timeCost < maxTimeSpan)
        //        {
        //            nowSetpointTemp = (float)(nowTime - startTime).TotalSeconds * heatingSlope + startTemperature;
        //        }
        //        else
        //        {
        //            nowSetpointTemp = targetTemperature;
        //        }

        //        presentError = (nowSetpointTemp - inputTemperature);
        //        //Console.WriteLine("xianzai target:"+nowSetpointTemp.ToString()+" ****input temp: "+inputTemperature.ToString());
        //        float deltaError = presentError - lastError;

        //        proportionalTerm = Kp * presentError;
        //        /*积分项目要做阈值限制，不能长时间积分太大*/
        //        integralTerm = integralTerm + presentError * deltaTime;
        //        /**/
        //        if (deltaTime > 0)
        //        {
        //            derivativeTerm = deltaError / deltaTime;
        //        }


        //        float output = Kp * presentError + Ki * integralTerm + Kd * derivativeTerm;
        //        output = output < 0 ? 0 : output;
        //        outputCurrent = output > maxCurrent ? maxCurrent : output;

        //        lastError = presentError;
        //        lastRoundTime = nowTime;//更新刷新时间
        //    }

        //    public void Clear()//每次结束要清零
        //    {
        //        heatingSlope = 0; //升温的斜率

        //        startTemperature = 0;//开始时候的温度
        //        nowSetpointTemp = 0;//现在设定温度点

        //        presentError = 0;//当前误差值
        //        lastError = 0; //上一轮的误差值


        //        proportionalTerm = 0;//比例项
        //        integralTerm = 0;//积分项
        //        derivativeTerm = 0;//微分项
        //    }

        //    public void HeatingPathPlan(float initT, float tgtT, float timeSpan)//本次加热要规划
        //    {
        //        startTime = DateTime.Now;
        //        nowSetpointTemp = initT;
        //        timeSpan = timeSpan > 0 ? timeSpan : 1;
        //        heatingSlope = (tgtT - initT) / timeSpan;
        //        maxTimeSpan = timeSpan;
        //        startTemperature = initT;
        //        targetTemperature = tgtT;
        //    }
        //}

        public override Task<HeartBeatReplyHHR> HeartBeat(HeartBeatRequestHHR request, ServerCallContext context)
        {
            //int hPLCHeartBeatStruct = adsClient.CreateVariableHandle("MAIN.PLCHeartBeatStruct1");
            //PLCHeartBeatStruct plcHeartBeatStruct = FillPLCHeartBeatStructByHeartBeatReply(request);
            //adsClient.WriteAny(hPLCHeartBeatStruct, plcHeartBeatStruct);

            HeartBeatReplyHHR heartBeatReply = new HeartBeatReplyHHR { SrcName = "HeartBeatReplyFromHHR" };
            return Task.FromResult(heartBeatReply);
        }

        private PLCHeartBeatStruct FillPLCHeartBeatStructByHeartBeatReply(HeartBeatRequestHHR request)
        {
            PLCHeartBeatStruct plcHeartBeatStruct = new PLCHeartBeatStruct();
            return plcHeartBeatStruct;
        }


        private Dictionary<string, object> TransOutputParas(MBTC_OUTPUT_STRUCT mbtcOutputParameter)
        {
            return new Dictionary<string, object>() {

                { "hhr.zone_temp_step.zone_a", mbtcOutputParameter.zone_temp_step.zone_a },
                { "hhr.zone_temp_step.zone_b", mbtcOutputParameter.zone_temp_step.zone_b },
                { "hhr.zone_temp_step.zone_c", mbtcOutputParameter.zone_temp_step.zone_c },
                { "hhr.zone_temp_step.zone_d", mbtcOutputParameter.zone_temp_step.zone_d },

                { "hhr.zone_temp_feedback.zone_a", mbtcOutputParameter.zone_temp_feedback.zone_a },
                { "hhr.zone_temp_feedback.zone_b", mbtcOutputParameter.zone_temp_feedback.zone_b },
                {  "hhr.zone_temp_feedback.zone_c", mbtcOutputParameter.zone_temp_feedback.zone_c },
                {  "hhr.zone_temp_feedback.zone_d", mbtcOutputParameter.zone_temp_feedback.zone_d },

                {  "hhr.zone_current_step.zone_1", mbtcOutputParameter.zone_current_step.zone_1 },
                {  "hhr.zone_current_step.zone_2", mbtcOutputParameter.zone_current_step.zone_2 },
                {  "hhr.zone_current_step.zone_3", mbtcOutputParameter.zone_current_step.zone_3 },
                {  "hhr.zone_current_step.zone_4", mbtcOutputParameter.zone_current_step.zone_4 },

                {  "hhr.zone_power_feedback.zone_1", mbtcOutputParameter.zone_power_feedback.zone_1 },
                {  "hhr.zone_power_feedback.zone_2", mbtcOutputParameter.zone_power_feedback.zone_2 },
                {  "hhr.zone_power_feedback.zone_3", mbtcOutputParameter.zone_power_feedback.zone_3 },
                {  "hhr.zone_power_feedback.zone_4", mbtcOutputParameter.zone_power_feedback.zone_4 }};
        }
    }
}
