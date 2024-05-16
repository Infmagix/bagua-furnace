using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using SciChart.Charting.Model.DataSeries;
using honghaier.Utility;

namespace honghaier.View
{
    public partial class RealTimeDataView : UserControl
    {
        // Data Sample Rate (sec) - 20 Hz
        private const double dtFreq = 20;

        // FIFO Size is 100 samples, meaning after 100 samples have been appended, each new sample appended
        // results in one sample being discarded
        private int FifoSize = Const.SciChartLength;

        // Timer to process updates
        private readonly Timer _timerNewDataUpdate;

        DateTime time = DateTime.Now;

        // The dataseries to fill
        private readonly List<IXyDataSeries<float, float>> _series=new List<IXyDataSeries<float, float>>();

        public RealTimeDataView()
        {
            InitializeComponent();

            _timerNewDataUpdate = new Timer(1000.0f / dtFreq) { AutoReset = true };
            // _timerNewDataUpdate.Elapsed += OnNewData;

            string[] titles = { "通道1", "通道2", "通道3", "通道4",
                "温度1", "温度2", "温度3", "温度4",
                "反射率1", "反射率2", "反射率3", "反射率4" };

            // Create new Dataseries of type X=double, Y=double
            foreach (string title in titles)
            {
                XyDataSeries<float, float> xydata = new XyDataSeries<float, float>()
                    { FifoCapacity = FifoSize, SeriesName = title };

                xydata.AcceptsUnsortedData = true;
                _series.Add(xydata);
            }

            sciChartSurface0.renderableSeries0.DataSeries = _series[0];
            sciChartSurface1.renderableSeries0.DataSeries = _series[1];
            sciChartSurface2.renderableSeries0.DataSeries = _series[2];
            sciChartSurface3.renderableSeries0.DataSeries = _series[3];

            sciChartSurface4.renderableSeries0.DataSeries = _series[4];
            sciChartSurface5.renderableSeries0.DataSeries = _series[5];
            sciChartSurface6.renderableSeries0.DataSeries = _series[6];
            sciChartSurface7.renderableSeries0.DataSeries = _series[7];

            sciChartSurface4.renderableSeries1.DataSeries = _series[8];
            sciChartSurface5.renderableSeries1.DataSeries = _series[9];
            sciChartSurface6.renderableSeries1.DataSeries = _series[10];
            sciChartSurface7.renderableSeries1.DataSeries = _series[11];
        }

        private void ClearDataSeries()
        {
            foreach(XyDataSeries<float, float> item in _series)
            {
                item.Clear();
            }
        }

        private void OnExampleLoaded(object sender, RoutedEventArgs e)
        {
            ClearDataSeries();

            _timerNewDataUpdate.Start();
        }

        private void OnExampleUnloaded(object sender, RoutedEventArgs e)
        {
            _timerNewDataUpdate?.Stop();
        }

        public void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ClearDataSeries();

            Model.RealTimeDataModel rtdm = sender as Model.RealTimeDataModel;
            if(rtdm == null)
            {
                return;
            }

            for (int i = 0; i < _series.Count; i++)
            {
                for (int j = 0; j < rtdm.channelPlotQueueList[i].Count && j < Const.SciChartLength; j++)
                {
                    _series[i].Append(j, rtdm.channelPlotQueueList[i][j]);
                }
            }
        }
    }
}
