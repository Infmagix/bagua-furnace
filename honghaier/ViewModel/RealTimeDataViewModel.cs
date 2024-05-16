using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using honghaier.Model;
using honghaier.Utility;
using honghaier.View;

/**
 * read:  amps/voltage/temperTC/rotateSpeed/state
 * write: amps/temperWK/temperTarget/state
 */
namespace honghaier.ViewModel
{
    public class RealTimeDataViewModel
    {
        public RealTimeDataModel realTimeDataModel { get; set; }
        public DelegateCommand ShowCommand { get; set; }
        private RealTimeDataView realTimeDataView { get; set; }
        public RealTimeDataViewModel(RealTimeDataView realTimeDataView)
        {
            this.realTimeDataView = realTimeDataView;
        }

        public void UpdateData(List<List<float>> channelPlotQueueList)
        {
            realTimeDataModel = new RealTimeDataModel();
            realTimeDataModel.PropertyChanged += realTimeDataView.PropertyChanged;
            realTimeDataModel.channelPlotQueueList = channelPlotQueueList;
        }
    }
}
