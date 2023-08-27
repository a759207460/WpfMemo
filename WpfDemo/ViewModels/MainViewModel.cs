using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using WpfDemo.Common;
using WpfDemo.Extensions;
using WpfDemo.Models;

namespace WpfDemo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {

        private IRegionManager _regionManager;
        private readonly IContainerProvider containerProvider;
        public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            this.containerProvider = containerProvider;
            MenuBars = new ObservableCollection<MenuBar>();
            NavigatCommand = new DelegateCommand<MenuBar>(Navigat);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });
            //注销
            LoginOutCommand = new DelegateCommand(() =>
            {

                App.LoginOut(containerProvider);
            });
        }


        public DelegateCommand<MenuBar> NavigatCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        public DelegateCommand LoginOutCommand { get; private set; }

        private bool isLeftDrawerOpen;

        public bool IsLeftDrawerOpen
        {
            get { return isLeftDrawerOpen; }
            set { isLeftDrawerOpen = value; RaisePropertyChanged();}
        }




        private ObservableCollection<MenuBar> _menuBars;
        private IRegionNavigationJournal journal;
        private void Navigat(MenuBar menuBar)
        {
            if (menuBar == null || string.IsNullOrWhiteSpace(menuBar.Navigation))
                return;
            _regionManager.Regions[PrismManage.MainViewRegionName].RequestNavigate(menuBar.Navigation, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
            IsLeftDrawerOpen = false;
        }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { _menuBars = value; RaisePropertyChanged(); }
        }

       
        public void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", Navigation = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", Navigation = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", Navigation = "MenoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", Navigation = "SettingsView" });
        }

        public void Configure()
        {
            CreateMenuBar();
            _regionManager.Regions[PrismManage.MainViewRegionName].RequestNavigate("IndexView");
        }
    }
}
