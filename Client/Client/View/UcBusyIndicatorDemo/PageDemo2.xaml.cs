﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.UcBusyIndicatorDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDemo2 : ContentPage
    {
        public PageDemo2()
        {
            InitializeComponent();
        }
    }

    public class PageDemo2_ViewModel : ViewModel.BaseViewModel
    {
        public PageDemo2_ViewModel()
        {

            initCommand();
        }

        void initCommand()
        {
            this.CMD_BtnLoad = new Command(BtnLoad);
        }

        public Command CMD_BtnLoad { get; private set; }

        void BtnLoad()
        {
            App.DebounceAction.Debounce
            (
                interval: App.ActionIntervalDefault,
                action: () =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        System.ComponentModel.BackgroundWorker mBgWorker = new System.ComponentModel.BackgroundWorker();
                        mBgWorker.DoWork += (bgSender, bgArgs) =>
                        {
                            System.Threading.Thread.Sleep(5000);
                        };

                        mBgWorker.RunWorkerCompleted += (bgSender, bgResult) =>
                        {
                            this.BtnLoad_IsBusy = false;

                            if (bgResult.Error != null)
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Toast("异常");
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Toast("正常");
                            }
                        };

                        this.BusyContent = $"正在处理单据:{OrderNo}";
                        this.BtnLoad_IsBusy = true;
                        mBgWorker.RunWorkerAsync(new object[] { });
                    });
                },
                syncInvoke: null
            );
        }

        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set
            {
                _OrderNo = value;
                this.OnPropertyChanged(nameof(OrderNo));
            }
        }

        private bool _BtnLoad_IsBusy;
        public bool BtnLoad_IsBusy
        {
            get { return _BtnLoad_IsBusy; }
            set
            {
                _BtnLoad_IsBusy = value;
                this.OnPropertyChanged(nameof(BtnLoad_IsBusy));
            }
        }

        private string _BusyContent = string.Empty;
        public string BusyContent
        {
            get { return _BusyContent; }
            set
            {
                _BusyContent = value;
                this.OnPropertyChanged(nameof(BusyContent));
            }
        }



    }
}
