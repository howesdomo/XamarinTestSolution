﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UcUserListDetailTabItem : ContentView
    {
        public UcUserListDetailTabItem()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            
        }
    }
}