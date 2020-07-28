using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.MarqueeDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarqueeDemoList : ContentPage
    {
        public MarqueeDemoList()
        {
            InitializeComponent();

            initEvent();
        }

        private void initEvent()
        {
            btnMarginChange.Clicked += BtnMarginChange_Clicked;
            btnPaddingChange.Clicked += BtnPaddingChange_Clicked;
            btnText20Change.Clicked += BtnText20Change_Clicked;
            btnText30Change.Clicked += BtnText30Change_Clicked;
            btnFontSizeChange.Clicked += BtnFontSizeChange_Clicked;


            btnWordsPerSecondPlus.Clicked += BtnWordsPerSecondPlus_Clicked;
            btnWordsPerSecondMinus.Clicked += BtnWordsPerSecondMinus_Clicked;

            btnSubEvent_ReadComplete.Clicked += BtnSubEvent_ReadComplete_Clicked;
            btnUnsubEvent_ReadComplete.Clicked += BtnUnsubEvent_ReadComplete_Clicked;

            btnSubEvent_ResetComplete.Clicked += BtnSubEvent_ResetComplete_Clicked;
            btnUnsubEvent_ResetComplete.Clicked += BtnUnsubEvent_ResetComplete_Clicked;

            btnUpdateBinding.Clicked += BtnUpdateBinding_Clicked;
        }

        private void BtnWordsPerSecondPlus_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.WordsPerSecond = marqueeLabel0.WordsPerSecond + 2;
        }

        private void BtnWordsPerSecondMinus_Clicked(object sender, EventArgs e)
        {
            try
            {
                marqueeLabel0.WordsPerSecond = marqueeLabel0.WordsPerSecond - 2;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetFullInfo());
            }
        }

        private void BtnMarginChange_Clicked(object sender, EventArgs e)
        {
            var a = new Thickness
            (
                marqueeLabel0.Margin.Left + 5,
                marqueeLabel0.Margin.Top + 5,
                marqueeLabel0.Margin.Right + 5,
                marqueeLabel0.Margin.Bottom + 5
            );

            marqueeLabel0.Margin = a;
        }


        private void BtnPaddingChange_Clicked(object sender, EventArgs e)
        {
            var a = new Thickness
            (
                marqueeLabel0.Padding.Left + 5,
                marqueeLabel0.Padding.Top + 5,
                marqueeLabel0.Padding.Right + 5,
                marqueeLabel0.Padding.Bottom + 5
            );

            marqueeLabel0.Padding = a;
        }

        private void BtnText30Change_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.Text = "布衾多年冷似铁，娇儿恶卧踏里裂。床头屋漏无干处，雨脚如麻未断绝。自经丧乱少睡眠，长夜沾湿何由彻！安得广厦千万间，大庇天下寒士俱欢颜！风雨不动安如山。";
        }

        private void BtnText20Change_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.Text = "床前明月光,疑是地上霜,举头望明月,低头思故乡。";
        }

        private void BtnFontSizeChange_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.FontSize = marqueeLabel0.FontSize + 2;
        }



        private void marqueeLabel_ReadCompleted_Handler(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Read ReadCompleted");
            lblReadCompleteInfo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

        private void BtnSubEvent_ReadComplete_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.ReadCompleted += new EventHandler<EventArgs>(marqueeLabel_ReadCompleted_Handler);
        }

        private void BtnUnsubEvent_ReadComplete_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.ReadCompleted -= new EventHandler<EventArgs>(marqueeLabel_ReadCompleted_Handler);
        }



        private void marqueeLabel_ResetCompleted_Handler(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Reset Completed");
            lblResetCompleteInfo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

        private void BtnSubEvent_ResetComplete_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.ResetCompleted += new EventHandler<EventArgs>(marqueeLabel_ResetCompleted_Handler);
        }

        private void BtnUnsubEvent_ResetComplete_Clicked(object sender, EventArgs e)
        {
            marqueeLabel0.ResetCompleted -= new EventHandler<EventArgs>(marqueeLabel_ResetCompleted_Handler);
        }



        private void BtnUpdateBinding_Clicked(object sender, EventArgs e)
        {
            MarqueeDemoList_ViewModel vm = this.BindingContext as MarqueeDemoList_ViewModel;
            vm.BackgroundColor = Color.Silver;
            vm.TextColor = Color.Blue;
            vm.Margin = new Thickness(5, 10, 15, 20);
            vm.Padding = new Thickness(20, 15, 10, 5);

        }
    }

    public class MarqueeDemoList_ViewModel : BaseViewModel
    {
        private string _Text = "自经丧乱少睡眠，长夜沾湿何由彻！安得广厦千万间，大庇天下寒士俱欢颜。风雨不动安如山！呜呼，何时眼前突兀见此屋，吾庐独破受冻死亦足！";

        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                this.OnPropertyChanged();
            }
        }

        private Color _BackgroundColor = Color.Yellow;

        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                _BackgroundColor = value;
                this.OnPropertyChanged();
            }
        }

        private Thickness _Margin = new Thickness(20, 15, 10, 5);

        public Thickness Margin
        {
            get { return _Margin; }
            set { _Margin = value; }
        }


        private Thickness _Padding = new Thickness(5, 10, 15, 20);

        public Thickness Padding
        {
            get { return _Padding; }
            set
            {
                _Padding = value;
                this.OnPropertyChanged();

            }
        }

        private Color _TextColor = Color.Orange;

        public Color TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                this.OnPropertyChanged();
            }
        }

        private int _WordsPerSecond = 7;

        public int WordsPerSecond
        {
            get { return _WordsPerSecond; }
            set
            {
                _WordsPerSecond = value;
                this.OnPropertyChanged();
            }
        }

        private double _StartBreakSecond = 1;

        public double StartBreakSecond
        {
            get { return _StartBreakSecond; }
            set
            {
                _StartBreakSecond = value;
                this.OnPropertyChanged();
            }
        }

        private double _EndBreakPoint = 1;

        public double EndBreakPoint
        {
            get { return _EndBreakPoint; }
            set
            {
                _EndBreakPoint = value;
                this.OnPropertyChanged();
            }
        }

        private double _ResetSecond = 1;

        public double ResetSecond
        {
            get { return _ResetSecond; }
            set
            {
                _ResetSecond = value;
                this.OnPropertyChanged();
            }
        }


    }
}