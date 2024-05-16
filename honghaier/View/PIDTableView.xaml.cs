using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace honghaier.View
{
    public partial class PIDTableView : UserControl
    {
        private int nAllZones = 8;
        private int nVisibleZones = 1;

        // test
        private int nPIDSize = 1; // 各区域的温度段数
        private List<PID_Parameter> pidParamList;

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


            //InitUI();
        }

        private void InitUI()
        {
            PIDTabControl.Items.Clear();

            TabItem zoneTabItem;
            StackPanel zoneStackPanel;

            for (int i = 0; i < nVisibleZones; i++)
            {
                zoneTabItem = new TabItem()
                {
                    Header = $"Zone{i}",
                    Name = $"PIDTable_Zone{i}TabItem",
                };

                zoneStackPanel = new StackPanel()
                { 
                    Orientation = Orientation.Horizontal,
                    Name = $"PIDTable_Zone{i}StackPanel",
                };
                // 添加控件到Zone{i}的StackPanel中
                for (int j = 0; j < pidParamList.Count; j++)
                {
                    zoneStackPanel.Children.Add(CreateVerticalStack("Index ", $"{pidParamList[j].Index}"));
                    zoneStackPanel.Children.Add(CreateVerticalStack("  Kp  ", $"{pidParamList[j].Kp}"));
                    zoneStackPanel.Children.Add(CreateVerticalStack("  Ki  ", $"{pidParamList[j].Ki}"));
                    zoneStackPanel.Children.Add(CreateVerticalStack("  Kd  ", $"{pidParamList[j].Kd}"));
                    zoneStackPanel.Children.Add(CreateVerticalStack("MAXOUT", $"{pidParamList[j].MaxOut}"));
                }

                zoneTabItem.Content = zoneStackPanel;
                PIDTabControl.Items.Add(zoneTabItem);
            }
        }

        // Create count zone TabItems in PID TabControl
        private void CreateZoneItems(int count)
        {

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
                pidParamList = new List<PID_Parameter>();
                for (int i = 0; i < nPIDSize; i++)
                {
                    pidParamList.Add(new PID_Parameter(i, i * 100, i * 100 + 100, i + 1, i + 2, i + 3, 10000));
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
    }
}
