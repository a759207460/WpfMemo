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
using WpfDemo.DomainDto;

namespace WpfDemo.ViewModels.Dialogs
{
    public class AddMenmoDialogViewModel :BindableBase, IDialogHostAware
    {
        public AddMenmoDialogViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogName))
                DialogHost.Close(DialogName, ButtonResult.No);
        }

        private MemoDto model;

        public MemoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(this.Model.Title) || string.IsNullOrWhiteSpace(this.Model.Content))
                return;
            if (DialogHost.IsDialogOpen(DialogName))
            {
                DialogParameters parame = new DialogParameters();
                parame.Add("value", Model);
                DialogHost.Close(DialogName, new DialogResult(ButtonResult.OK, parame));
            }

        }

        public string DialogName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if(parameters.ContainsKey("value"))
            {
                Model = parameters.GetValue<MemoDto>("value");
            }else
            {
                Model = new MemoDto();
            }
        }
    }
}
