using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;

namespace WpfDemo.ViewModels
{
    public class MsgViewModel:BindableBase,IDialogHostAware
    {
        public MsgViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }


        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogName))
                 DialogHost.Close(DialogName, new DialogResult(ButtonResult.No));
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogName))
            {
                DialogParameters parame = new DialogParameters();
                DialogHost.Close(DialogName, new DialogResult(ButtonResult.OK, parame));
            }

        }

        public string DialogName { get; set; } = "Root";
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
                this.Title = parameters.GetValue<string>("Title");
            if (parameters.ContainsKey("Content"))
                this.Content = parameters.GetValue<string>("Content").ToString();
        }
    }
}
