using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Client.ViewModel
{
    public class ModelItem<T> : INotifyPropertyChanged
    {
        public ModelItem(T t)
        {
            this.Model = t;
        }

        public T Model
        {
            get;
            protected set;
        }

        //private bool isChecked;
        ///// <summary>
        ///// 是否选中
        ///// </summary>
        //public bool IsChecked
        //{
        //    get { return isChecked; }
        //    set
        //    {
        //        this.isChecked = value;
        //        ChangeIsCheckedEventArgs arg = new ChangeIsCheckedEventArgs(string.Empty, value);
        //        this.OnChangeIsChecked(arg);
        //        this.OnPropertyChanged("IsChecked");
        //    }
        //}

        ///// <summary>
        ///// 变更选中事件 
        ///// </summary>
        //public event EventHandler ChangeIsChecked;

        ///// <summary>
        ///// 触发变更选中的事件
        ///// </summary>
        ///// <param name="arg"></param>
        //protected virtual void OnChangeIsChecked(ChangeIsCheckedEventArgs arg)
        //{
        //    if (this.ChangeIsChecked != null)
        //    {
        //        this.ChangeIsChecked(this, arg);
        //    }
        //}

        //protected string GetCodeName(string code, string name)
        //{
        //    if (code.Equals(name))
        //    {
        //        return code;
        //    }
        //    string codeName = string.Empty;
        //    if (!string.IsNullOrEmpty(code))
        //    {
        //        if (!string.IsNullOrEmpty(name))
        //        {
        //            codeName = string.Format("{0}-{1}", code, name);
        //        }
        //        if (string.IsNullOrEmpty(codeName))
        //        {
        //            codeName = code;
        //        }
        //    }

        //    if (string.IsNullOrEmpty(codeName))
        //    {
        //        codeName = name;
        //    }
        //    return codeName;
        //}

        #region INotifyPropertyChanged成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }

    ///// <summary>
    ///// 变更选中事件的数据
    ///// </summary>
    //public class ChangeIsCheckedEventArgs : EventArgs
    //{
    //    public string Result
    //    {
    //        get;
    //        private set;
    //    }

    //    public bool Value
    //    {
    //        get;
    //        private set;
    //    }

    //    public ChangeIsCheckedEventArgs(string result, bool value)
    //    {
    //        this.Result = result;
    //        this.Value = value;
    //    }
    //}

}
