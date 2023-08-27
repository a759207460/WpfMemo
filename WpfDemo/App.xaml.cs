using System;
using System.Windows;
using DryIoc;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using WpfDemo.Common;
using WpfDemo.Service;
using WpfDemo.Service.MenmoService;
using WpfDemo.Service.ToDoService;
using WpfDemo.Service.UserServices;
using WpfDemo.ViewModels;
using WpfDemo.ViewModels.Dialogs;
using WpfDemo.Views;
using WpfDemo.Views.Dialogs;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 设置默认首页
        /// </summary>
        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Application.Current.Shutdown();
                    return;
                }
                else
                {
                    var service = App.Current.MainWindow.DataContext as IConfigureService;
                    if (service != null)
                    {
                        service.Configure();
                    }
                    base.OnInitialized();
                }
            });
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().Register<HttpClientService>(made: Parameters.Of.Type<string>(serviceKey: "url"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5082/api/WpfApi/", serviceKey: "url");
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMenmoService, MenmoService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<AddToDoDialog, AddToDoDialogViewModel>();
            containerRegistry.RegisterForNavigation<AddMenmoDialog, AddMenmoDialogViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<MenoView, MenoViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<SystemSettingView, SystemSettingViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();
        }


        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();
            var dialog = containerProvider.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                else
                {
                    Current.MainWindow.Show();
                }
            });
        }
    }
}
