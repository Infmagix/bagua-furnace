using honghaier.Model;
using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace honghaier.View
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView : UserControl, INotifyClass
    {
        private List<XyyDataSeries<DateTime, float>> dataSeriesList;
        private string[] titles = { "Power A", "Power B", "Power C", "Power D" };

        public TestView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            dataSeriesList = new List<XyyDataSeries<DateTime, float>>();
            foreach (var title in titles)
            {
                var series = new XyyDataSeries<DateTime, float>
                {
                    SeriesName = title,
                    FifoCapacity = 20000,
                    AcceptsUnsortedData = true,
                };
                dataSeriesList.Add(series);
            }
        }

        private void ResetSeriesDataToChart()
        {
            renderableSeries0.DataSeries = dataSeriesList[0];
            renderableSeries1.DataSeries = dataSeriesList[1];
            renderableSeries2.DataSeries = dataSeriesList[2];
        }

        public void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                var testDM = sender as TestDataModel;
                if (testDM == null || testDM.DataPlotQueueList == null) return;

                var xValues = new List<DateTime> { DateTime.Now };
                var temp = testDM.DataPlotQueueList;

                // assert(yBuffer.Count == dataSeriesList.Count);

                for (int i = 0; i < temp.Count; i++)
                {
                    var yValues = new List<float> { temp[i] };
                    dataSeriesList[i].Append(xValues, yValues, yValues);
                }

                ResetSeriesDataToChart();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
