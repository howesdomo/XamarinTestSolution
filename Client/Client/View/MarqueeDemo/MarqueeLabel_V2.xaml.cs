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
    public partial class MarqueeLabel_V2 : ContentPage
    {
        public MarqueeLabel_V2()
        {
            InitializeComponent();

            List<MarqueeLabel_V2_Data> list = new List<MarqueeLabel_V2_Data>();
            list.Add(new MarqueeLabel_V2_Data() { Seq = 1, OrderNo = "Carton01" });
            list.Add(new MarqueeLabel_V2_Data() { Seq = 2, OrderNo = "Carton02" });
            list.Add(new MarqueeLabel_V2_Data() { Seq = 3, OrderNo = "Carton03" });
            list.Add(new MarqueeLabel_V2_Data() { Seq = 4, OrderNo = "Carton04" });

            marquee3.ItemsSource = list;
        }
    }

    public class MarqueeLabel_V2_Data
    {
        public int Seq { get; set; }

        public string OrderNo { get; set; }
    }
}