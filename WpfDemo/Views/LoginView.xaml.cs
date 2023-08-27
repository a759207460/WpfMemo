﻿using Prism.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDemo.Extensions;

namespace WpfDemo.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
      
        public LoginView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.RegisterMessge(arg =>
            {
                this.LoginSnackbar.MessageQueue.Enqueue(arg.Message);
            }, "Login");
        }
    }
}
