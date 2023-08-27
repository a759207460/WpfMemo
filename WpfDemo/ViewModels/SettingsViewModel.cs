using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using WpfDemo.Extensions;
using WpfDemo.Models;

namespace WpfDemo.ViewModels
{
    public class SettingsViewModel:BindableBase
    {
        private IRegionManager _regionManager;
        public SettingsViewModel(IRegionManager regionManager) {
            this._regionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            NavigatCommand = new DelegateCommand<MenuBar>(Navigat);
            CreateMenuBar();
        }
        public DelegateCommand<MenuBar> NavigatCommand { get; private set; }

        private ObservableCollection<MenuBar> _menuBars;

        private IRegionNavigationJournal journal;

        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }


        public void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "个性化", Navigation = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", Navigation = "SystemSettingView" });
            MenuBars.Add(new MenuBar() { Icon = "Information", Title = "关于更多", Navigation = "AboutView" });
        }

        private void Navigat(MenuBar menuBar)
        {
            if (menuBar == null || string.IsNullOrWhiteSpace(menuBar.Navigation))
                return;
            _regionManager.Regions[PrismManage.SettingsViewRegionName].RequestNavigate(menuBar.Navigation, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
        }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { _menuBars = value; RaisePropertyChanged(); }
        }
    }
}
