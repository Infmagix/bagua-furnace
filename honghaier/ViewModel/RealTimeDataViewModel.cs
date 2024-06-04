using System.Collections.Generic;
using System.Windows.Controls;
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
        public RealTimeDataModel RealTimeDataModel { get; set; }
        public DelegateCommand ShowCommand { get; set; }
        private List<UserControl> dataViewList = new List<UserControl>();

        public RealTimeDataViewModel()
        {
            RealTimeDataModel = new RealTimeDataModel();
        }

        public void RegisterViews(UserControl view)
        {
            dataViewList.Add(view);
            view.DataContext = RealTimeDataModel;
            RealTimeDataModel.PropertyChanged += ((INotifyClass)view).PropertyChanged;
        }

        public void UpdateData(List<List<float>> channelPlotQueueList)
        {
            RealTimeDataModel.ChannelPlotQueueList = channelPlotQueueList;
        }
    }
}
