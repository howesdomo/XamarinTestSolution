using DevExpress.Mobile.DataGrid;
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
    public partial class UcUserListDetail : ContentView
    {
        public UcUserListDetail()
        {
            InitializeComponent();

            IconSetFormatCondition condition = new IconSetFormatCondition();
            condition.FieldName = "Level";
            // condition.PredefinedFormatName = "Signs3IconSet";
            condition.PredefinedFormatName = "Arrows5ColoredIconSet";

//Arrows3ColoredIconSet
//Arrows3GrayIconSet
//Triangles3IconSet
//Arrows4GrayIconSet
//Arrows4ColoredIconSet
//Arrows5GrayIconSet
//Arrows5ColoredIconSet
//TrafficLights3UnrimmedIconSet
//TrafficLights3RimmedIconSet
//Signs3IconSet
//TrafficLights4IconSet
//RedToBlackIconSet
//Symbols3CircledIconSet
//Symbols3UncircledIconSet
//Flags3IconSet
//Stars3IconSet
//Ratings4IconSet
//Quarters5IconSet
//Ratings5IconSet
//Boxes5IconSet
//PositiveNegativeArrowsColoredIconSet
//PositiveNegativeArrowsGrayIconSet
//PositiveNegativeTrianglesIconSet

            grid.FormatConditions.Add(condition);

            // 红底黑字
            //TopBottomRuleFormatCondition condition2 = new TopBottomRuleFormatCondition();
            //condition2.FieldName = "MaxUseTime";
            //condition2.Rule = DevExpress.Mobile.Core.ConditionalFormatting.TopBottomRule.BottomPercent;
            //condition2.PredefinedFormatName = "LightRedFillWithDarkRedText";

            //grid.FormatConditions.Add(condition2);
        }

        public void SetBindingContext(object o)
        {
            grid.ItemsSource = o;
        }
    }
}