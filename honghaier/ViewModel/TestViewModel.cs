using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using honghaier.Model;
using System.Windows.Controls;
using honghaier.View;

namespace honghaier.ViewModel
{
    public class TestViewModel
    {
        private TestDataModel testDataModel { get; set; }
        private List<UserControl> dataViewList;

        public TestViewModel()
        {
            testDataModel = new TestDataModel();
            dataViewList = new List<UserControl>();
        }

        public void RegisterViews(UserControl view)
        {
            dataViewList.Add(view);
            view.DataContext = testDataModel;
            testDataModel.PropertyChanged += ((INotifyClass)view).PropertyChanged;
        }

        public void UpdateData(List<float> inData)
        {
            testDataModel.DataPlotQueueList = inData;
        }
    }
}
