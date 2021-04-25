using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.FileExplorer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFileExplorerMenu : ContentPage
    {
        public PageFileExplorerMenu()
        {
            InitializeComponent();            
            initUIAdv();
        }

        private void initUIAdv()
        {
            this.Title = "MyFileExplorer(待测试)";

            // iOS 特有
            if (Xamarin.Essentials.DeviceInfo.Platform == Xamarin.Essentials.DevicePlatform.iOS)
            {
                var btn = getButton("CurrentDirectory");
                btn.Clicked += async (s, e) =>
                {
                    try
                    {
                        await Navigation.PushModalAsync(new View.FileExplorer.PageFileExplorer(Environment.CurrentDirectory));
                    }
                    catch (Exception ex)
                    {
                        string msg = "{0}".FormatWith(ex.GetFullInfo());
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                };

                sl.Children.Add(btn);
            }

            // Android 特有
            if (Xamarin.Essentials.DeviceInfo.Platform == Xamarin.Essentials.DevicePlatform.Android)
            {
                var btnExternalStoragePath = getButton("安卓系统外部存储根目录(ExternalStoragePath)");
                btnExternalStoragePath.Clicked += async (s, e) =>
                {
                    try
                    {
                        await Navigation.PushModalAsync(new Util.XamariN.FileExplorer.MyFileExplorer(Common.StaticInfo.AndroidExternalPath));
                    }
                    catch (Exception ex)
                    {
                        string msg = "{0}".FormatWith(ex.GetFullInfo());
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                };

                sl.Children.Add(btnExternalStoragePath);

                var btnAndroidExternalPath = getButton("安卓程序外部存储目录");
                btnAndroidExternalPath.Clicked += async (s, e) =>
                {
                    try
                    {
                        var di = new System.IO.DirectoryInfo(Common.StaticInfo.AndroidExternalFilesPath);                        
                        await Navigation.PushModalAsync(new Util.XamariN.FileExplorer.MyFileExplorer(di.Parent.FullName));
                    }
                    catch (Exception ex)
                    {
                        string msg = "{0}".FormatWith(ex.GetFullInfo());
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                };

                sl.Children.Add(btnAndroidExternalPath);                
            }

            // 遍历 Environment.SpecialFolder
            foreach (Environment.SpecialFolder enumValue in Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                string dirPath = string.Empty;
                try
                {
                    dirPath = Environment.GetFolderPath(enumValue);
                    if (System.IO.Directory.Exists(dirPath) == false)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    continue;
                }

                string enumName = Enum.GetName(typeof(Environment.SpecialFolder), enumValue);
                var a = getButton(enumName);
                a.Clicked += async (s, e) =>
                {
                    try
                    {
                        await Navigation.PushModalAsync(new Util.XamariN.FileExplorer.MyFileExplorer(dirPath));
                    }
                    catch (Exception ex)
                    {
                        string msg = "{0}".FormatWith(ex.GetFullInfo());
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                };
                sl.Children.Add(a);
            }
        }

        private Button getButton(string a)
        {
            Button r = new Button();
            r.Text = a;
            r.BackgroundColor = Color.SkyBlue;
            return r;
        }
    }
}