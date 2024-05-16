using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honghaierTest.utility
{
    public class Const
    {
        public const int ARRAY_LENGTH = 8;
        public static int MaxTemp = Int32.Parse(ConfigurationManager.AppSettings["MaxTemp"]);
        public static int MaxTimeSpan = Int32.Parse(ConfigurationManager.AppSettings["MaxTimeSpan"]);
    }
}
