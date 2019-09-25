using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.FileExplorer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFileExplorer : ContentPage
    {
        PageFileExplorer_ViewModel ViewModel { get; set; }

        public PageFileExplorer(string baseDirectory)
        {
            InitializeComponent();
            initEvent();
            this.ViewModel = new PageFileExplorer_ViewModel(baseDirectory);
            this.BindingContext = this.ViewModel;

            search(this.ViewModel.BaseDirectory);
        }

        private void initEvent()
        {
            var a = new TapGestureRecognizer();

            lv.GestureRecognizers.Add(new TapGestureRecognizer());

            lv.ItemTapped += Lv_ItemTapped;
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            search(this.ViewModel.BaseDirectory);
        }

        #region Search

        async void search(string path, int level = 1)
        {
            List<FileInfoModel> list = new List<FileInfoModel>();
            this.loopDirectoriesAndFiles(path, level, list);
            this.ViewModel.List = list;

            this.ViewModel.CurrentDirectory = path;

            drawStacklayoutCurrentDirectory(path, level);

            // 自动滚动到最后
            await scrollViewCurrentDirectory.ScrollToAsync(element: scrollViewCurrentDirectory, position: ScrollToPosition.End, animated: true);
        }

        /// <summary>
        /// 遍历文件夹
        /// </summary>
        /// <param name="dirPath">Dir path.</param>
        /// <param name="level">Level.</param>
        private void loopDirectoriesAndFiles(string dirPath, int level, IList<FileInfoModel> list)
        {
            List<Exception> dirExList = new List<Exception>();
            List<Exception> fileExList = new List<Exception>();

            try
            {
                #region 若不是根目录, 在第一位加上目录 .. (返回)

                if (dirPath.Equals(this.ViewModel.BaseDirectory, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dirPath);
                    FileInfoModel toAdd = new FileInfoModel()
                    {
                        IsDirectory = true,
                        IsDirectoryBack = true,

                        // 通用
                        Level = level - 1,

                        // 目录
                        DirectoryName = "..",
                        Directory = di.Parent.FullName,

                        // 文件
                        Name = string.Empty,
                        Extension = string.Empty,
                        FullName = string.Empty,

                        // 图标
                        ModelIcon = ImageSource.FromResource("Client.Images.FileExplorer.record.png")
                    };

                    list.Add(toAdd);
                }

                #endregion

                foreach (var item in System.IO.Directory.GetDirectories(dirPath).OrderBy(i => i))
                {
                    try
                    {
                        string msg = "|{0}{1}".FormatWith("".PadLeft(level, '_'), item);
                        System.Diagnostics.Debug.WriteLine(msg);
                        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(item);

                        FileInfoModel toAdd = new FileInfoModel()
                        {
                            IsDirectory = true,

                            // 通用
                            Level = level,
                            LastWriteTime = di.LastWriteTime,

                            // 目录
                            DirectoryName = di.Name,
                            Directory = di.FullName,
                            ContainFileCount = di.GetFiles().Length,

                            // 文件
                            Name = di.Name,
                            Extension = di.Extension,
                            FullName = di.FullName,

                            // 图标
                            ModelIcon = ImageSource.FromResource("Client.Images.FileExplorer.record.png")
                        };

                        list.Add(toAdd);
                    }
                    catch (Exception dirEx)
                    {
                        dirExList.Add(dirEx);
                    }
                }

                foreach (var item in System.IO.Directory.GetFiles(dirPath).OrderBy(i => i))
                {
                    try
                    {
                        string msg = "|{0}{1}".FormatWith("".PadLeft(level, '_'), item);
                        System.Diagnostics.Debug.WriteLine(msg);
                        System.IO.FileInfo di = new System.IO.FileInfo(item);

                        FileInfoModel toAdd = new FileInfoModel()
                        {
                            IsDirectory = false,

                            // 通用
                            Level = level,
                            LastWriteTime = di.LastWriteTime,

                            // 目录
                            DirectoryName = di.DirectoryName,
                            Directory = di.Directory.FullName,

                            // 文件
                            Name = di.Name,
                            Extension = di.Extension,
                            FullName = di.FullName,
                            FileLength = di.Length,
                            FileLengthInfo = Util.IO.FileUtils.GetFileLengthInfo(di.Length),

                            // 图标
                            // ModelIcon = ImageSource.FromResource("Client.Images.FileExplorer.file.svg")
                            ModelIcon = ImageSource.FromResource("Client.Images.FileExplorer.file.png")
                            // ModelIcon = ImageSource.FromFile()
                        };

                        list.Add(toAdd);
                    }
                    catch (Exception fileEx)
                    {
                        fileExList.Add(fileEx);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.GetFullInfo();
                System.Diagnostics.Debug.WriteLine(msg);
                DisplayAlert("Error", msg, "确认");
            }
            finally
            {
                string msg = string.Empty;
                if (dirExList.Count > 0)
                {
                    msg += $"遍历文件夹时捕获到读取文件夹信息错误共 {dirExList.Count} 个;";
                }

                if (fileExList.Count > 0)
                {
                    msg += $"遍历文件夹时捕获到读取文件错误共 {fileExList.Count} 个;";
                }

                if (msg.IsNullOrWhiteSpace() == false)
                {
                    System.Diagnostics.Debug.WriteLine(msg);
                    DisplayAlert("Error", msg, "确认");
                }
            }
        }

        #endregion

        #region Tappe Item

        private void Lv_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is FileInfoModel)
            {
                var m = e.Item as FileInfoModel;
                if (m.IsDirectory == true)
                {
                    if (m.IsDirectoryBack == false)
                    {
                        search(m.Directory, m.Level + 1);
                    }
                    else
                    {
                        search(m.Directory, m.Level);
                    }
                }
                else
                {
                    // TODO 长按 或 点击 弹出打开方式菜单
                }
            }
        }

        #endregion

        private void drawStacklayoutCurrentDirectory(string dirPath, int level = 1)
        {
            int index = dirPath.IndexOf(dirPath);
            if (index < 0)
            {
                throw new BusinessException("路径错误");
            }

            // 清空 stacklayoutCurrentDirectory
            stacklayoutCurrentDirectory.Children.Clear();

            // 重新绘制 stacklayoutCurrentDirectory
            drawSL_Recursive(new System.IO.DirectoryInfo(dirPath), level);
        }

        /// <summary>
        /// 递归绘制 stacklayoutCurrentDirectory
        /// </summary>
        /// <param name="di"></param>
        /// <param name="level"></param>
        private void drawSL_Recursive(System.IO.DirectoryInfo di, int level)
        {
            if (di.Parent.FullName == this.ViewModel.BaseDirectory)
            {
                stacklayoutCurrentDirectory.Children.Add(get_SL_Button(di.Parent, level));
                stacklayoutCurrentDirectory.Children.Add(get_SL_Label());

                stacklayoutCurrentDirectory.Children.Add(get_SL_Button(di, level));
                return;
            }
            else if (di.FullName == this.ViewModel.BaseDirectory)
            {
                stacklayoutCurrentDirectory.Children.Add(get_SL_Button(di, level));
                return;
            }
            else
            {
                drawSL_Recursive(di.Parent, level - 1);
                stacklayoutCurrentDirectory.Children.Add(get_SL_Label());
            }

            stacklayoutCurrentDirectory.Children.Add(get_SL_Button(di, level));
        }

        private Label get_SL_Label()
        {
            Label a = new Label();
            a.Text = ">";
            a.TextColor = Color.Silver;
            a.FontSize = 11;
            a.Margin = new Thickness(left: 0, top: 0, right: 0, bottom: 0);
            a.VerticalTextAlignment = TextAlignment.Center;
            return a;
        }

        private Button get_SL_Button(System.IO.DirectoryInfo di, int level)
        {
            Button a = new Button();

            // UI Info
            a.TextColor = Color.Gray;
            a.FontSize = 11;

            if (di.FullName == this.ViewModel.BaseDirectory)
            {
                a.Text = "BaseDirectory";
                // a.Text = di.FullName;
                a.Margin = new Thickness(left: 0, top: 0, right: 5, bottom: 0);
                // a.BackgroundColor = Color.FromRgb(250d, 171d, 173d);
                a.BackgroundColor = Color.MediumVioletRed;
            }
            else
            {
                a.Text = di.Name;
                a.Margin = new Thickness(left: 5, top: 0, right: 5, bottom: 0);
                // a.BackgroundColor = Color.FromRgb(217d, 217d, 217d);
                a.BackgroundColor = Color.Silver;
            }

            // Event
            a.Clicked += (s, e) => { search(di.FullName, level); };

            return a;
        }
    }

    public class PageFileExplorer_ViewModel : ViewModel.BaseViewModel
    {
        public PageFileExplorer_ViewModel(string baseDirectory)
        {
            this._BaseDirectory = baseDirectory;
        }

        private string _BaseDirectory;

        /// <summary>
        /// 最低层的路径
        /// </summary>
        public string BaseDirectory
        {
            get
            {
                return _BaseDirectory;
            }
        }


        private string _CurrentDirectory;

        /// <summary>
        /// 当前的目录路径
        /// </summary>
        public string CurrentDirectory
        {
            get { return _CurrentDirectory; }
            set
            {
                _CurrentDirectory = value;
                this.OnPropertyChanged("CurrentDirectory");
            }
        }

        private List<FileInfoModel> list;

        /// <summary>
        /// 当前目录包含的所有文件夹与文件
        /// </summary>
        public List<FileInfoModel> List
        {
            get { return list; }
            set
            {
                list = value;
                executeFilter();
            }
        }

        private string filter;

        /// <summary>
        /// 过滤信息
        /// </summary>
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                this.OnPropertyChanged("Filter");
                this.executeFilter();
            }
        }

        private void executeFilter()
        {
            if (this.List != null)
            {
                if (this.Filter.IsNullOrWhiteSpace() == true)
                {
                    this.FilterList = this.List.ToList();
                }
                else
                {
                    this.FilterList = this.List.Where(i => i.Info.IsMatch(this.Filter) == true).ToList();
                }
            }
        }

        private List<FileInfoModel> _FilterList;

        /// <summary>
        /// <para>当前目录包含的所有文件夹与文件 (已过滤信息)</para>
        /// <para>界面上绑定本集合</para>
        /// </summary>
        public List<FileInfoModel> FilterList
        {
            get { return _FilterList; }
            set
            {
                _FilterList = value;
                this.OnPropertyChanged("FilterList");
            }
        }
    }

    public class FileInfoModel : Util.UIComponent.VirtualModel
    {
        public FileInfoModel()
        {
            ShowAlertCommand = new Command
            (
                execute: () => 
                {
                    string msg = $"Execute";
                    System.Diagnostics.Debug.WriteLine(msg);

                    System.Diagnostics.Debugger.Break();
                }, 
                canExecute: () => 
                {
                    string msg = $"can Execute";
                    System.Diagnostics.Debug.WriteLine(msg);

                    System.Diagnostics.Debugger.Break();

                    return true;
                }
            );
        }

        public ImageSource ModelIcon { get; set; }

        public int Level { get; set; }

        public bool IsDirectory { get; set; }

        /// <summary>
        /// 返回上一层
        /// </summary>
        public bool IsDirectoryBack { get; set; }

        #region 目录

        public string Directory { get; set; }

        public string DirectoryName { get; set; }

        public int ContainFileCount { get; set; }

        #endregion

        #region 文件

        public string Name { get; set; }

        public string Extension { get; set; }

        public string FullName { get; set; }

        public long FileLength { get; set; }

        public string FileLengthInfo { get; set; }

        #endregion

        public DateTime CreationTime { get; set; }

        public DateTime LastWriteTime { get; set; }

        #region UI

        public string Info
        {
            get
            {
                if (IsDirectory == true)
                {
                    return this.DirectoryName;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public string Info2
        {
            get
            {
                if (IsDirectory == true)
                {
                    if (IsDirectoryBack == false)
                    {
                        return "{0} 项".FormatWith(this.ContainFileCount);
                    }
                    else
                    {
                        return "返回上一层";
                    }
                }
                else
                {
                    return this.FileLengthInfo;
                }
            }
        }

        public string LastWriteDateInfo
        {
            get
            {
                string r = string.Empty;
                if (IsDirectoryBack == false)
                {
                    r = this.LastWriteTime.ToString("yyyy-MM-dd");
                }
                return r;
            }
        }

        public string LastWriteTimeInfo
        {
            get
            {
                string r = string.Empty;
                if (IsDirectoryBack == false)
                {
                    r = this.LastWriteTime.ToString("HH:mm:ss.fff");
                }
                return r;
            }
        }




        #endregion

        public System.Windows.Input.ICommand ShowAlertCommand
        {
            get;
            private set;
        }
    }
}