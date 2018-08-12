using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface ILBS
    {
        void GetGPSInfo();
    }

    public class LBS
    {
        public static EventHandler<LBSModel> GetGPSInfoEvent;

        public static void OnGetGPSInfo(LBSModel args)
        {
            if (LBS.GetGPSInfoEvent != null)
            {
                LBS.GetGPSInfoEvent.Invoke(null, args);
            }
        }

    }

    public class LBSModel : EventArgs
    {
        public LBSModel
            (
                string _GPSInfoType,
                string _Latitude,
                string _Longitude,
                string _Radius = "",
                string _Country = "",
                string _Province = "",
                string _City = "",
                string _Street = "",
                string _Address = "",
                string _LocationDescribe = ""
            )
        {
            IsComplete = true;
            ExceptionInfo = string.Empty;
            IsSuccess = true;
            BusinessExceptionInfo = string.Empty;

            this.GPSInfoType = _GPSInfoType;

            this.Latitude = _Latitude;
            this.Longitude = _Longitude;
            this.ReceiveTime = DateTime.Now;

            this.Radius = fixEmptyString(_Radius);
            this.Country = fixEmptyString(_Country);
            this.Province = fixEmptyString(_Province);
            this.City = fixEmptyString(_City);
            this.Street = fixEmptyString(_Street);
            this.Address = fixEmptyString(_Address);
            this.LocationDescribe = fixEmptyString(_LocationDescribe);
        }

        private string fixEmptyString(string args)
        {
            if (args.IsNullOrEmpty() == true)
            {
                return "未知";
            }
            else
            {
                return args.TrimAdv();
            }
        }

        public LBSModel(string _ExceptionInfo)
        {
            IsComplete = false;
            ExceptionInfo = _ExceptionInfo;
            IsSuccess = false;
            BusinessExceptionInfo = string.Empty;
        }

        #region EventArgs Base

        /// <summary>
        /// 程序执行错误
        /// </summary>
        public bool IsComplete { get; private set; }

        /// <summary>
        /// 程序执行错误
        /// </summary>
        public string ExceptionInfo { get; private set; }

        /// <summary>
        /// 业务逻辑运行成功
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// 业务逻辑报错信息
        /// </summary>
        public string BusinessExceptionInfo { get; private set; }

        #endregion

        /// <summary>
        /// 定位方式
        /// </summary>
        public string GPSInfoType { get; private set; }

        public string Latitude { get; private set; }

        public string Longitude { get; private set; }

        public DateTime ReceiveTime { get; private set; }

        public string Radius { get; private set; }

        public string Country { get; private set; }

        public string Province { get; private set; }

        public string City { get; private set; }

        public string Street { get; private set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string LocationDescribe { get; private set; }


    }
}
