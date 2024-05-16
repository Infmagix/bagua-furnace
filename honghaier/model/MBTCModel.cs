using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace honghaier.Model
{
    public enum SYS_CTRL_MODE_ENUM
    {
        MODE_MPC_SPEED_PRIOR = 0,
        MODE_MPC_OVERSHOOT_PRIOR,
        MODE_PID_CLASSIC,
        MODE_CONST_CURRENT,
        MODE_DISABLE = 100,
    }

    public enum SYS_CTRL_VARIABLE_ENUM 
    {
        VARIABLE_TC_CTRL = 0,       //TC表面温度控制,热电偶。
        VARIABLE_PYRO_WAFER_TOP_CTRL,   //PYRO-wafer表面温度控制
        VARIABLE_PYRO_WAFER_BOTTOM_CTRL,    //PYRO-wafer底部温度控制
        VARIABLE_PYRO_SUSC_TOP_CTRL,		//PYRO-susc表面温度控制
    }

    public enum SYS_STATE_ENUM 
    {
        /// SYS_STATE_NONE -> 0
        SYS_STATE_NONE = 0,
        SYS_STATE_INITIALIZED = 1,
        SYS_STATE_HEATING = 2,
        SYS_STATE_IDLE = 3,
        SYS_STATE_ERROR = 4,
        SYS_STATE_TERMINATED = 5,
    }

    public enum TEMP_ZONE_ENUM 
    {
        /// TEMP_ZONE_MIN -> 0
        TEMP_ZONE_MIN = 0,
        TEMP_ZONE_A,
        TEMP_ZONE_B,
        TEMP_ZONE_C,
        TEMP_ZONE_MAX,
    }

    public enum HEATER_ZONE_ENUM 
    {
        /// HEATER_ZONE_MIN -> 0
        HEATER_ZONE_MIN = 0,
        HEATER_ZONE_1,
        HEATER_ZONE_2,
        HEATER_ZONE_3,
        HEATER_ZONE_4,
        HEATER_ZONE_MAX,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ZONE_TEMP_STRUCT
    {
        public double zone_a;
        public double zone_b;
        public double zone_c;
        public double zone_d;
    }

    //区域功率的结构体
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ZONE_POWER_STRUCT
    {
        public double zone_1;      //zone1电压
        public double zone_2;      //zone2电压
        public double zone_3;      //zone3电压
        public double zone_4;      //zone4电压
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SUSCEPTOR_WAFER_TEMP_STRUCT
    {
        public double susceptor_top_a;
        public double susceptor_top_b;
        public double susceptor_top_c;
        public double susceptor_top_d;
        public double wafer_top_a;
        public double wafer_top_b;
        public double wafer_top_c;
        public double wafer_top_d;
        public double wafer_bottom_a;
        public double wafer_bottom_b;
        public double wafer_bottom_c;
        public double wafer_bottom_d;
    }

    public enum PYRO_TEMP_TYPE_ENUM 
    {
        PYRO_WAFER_TOP,
        PYRO_WAFER_BOTTOM,
        PYRO_SUSC_TOP,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ZONE_CTRLVAR_STRUCT
    {
        public double zone_1;
        public double zone_2;
        public double zone_3;
        public double zone_4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ZONE_CURRENT_STRUCT
    {
        public double zone_1;
        public double zone_2;
        public double zone_3;
        public double zone_4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ZONE_VOLTAGE_STRUCT
    {
        public double zone_1;
        public double zone_2;
        public double zone_3;
        public double zone_4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TC_TEMP_STRUCT
    {
        public double tc_1;
        public double tc_2;
        public double tc_3;
        public double tc_4;
    }

    // [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MBTC_INPUT_STRUCT
    {
        public int sys_enable;
        /*0:Ramp start from feedback  1:Ramp start from target */
        public int parameter;
        public double start_time_stamp;
        public double ramp_time;
        public ZONE_TEMP_STRUCT zone_temp_target;

        public SYS_CTRL_MODE_ENUM ctrl_mode;
        public SYS_CTRL_VARIABLE_ENUM ctrl_variable;

        public TC_TEMP_STRUCT tc_temp;
        public ZONE_CURRENT_STRUCT zone_current_feedback;
        public ZONE_VOLTAGE_STRUCT zone_voltage_feedback;
        public SUSCEPTOR_WAFER_TEMP_STRUCT zone_temp_feedback;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MBTC_OUTPUT_STRUCT
    {
        /// int
        public int error;
        /// int[10]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] warning;

        public SYS_STATE_ENUM system_state;
        public ZONE_TEMP_STRUCT zone_temp_step;
        public ZONE_CTRLVAR_STRUCT zone_current_step;
        public ZONE_POWER_STRUCT zone_power_feedback;
        public ZONE_TEMP_STRUCT zone_temp_feedback;
    }

}
