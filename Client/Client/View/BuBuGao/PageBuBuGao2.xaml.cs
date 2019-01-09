using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.BuBuGao
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBuBuGao2 : ContentPage
    {
        public PageBuBuGao2_ViewModel ViewModel { get; set; }

        public PageBuBuGao2()
        {
            InitializeComponent();
            initUI();

            this.ViewModel = new PageBuBuGao2_ViewModel();
            this.BindingContext = this.ViewModel;
            initEvent();
        }

        private void initUI()
        {
            this.btnLast.Text = "<";
            this.btnNext2.Text = ">";
        }

        private void initEvent()
        {
            this.btnNext.Clicked += BtnNext_Clicked;
            this.btnPlaySound.Clicked += BtnPlaySound_Clicked;
        }

        async void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (this.ViewModel.Index + 1 >= this.ViewModel.Question.Words.Count)
            {
                await DisplayAlert("提示", "答题完毕", "确定");
                return;
            }

            this.ViewModel.Index += 1;
            this.ViewModel.Content = this.ViewModel.Question.Words[this.ViewModel.Index].Content;

            if (this.ViewModel.IsSoundPlay)
            {
                App.TTS.Play(this.ViewModel.Content);
            }
        }

        private void BtnPlaySound_Clicked(object sender, EventArgs e)
        {
            App.TTS.Play(this.ViewModel.Content);
        }
    }

    public class PageBuBuGao2_ViewModel : ViewModel.BaseViewModel
    {
        private int index = -1;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                this.OnPropertyChanged("Index");
            }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                this.OnPropertyChanged("Content");
            }
        }


        private Question question;

        public Question Question
        {
            get { return question; }
            set
            {
                question = value;
                this.OnPropertyChanged("Question");
            }
        }

        private bool isSoundPlay;

        public bool IsSoundPlay
        {
            get { return isSoundPlay; }
            set
            {
                isSoundPlay = value;
                this.OnPropertyChanged("IsSoundPlay");
            }
        }
    }
}