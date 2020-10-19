using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.View.Menu;
using Android.Views;
using Android.Widget;

using Util.XamariN.AndroiD.Extensions;

using Com.Tencent.MM.Opensdk.Modelmsg;
using Com.Tencent.MM.Opensdk.Openapi;
using Client.Common;

namespace Client.Droid
{
    public class MyWeChatOpenSDK : IWechatOpenSDK
    {
        private Com.Tencent.MM.Opensdk.Openapi.IWXAPI api;

        /// <summary>
        /// 从官方网站申请到的合法 APP_ID
        /// </summary>
        public string mAPP_ID { get; private set; }

        /// <summary>
        /// 支持朋友圈的最小版本
        /// </summary>
        private const int TIMELINE_SUPPORTED_VERSION = 0x21020001;

        public MyWeChatOpenSDK(string app_id)
        {
            mAPP_ID = app_id;
            api = WXAPIFactory.CreateWXAPI(Android.App.Application.Context, mAPP_ID, false); // 通过WXAPIFactory工厂，获取实例
        }

        /// <summary>
        /// 是否支持朋友圈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsSupportTimeLine(object sender, EventArgs e)
        {
            int wxSdkVersion = api.WXAppSupportAPI;
            return wxSdkVersion >= TIMELINE_SUPPORTED_VERSION;
        }

        public bool OpenWechat()
        {
            return api.OpenWXApp();
        }

        /// <summary>
        /// 分享文字信息
        /// </summary>
        /// <param name="msg">显示在微信聊天记录的信息</param>
        /// <param name="description">显示在按[分享]时候的信息</param>
        /// <returns>返回记录</returns>
        public bool SendMsg(string msg, string description = "", WXScene scene = 0)
        {
            // 初始化一个 WXTextObject 对象，填写分享的文本内容
            WXTextObject textObj = new WXTextObject();
            textObj.Text = msg;

            // 用 WXTextObject 对象初始化一个 WXMediaMessage 对象
            WXMediaMessage wxMediaMessage = new WXMediaMessage();
            wxMediaMessage.MyMediaObject = textObj;
            wxMediaMessage.Description = description;

            SendMessageToWX.Req req = new SendMessageToWX.Req();

            req.Transaction = System.Guid.NewGuid().ToString(); // 唯一的请求标志
            req.Message = wxMediaMessage;

            // req.Scene = SendMessageToWX.Req.WXSceneSession; // 发送信息
            // req.Scene = SendMessageToWX.Req.WXSceneTimeline; // 发送朋友圈
            req.Scene = (int)scene;

            return api.SendReq(req);
        }

        public bool SendImage(string imagePath, WXScene scene = 0)
        {
            if (System.IO.File.Exists(imagePath) == false)
            {
                throw new Exception($"图片文件不存在。文件路径:\r\n{imagePath}");
            }

            Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeFile(imagePath);

            return SendImage(bitmap, scene);
        }

        public bool SendImage(Android.Graphics.Bitmap bitmap, WXScene scene = 0)
        {
            if (bitmap == null)
            {
                throw new Exception("(Android.Graphics.Bitmap) bitmap is null");
            }

            WXImageObject imgObj = new WXImageObject(bitmap);

            WXMediaMessage wxMediaMessage = new WXMediaMessage();
            wxMediaMessage.MyMediaObject = imgObj;

            // 设置缩略图
            int thumbSize = 150; // 150是根据官网demo设置
            Android.Graphics.Bitmap thumbBmp = bitmap.ScaledBitmap(thumbSize, thumbSize, false); // 扩展方法
            bitmap.Recycle();

            wxMediaMessage.ThumbData = thumbBmp.ToArray(true); // 扩展方法

            //构造一个Req请求
            SendMessageToWX.Req req = new SendMessageToWX.Req();

            //唯一的请求标志
            req.Transaction = System.Guid.NewGuid().ToString();
            req.Message = wxMediaMessage;

            // req.Scene = SendMessageToWX.Req.WXSceneSession; // 发送信息
            // req.Scene = SendMessageToWX.Req.WXSceneTimeline; // 发送朋友圈
            req.Scene = (int)scene;

            //发送数据
            return api.SendReq(req);
        }
    
    }
}