using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace honghaier.Utility
{    
    /// <summary>
    /// PLC读结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PLCReadStruct
    {
        /// <summary>
        /// 电流
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curAmps = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// 电压
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curVoltage = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// MPC temp setpoint
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curTempStep = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// 热电偶温度
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curTCTemper = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// Pyro温度
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curWaferOffTemper = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// Pyro温度
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curWaferOnTemper = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// 当前托盘转速
        /// </summary>
        [MarshalAs(UnmanagedType.R4)]
        public float curTrayRotateSpeed;

        /// <summary>
        /// 当前状态{0:正常、1:出错、2:待机、3：主控机台正在控制、4：欧陆表正在控制}
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 curState;

        /// <summary>
        /// 错误码{0:正常}
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 errorCode;

        public override string ToString()
        {
            string amps = string.Format("电流:{0},{1},{2},{3}\n", curAmps[0], curAmps[1], curAmps[2], curAmps[3]);
            string voltages = string.Format("电压:{0},{1},{2},{3}\n", curVoltage[0], curVoltage[1], curVoltage[2], curVoltage[3]);
            string tcTempers = string.Format("温度:{0},{1},{2},{3}\n", curTCTemper[0], curTCTemper[1], curTCTemper[2], curTCTemper[3]);
            string leftPart = string.Format("转速:{0},状态:{1},错误码:{2}\n", curTrayRotateSpeed, curState, errorCode);
            amps += voltages + tcTempers + leftPart;
            return "读取PLC：\n" + amps;
        }
    }

    /// <summary>
    /// PLC心跳结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PLCHeartBeatStruct
    {
        /// <summary>
        /// 从 PLC->上位机 的流水号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 curSNRead;

        /// <summary>
        /// 从 上位机->PLC 的流水号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 curSNWrite;

        /// <summary>
        /// 超时间隔 3~5秒
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 timeOutSpan;

        public override string ToString()
        {
            string leftPart = string.Format("从 PLC->上位机 的流水号:{0}\n从 上位机->PLC 的流水号:{1}\n超时间隔:{2}\n", 
                curSNRead, curSNWrite, timeOutSpan);
            return "心跳PLC：\n" + leftPart;
        }
    }

    /// <summary>
    /// PLC写结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PLCWriteStruct
    {
        /// <summary>
        /// 目标电流
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curAmps = new float[Const.ARRAY_LENGTH];
        
        /// <summary>
        /// 目标电压
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Const.ARRAY_LENGTH)]
        public float[] curVoltage = new float[Const.ARRAY_LENGTH];

        /// <summary>
        /// 操作命令字{0：无  1：重新连接}|
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 optCmd;

        public override string ToString()
        {
            string amps = string.Format("电流:{0},{1},{2},{3}\n", curAmps[0], curAmps[1], curAmps[2], curAmps[3]);
            string voltages = string.Format("电压:{0},{1},{2},{3}\n", curVoltage[0], curVoltage[1], curVoltage[2], curVoltage[3]);
            amps += voltages + "操作命令字:" + optCmd;
            return "写入PLC：\n" + amps;
        }
    }
}
