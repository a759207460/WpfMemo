using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Models
{
    public class TaskBar:BindableBase
    {
		private string  _icon;
        private string _title;
        private string _content;
        private string _color;
        private string _target;
        public string  Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}
		public string  Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public string  Content
		{
			get { return _content; }
			set { _content = value; }
		}
		public string Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public string Target
		{
			get { return _target; }
			set { _target = value; }
		}
	}
}
