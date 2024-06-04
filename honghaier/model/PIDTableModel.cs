using Cytek.Generics.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

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

    [Serializable]
    [XmlRoot("PIDTableModel")]
    public class PIDTableModel
    {

        private string heaterZoneNum;
        [XmlElement("HeaterZoneNum")]
        public string HeaterZoneNum
        {
            get { return heaterZoneNum; }
            set { heaterZoneNum = value; }
        }

        private List<PID> zone0 = new List<PID>();
        [XmlArray("Zone0")]
        [XmlArrayItem("PID")]
        public List<PID> Zone0
        {
            get { return zone0; }
        }

        private List<PID> zone1 = new List<PID>();
        [XmlArray("Zone1")]
        [XmlArrayItem("PID")]
        public List<PID> Zone1
        {
            get { return zone1; }
        }

        private List<PID> zone2 = new List<PID>();
        [XmlArray("Zone2")]
        [XmlArrayItem("PID")]
        public List<PID> Zone2
        {
            get { return zone2; }
        }

        private List<PID> zone3 = new List<PID>();
        [XmlArray("Zone3")]
        [XmlArrayItem("PID")]
        public List<PID> Zone3
        {
            get { return zone3; }
        }

        private List<PID> zone4 = new List<PID>();
        [XmlArray("Zone4")]
        [XmlArrayItem("PID")]
        public List<PID> Zone4
        {
            get { return zone4; }
        }

        private List<PID> zone5 = new List<PID>();
        [XmlArray("Zone5")]
        [XmlArrayItem("PID")]
        public List<PID> Zone5
        {
            get { return zone5; }
        }

        private List<PID> zone6 = new List<PID>();
        [XmlArray("Zone6")]
        [XmlArrayItem("PID")]
        public List<PID> Zone6
        {
            get { return zone6; }
        }

        private List<PID> zone7 = new List<PID>();
        [XmlArray("Zone7")]
        [XmlArrayItem("PID")]
        public List<PID> Zone7
        {
            get { return zone7; }
        }

        public void addPID(List<PID> zone, PID e)
        {
            zone.Add(e);
        }
    }



    [XmlRoot("SimplePIDConfig")]
    public class SimplePIDConfig
    {
        [XmlElement("HeaterZoneNum")]
        public int HeaterZoneNum { get; set; }

        [XmlIgnore]
        public Dictionary<string, List<PID>> Zones { get; set; } = new Dictionary<string, List<PID>>();

        [XmlArray("Zone0")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone0
        {
            get { return Zones.ContainsKey("Zone0") ? Zones["Zone0"] : null; }
            set { if (value != null) Zones["Zone0"] = value; }
        }

        [XmlArray("Zone1")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone1
        {
            get { return Zones.ContainsKey("Zone1") ? Zones["Zone1"] : null; }
            set { if (value != null) Zones["Zone1"] = value; }
        }

        [XmlArray("Zone2")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone2
        {
            get { return Zones.ContainsKey("Zone2") ? Zones["Zone2"] : null; }
            set { if (value != null) Zones["Zone2"] = value; }
        }

        [XmlArray("Zone3")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone3
        {
            get { return Zones.ContainsKey("Zone3") ? Zones["Zone3"] : null; }
            set { if (value != null) Zones["Zone3"] = value; }
        }

        [XmlArray("Zone4")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone4
        {
            get { return Zones.ContainsKey("Zone4") ? Zones["Zone4"] : null; }
            set { if (value != null) Zones["Zone4"] = value; }
        }

        [XmlArray("Zone5")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone5
        {
            get { return Zones.ContainsKey("Zone5") ? Zones["Zone5"] : null; }
            set { if (value != null) Zones["Zone5"] = value; }
        }

        [XmlArray("Zone6")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone6
        {
            get { return Zones.ContainsKey("Zone6") ? Zones["Zone6"] : null; }
            set { if (value != null) Zones["Zone6"] = value; }
        }

        [XmlArray("Zone7")]
        [XmlArrayItem("PID", typeof(PID))]
        public List<PID> Zone7
        {
            get { return Zones.ContainsKey("Zone7") ? Zones["Zone7"] : null; }
            set { if (value != null) Zones["Zone7"] = value; }
        }
    }

    [XmlRoot("PID")]
    public class PID
    {
        [XmlAttribute("Index")]
        public int Index { get; set; }

        [XmlAttribute("LowerLimit")]
        public double LowerLimit { get; set; }

        [XmlAttribute("UpperLimit")]
        public double UpperLimit { get; set; }

        [XmlAttribute("Kp")]
        public double Kp { get; set; }

        [XmlAttribute("Ki")]
        public double Ki { get; set; }

        [XmlAttribute("Kd")]
        public double Kd { get; set; }

        [XmlAttribute("MAXOUT")]
        public double MAXOUT { get; set; }
    }
}
