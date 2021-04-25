using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// !!!!
// Ǩ�Ƶ� AssemblyInfo.cs �ļ���
// [assembly: XamlCompilation(XamlCompilationOptions.Compile)] 

namespace Client
{
    public partial class App : Application
    {
        public static NavigationPage Navigation { get; set; }



        public static Util.XamariN.IScreen Screen { get; set; }

        public static Util.XamariN.IAudioPlayer AudioPlayer { get; set; }

        public static Util.XamariN.ITTS TTS { get; set; }

        public static Util.XamariN.IBluetooth Bluetooth { get; set; }

        public static Util.XamariN.IShareUtils MyShareUtils { get; set; }

        public static Client.Common.ILBS LBS { get; set; }

        public static Client.Common.I_IR IR { get; set; }

        public static Client.Common.IOutput Output { get; set; }

        

        public static Util.Excel.IExcelUtils ExcelUtils_Aspose { get; set; }


        #region ��ֹ��

        /// <summary>
        /// Ĭ�Ϸ�ֹ�󴥼��ʱ��
        /// </summary>
        public static double ActionIntervalDefault { get { return 1000; } }

        /// <summary>
        /// �����Ķ�ε��ã���ÿ��ʱ��ε�������ִֻ�е�һ�δ�����̡�
        /// </summary>
        public static Util.ActionUtils.ThrottleAction ThrottleAction { get; set; } = new Util.ActionUtils.ThrottleAction();

        /// <summary>
        /// �����Ķ�ε��ã�ֻ���ڵ���ֹ֮ͣ���һ��ʱ��(ָ�����ʱ��)�ڲ��ٵ��ã�Ȼ���ִ��һ�δ�����̡�
        /// </summary>
        public static Util.ActionUtils.DebounceAction DebounceAction { get; set; } = new Util.ActionUtils.DebounceAction();

        #endregion

        #region ��׿

        public static Util.XamariN.IAndroidAssetsUtils AndroidAssetsUtils { get; set; }

        public static Util.XamariN.IAndroidIntentUtils AndroidIntentUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils { get; set; }

        public static Util.XamariN.IAndroidPermission AndroidPermissionUtils_InTestSolution { get; set; }

        public static Util.XamariN.IAndroidScreenshot AndroidScreenshot { get; set; }

        public static Util.XamariN.IAndroidScreenRecord AndroidScreenRecord { get; set; }

        #endregion
        
        public static Client.Common.IWechatOpenSDK MyWechatOpenSDK { get; set; }

        public App()
        {
            InitializeComponent();

            // ʵ���Ա�־
            Device.SetFlags(new string[]
            {
                "Expander_Experimental",
                "SwipeView_Experimental",
                "Shapes_Experimental"
            });

#if DEBUG
            // VS 2019 16.3 �汾��, �����õ� Hot Reload

            // HotReloader.Current.Run(this);

            //HotReloader.Current.Run(this, new HotReloader.Configuration
            //{
            //    ExtensionIpAddress = System.Net.IPAddress.Parse("192.168.1.215") // ��д����Ե�ip
            //});
#endif

            // MainPage = new MainPage(); // Դ����ע��, ���� MainPage = new NavigationPage(new MainPage()); ����

            // *** ���´��뼫Ϊ��Ҫ ***
            // MainPage������ NavigationPage, �޷�ʹ�� Navigation.PushAsync(somePage) ������ת��һ������

            var nPage = new NavigationPage(new MainPage());
            
            App.Navigation = nPage;
            MainPage = nPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
