﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MarqueeDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarqueeLabel_V1 : ContentPage
    {
        public MarqueeLabel_V1()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btnStart.Clicked += BtnStart_Clicked;
        }

        async void BtnStart_Clicked(object sender, EventArgs e)
        {
            // lblMarquee.Text = "氢(qīng) 氦(hài) 锂(lǐ) 铍(pí) 硼(péng) 碳(tàn) 氮(dàn) 氧(yǎng) 氟(fú) 氖(nǎi) 钠(nà) 镁(měi) 铝(lǚ) 硅(guī) 磷(lín) 硫(liú) 氯(lǜ) 氩(yà) 钾(jiǎ) 钙(gài) 钪(kàng) 钛(tài) 钒(fán) 铬(gè) 锰(měng) 铁(tiě) 钴(gǔ) 镍(niè) 铜(tóng) 锌(xīn) 镓(jiā) 锗(zhě) 砷(shēn) 硒(xī) 溴(xiù) 氪(kè) 铷(rú) 锶(sī) 钇(yǐ) 锆(gào) 铌(ní) 钼(mù) 锝(dé) 钌(liǎo) 铑(lǎo) 钯(pá) 银(yín) 镉(gé) 铟(yīn) 锡(xī) 锑(tī) 碲(dì) 碘(diǎn) 氙(xiān) 铯(sè) 钡(bèi) 镧(lán) 铪(hā) 钽(tǎn) 钨(wū) 铼(lái) 锇(é) 铱(yī) 铂(bó) 金(jīn) 汞(gǒng) 铊(tā) 铅(qiān) 铋(bì) 钋(pō) 砹(ài) 氡(dōng) 钫(fāng) 镭(léi) 锕(ā) 钅卢(lú) 钅杜(dù) 钅喜(xǐ) 钅波(bō) 钅黑(hēi) 钅麦(mài) 钅达(dá) 钅仑(lún) 镧(lán) 铈(shì) 镨(pǔ) 钕(nǚ) 钷(pǒ) 钐(shān) 铕(yǒu) 钆(gá) 铽(tè) 镝(dí) 钬(huǒ) 铒(ěr) 铥(diū) 镱(yì) 镥(lǔ) 锕(ā) 钍(tǔ) 镤(pú) 铀(yóu) 镎(ná) 钚(bù) 镅(méi) 锔(jū) 锫(péi) 锎(kāi) 锿(āi) 镄(fèi) 钔(mén) 锘(nuò) 铹(láo)";

            double dblWidth = objStackLayout.Width + lblMarquee.Width;
            // Infinite loop
            while (true)
            {
                lblMarquee.IsVisible = false;
                await lblMarquee.TranslateTo(dblWidth / 2, 0, 500, Easing.SinIn);

                lblMarquee.IsVisible = true;
                await lblMarquee.TranslateTo(-(dblWidth / 2), 0, 10000, Easing.SinIn);

            }
        }
    }
}