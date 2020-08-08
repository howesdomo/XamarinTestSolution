using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XF_4p3_Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCollectionView_UseSwipeView : ContentPage
    {
        public PageCollectionView_UseSwipeView()
        {
            InitializeComponent();

            // 使用 SwipeView 要先在 Android / iOS 代码中 声明 
            //Xamarin.Forms.Forms.SetFlags(new string[]
            //{
            //                "Expander_Experimental",
            //                "SwipeView_Experimental"
            //});
        }
    }
}