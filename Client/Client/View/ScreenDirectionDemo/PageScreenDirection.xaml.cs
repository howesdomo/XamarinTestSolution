﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageScreenDirection : ContentPage
    {
        public PageScreenDirection()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btn1.Clicked += Btn1_Clicked;
            this.btn2.Clicked += Btn2_Clicked;
            this.btn3.Clicked += Btn3_Clicked;
            this.btn4.Clicked += Btn4_Clicked;

            //this.btnEss1.Clicked += BtnEss1_Clicked;
            //this.btnEss2.Clicked += BtnEss2_Clicked;
            //this.btnEss3.Clicked += BtnEss3_Clicked;
            //this.btnEss4.Clicked += BtnEss4_Clicked;

        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            App.ScreenDirection.Unspecified();
        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            App.ScreenDirection.ForcePortrait();
        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            App.ScreenDirection.ForceLandscape();
        }

        private void Btn4_Clicked(object sender, EventArgs e)
        {
            App.ScreenDirection.ForceNosensor();
        }



        //private void BtnEss1_Clicked(object sender, EventArgs e)
        //{
        //    Xamarin.Essentials.DisplayInfo
        //    // Xamarin.Essentials.DisplayOrientation.Landscape
        //}

        //private void BtnEss2_Clicked(object sender, EventArgs e)
        //{
            
        //}

        //private void BtnEss3_Clicked(object sender, EventArgs e)
        //{
            
        //}

        //private void BtnEss4_Clicked(object sender, EventArgs e)
        //{
            
        //}
    }
}