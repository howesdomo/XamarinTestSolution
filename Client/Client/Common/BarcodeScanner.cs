using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public class BarcodeScanner
    {
        public static EventHandler<EnpotBarcodeScanModel> BarcodeScanEvent;

        public static void OnBarcodeScan(EnpotBarcodeScanModel args)
        {
            if (BarcodeScanEvent != null)
            {
                BarcodeScanner.BarcodeScanEvent.Invoke(null, args);
            }
        }

    }

    public class EnpotBarcodeScanModel : EventArgs
    {
        public EnpotBarcodeScanModel
        (
            string _BarcodeContent,
            string _BarcodeType,
            DateTime _ScanTime,
            byte[] _RawBarcodeContent
        )
        {
            IsComplete = true;
            ExceptionInfo = string.Empty;
            IsSuccess = true;
            BusinessExceptionInfo = string.Empty;

            this.BarcodeContent = _BarcodeContent;
            this.BarcodeType = _BarcodeType;
            this.ScanTime = _ScanTime;
            this.RawBarcodeContent = _RawBarcodeContent;
        }

        public EnpotBarcodeScanModel(string _ExceptionInfo)
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
        public bool IsComplete { get; set; }

        /// <summary>
        /// 程序执行错误
        /// </summary>
        public string ExceptionInfo { get; set; }

        /// <summary>
        /// 业务逻辑运行成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 业务逻辑报错信息
        /// </summary>
        public string BusinessExceptionInfo { get; set; }

        #endregion

        public string BarcodeContent { get; private set; }

        public string BarcodeType { get; private set; }

        public DateTime ScanTime { get; private set; }

        public byte[] RawBarcodeContent { get; private set; }
    }
}
