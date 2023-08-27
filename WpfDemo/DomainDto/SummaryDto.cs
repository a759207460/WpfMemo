using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.DomainDto
{
    public class SummaryDto:BindableBase
    {
		private int sum;

		public int Sum
        {
			get { return sum; }
			set { sum = value; RaisePropertyChanged(); }
		}

        private int compltedCount;

        public int CompltedCount
        {
            get { return compltedCount; }
            set { compltedCount = value; RaisePropertyChanged(); }
        }

        private int memoCount;

        public int MemoCount
        {
            get { return memoCount; }
            set { memoCount = value; RaisePropertyChanged(); }
        }

        private string compltedRatio;

        public string CompltedRatio
        {
            get { return compltedRatio; }
            set { compltedRatio = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoDto> todoList;

        public ObservableCollection<ToDoDto> TodoList
        {
            get { return todoList; }
            set { todoList = value;RaisePropertyChanged(); }
        }


        private ObservableCollection<MemoDto> memoList;

        public ObservableCollection<MemoDto> MemoList
        {
            get { return memoList; }
            set { memoList = value; RaisePropertyChanged(); }
        }

    }
}
