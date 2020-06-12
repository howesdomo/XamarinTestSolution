using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.ColorListDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageColorListDemo : ContentPage
    {
        public PageColorListDemo()
        {
            InitializeComponent();            
        }
    }

    public class PageColorListDemo_ViewModel : ViewModel.BaseViewModel
    {
        public PageColorListDemo_ViewModel()
        {
            List<ColorModel> temp = new List<ColorModel>();

            temp.Add(new ColorModel() { EnglishName = "AliceBlue", ChineseName = "艾莉斯蓝", HexValue = "#F0F8FF", BackgroundColor = Color.AliceBlue });
            temp.Add(new ColorModel() { EnglishName = "MintCream", ChineseName = "薄荷乳白色", HexValue = "#F5FFFA", BackgroundColor = Color.MintCream });
            temp.Add(new ColorModel() { EnglishName = "MistyRose", ChineseName = "浅玫瑰色", HexValue = "#FFE4E1", BackgroundColor = Color.MistyRose });
            temp.Add(new ColorModel() { EnglishName = "Moccasin", ChineseName = "鹿皮黄色", HexValue = "#FFE4B5", BackgroundColor = Color.Moccasin });
            temp.Add(new ColorModel() { EnglishName = "NavajoWhite", ChineseName = "印第安黄", HexValue = "#FFDEAD", BackgroundColor = Color.NavajoWhite });
            temp.Add(new ColorModel() { EnglishName = "Navy", ChineseName = "海军蓝", HexValue = "#000080", BackgroundColor = Color.Navy });
            temp.Add(new ColorModel() { EnglishName = "OldLace", ChineseName = "浅米色", HexValue = "#FDF5E6", BackgroundColor = Color.OldLace });
            temp.Add(new ColorModel() { EnglishName = "MidnightBlue", ChineseName = "中灰兰色", HexValue = "#191970", BackgroundColor = Color.MidnightBlue });
            temp.Add(new ColorModel() { EnglishName = "Olive", ChineseName = "橄榄色", HexValue = "#808000", BackgroundColor = Color.Olive });
            temp.Add(new ColorModel() { EnglishName = "Orange", ChineseName = "橙色", HexValue = "#FFA500", BackgroundColor = Color.Orange });
            temp.Add(new ColorModel() { EnglishName = "OrangeRed", ChineseName = "橙红色", HexValue = "#FF4500", BackgroundColor = Color.OrangeRed });
            temp.Add(new ColorModel() { EnglishName = "Orchid", ChineseName = "兰花紫", HexValue = "#DA70D6", BackgroundColor = Color.Orchid });
            temp.Add(new ColorModel() { EnglishName = "PaleGoldenrod", ChineseName = "淡菊黄色", HexValue = "#EEE8AA", BackgroundColor = Color.PaleGoldenrod });
            temp.Add(new ColorModel() { EnglishName = "PaleGreen", ChineseName = "苍绿色", HexValue = "#98FB98", BackgroundColor = Color.PaleGreen });
            temp.Add(new ColorModel() { EnglishName = "PaleTurquoise", ChineseName = "苍宝石绿", HexValue = "#AFEEEE", BackgroundColor = Color.PaleTurquoise });
            temp.Add(new ColorModel() { EnglishName = "OliveDrab", ChineseName = "淡绿褐色", HexValue = "#6B8E23", BackgroundColor = Color.OliveDrab });
            temp.Add(new ColorModel() { EnglishName = "PaleVioletRed", ChineseName = "苍紫罗兰色", HexValue = "#DB7093", BackgroundColor = Color.PaleVioletRed });
            temp.Add(new ColorModel() { EnglishName = "MediumVioletRed", ChineseName = "中紫红色", HexValue = "#C71585", BackgroundColor = Color.MediumVioletRed });
            temp.Add(new ColorModel() { EnglishName = "MediumSpringGreen", ChineseName = "中草绿色", HexValue = "#00FA9A", BackgroundColor = Color.MediumSpringGreen });
            temp.Add(new ColorModel() { EnglishName = "LightSkyBlue", ChineseName = "浅天蓝色", HexValue = "#87CEFA", BackgroundColor = Color.LightSkyBlue });
            temp.Add(new ColorModel() { EnglishName = "LightSlateGray", ChineseName = "浅青灰色", HexValue = "#778899", BackgroundColor = Color.LightSlateGray });
            temp.Add(new ColorModel() { EnglishName = "LightSteelBlue", ChineseName = "浅铁青色", HexValue = "#B0C4DE", BackgroundColor = Color.LightSteelBlue });
            temp.Add(new ColorModel() { EnglishName = "LightYellow", ChineseName = "浅黄色", HexValue = "#FFFFE0", BackgroundColor = Color.LightYellow });
            temp.Add(new ColorModel() { EnglishName = "Lime", ChineseName = "酸橙色", HexValue = "#00FF00", BackgroundColor = Color.Lime });
            temp.Add(new ColorModel() { EnglishName = "LimeGreen", ChineseName = "暗绿色", HexValue = "#32CD32", BackgroundColor = Color.LimeGreen });
            temp.Add(new ColorModel() { EnglishName = "MediumTurquoise", ChineseName = "中宝石绿", HexValue = "#48D1CC", BackgroundColor = Color.MediumTurquoise });
            temp.Add(new ColorModel() { EnglishName = "Linen", ChineseName = "亚麻色", HexValue = "#FAF0E6", BackgroundColor = Color.Linen });
            temp.Add(new ColorModel() { EnglishName = "Maroon", ChineseName = "栗色", HexValue = "#800000", BackgroundColor = Color.Maroon });
            temp.Add(new ColorModel() { EnglishName = "MediumAquamarine", ChineseName = "中绿色", HexValue = "#66CDAA", BackgroundColor = Color.MediumAquamarine });
            temp.Add(new ColorModel() { EnglishName = "MediumBlue", ChineseName = "中蓝色", HexValue = "#0000CD", BackgroundColor = Color.MediumBlue });
            temp.Add(new ColorModel() { EnglishName = "MediumOrchid", ChineseName = "淡兰花紫", HexValue = "#BA55D3", BackgroundColor = Color.MediumOrchid });
            temp.Add(new ColorModel() { EnglishName = "MediumPurple", ChineseName = "中紫色", HexValue = "#9370DB", BackgroundColor = Color.MediumPurple });
            temp.Add(new ColorModel() { EnglishName = "MediumSeaGreen", ChineseName = "中海绿色", HexValue = "#3CB371", BackgroundColor = Color.MediumSeaGreen });
            temp.Add(new ColorModel() { EnglishName = "Magenta", ChineseName = "洋红色", HexValue = "#FF00FF", BackgroundColor = Color.Magenta });
            temp.Add(new ColorModel() { EnglishName = "LightSeaGreen", ChineseName = "浅海绿色", HexValue = "#20B2AA", BackgroundColor = Color.LightSeaGreen });
            temp.Add(new ColorModel() { EnglishName = "PapayaWhip", ChineseName = "粉木瓜橙", HexValue = "#FFEFD5", BackgroundColor = Color.PapayaWhip });
            temp.Add(new ColorModel() { EnglishName = "Peru", ChineseName = "秘鲁色", HexValue = "#CD853F", BackgroundColor = Color.Peru });
            temp.Add(new ColorModel() { EnglishName = "SpringGreen", ChineseName = "嫩绿色", HexValue = "#00FF7F", BackgroundColor = Color.SpringGreen });
            temp.Add(new ColorModel() { EnglishName = "SteelBlue", ChineseName = "铁青色", HexValue = "#4682B4", BackgroundColor = Color.SteelBlue });
            temp.Add(new ColorModel() { EnglishName = "Tan", ChineseName = "棕褐色", HexValue = "#D2B48C", BackgroundColor = Color.Tan });
            temp.Add(new ColorModel() { EnglishName = "Teal", ChineseName = "青色", HexValue = "#008080", BackgroundColor = Color.Teal });
            temp.Add(new ColorModel() { EnglishName = "Thistle", ChineseName = "蓟色", HexValue = "#D8BFD8", BackgroundColor = Color.Thistle });
            temp.Add(new ColorModel() { EnglishName = "Tomato", ChineseName = "番茄色", HexValue = "#FF6347", BackgroundColor = Color.Tomato });
            temp.Add(new ColorModel() { EnglishName = "Snow", ChineseName = "雪白色", HexValue = "#FFFAFA", BackgroundColor = Color.Snow });
            temp.Add(new ColorModel() { EnglishName = "Transparent", ChineseName = "", HexValue = "#FFFF00", BackgroundColor = Color.Transparent });
            temp.Add(new ColorModel() { EnglishName = "Violet", ChineseName = "紫罗兰色", HexValue = "#EE82EE", BackgroundColor = Color.Violet });
            temp.Add(new ColorModel() { EnglishName = "Wheat", ChineseName = "小麦色", HexValue = "#F5DEB3", BackgroundColor = Color.Wheat });
            temp.Add(new ColorModel() { EnglishName = "White", ChineseName = "白色", HexValue = "#FFFFFF", BackgroundColor = Color.White });
            temp.Add(new ColorModel() { EnglishName = "WhiteSmoke", ChineseName = "烟白色", HexValue = "#F5F5F5", BackgroundColor = Color.WhiteSmoke });
            temp.Add(new ColorModel() { EnglishName = "Yellow", ChineseName = "黄色", HexValue = "#FFFF00", BackgroundColor = Color.Yellow });
            temp.Add(new ColorModel() { EnglishName = "YellowGreen", ChineseName = "黄绿色", HexValue = "#9ACD32", BackgroundColor = Color.YellowGreen });
            temp.Add(new ColorModel() { EnglishName = "Turquoise", ChineseName = "青绿色", HexValue = "#40E0D0", BackgroundColor = Color.Turquoise });
            temp.Add(new ColorModel() { EnglishName = "PeachPuff", ChineseName = "桃红色", HexValue = "#FFDAB9", BackgroundColor = Color.PeachPuff });
            temp.Add(new ColorModel() { EnglishName = "SlateGray", ChineseName = "石板灰", HexValue = "#708090", BackgroundColor = Color.SlateGray });
            temp.Add(new ColorModel() { EnglishName = "SkyBlue", ChineseName = "天蓝色", HexValue = "#87CEEB", BackgroundColor = Color.SkyBlue });
            temp.Add(new ColorModel() { EnglishName = "Pink", ChineseName = "粉红色", HexValue = "#FF66FF", BackgroundColor = Color.Pink });
            temp.Add(new ColorModel() { EnglishName = "Plum", ChineseName = "深紫色", HexValue = "#DDA0DD", BackgroundColor = Color.Plum });
            temp.Add(new ColorModel() { EnglishName = "PowderBlue", ChineseName = "粉蓝色", HexValue = "#B0E0E6", BackgroundColor = Color.PowderBlue });
            temp.Add(new ColorModel() { EnglishName = "Purple", ChineseName = "紫色", HexValue = "#800080", BackgroundColor = Color.Purple });
            temp.Add(new ColorModel() { EnglishName = "Red", ChineseName = "红色", HexValue = "#FF0000", BackgroundColor = Color.Red });
            temp.Add(new ColorModel() { EnglishName = "RosyBrown", ChineseName = "玫瑰棕色", HexValue = "#BC8F8F", BackgroundColor = Color.RosyBrown });
            temp.Add(new ColorModel() { EnglishName = "SlateBlue", ChineseName = "青蓝色", HexValue = "#6A5ACD", BackgroundColor = Color.SlateBlue });
            temp.Add(new ColorModel() { EnglishName = "RoyalBlue", ChineseName = "宝蓝色", HexValue = "#4169E1", BackgroundColor = Color.RoyalBlue });
            temp.Add(new ColorModel() { EnglishName = "Salmon", ChineseName = "浅橙色", HexValue = "#FA8072", BackgroundColor = Color.Salmon });
            temp.Add(new ColorModel() { EnglishName = "SandyBrown", ChineseName = "沙褐色", HexValue = "#F4A460", BackgroundColor = Color.SandyBrown });
            temp.Add(new ColorModel() { EnglishName = "SeaGreen", ChineseName = "海绿色", HexValue = "#2E8B57", BackgroundColor = Color.SeaGreen });
            temp.Add(new ColorModel() { EnglishName = "SeaShell", ChineseName = "贝壳色", HexValue = "#FFF5EE", BackgroundColor = Color.SeaShell });
            temp.Add(new ColorModel() { EnglishName = "Sienna", ChineseName = "赭色", HexValue = "#A0522D", BackgroundColor = Color.Sienna });
            temp.Add(new ColorModel() { EnglishName = "Silver", ChineseName = "银色", HexValue = "#C0C0C0", BackgroundColor = Color.Silver });
            temp.Add(new ColorModel() { EnglishName = "SaddleBrown", ChineseName = "重褐色", HexValue = "#8B4513", BackgroundColor = Color.SaddleBrown });
            temp.Add(new ColorModel() { EnglishName = "LightSalmon", ChineseName = "浅橙红色", HexValue = "#FFA07A", BackgroundColor = Color.LightSalmon });
            temp.Add(new ColorModel() { EnglishName = "MediumSlateBlue", ChineseName = "中灰蓝色", HexValue = "#7B68EE", BackgroundColor = Color.MediumSlateBlue });
            temp.Add(new ColorModel() { EnglishName = "LightGreen", ChineseName = "浅绿色", HexValue = "#90EE90", BackgroundColor = Color.LightGreen });
            temp.Add(new ColorModel() { EnglishName = "LightPink", ChineseName = "浅粉色", HexValue = "#FFB6C1", BackgroundColor = Color.LightPink });
            temp.Add(new ColorModel() { EnglishName = "Cyan", ChineseName = "蓝绿色", HexValue = "#00FFFF", BackgroundColor = Color.Cyan });
            temp.Add(new ColorModel() { EnglishName = "DarkBlue", ChineseName = "深蓝色", HexValue = "#00008B", BackgroundColor = Color.DarkBlue });
            temp.Add(new ColorModel() { EnglishName = "DarkCyan", ChineseName = "深青色", HexValue = "#008B8B", BackgroundColor = Color.DarkCyan });
            temp.Add(new ColorModel() { EnglishName = "DarkGoldenrod", ChineseName = "暗黄色", HexValue = "#B8860B", BackgroundColor = Color.DarkGoldenrod });
            temp.Add(new ColorModel() { EnglishName = "DarkGray", ChineseName = "深灰色", HexValue = "#A9A9A9", BackgroundColor = Color.DarkGray });
            temp.Add(new ColorModel() { EnglishName = "Cornsilk", ChineseName = "玉米黄", HexValue = "#FFF8DC", BackgroundColor = Color.Cornsilk });
            temp.Add(new ColorModel() { EnglishName = "DarkGreen", ChineseName = "深绿色", HexValue = "#006400", BackgroundColor = Color.DarkGreen });
            temp.Add(new ColorModel() { EnglishName = "DarkMagenta", ChineseName = "深洋红色", HexValue = "#8B008B", BackgroundColor = Color.DarkMagenta });
            temp.Add(new ColorModel() { EnglishName = "DarkOliveGreen", ChineseName = "深橄榄绿", HexValue = "#556B2F", BackgroundColor = Color.DarkOliveGreen });
            temp.Add(new ColorModel() { EnglishName = "DarkOrange", ChineseName = "深橘色", HexValue = "#FF8C00", BackgroundColor = Color.DarkOrange });
            temp.Add(new ColorModel() { EnglishName = "DarkOrchid", ChineseName = "暗紫色", HexValue = "#9932CC", BackgroundColor = Color.DarkOrchid });
            temp.Add(new ColorModel() { EnglishName = "DarkRed", ChineseName = "深红色", HexValue = "#8B0000", BackgroundColor = Color.DarkRed });
            temp.Add(new ColorModel() { EnglishName = "DarkSalmon", ChineseName = "深橙红", HexValue = "#E9967A", BackgroundColor = Color.DarkSalmon });
            temp.Add(new ColorModel() { EnglishName = "DarkKhaki", ChineseName = "深黄褐色", HexValue = "#BDB76B", BackgroundColor = Color.DarkKhaki });
            temp.Add(new ColorModel() { EnglishName = "DarkSeaGreen", ChineseName = "深海洋绿", HexValue = "#8FBC8F", BackgroundColor = Color.DarkSeaGreen });
            temp.Add(new ColorModel() { EnglishName = "CornflowerBlue", ChineseName = "菊蓝色", HexValue = "#6495ED", BackgroundColor = Color.CornflowerBlue });
            temp.Add(new ColorModel() { EnglishName = "Chocolate", ChineseName = "巧克力色", HexValue = "#D2691E", BackgroundColor = Color.Chocolate });
            temp.Add(new ColorModel() { EnglishName = "AntiqueWhite", ChineseName = "古董白", HexValue = "#FAEBD7", BackgroundColor = Color.AntiqueWhite });
            temp.Add(new ColorModel() { EnglishName = "Aqua", ChineseName = "水绿色", HexValue = "#00FFFF", BackgroundColor = Color.Aqua });
            temp.Add(new ColorModel() { EnglishName = "Aquamarine", ChineseName = "碧绿色", HexValue = "#7FFFD4", BackgroundColor = Color.Aquamarine });
            temp.Add(new ColorModel() { EnglishName = "Azure", ChineseName = "天蓝色", HexValue = "#F0FFFF", BackgroundColor = Color.Azure });
            temp.Add(new ColorModel() { EnglishName = "Beige", ChineseName = "米黄色", HexValue = "#F5F5DC", BackgroundColor = Color.Beige });
            temp.Add(new ColorModel() { EnglishName = "Bisque", ChineseName = "乳黄色", HexValue = "#FFE4C4", BackgroundColor = Color.Bisque });
            temp.Add(new ColorModel() { EnglishName = "Coral", ChineseName = "珊瑚红", HexValue = "#FF7F50", BackgroundColor = Color.Coral });
            temp.Add(new ColorModel() { EnglishName = "Black", ChineseName = "黑色", HexValue = "#000000", BackgroundColor = Color.Black });
            temp.Add(new ColorModel() { EnglishName = "Blue", ChineseName = "蓝色", HexValue = "#0000FF", BackgroundColor = Color.Blue });
            temp.Add(new ColorModel() { EnglishName = "BlueViolet", ChineseName = "蓝紫色", HexValue = "#8A2BE2", BackgroundColor = Color.BlueViolet });
            temp.Add(new ColorModel() { EnglishName = "Brown", ChineseName = "棕色", HexValue = "#A52A2A", BackgroundColor = Color.Brown });
            temp.Add(new ColorModel() { EnglishName = "BurlyWood", ChineseName = "实木色", HexValue = "#DEB887", BackgroundColor = Color.BurlyWood });
            temp.Add(new ColorModel() { EnglishName = "CadetBlue", ChineseName = "藏青色", HexValue = "#5F9EA0", BackgroundColor = Color.CadetBlue });
            temp.Add(new ColorModel() { EnglishName = "Chartreuse", ChineseName = "浅黄绿色", HexValue = "#7FFF00", BackgroundColor = Color.Chartreuse });
            temp.Add(new ColorModel() { EnglishName = "BlanchedAlmond", ChineseName = "白杏色", HexValue = "#FFEBCD", BackgroundColor = Color.BlanchedAlmond });
            temp.Add(new ColorModel() { EnglishName = "DarkSlateBlue", ChineseName = "深灰蓝色", HexValue = "#483D8B", BackgroundColor = Color.DarkSlateBlue });
            temp.Add(new ColorModel() { EnglishName = "Crimson", ChineseName = "暗红色", HexValue = "#DC143C", BackgroundColor = Color.Crimson });
            temp.Add(new ColorModel() { EnglishName = "DarkTurquoise", ChineseName = "暗宝石绿", HexValue = "#00CED1", BackgroundColor = Color.DarkTurquoise });
            temp.Add(new ColorModel() { EnglishName = "HotPink", ChineseName = "亮粉色", HexValue = "#FF69B4", BackgroundColor = Color.HotPink });
            temp.Add(new ColorModel() { EnglishName = "IndianRed", ChineseName = "印度红", HexValue = "#CD5C5C", BackgroundColor = Color.IndianRed });
            temp.Add(new ColorModel() { EnglishName = "Indigo", ChineseName = "靛蓝色", HexValue = "#4B0082", BackgroundColor = Color.Indigo });
            temp.Add(new ColorModel() { EnglishName = "Ivory", ChineseName = "象牙色", HexValue = "#FFFFF0", BackgroundColor = Color.Ivory });
            temp.Add(new ColorModel() { EnglishName = "Khaki", ChineseName = "卡其色", HexValue = "#F0E68C", BackgroundColor = Color.Khaki });
            temp.Add(new ColorModel() { EnglishName = "Lavender", ChineseName = "淡紫色", HexValue = "#E6E6FA", BackgroundColor = Color.Lavender });
            temp.Add(new ColorModel() { EnglishName = "Honeydew", ChineseName = "蜜色", HexValue = "#F0FFF0", BackgroundColor = Color.Honeydew });
            temp.Add(new ColorModel() { EnglishName = "LavenderBlush", ChineseName = "淡紫红", HexValue = "#FFF0F5", BackgroundColor = Color.LavenderBlush });
            temp.Add(new ColorModel() { EnglishName = "LemonChiffon", ChineseName = "柠檬纱色", HexValue = "#FFFACD", BackgroundColor = Color.LemonChiffon });
            temp.Add(new ColorModel() { EnglishName = "LightBlue", ChineseName = "浅蓝色", HexValue = "#ADD8E6", BackgroundColor = Color.LightBlue });
            temp.Add(new ColorModel() { EnglishName = "LightCoral", ChineseName = "浅珊瑚色", HexValue = "#F08080", BackgroundColor = Color.LightCoral });
            temp.Add(new ColorModel() { EnglishName = "LightCyan", ChineseName = "淡青色", HexValue = "#E0FFFF", BackgroundColor = Color.LightCyan });
            temp.Add(new ColorModel() { EnglishName = "LightGoldenrodYellow", ChineseName = "浅金黄色", HexValue = "#FAFAD2", BackgroundColor = Color.LightGoldenrodYellow });
            temp.Add(new ColorModel() { EnglishName = "DarkSlateGray", ChineseName = "墨绿色", HexValue = "#2F4F4F", BackgroundColor = Color.DarkSlateGray });
            temp.Add(new ColorModel() { EnglishName = "LawnGreen", ChineseName = "草绿色", HexValue = "#7CFC00", BackgroundColor = Color.LawnGreen });
            temp.Add(new ColorModel() { EnglishName = "GreenYellow", ChineseName = "黄绿色", HexValue = "#ADFF2F", BackgroundColor = Color.GreenYellow });
            temp.Add(new ColorModel() { EnglishName = "LightGray", ChineseName = "浅灰色", HexValue = "#D3D3D3", BackgroundColor = Color.LightGray });
            temp.Add(new ColorModel() { EnglishName = "Gray", ChineseName = "灰色", HexValue = "#808080", BackgroundColor = Color.Gray });
            temp.Add(new ColorModel() { EnglishName = "Green", ChineseName = "绿色", HexValue = "#008000", BackgroundColor = Color.Green });
            temp.Add(new ColorModel() { EnglishName = "DarkViolet", ChineseName = "深紫红色", HexValue = "#9400D3", BackgroundColor = Color.DarkViolet });
            temp.Add(new ColorModel() { EnglishName = "DeepPink", ChineseName = "深粉红色", HexValue = "#FF1493", BackgroundColor = Color.DeepPink });
            temp.Add(new ColorModel() { EnglishName = "DimGray", ChineseName = "暗灰色", HexValue = "#696969", BackgroundColor = Color.DimGray });
            temp.Add(new ColorModel() { EnglishName = "DodgerBlue", ChineseName = "闪蓝色", HexValue = "#1E90FF", BackgroundColor = Color.DodgerBlue });
            temp.Add(new ColorModel() { EnglishName = "Firebrick", ChineseName = "砖红色", HexValue = "#B22222", BackgroundColor = Color.Firebrick });
            temp.Add(new ColorModel() { EnglishName = "FloralWhite", ChineseName = "花白色", HexValue = "#FFFAF0", BackgroundColor = Color.FloralWhite });
            temp.Add(new ColorModel() { EnglishName = "DeepSkyBlue", ChineseName = "深天蓝色", HexValue = "#00BFFF", BackgroundColor = Color.DeepSkyBlue });
            temp.Add(new ColorModel() { EnglishName = "Fuchsia", ChineseName = "紫红色", HexValue = "#FF00FF", BackgroundColor = Color.Fuchsia });
            temp.Add(new ColorModel() { EnglishName = "Gainsboro", ChineseName = "亮灰色", HexValue = "#DCDCDC", BackgroundColor = Color.Gainsboro });
            temp.Add(new ColorModel() { EnglishName = "GhostWhite", ChineseName = "苍白色", HexValue = "#F8F8FF", BackgroundColor = Color.GhostWhite });
            temp.Add(new ColorModel() { EnglishName = "Gold", ChineseName = "金色", HexValue = "#FFD700", BackgroundColor = Color.Gold });
            temp.Add(new ColorModel() { EnglishName = "Goldenrod", ChineseName = "金黄色", HexValue = "#DAA520", BackgroundColor = Color.Goldenrod });
            temp.Add(new ColorModel() { EnglishName = "ForestGreen", ChineseName = "葱绿色", HexValue = "#228B22", BackgroundColor = Color.ForestGreen });


            MyList = temp;
        }

        private List<ColorModel> _MyList;

        public List<ColorModel> MyList
        {
            get { return _MyList; }
            set
            {
                _MyList = value;
                this.OnPropertyChanged();
            }
        }
    }

    public class ColorModel
    {
        public string ChineseName { get; set; }

        public string EnglishName { get; set; }

        public string HexValue { get; set; }

        public Color BackgroundColor { get; set; }
    }
}