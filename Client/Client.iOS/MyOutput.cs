using System;
using Client.Common;

namespace Client.iOS
{
    public class MyOutput : IOutput
    {
        public void Info(string tag, string msg)
        {
            // Android.Util.Log.Info(tag, msg);
        }

        public void Error(string tag, string msg)
        {
            // Android.Util.Log.Error(tag, msg);
        }

        public void Warn(string tag, string msg)
        {
            //Android.Util.Log.Warn(tag, msg);
        }

    }
}