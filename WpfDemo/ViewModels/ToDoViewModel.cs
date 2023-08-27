using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfDemo.Common;
using WpfDemo.DomainDto;
using WpfDemo.Extensions;
using WpfDemo.Service;
using WpfDemo.Service.ToDoService;

namespace WpfDemo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {

        private readonly IToDoService toDoService;
        private readonly IDialogHostService dialogHostService;

        public ToDoViewModel(IToDoService toDoService, IContainerProvider provider, IDialogHostService dialogHostService) : base(provider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            ExcuteCommand = new DelegateCommand<string>(Excute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            this.toDoService = toDoService;
            this.dialogHostService = provider.Resolve<IDialogHostService>();
        }


        private void Excute(string type)
        {
            switch (type)
            {
                case "新增": Add(); break;
                case "查询": Seach(); break;
                case "保存": Save(); break;
            }
        }

        private ToDoDto currenToDo;

        public ToDoDto CurrenToDo
        {
            get { return currenToDo; }
            set { currenToDo = value; RaisePropertyChanged(); }
        }

        private string search;

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private int selectIndex;

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); }
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
            this.CurrenToDo = new ToDoDto();
        }

        private async void Save()
        {
            try
            {
                UpdateLoading(true);
                if (string.IsNullOrEmpty(CurrenToDo.Title) && string.IsNullOrEmpty(CurrenToDo.Content))
                    return;
                if (CurrenToDo.Id > 0)
                {
                    ApiResponse resust = await toDoService.UpdateToDoAsync(CurrenToDo);
                    if (resust != null && resust.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(t => t.Id == CurrenToDo.Id);
                        if (todo != null)
                        {
                            todo.Content = CurrenToDo.Content;
                            todo.Title = CurrenToDo.Title;
                            todo.Status = CurrenToDo.Status;
                        }
                    }
                }
                else
                {
                    CurrenToDo.Status = 1;
                    ApiResponse resust = await toDoService.AddToDoAsync(CurrenToDo);
                    if (resust != null && resust.Status)
                    {
                        var todo = JsonConvert.DeserializeObject<ToDoDto>(resust.Resulst.ToString());
                        var response = await toDoService.GetToDoByIdAsync(todo.Id);
                        this.ToDoDtos.Add(response);
                    }
                }
                IsRightDrawerOpen = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }

        }

        private async void Seach()
        {
            try
            {
                UpdateLoading(true);
                SearchDto searchDto = new SearchDto();
                searchDto.Title = string.IsNullOrEmpty(this.search) ? "" : this.search;
                searchDto.Status = this.SelectIndex;
                ApiResponse<List<ToDoDto>> response = await toDoService.GetToDoDtosAsync(searchDto);
                if (response.Resulst != null && response.Status)
                {
                    this.ToDoDtos.Clear();
                    foreach (var item in response.Resulst)
                    {
                        this.ToDoDtos.Add(item);
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private async void Delete(ToDoDto toDoDto)
        {
            try
            {
                var dialogHost = await dialogHostService.Question("温馨提示！", $"请确认是否删除待办事项!{toDoDto.Title}?");
                if (dialogHost == null || dialogHost.Result == Prism.Services.Dialogs.ButtonResult.No) return;
                UpdateLoading(true);
                var response = await toDoService.GetToDoByIdAsync(toDoDto.Id);
                if (response != null)
                {
                    ApiResponse resust = await toDoService.DeleteToDoAsync(response.Id);
                    var todo = ToDoDtos.FirstOrDefault(t => t.Id == response.Id);
                    if (todo != null)
                        this.ToDoDtos.Remove(todo);
                }
            }
            catch (Exception ex)
            {

            }
            finally { UpdateLoading(false); }

        }
        private async void Selected(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var todo = await toDoService.GetToDoByIdAsync(obj.Id);
                IsRightDrawerOpen = true;
                this.CurrenToDo = todo;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                UpdateLoading(false);
            }

        }

        public DelegateCommand<string> ExcuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }

        private bool isRightDrawerOpen;
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoDto> toDoDto;


        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDto; }
            private set { toDoDto = value; RaisePropertyChanged(); }
        }

        async void GetToDoList(int status)
        {
            try
            {
                UpdateLoading(true);
                SearchDto searchDto = new SearchDto();
                searchDto.Status = status != 0 ? status : 0;
                ApiResponse<List<ToDoDto>> response = await toDoService.GetToDoDtosAsync(searchDto);
                if (response.Resulst != null && response.Status)
                {
                    foreach (var item in response.Resulst)
                    {
                        this.ToDoDtos.Add(item);
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext != null && navigationContext.Parameters.ContainsKey("value"))
                selectIndex = navigationContext.Parameters.GetValue<int>("value");
            GetToDoList(selectIndex);
        }
    }
}
