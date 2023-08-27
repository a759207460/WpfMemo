using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;
using WpfDemo.Service.UserServices;

namespace WpfDemo.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly IUserService userService;
        private readonly IEventAggregator eventAggregator;

        public LoginViewModel(IUserService userService,IEventAggregator eventAggregator)
        {
            ExecutedCommand = new DelegateCommand<string>(Execute);
            this.userService = userService;
            this.eventAggregator = eventAggregator;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "LoginOut": LoginOut(); break;
                case "Go": SelectedIndex = 1; break;
                case "Return": SelectedIndex = 0; break;
                case "Register": Register(); break;
            }
        }

        private void Register()
        {
            
        }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(PassWord))
                return;
            UserDto u = new UserDto { Account = Account, PassWord = PassWord };

            var user = userService.GetUser(u);
            if (user != null)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
        }

        private void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public string Title { get; set; } = "待办";


        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        private UserDto userDto;

        public UserDto UserDto
        {
            get { return userDto; }
            set { userDto = value; RaisePropertyChanged(); }
        }


        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }

        private string passWord;


        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExecutedCommand { get; private set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LoginOut();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
