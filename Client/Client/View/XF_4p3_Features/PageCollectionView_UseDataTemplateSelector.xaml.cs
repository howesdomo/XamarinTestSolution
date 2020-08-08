using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.XF_4p3_Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCollectionView_UseDataTemplateSelector : ContentPage
    {
        public PageCollectionView_UseDataTemplateSelector()
        {
            InitializeComponent();
        }
    }

    public class OddEvenSelector : DataTemplateSelector
    {
        /// <summary>
        /// 奇数
        /// </summary>
        public DataTemplate OddData { get; set; }

        /// <summary>
        /// 偶数
        /// </summary>
        public DataTemplate EvenData { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return (item as Order).OrderType.OrderTypeId % 2 == 0 ? OddData : EvenData;
        }
    }
}