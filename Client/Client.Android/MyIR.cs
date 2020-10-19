using Android.Content;

namespace Client.Droid
{
    public class MyIR : Client.Common.I_IR
    {
        private static Android.Hardware.ConsumerIrManager sCIR { get; set; }

        #region 构造函数 + 单例模式

        private MyIR()
        {

        }

        private MyIR(Context context)
        {
            if (sCIR == null)
            {
                sCIR = context.GetSystemService(Context.ConsumerIrService) as Android.Hardware.ConsumerIrManager;
            }
        }

        private static MyIR s_Instance;

        private static object objLock = new object();

        public static MyIR GetInstance(Context context)
        {
            lock (objLock)
            {
                if (s_Instance == null)
                {
                    s_Instance = new MyIR(context);
                }

                return s_Instance;
            }
        }

        #endregion 

        public void Send(int carrierFrequency, int[] args)
        {
            // sCIR.TransmitAsync(carrierFrequency, args);
            sCIR.Transmit(carrierFrequency, args);
        }
    }
}