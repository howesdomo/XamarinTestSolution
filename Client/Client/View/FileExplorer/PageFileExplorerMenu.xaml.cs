﻿using System;
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

        #region 弃用

        //private void initUI()
        //{
        //    var a = getButton("Environment.SpecialFolder.MyDocuments");
        //    a.Clicked += async (s, e) =>
        //    {
        //        try
        //        {
        //            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }
        //    };
        //    sl.Children.Add(a);

        //    // ***** Next
        //    a = getButton("Environment.SpecialFolder.CommonDocuments");
        //    a.Clicked += async (s, e) =>
        //    {
        //        try
        //        {
        //            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)));
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }
        //    };
        //    sl.Children.Add(a);

        //    // ***** Next
        //    a = getButton("Environment.SpecialFolder.Personal");
        //    a.Clicked += async (s, e) =>
        //    {
        //        try
        //        {
        //            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.GetFolderPath(Environment.SpecialFolder.Personal)));
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }
        //    };
        //    sl.Children.Add(a);
        //}
        //private void initUI()
        //{
        //    var a = getButton("Environment.SpecialFolder.MyDocuments");
        //    a.Clicked += async (s, e) =>
        //    {
        //        try
        //        {
        //            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }
        //    };
        //    sl.Children.Add(a);

        //    // ***** Next
        //    a = getButton("Environment.SpecialFolder.CommonDocuments");
        //    a.Clicked += async (s, e) =>
        //    {
        //        try
        //        {
        //            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)));
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }
        //    };
        //    sl.Children.Add(a);

        //    // ***** Next
        //    a = getButton("Environment.SpecialFolder.Personal");
        //    a.Clicked += async (s, e) =>
        //    {
        //        try
        //        {
        //            await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.GetFolderPath(Environment.SpecialFolder.Personal)));
        //        }
        //        catch (Exception ex)
        //        {
        //            string msg = "{0}".FormatWith(ex.GetFullInfo());
        //            System.Diagnostics.Debug.WriteLine(msg);
        //        }
        //    };
        //    sl.Children.Add(a);
        //}

        #endregion

        private void initUIAdv()
        {
            var btn = getButton("CurrentDirectory");
            btn.Clicked += async (s, e) =>
            {
                try
                {
                    await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(Environment.CurrentDirectory));
                }
                catch (Exception ex)
                {
                    string msg = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            };
            sl.Children.Add(btn);


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
                        await Navigation.PushAsync(new View.FileExplorer.PageFileExplorer(dirPath));
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