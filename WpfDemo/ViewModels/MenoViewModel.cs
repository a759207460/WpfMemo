using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfDemo.Common;
using WpfDemo.DomainDto;
using WpfDemo.Extensions;
using WpfDemo.Service;
using WpfDemo.Service.MenmoService;

namespace WpfDemo.ViewModels
{
    public class MenoViewModel : NavigationViewModel
    {
        private readonly IMenmoService menmoService;
        private readonly IDialogHostService dialogHostService;

        public MenoViewModel(IMenmoService menmoService, IDialogHostService dialogHostService, IContainerProvider provider) : base(provider)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            ExcuteCommand = new DelegateCommand<string>(Excute);
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            this.menmoService = menmoService;
            this.dialogHostService = dialogHostService;
        }
        private ObservableCollection<MemoDto> memoDto;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDto; }
            private set { memoDto = value; RaisePropertyChanged(); }
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

        private MemoDto currenToDo;

        public MemoDto CurrenToDo
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

        private void Add()
        {
            IsRightDrawerOpen = true;
            this.CurrenToDo = new MemoDto();
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
                    ApiResponse resust = await menmoService.UpdateMemoDtoAsync(CurrenToDo);
                    if (resust != null && resust.Status)
                    {
                        var todo = MemoDtos.FirstOrDefault(t => t.Id == CurrenToDo.Id);
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
                    ApiResponse resust = await menmoService.AddMemoDtoAsync(CurrenToDo);
                    if (resust != null && resust.Status)
                    {
                        var menmo = JsonConvert.DeserializeObject<MemoDto>(resust.Resulst.ToString());
                        var response = await menmoService.GetMemoDtoByIdAsync(menmo.Id);
                        this.MemoDtos.Add(response);
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
                searchDto.Title =this.search;
                ApiResponse<List<MemoDto>> response = await menmoService.GetMemoDtosAsync(searchDto);
                if (response.Resulst != null && response.Status)
                {
                    this.MemoDtos.Clear();
                    foreach (var item in response.Resulst)
                    {
                        this.MemoDtos.Add(item);
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private async void Delete(MemoDto memoDto)
        {
            try
            {
                var dialogHost = await dialogHostService.Question("温馨提示！", $"请确认是否删除备忘录!{memoDto.Title}?");
                if (dialogHost == null || dialogHost.Result == Prism.Services.Dialogs.ButtonResult.No) return;

                UpdateLoading(true);
                var response = await menmoService.GetMemoDtoByIdAsync(memoDto.Id);
                if (response != null)
                {
                    ApiResponse resust = await menmoService.DeleteMemoDtoAsync(response.Id);
                    var todo = MemoDtos.FirstOrDefault(t => t.Id == response.Id);
                    if (todo != null)
                        this.MemoDtos.Remove(todo);
                }
            }
            catch (Exception ex)
            {

            }
            finally { UpdateLoading(false); }

        }
        private async void Selected(MemoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var todo = await menmoService.GetMemoDtoByIdAsync(obj.Id);
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
        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }

        private bool isRightDrawerOpen;
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        public DelegateCommand AddCommand { get; private set; }
        async void GetToDoList()
        {
            try
            {
                UpdateLoading(true);
                SearchDto searchDto = new SearchDto();
                ApiResponse<List<MemoDto>> response = await menmoService.GetMemoDtosAsync(searchDto);
                if (response.Resulst != null && response.Status)
                {
                    foreach (var item in response.Resulst)
                    {
                        this.MemoDtos.Add(item);
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
            GetToDoList();
        }
    }


    
}
