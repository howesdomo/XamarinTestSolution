using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.BaseView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UcAudioSetting : ContentView
    {
        UcAudioSettingViewModel ViewModel { get; set; }

        public UcAudioSetting()
        {
            InitializeComponent();
            initUI();

            this.ViewModel = new UcAudioSettingViewModel();
            initData();
            this.BindingContext = this.ViewModel;
        }

        private void initUI()
        {
            //gMain.Margin = new Thickness() { Top = 5, Bottom = 5 };

            //lbl1.Margin = new Thickness() { Left = 20 };
            //sld1.Margin = new Thickness() { Left = 20, Right = 20 };

            //lbl2.Margin = new Thickness() { Left = 20 };
            //sld2.Margin = new Thickness() { Left = 20, Right = 20 };
        }

        private void initData()
        {
            this.ViewModel.IsBackgroundMusicOn = App.AudioPlayer.GetIsBackgroundMusicOn();
            this.ViewModel.BackgroundMusicVolume = App.AudioPlayer.GetBackgroundMusicVolume();

            this.ViewModel.IsEffectsOn = App.AudioPlayer.GetIsEffectsOn();
            this.ViewModel.EffectsVolume = App.AudioPlayer.GetEffectsVolume();
        }
    }

    public class UcAudioSettingViewModel : ViewModel.BaseViewModel
    {
        public bool _IsBackgroundMusicOn;

        public bool IsBackgroundMusicOn
        {
            get { return _IsBackgroundMusicOn; }
            set
            {
                _IsBackgroundMusicOn = value;
                this.OnPropertyChanged("IsBackgroundMusicOn");
                App.AudioPlayer.SetIsBackgroundMusicOn(value);
            }
        }

        public float _BackgroundMusicVolume;

        public float BackgroundMusicVolume
        {
            get { return _BackgroundMusicVolume; }
            set
            {
                _BackgroundMusicVolume = value;
                this.OnPropertyChanged("BackgroundMusicVolume");
                App.AudioPlayer.SetBackgroundMusicVolume(value);
            }
        }

        public bool _IsEffectsOn;

        public bool IsEffectsOn
        {
            get { return _IsEffectsOn; }
            set
            {
                _IsEffectsOn = value;
                this.OnPropertyChanged("IsEffectsOn");
                App.AudioPlayer.SetIsEffectsOn(value);
            }
        }

        public float _EffectsVolume;

        public float EffectsVolume
        {
            get { return _EffectsVolume; }
            set
            {
                _EffectsVolume = value;
                this.OnPropertyChanged("EffectsVolume");
                App.AudioPlayer.SetEffectsVolume(value);            
            }
        }
    }
}