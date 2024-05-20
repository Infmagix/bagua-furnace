using honghaier.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace honghaier.View
{
    public partial class PIDTableView : UserControl
    {
        private int nAllZones = 8;
        private int nVisibleZones = 1;

        // test
        private int nZones = 4;
        private int nPIDs = 4; // 各区域的温度段数
        private Dictionary<string, List<PID_Parameter>> zoneID2PidParamList;

        private log4net.ILog log = log4net.LogManager.GetLogger("PIDTableView");


        public PIDTableView()
        {
            InitializeComponent();

            //for (int i = 0; i < NumDataSeries; i++)
            //{
            //    var dataSeries0 = new XyyDataSeries<DateTime, double>();
            //    dataSeries0.AcceptsUnsortedData = true;
            //    dataSeries0.FifoCapacity = FIFOSize;
            //    listDataSeries.Add(dataSeries0);
            //}

            // test
            GenPIDData();


            InitUI();
        }

        //private void InitUI(int ii)
        //{
        //    PIDTabControl.Items.Clear();

        //    TabItem zoneTabItem;
        //    StackPanel zoneStackPanel;

        //    for (int i = 0; i < nVisibleZones; i++)
        //    {
        //        zoneTabItem = new TabItem()
        //        {
        //            Header = $"Zone{i}",
        //            Name = $"PIDTable_Zone{i}TabItem",
        //        };

        //        zoneStackPanel = new StackPanel()
        //        { 
        //            Orientation = Orientation.Horizontal,
        //            Name = $"PIDTable_Zone{i}StackPanel",
        //        };
        //        // 添加控件到Zone{i}的StackPanel中
        //        for (int j = 0; j < pidParamList.Count; j++)
        //        {
        //            zoneStackPanel.Children.Add(CreateVerticalStack("Index ", $"{pidParamList[j].Index}"));
        //            zoneStackPanel.Children.Add(CreateVerticalStack("  Kp  ", $"{pidParamList[j].Kp}"));
        //            zoneStackPanel.Children.Add(CreateVerticalStack("  Ki  ", $"{pidParamList[j].Ki}"));
        //            zoneStackPanel.Children.Add(CreateVerticalStack("  Kd  ", $"{pidParamList[j].Kd}"));
        //            zoneStackPanel.Children.Add(CreateVerticalStack("MAXOUT", $"{pidParamList[j].MaxOut}"));
        //        }

        //        zoneTabItem.Content = zoneStackPanel;
        //        PIDTabControl.Items.Add(zoneTabItem);
        //    }
        //}

        private void InitUI()
        {
            for (int i = 0; i < zoneID2PidParamList.Count; i++)
            {
                if (PIDTabControl.Items[i] is TabItem tabItem)
                {
                    if (tabItem.Content is StackPanel vsp)
                    {
                        var key = $"Zone{i}";
                        var pidParamList = zoneID2PidParamList[key];
                        for (int j = 0; j < pidParamList.Count; j++)
                        {
                            var hsp = CreateHorizontalStackPanel(key + $"_{j}_", pidParamList[j]);
                            vsp.Children.Add(hsp);
                        }
                    }
                }
            }
        }

        private StackPanel CreateHorizontalStackPanel(string id, PID_Parameter pidParam)
        {
            var sp = new StackPanel() { 
                Orientation = Orientation.Horizontal,
            };
            var paramArr = pidParam.GetAllValues();

            TextBox tb;
            for (int i = 0; i < paramArr.Length; i++)
            {
                tb = CreateTextBox(id + pidParam.GetPropertyName(i), $"{paramArr[i]}");
                sp.Children.Add(tb);
            }
            
            //TextBox indexTB = CreateTextBox($"{pidParam.Index}");
            //sp.Children.Add(indexTB);

            //TextBox lowerTB = CreateTextBox($"{pidParam.LowerLimit}");
            //sp.Children.Add(lowerTB);

            //TextBox upperTB = CreateTextBox($"{pidParam.UpperLimit}");
            //sp.Children.Add(upperTB);

            //TextBox kpTB = CreateTextBox($"{pidParam.Kp}");
            //sp.Children.Add(kpTB);

            //TextBox kiTB = CreateTextBox($"{pidParam.Ki}");
            //sp.Children.Add(kiTB);

            //TextBox kdTB = CreateTextBox($"{pidParam.Kd}");
            //sp.Children.Add(kdTB);

            //TextBox maxoutTB = CreateTextBox($"{pidParam.MaxOut}");
            //sp.Children.Add(maxoutTB);

            return sp;
        }
        
        private TextBox CreateTextBox(string name, string text)
        {
            var tb = new TextBox() {
                Text = text ?? "",
                HorizontalContentAlignment = HorizontalAlignment.Center, 
                Width = 70,
                Name = name,
            };
            tb.TextChanged += (sender, e) => { 
                var tBox = sender as TextBox;
                if (!string.IsNullOrEmpty(tBox.Text))
                {
                    var strs = tBox.Name.Split('_');
                    int idx = int.Parse(strs[1]);
                    var ref2PIDParam = zoneID2PidParamList[strs[0]][idx];

                    if ("Index" == strs[2])
                        ref2PIDParam.Index = int.Parse(tBox.Text);
                    else if ("LowerLimit" == strs[2])
                        ref2PIDParam.LowerLimit = double.Parse(tBox.Text);
                    else if ("UpperLimit" == strs[2])
                        ref2PIDParam.UpperLimit = double.Parse(tBox.Text);
                    else if ("Kp" == strs[2])
                        ref2PIDParam.Kp = double.Parse(tBox.Text);
                    else if ("Ki" == strs[2])
                        ref2PIDParam.Ki = double.Parse(tBox.Text);
                    else if ("Kd" == strs[2])
                        ref2PIDParam.Kd = double.Parse(tBox.Text);
                    else if ("MaxOut" == strs[2])
                        ref2PIDParam.MaxOut = double.Parse(tBox.Text);
                    else
                    {
                        // error
                    }

                    LogHelper.Debug(strs[0] + " changed:", log);
                    LogHelper.Debug(ref2PIDParam.ToString(), log);
                }
            };
            return tb;
        }




        // 辅助方法：创建一个包含Label和TextBox的垂直StackPanel  
        private StackPanel CreateVerticalStack(string labelContent, string textBoxText)
        {
            StackPanel stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            stackPanel.Children.Add(new Label { Content = labelContent });
            TextBox textBox = new TextBox { Text = textBoxText, TextAlignment = TextAlignment.Center };
            //textBox.Name = labelContent + "_TBox"; // 注意：这里简单地将Name设置为Content_TBox，可能需要根据实际逻辑进行调整  
            stackPanel.Children.Add(textBox);
            return stackPanel;
        }

        // test
        private void GenPIDData()
        {
            try
            {
                zoneID2PidParamList = new Dictionary<string, List<PID_Parameter>>();
                for (int i = 0; i < nZones; i++)
                {
                    LogHelper.Debug($"Initializing Zone{i}:", log);

                    var pidParamList = new List<PID_Parameter>();
                    pidParamList.Add(new PID_Parameter(0, 0, 100, 1, 2, 3, 10000));
                    LogHelper.Debug(pidParamList[0].ToString(), log);

                    for (int j = 0; j < nPIDs; j++)
                    {
                        pidParamList.Add(new PID_Parameter(j, j * 100 + 100, j * 100 + 200, i + 1, i + 2, i + 3, 10000));
                        LogHelper.Debug(pidParamList[j + 1].ToString(), log);
                    }
                    zoneID2PidParamList[$"Zone{i}"] = pidParamList;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    internal class PID_Parameter
    {
        private int index;
        private double lowerLimit;
        private double upperLimit;
        private double kp;
        private double ki;
        private double kd;
        private double maxOut;

        public PID_Parameter(int index, double ll, double ul, double kp, double ki, double kd, double mo)
        {
            this.index = index;
            lowerLimit = ll;
            upperLimit = ul;
            this.kp = kp;
            this.ki = ki;
            this.kd = kd;
            maxOut = mo;
        }

        public int Index { get => index; set => index = value; }
        public double LowerLimit { get => lowerLimit; set => lowerLimit = value; }
        public double UpperLimit { get => upperLimit; set => upperLimit = value; }
        public double Kp { get => kp; set => kp = value; }
        public double Ki { get => ki; set => ki = value; }
        public double Kd { get => kd; set => kd = value; }
        public double MaxOut { get => maxOut; set => maxOut = value; }

        public double[] GetAllValues()
        {
            return new double[] { Index, LowerLimit, UpperLimit, Kp, Ki, Kd, MaxOut };
        }

        public string GetPropertyName(int index)
        {
            var strs = new string[] { "Index", "LowerLimit", "UpperLimit", "Kp", "Ki", "Kd", "MaxOut", };
            return strs[index];
        }

        override public string ToString()
        {
            return $"Index={Index} LL={LowerLimit} UL={UpperLimit} Kp={Kp} Ki={Ki} Kd={Kd} MaxOut={MaxOut}";
        }
    }
}
