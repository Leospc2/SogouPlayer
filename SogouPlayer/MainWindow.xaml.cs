using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForm = System.Windows.Forms;
using SogouPlayer.Utilities;
using System.Threading;
using System.IO;
using System.Reflection;

namespace SogouPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Field
        private WinForm.NotifyIcon notifyIcon = null;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            InitialTray();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }
        #endregion

        //#region Dependency Property
        ///// <summary>
        ///// 当前播放歌曲标题
        ///// </summary>
        //public string CurrentPlayingMusicTitle
        //{
        //    get { return (string)GetValue(CurrentPlayingMusicTitleProperty); }
        //    set { SetValue(CurrentPlayingMusicTitleProperty, value); }
        //}

        //public static DependencyProperty CurrentPlayingMusicTitleProperty = DependencyProperty.Register("CurrentPlayingMusicTitle",
        //    typeof(string),
        //    typeof(MainWindow),
        //    new PropertyMetadata("N/A", new PropertyChangedCallback((DependencyObject obj, DependencyPropertyChangedEventArgs args) =>
        //    {
        //        if (args != null && obj is MainWindow)
        //        {
        //            MainWindow temp = obj as MainWindow;
        //            temp.currentPlayingMusicTitle.Text = args.NewValue.ToString();
        //        }
        //    })));
        //#endregion

        #region Event
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadRankingList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //wpfMediaPlayer.URL = @"http://infinitinb.net/COFFdD0xMzU5NDQzMDI2Jmk9MjA3LjQ2LjU1LjI5JnU9U29uZ3MvdjEvZmFpbnRRQy9hZS9lNjc3M2YxNmUyMzliNWQ3MTUyMDU1Y2QxZGQ4YmNhZS5tcDMmbT1iYmVlNjZhY2ZhZWZjYjMxZWRhODUzNDc2OWMyZDM0ZSZ2PWxpc3RlbiZuPb/xz+vH+l9saXZlJnM9z/S+tMzaJnA9cw==.mp3";
            //wpfMediaPlayer.enableContextMenu = false;
        }

        private void listBoxRinkingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RankItemEntity selectItem = listBoxRinkingList.SelectedItem as RankItemEntity;
                if (selectItem == null)
                    return;

                string url = selectItem.Link + "&jsoncallback=jsonp1359365745585";

                new Thread(new ThreadStart(() =>
                {
                    MusicListEntity musicList = SogouUtility.GetMusicList(url);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        listBoxMusicList.ItemsSource = musicList.List;
                    }), null);
                })).Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void buttonMaximize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void smallPlayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button imagePlay = (sender as Button);
                string encodedTitleAndSinger = imagePlay.Tag.ToString();
                //this.CurrentPlayingMusicTitle = (imagePlay.Content as Image).Tag.ToString();
                MusicPlay(encodedTitleAndSinger);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Helper
        private void LoadRankingList()
        {
            string url = @"http://player.mbox.sogou.com/data/menu.jsp?id=0&jsoncallback=jsonp1359361412816";
            new Thread(new ThreadStart(() =>
            {
                RankingListEntity rankingList = SogouUtility.GetRankingList(url);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    listBoxRinkingList.ItemsSource = rankingList.List;
                    listBoxRinkingList.SelectedIndex = 0;
                }), null);
            })).Start();
        }

        private void InitialTray()
        {
            ////隐藏主窗体
            //this.Visibility = Visibility.Hidden;

            //设置托盘的各个属性
            notifyIcon = new WinForm.NotifyIcon();
            notifyIcon.BalloonTipText = "systray runnning...";
            notifyIcon.Text = "systray";
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            Stream notifyIconStream = GetEmbeddedFile("NotifyIcon.ico");
            if (notifyIconStream != null)
            {
                notifyIcon.Icon = new System.Drawing.Icon(notifyIconStream);
            }

            ////设置菜单项
            //WinForm.MenuItem setting1 = new WinForm.MenuItem("setting1");
            //WinForm.MenuItem setting2 = new WinForm.MenuItem("setting2");
            //WinForm.MenuItem setting = new WinForm.MenuItem("setting", new MenuItem[] { setting1, setting2 });

            ////帮助选项
            //WinForm.MenuItem help = new WinForm.MenuItem("help");

            ////关于选项
            //WinForm.MenuItem about = new WinForm.MenuItem("about");

            //退出菜单项
            WinForm.MenuItem exitMenu = new WinForm.MenuItem("Exit");
            exitMenu.Click += new EventHandler(exitMenu_Click);

            //关联托盘控件
            WinForm.MenuItem[] childen = new WinForm.MenuItem[] { exitMenu };
            notifyIcon.ContextMenu = new WinForm.ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(SysTray_StateChanged);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //如果鼠标左键单击
            if (e.Button == WinForm.MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }

        /// <summary>
        /// 窗体状态改变时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// 退出选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitMenu_Click(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure to exit?",
                                               "Cherry Player - By Leo",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private Stream GetEmbeddedFile(string fileName)
        {
            string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Stream stream = null;
            string currentFile = files.First<string>(f => f.ToLower().Contains(fileName.ToLower()));
            if (!string.IsNullOrEmpty(currentFile))
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(currentFile);
            }
            return stream;
        }

        private void MusicPlay(string encodedTitleAndSinger)
        {
            string url = @"http://mp3.sogou.com/api/links2?pf=mp3&id=1&jsoncallback=jsonp1359445062601&query=" + encodedTitleAndSinger;
            new Thread(new ThreadStart(() =>
            {
                MusicItemEntity music = SogouUtility.GetMusic(url);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    List<MusicItemInfoEntity> musicURLs = music.List;
                    //wpfMediaPlayer.URL = musicURLs.First().Url;
                }), null);
            })).Start();
        }
        #endregion

    }
}
