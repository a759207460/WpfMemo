using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Models
{

    /// <summary>
    /// 导航菜单
    /// </summary>
    public class MenuBar : BindableBase
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 菜单图标
        /// </summary>
        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        /// <summary>
        /// 导航地址
        /// </summary>
        private string _navigation;

        public string Navigation
        {
            get { return _navigation; }
            set { _navigation = value; }
        }

    }
}
