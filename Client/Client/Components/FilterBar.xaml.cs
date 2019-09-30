using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Components
{
    /// <summary>
    /// V 1.0.0 - 2019-9-29 16:31:22
    /// Xamarin.Forms 原生 SearchBar有一个缺点输入框没有内容时不会触发 SearchCommand, 
    /// 故自制的控件 FilterBar 修复输入框没有内容时不会触发 SearchCommand 的问题
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterBar : ContentView
    {
        #region 依赖属性

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create
        (
            // 必填 3 项
            propertyName: "SearchCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(FilterBar),

            // 选填
            defaultValue: null
        );

        public static readonly BindableProperty IsTextChangeExecuteProperty = BindableProperty.Create
        (
            // 必填 3 项
            propertyName: "IsTextChangeExecute",
            returnType: typeof(bool),
            declaringType: typeof(FilterBar),

            // 选填
            defaultValue: false
        );

        #endregion

        public FilterBar()
        {
            InitializeComponent();
            initUI();
            initEvent();
        }

        private void initUI()
        {
            // TODO 存放资源文件到持久化设备
            imgFilter.Source = ImageSource.FromStream(() =>
            {
                MemoryStream s = new MemoryStream();
                string base64Str = "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";
                byte[] bArr = Convert.FromBase64String(base64Str);
                s.Write(bArr, 0, bArr.Length);
                s.Seek(0, SeekOrigin.Begin);
                return s;
            });


            imgDel.Source = ImageSource.FromStream(() =>
            {
                MemoryStream s = new MemoryStream();
                string base64Str = "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";
                byte[] bArr = Convert.FromBase64String(base64Str);
                s.Write(bArr, 0, bArr.Length);
                s.Seek(0, SeekOrigin.Begin);
                return s;
            });
        }

        private void initEvent()
        {
            txtFilter.TextChanged += txtFilter_TextChanged;
            txtFilter.Completed += txtFilter_Completed;

            imgFilter.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() =>
                {
                    searchCommandExecute();
                    onSearch();
                })
            });

            imgDel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() =>
                {
                    txtFilter.Text = string.Empty;
                    this.imgDel.IsVisible = false;

                    if (IsTextChangeExecute == false)
                    {                       
                        searchCommandExecute(isEmptyQuery: true);
                        onSearch(isEmptyQuery: true);
                    }
                })
            });
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            if (this.imgDel.IsVisible == false && string.IsNullOrEmpty(text) == false)
            {
                this.imgDel.IsVisible = true;
            }

            if (this.imgDel.IsVisible == true && string.IsNullOrEmpty(text) == true)
            {
                this.imgDel.IsVisible = false;
            }

            if (IsTextChangeExecute == true)
            {
                searchCommandExecute();
                onSearch();
            }
        }

        private void txtFilter_Completed(object sender, EventArgs e)
        {
            if (IsTextChangeExecute == false)
            {
                searchCommandExecute();
                onSearch();
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return (ICommand)base.GetValue(SearchCommandProperty);
            }
            set
            {
                base.SetValue(SearchCommandProperty, value);
            }
        }

        public bool IsTextChangeExecute
        {
            get
            {
                return (bool)base.GetValue(IsTextChangeExecuteProperty);
            }
            set
            {
                base.SetValue(IsTextChangeExecuteProperty, value);
            }
        }

        private void searchCommandExecute(bool? isEmptyQuery = null)
        {
            if (this.SearchCommand == null)
            {
                //string msg = "SearchCommand is null";
                //System.Diagnostics.Debug.WriteLine(msg);

                return;
            }

            if (isEmptyQuery.HasValue == true && isEmptyQuery.Value == true)
            {
                this.SearchCommand.Execute(string.Empty);
            }
            else
            {
                this.SearchCommand.Execute(this.txtFilter.Text);
            }
        }

        public EventHandler<FilterBarEventArgs> Search;

        private void onSearch(bool? isEmptyQuery = null)
        {
            if (this.Search == null)
            {
                //string msg = "Search is null";
                //System.Diagnostics.Debug.WriteLine(msg);

                return;
            }

            if (isEmptyQuery.HasValue == true && isEmptyQuery.Value == true)
            {
                Search.Invoke(this, new FilterBarEventArgs(string.Empty));
            }
            else
            {
                Search.Invoke(this, new FilterBarEventArgs(this.txtFilter.Text));
            }
        }
    }

    public class FilterBarEventArgs : EventArgs
    {
        public string Query { get; private set; }

        public FilterBarEventArgs(string query)
        {
            this.Query = query;
        }
    }
}