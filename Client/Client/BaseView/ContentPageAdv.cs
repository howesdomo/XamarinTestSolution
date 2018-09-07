using System;

namespace Xamarin.Forms
{
    /// <summary>
    /// 可控制 Nav Back按钮的 ContentPage
    /// 
    /// V2
    /// 经测试 无需实现 ContentPageAdv ,
    /// 只需加载 V7版本 的 Widget.Toolbar, 即可实现软硬Back的监控
    /// 
    /// 待测试苹果的实际效果
    /// </summary>
    public class ContentPageAdv : ContentPage
    {
        /// <summary>
        /// Gets or Sets the Back button click overriden custom action
        /// </summary>
        public Action CustomBackButtonAction { get; set; }

        public static readonly BindableProperty EnableBackButtonOverrideProperty =
               BindableProperty.Create(
               nameof(EnableBackButtonOverride),
               typeof(bool),
               typeof(ContentPageAdv),
               false);

        /// <summary>
        /// Gets or Sets Custom Back button overriding state
        /// </summary>
        public bool EnableBackButtonOverride
        {
            get
            {
                return (bool)GetValue(EnableBackButtonOverrideProperty);
            }
            set
            {
                SetValue(EnableBackButtonOverrideProperty, value);
            }
        }
    }
}
