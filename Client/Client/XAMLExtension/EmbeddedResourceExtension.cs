using System;


/// <summary>
/// Xamarin.Forms.Xaml.IMarkupExtension
/// 在 Xaml 中, 直接调用嵌入资源, 不需要在 Code Behind 中用 ImageSource.FromResource(Source); 进行加载
/// 
/// 使用例子
/// <Image Source="{local:EmbeddedResource Client.Images.SQLiteDemo.check.png}" />
/// </summary>
namespace Client
{
    // 設定 ContentProperty 的話，Xaml 內就不用再輸入 Source Property
    [Xamarin.Forms.ContentProperty("Source")]
    public class EmbeddedResourceExtension : Xamarin.Forms.Xaml.IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            return Xamarin.Forms.ImageSource.FromResource(Source);
        }
    }
}
