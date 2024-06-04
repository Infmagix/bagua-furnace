using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using SciChart.Charting.Model.DataSeries;
using honghaier.Utility;
using honghaier.Model;

namespace honghaier.View
{
    public partial class RealTimeDataView : UserControl, INotifyClass
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("RealTimeDataView");
        // Data Sample Rate (sec) - 20 Hz
        private const double dtFreq = 20;

        // FIFO Size is 100 samples, meaning after 100 samples have been appended, each new sample appended
        // results in one sample being discarded
        private readonly int FifoSize = Const.SciChartLength;

        // Timer to process updates
        private readonly Timer _timerNewDataUpdate;

        DateTime time = DateTime.Now;

        // The dataseries to fill
        private readonly List<IXyDataSeries<float, float>> _series = new List<IXyDataSeries<float, float>>();

        public RealTimeDataView()
        {
            InitializeComponent();

            _timerNewDataUpdate = new Timer(1000.0f / dtFreq) { AutoReset = true };
            // _timerNewDataUpdate.Elapsed += OnNewData;

            string[] titles = { "功率1", "功率2", "功率3", "功率4", };

            // Create new Dataseries of type X=double, Y=double
            foreach (string title in titles)
            {
                var xydata = new XyDataSeries<float, float>
                {
                    FifoCapacity = FifoSize,
                    SeriesName = title,
                    AcceptsUnsortedData = true,
                };
                _series.Add(xydata);
            }

            sciChartSurface0.renderableSeries0.DataSeries = _series[0];
            sciChartSurface0.renderableSeries1.DataSeries = _series[1];
            sciChartSurface0.renderableSeries2.DataSeries = _series[2];
            //sciChartSurface0.renderableSeries3.DataSeries = _series[3];

            //sciChartSurface1.renderableSeries0.DataSeries = _series[1];
            //sciChartSurface2.renderableSeries0.DataSeries = _series[2];
            //sciChartSurface3.renderableSeries0.DataSeries = _series[3];

            //sciChartSurface4.renderableSeries0.DataSeries = _series[4];
            //sciChartSurface5.renderableSeries0.DataSeries = _series[5];
            //sciChartSurface6.renderableSeries0.DataSeries = _series[6];
            //sciChartSurface7.renderableSeries0.DataSeries = _series[7];

            //sciChartSurface4.renderableSeries1.DataSeries = _series[8];
            //sciChartSurface5.renderableSeries1.DataSeries = _series[9];
            //sciChartSurface6.renderableSeries1.DataSeries = _series[10];
            //sciChartSurface7.renderableSeries1.DataSeries = _series[11];
        }

        private void ClearDataSeries()
        {
            foreach (var item in _series)
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
            try
            {
                if (!(sender is RealTimeDataModel rtdm) || rtdm.ChannelPlotQueueList == null ||
                    rtdm.ChannelPlotQueueList.Count == 0 || e.PropertyName != "ChannelPlotQueueList") return;

                //var series = new List<IXyDataSeries<float, float>>();
                //for (int i = 0; i < _series.Count; i++)
                //{
                //    var xydata = new XyDataSeries<float, float>()
                //    {
                //        FifoCapacity = FifoSize,
                //        AcceptsUnsortedData = true,
                //    };
                //    _series[i] = xydata;
                //}

                for (int i = 0; i < rtdm.ChannelPlotQueueList.Count; i++)
                {
                    for (int j = 0; j < rtdm.ChannelPlotQueueList[i].Count && j < Const.SciChartLength; j++)
                    {
                        _series[i].Append(j, rtdm.ChannelPlotQueueList[i][j]);
                    }
                }


            }
            catch (Exception ex)
            {
                LogHelper.Exception(log, ex, false);
            }

            //ClearDataSeries();

            //Model.RealTimeDataModel rtdm = sender as Model.RealTimeDataModel;
            //if (rtdm == null)
            //{
            //    return;
            //}

            //for (int i = 0; i < _series.Count; i++)
            //{
            //    for (int j = 0; j < rtdm.ChannelPlotQueueList[i].Count && j < Const.SciChartLength; j++)
            //    {
            //        _series[i].Append(j, rtdm.ChannelPlotQueueList[i][j]);
            //    }
            //}
        }
    }

    interface INotifyClass
    {
        void PropertyChanged(object sender, PropertyChangedEventArgs e);
    }
}
