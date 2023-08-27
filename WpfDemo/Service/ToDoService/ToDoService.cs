using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;

namespace WpfDemo.Service.ToDoService
{
    public class ToDoService : IToDoService
    {
        private readonly HttpClientService httpClientService;

        public ToDoService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<ApiResponse> AddToDoAsync(ToDoDto toDoDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Post,
                Body= toDoDto,
                Route = "AddToDo"
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            return apiResponse;
        }

        public async Task<ApiResponse> DeleteToDoAsync(int id)
        {
            BaseRequest baseRequest = new BaseRequest() {
                ContentType = "application/json",
                Method = Method.Delete,
                Route = "DeleteToDo/" + id
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            return apiResponse;
        }

        public async Task<ToDoDto> GetToDoByIdAsync(int id)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Get,
                Route = "GetToDo/" + id
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            ToDoDto toDoDto = JsonConvert.DeserializeObject<ToDoDto>(apiResponse.Resulst.ToString());
            return toDoDto;
        }

        public async Task<ApiResponse<List<ToDoDto>>> GetToDoDtosAsync(SearchDto searchDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Route = "GetToDoList",
                Method =Method.Post,
                Body =searchDto
            };
            ApiResponse<List<ToDoDto>> apiResponse = await httpClientService.ExecuteHttpClient<List<ToDoDto>>(baseRequest);
            return apiResponse;
        }

        public async Task<ApiResponse<List<ToDoDto>>> GetToDoDtosPageAsync(int pageIndex, int pageSize, int indexFrom, SearchDto searchDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Route = $"GetToDoListPage?pageIndex={pageIndex}&&pageSize={pageSize}&&indexFrom={indexFrom}",//
                Method = Method.Post,
                Body =searchDto
            };
            ApiResponse<List<ToDoDto>> apiResponse = await httpClientService.ExecuteHttpClient<List<ToDoDto>>(baseRequest);
            return apiResponse;
        }


        public async Task<ApiResponse<SummaryDto>> GetSummary()
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Route = $"GetSummary",//
                Method = Method.Get
            };
            ApiResponse<SummaryDto> apiResponse = await httpClientService.ExecuteHttpClient<SummaryDto>(baseRequest);
            return apiResponse;
        }

        public async Task<ApiResponse> UpdateToDoAsync(ToDoDto toDoDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Post,
                Route = "UpdateToDo",
                Body=toDoDto
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            return apiResponse;
        }
    }
}
