using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDemo.Common;
using WpfDemo.Extensions;
using WpfDemo.ViewModels;

namespace WpfDemo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
  
        private readonly IDialogHostService dialogHostService;

       
        public MainView( IEventAggregator eventAggregator, IDialogHostService dialogHostService)
        {
            InitializeComponent();
            this.dialogHostService = dialogHostService;
            eventAggregator.Register(arg =>
            {
                MyDialogHost.IsOpen = arg.IsOpen;
                if (MyDialogHost.IsOpen)
                {
                    MyDialogHost.DialogContent = new ProgressView();
                }
            });



            //通知消息注册
            eventAggregator.RegisterMessge(arg =>
            {
                SnackBar.MessageQueue.Enqueue(arg.Message);
            });

            //选择事件
            //menuBar.SelectionChanged += (s, e) =>
            //{
            //    drawerHost.IsLeftDrawerOpen = false;
            //};

            #region 按钮点击事件

            this.btnMin.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            this.btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };
            this.btnClose.Click += async (s, e) =>
            {
                var dialogHost = await dialogHostService.Question("温馨提示！", $"请确认是否退出系统!");
                if (dialogHost == null || dialogHost.Result == Prism.Services.Dialogs.ButtonResult.No) return;
                this.Close();
            };

            this.colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            this.colorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };

            #endregion

        }
    }
}
