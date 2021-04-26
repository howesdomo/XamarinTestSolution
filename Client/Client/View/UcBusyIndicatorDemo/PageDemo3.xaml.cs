using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.UcBusyIndicatorDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDemo3 : ContentPage
    {
        public PageDemo3()
        {
            InitializeComponent();
        }
    }

    public class PageDemo3_ViewModel : ViewModel.BaseViewModel
    {
        public PageDemo3_ViewModel()
        {
            this.IsBusy = true;
            initCMD();
        }

        void initCMD()
        {
            this.CMD_SubmitUserDefinePathData = new Command(SubmitUserDefinePathData);
        }


        private string _PathData = "M 0,19 L 10,19 L 10,21 Z";
        public string PathData
        {
            get { return _PathData; }
            set
            {
                _PathData = value;
                this.OnPropertyChanged(nameof(PathData));
            }
        }

        public Command CMD_SubmitUserDefinePathData { get; private set; }
        void SubmitUserDefinePathData(object args)
        {
            try
            {
                // XamarinForms 版本貌似不能显示负数的内容, 故起始由9点方位的横线开始
                var temp = (Xamarin.Forms.Shapes.Geometry)new Xamarin.Forms.Shapes.PathGeometryConverter()
                            .ConvertFromInvariantString(args.ToString());

                System.ComponentModel.BackgroundWorker mBgWorker = new System.ComponentModel.BackgroundWorker();

                mBgWorker = new System.ComponentModel.BackgroundWorker();
                mBgWorker.DoWork += (bgSender, bgArgs) =>
                {
                    System.Threading.Thread.Sleep(3000);
                };

                mBgWorker.RunWorkerCompleted += (bgSender, bgResult) =>
                {
                    this.PathData = args.ToString();
                    this.IsBusy = true;
                };

                this.IsBusy = false;
                mBgWorker.RunWorkerAsync(new object[] { });
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetInfo());
                System.Diagnostics.Debugger.Break();
                Acr.UserDialogs.UserDialogs.Instance.Toast("PathData 不能成功转换");
            }
        }


    }
}