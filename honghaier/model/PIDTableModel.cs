using System.Runtime.InteropServices;

namespace honghaier.Model
{
    //public class PIDTableModel
    //{
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PID_PARAM_STRUCT
        {
            public int    index;
            public double lowerLimit;
            public double upperLimit;
            public double kp;
            public double ki;
            public double kd;
            public double maxOut;
        }
    //}
}
