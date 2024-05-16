using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honghaier.Utility
{
    public class Const
    {
        public const int ARRAY_LENGTH = 8;
        //public static float PID_P = (float)Double.Parse(ConfigurationManager.AppSettings["PID-P"]);
        //public static float PID_I = (float)Double.Parse(ConfigurationManager.AppSettings["PID-I"]);
        //public static float PID_D = (float)Double.Parse(ConfigurationManager.AppSettings["PID-D"]);
        //public static float Max_Amp = (float)Double.Parse(ConfigurationManager.AppSettings["MaxAmp"]);
        public static int SciChartLength = Int32.Parse(ConfigurationManager.AppSettings["SciChartLength"]);

        public static bool TimeScaleDBEnable = bool.Parse(ConfigurationManager.AppSettings["TimeScaleDBEnable"]);

        public static string TimeScaleDBName = ConfigurationManager.AppSettings["TimeScaleDBName"];
        public static int TimeScaleDBPort = int.Parse(ConfigurationManager.AppSettings["TimeScaleDBPort"]);
        public static string TimeScaleServerIP = ConfigurationManager.AppSettings["TimeScaleServerIP"];
        public static string TimeScaleServerUser = ConfigurationManager.AppSettings["TimeScaleServerUser"];
        public static string TimeScaleDBPassword = ConfigurationManager.AppSettings["TimeScaleDBPassword"];
        public static string TimeScaleTableName
        {
            get
            {
                var table = ConfigurationManager.AppSettings["TimeScaleTableName"];
                if (string.IsNullOrEmpty(table))
                {
                    // Get Env variable for Kxware runtime name
                    var projName = Environment.GetEnvironmentVariable("ToolName");
                    if (string.IsNullOrEmpty(projName))
                    {
                        // Get Env variable for Archon runtime name
                        projName = Environment.GetEnvironmentVariable("RuntimeName");
                        if (!string.IsNullOrEmpty(projName) && !projName.Contains(" "))
                        {
                            table = $"{projName}_Merlin_HHR";
                        }
                    }
                    else if (!projName.Contains(" "))
                    {
                        table = $"{projName}_Merlin_HHR";
                    }
                    if (string.IsNullOrEmpty(table))
                    {
                        table = "Unknown_Merlin_HHR";
                    }
                }
                return table;
            }
        }


        // InfluxDB
        public static bool InfluxDBEnable = bool.Parse(ConfigurationManager.AppSettings["InfluxDBEnable"]);
        public static string InfluxDBToken = ConfigurationManager.AppSettings["InfluxDBToken"];
        public static string InfluxDBBucket = ConfigurationManager.AppSettings["InfluxDBBucket"];
        public static string InfluxDBOrg = ConfigurationManager.AppSettings["InfluxDBOrg"];
        public static string InfluxServerIP = ConfigurationManager.AppSettings["InfluxServerIP"];
        public static int InfluxServerPort = Int32.Parse(ConfigurationManager.AppSettings["InfluxServerPort"]);
    }
}
