using System.Collections.Generic;
using System.ComponentModel;

namespace honghaier.Model
{
    public class RealTimeDataModel : INotifyPropertyChanged
    {
        public List<List<float>> ChannelPlotQueueList { get; set; }

        void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
