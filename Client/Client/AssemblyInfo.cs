using Xamarin.Forms.Xaml;
using Xamarin.Forms;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

// 自定义导入字体
// 字体
// [assembly: ExportFont("TODO", Alias = "TODO")]
// IconFont (图标字体)
[assembly: ExportFont("Font Awesome 5 Pro-Regular-400.otf", Alias = "FontAwesome")]
[assembly: ExportFont("Font Awesome 5 Brands-Regular-400.otf", Alias = "FontAwesome_Brands")] // 商标