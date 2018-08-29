using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.Games.CRW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UcCRWKeyboard : ContentView
    {
        public UcCRWKeyboard()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.btn0.Clicked += Btn_Clicked;
            this.btn1.Clicked += Btn_Clicked;
            this.btn2.Clicked += Btn_Clicked;
            this.btn3.Clicked += Btn_Clicked;
            this.btn4.Clicked += Btn_Clicked;
            this.btn5.Clicked += Btn_Clicked;
            this.btn6.Clicked += Btn_Clicked;
            this.btn7.Clicked += Btn_Clicked;
            this.btn8.Clicked += Btn_Clicked;
            this.btn9.Clicked += Btn_Clicked;
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var args = new CRW_Keyboard_EventArgs(int.Parse(btn.Text));
            onInputValueEvent(args);
        }

        public EventHandler<CRW_Keyboard_EventArgs> InputValueEvent;

        private void onInputValueEvent(CRW_Keyboard_EventArgs args)
        {
            if (this.InputValueEvent != null)
            {
                InputValueEvent.Invoke(null, args);
            }
        }

    }

    public class CRW_Keyboard_EventArgs : EventArgs
    {
        public int Value { get; private set; }

        public CRW_Keyboard_EventArgs(int value)
        {
            this.Value = value;
        }
    }
}