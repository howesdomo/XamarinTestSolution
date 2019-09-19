using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Client.View
{
    public class PingDemoViewModel : ViewModel.BaseViewModel
    {
        public PingDemoViewModel()
        {
            Result = new ObservableCollection<PingReplyModel>();
        }

        private ObservableCollection<PingReplyModel> _Result;

        public ObservableCollection<PingReplyModel> Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
                if (_Result != null)
                {
                    _Result.CollectionChanged += _Result_CollectionChanged;
                }
                this.OnPropertyChanged("Result");
            }
        }

        private void _Result_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("Result");
        }
    }
}
