using DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Extensions;

namespace WpfDemo.ViewModels
{
    public class NavigationViewModel:BindableBase,INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        private readonly IEventAggregator eventAggregator;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
            this.eventAggregator=containerProvider.Resolve<IEventAggregator>();
        }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public void UpdateLoading(bool isOpen)
        {
            eventAggregator.UpdateLoading(new Event.UpDateModel
            {
                IsOpen = isOpen
            }) ;
        }
    }
}
