//using System;

//[assembly: Xamarin.Forms.ExportRenderer(typeof(Client.Components.SearchBarFix), typeof(Client.Droid.Component.SearchBarFixRenderer))]
//namespace Client.Droid.Component
//{
//    /// <summary>
//    /// 未能实现预期效果 ( 对 Search 按钮的监控, 然后执行 SearchCommand.Execute() )
//    /// </summary>
//    [Obsolete]
//    public class SearchBarFixRenderer : Xamarin.Forms.Platform.Android.SearchBarRenderer
//    {

//        public SearchBarFixRenderer(Android.Content.Context context) : base(context)
//        {

//        }

//        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.SearchBar> e)
//        {
//            base.OnElementChanged(e);
//            if (e.OldElement == null)
//            {
//                // this.Control.SetOnKeyListener(new MyListener.SearchView_OnKeyListener());
//                //this.Control.KeyPress += Control_KeyPress;
//                //this.Control.QueryTextSubmit += (sender, args) =>
//                //{
//                //    if (string.IsNullOrEmpty(args.Query) && Element.SearchCommand != null)
//                //    {
//                //        Element.SearchCommand.Execute(null);
//                //    }        
//                //};
//            }
//        }
//    }
//}