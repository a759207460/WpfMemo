using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;
using WpfDemo.DomainDto;
using WpfDemo.Extensions;
using WpfDemo.Models;
using WpfDemo.Service;
using WpfDemo.Service.MenmoService;
using WpfDemo.Service.ToDoService;

namespace WpfDemo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogService;
        private readonly IEventAggregator eventAggregator;
        private readonly IToDoService toDoService;
        private readonly IMenmoService menmoService;
        private readonly IRegionManager regionManager;

        public IndexViewModel(IDialogHostService dialogService, IEventAggregator eventAggregator, IToDoService toDoService, IMenmoService menmoService, IRegionManager regionManager, IContainerProvider provider) : base(provider)
        {
            ExcuteCommand = new DelegateCommand<string>(Excute);
            EditTodoCommand = new DelegateCommand<ToDoDto>(AddTodo);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMeno);
            TodoCompltedCommand = new DelegateCommand<ToDoDto>(Complted);
            NavigatCommand = new DelegateCommand<TaskBar>(Navigat);
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;
            this.toDoService = toDoService;
            this.menmoService = menmoService;
            this.regionManager = regionManager;
        }

        private void Navigat(TaskBar obj)
        {
            try
            {
                UpdateLoading(true);
                if (obj == null || string.IsNullOrWhiteSpace(obj.Target)) return;
                NavigationParameters pairs = new NavigationParameters();
                if (obj.Target == "已完成")
                {
                    pairs.Add("value", 2);
                }
                regionManager.Regions[PrismManage.MainViewRegionName].RequestNavigate(obj.Target, pairs);
            }
            finally
            {
                UpdateLoading(false);
            }

        }

        private async void Complted(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var uresulst = await toDoService.UpdateToDoAsync(obj);
                if (uresulst.Status)
                {
                    var todo = Summary.TodoList.FirstOrDefault(t => t.Id == obj.Id);
                    Summary.TodoList.Remove(todo);
                    Summary.CompltedCount++;
                    Summary.CompltedRatio = (Summary.CompltedCount / (double)Summary.Sum).ToString("0%");
                }
                eventAggregator.SendMessage("已完成!");
            }
            finally
            {
                UpdateLoading(false);
            }

        }

        public DelegateCommand<ToDoDto> EditTodoCommand { get; private set; }

        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }

        public DelegateCommand<ToDoDto> TodoCompltedCommand { get; private set; }

        public DelegateCommand<TaskBar> NavigatCommand { get; private set; }
        private void Excute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddTodo(null); break;
                case "新增备忘录": AddMeno(null); break;
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        /// <summary>
        /// 添加待办
        /// </summary>
        public async void AddTodo(ToDoDto toDoDto)
        {
            DialogParameters parme = new DialogParameters();
            if (toDoDto != null)
                parme.Add("value", toDoDto);
            var dialogResulst = await dialogService.ShowDialog("AddToDoDialog", parme);
            if (dialogResulst.Result == ButtonResult.OK)
            {
                var todo = dialogResulst.Parameters.GetValue<ToDoDto>("value");
                if (todo != null && todo.Id > 0)
                {
                    var uresulst = await toDoService.UpdateToDoAsync(todo);
                    if (uresulst.Status)
                    {
                        var todomodel = Summary.TodoList.FirstOrDefault(t => t.Id == todo.Id);
                        if (todomodel != null)
                        {
                            todomodel.Title = todo.Title;
                            todomodel.Content = todo.Content;
                        }
                    }
                }
                else
                {
                    todo.Status = 1;
                    var resulst = await toDoService.AddToDoAsync(todo);
                    if (resulst.Status)
                    {
                        Summary.CompltedCount++;
                        Summary.TodoList.Add(todo);
                    }
                }
            }
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        public async void AddMeno(MemoDto memoDto)
        {
            DialogParameters parme = new DialogParameters();
            if (memoDto != null)
                parme.Add("value", memoDto);

            var dialogResulst = await dialogService.ShowDialog("AddMenmoDialog", parme);
            if (dialogResulst.Result == ButtonResult.OK)
            {
                var memo = dialogResulst.Parameters.GetValue<MemoDto>("value");
                if (memo != null && memo.Id > 0)
                {
                    var uresulst = await menmoService.UpdateMemoDtoAsync(memo);
                    if (uresulst.Status)
                    {
                        var memomodel = Summary.MemoList.FirstOrDefault(t => t.Id == memo.Id);
                        if (memomodel != null)
                        {
                            memomodel.Title = memo.Title;
                            memomodel.Content = memo.Content;
                        }
                    }
                }
                else
                {
                    var resulst = await menmoService.AddMemoDtoAsync(memo);
                    if (resulst.Status)
                    {
                        Summary.MemoList.Add(memo);
                    }
                }
            }
        }

        public DelegateCommand<string> ExcuteCommand { get; private set; }

        #region 属性

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            private set { taskBars = value; RaisePropertyChanged(); }
        }

        private SummaryDto summary;

        public SummaryDto Summary
        {
            get { return summary; }
            private set { summary = value; RaisePropertyChanged(); }
        }

        #endregion

        private void CreateTaskBars(SummaryDto summaryDto)
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar { Color = "#FF0CA0FF", Content = $"{summaryDto.Sum}", Icon = "ClockFast", Target = "ToDoView", Title = "汇总" });
            TaskBars.Add(new TaskBar { Color = "#FF1ECA3A", Content = $"{summaryDto.CompltedCount}", Icon = "ClockCheckOutline", Target = "ToDoView", Title = "已完成" });
            TaskBars.Add(new TaskBar { Color = "#FF02C6DC", Content = $"{summaryDto.CompltedRatio}", Icon = "ChartlineVariant", Target = "ToDoView", Title = "完成比率" });
            TaskBars.Add(new TaskBar { Color = "#FFFFA000", Content = $"{summaryDto.MemoCount}", Icon = "PlaylistStar", Target = "MenoView", Title = "备忘录" });
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                UpdateLoading(true);
                var sum = await toDoService.GetSummary();
                if (sum.Status)
                {
                    this.Summary = sum.Resulst;
                }
                CreateTaskBars(sum.Resulst);
                this.Title = $"你好,小一 今天是{DateTime.Now.GetDateTimeFormats('D')[1].ToString()}";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }

            base.OnNavigatedTo(navigationContext);

        }
    }
}
