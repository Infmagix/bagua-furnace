using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace honghaier.View
{
    /// <summary>
    /// CustomTable.xaml 的交互逻辑
    /// </summary>
    public partial class CustomTable : UserControl
    {
        // Used to generate Random Walk
        private Random _random = new Random(251916);
        const int Count = 2000;
        private DispatcherTimer timer;
        private List<XyDataSeries<double, double>> listDataSeries = new List<XyDataSeries<double, double>>();

        public CustomTable()
        {
            InitializeComponent();
        }

        private void TestGenDataAndPlot()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnNewData;

            for (int i = 0; i < 3; i++)
            {
                var dataSeries0 = new XyDataSeries<double, double>();
                dataSeries0.AcceptsUnsortedData = true;
                listDataSeries.Add(dataSeries0);
            }
        }

        private void OnNewData(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void QuadLeftRightAxes_Loaded(object sender, RoutedEventArgs e)
        {
            // timer.Start();
        }

        private void UpdateData()
        {
            // Batch updates with one redraw
            using (sciChart.SuspendUpdates())
            {
                sciChart.RenderableSeries[0].DataSeries = FillData(listDataSeries[0], "Red Line");
                sciChart.RenderableSeries[1].DataSeries = FillData(listDataSeries[1], "Red Line");
                sciChart.RenderableSeries[2].DataSeries = FillData(listDataSeries[2], "Red Line");
            }
        }

        private IDataSeries FillData(IXyDataSeries<double, double> dataSeries, string name)
        {
            dataSeries.Clear();

            double randomWalk = 10.0;
            var startDate = new DateTime(2012, 01, 01);

            // Generate the X,Y data with sequential dates on the X-Axis and slightly positively biased random walk on the Y-Axis
            var xBuffer = new double[Count];
            var yBuffer = new double[Count];
            for (int i = 0; i < Count; i++)
            {
                randomWalk += (_random.NextDouble() - 0.498);
                yBuffer[i] = randomWalk;
                xBuffer[i] = i;
            }

            // Buffer above and append all in one go to avoid multiple recalculations of series range
            dataSeries.Append(xBuffer, yBuffer);
            dataSeries.SeriesName = name;

            return dataSeries;
        }
    }
}
