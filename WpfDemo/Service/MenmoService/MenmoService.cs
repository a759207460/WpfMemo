using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;

namespace WpfDemo.Service.MenmoService
{
    public class MenmoService: IMenmoService
    {
        private readonly HttpClientService httpClientService;

        public MenmoService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<ApiResponse> AddMemoDtoAsync(MemoDto toDoDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Post,
                Body = toDoDto,
                Route = "AddMenmo"
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            return apiResponse;
        }

        public async Task<ApiResponse> DeleteMemoDtoAsync(int id)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Delete,
                Route = "DeleteMenmo/" + id
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            return apiResponse;
        }

        public async Task<MemoDto> GetMemoDtoByIdAsync(int id)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Get,
                Route = "GetMenmo/" + id
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            MemoDto memoDto = JsonConvert.DeserializeObject<MemoDto>(apiResponse.Resulst.ToString());
            return memoDto;
        }

        public async Task<ApiResponse<List<MemoDto>>> GetMemoDtosAsync(SearchDto searchDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Route = "GetMenmoList",
                Method = Method.Post,
                Body = searchDto
            };
            ApiResponse<List<MemoDto>> apiResponse = await httpClientService.ExecuteHttpClient<List<MemoDto>>(baseRequest);
            return apiResponse;
        }

        public async Task<ApiResponse<List<MemoDto>>> GetMemoDtosPageAsync(int pageIndex, int pageSize, int indexFrom, SearchDto searchDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Route = $"GetMenmoDtoListPageAsync?pageIndex={pageIndex}&&pageSize={pageSize}&&indexFrom={indexFrom}",//
                Method = Method.Post,
                Body = searchDto
            };
            ApiResponse<List<MemoDto>> apiResponse = await httpClientService.ExecuteHttpClient<List<MemoDto>>(baseRequest);
            return apiResponse;
        }


        public async Task<ApiResponse> UpdateMemoDtoAsync(MemoDto toDoDto)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = Method.Post,
                Route = "UpdateMenmo",
                Body = toDoDto
            };
            ApiResponse apiResponse = await httpClientService.ExecuteHttpClient(baseRequest);
            return apiResponse;
        }
    }
}
