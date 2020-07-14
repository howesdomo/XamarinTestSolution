using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Client.ViewModel
{
    /// <summary>
    /// 1.0.3 - 2020-07-09 14:41:05
    /// 增加 SetProperty 方法
    /// 
    /// 1.0.2 - 2019-10-28 10:36:44
    /// 整合 NotifyPropertyChanged 到 OnPropertyChanged
    /// 
    /// 1.0.1 - 2019-09-26 15:46:09
    /// 增加 NotifyPropertyChanged 方法
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }

        bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }

        /// <summary>
        /// 整合后的 Set 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        protected bool SetProperty<T>
        (
            ref T backingStore,
            T value,
            [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "",
            Action onChanged = null
        )
        {
            // 判断新旧值是否相等, 若相等则停止并返回 false
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value; // 赋值
            OnPropertyChanged(propertyName); // 通知界面更新本PropertyName
            onChanged?.Invoke(); // 若定义了其他的通知, 则在此执行其他通知逻辑
            return true;
        }

        #region INotifyPropertyChanged成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
