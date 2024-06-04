using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honghaier.Model
{
    public class TestDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<float> dataPlotQueueList;
        public List<float> DataPlotQueueList
        {
            get => dataPlotQueueList; 
            set
            {
                dataPlotQueueList = value;
                OnPropertyChanged(nameof(dataPlotQueueList));
            }
        }

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
