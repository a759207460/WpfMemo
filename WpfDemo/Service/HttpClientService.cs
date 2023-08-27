using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfDemo.Service
{
    public class HttpClientService
    {
        private readonly RestClient restClient;

        private readonly string baseUrl;
        public HttpClientService(string url)
        {
            restClient = new RestClient();
            baseUrl = url;
        }

        /// <summary>
        /// 非泛型接口请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ExecuteHttpClient(BaseRequest baseRequest)
        {
            ApiResponse resulst = new ApiResponse();
            try
            {
                string requstUrl = baseUrl + baseRequest.Route;
                var request = new RestRequest(requstUrl, baseRequest.Method);
                request.AddHeader("Content-Type", baseRequest.ContentType);
                if(baseRequest.Body!=null)
                {
                    request.AddStringBody(JsonConvert.SerializeObject(baseRequest.Body), DataFormat.Json);
                }
                var response = await restClient.ExecuteAsync(request);
                resulst.Message = response.ErrorMessage;
                resulst.Resulst =response.Content;
                resulst.Status = response.IsSuccessful;
            }
            catch (Exception ex)
            {
                resulst.Message = ex.Message;
                resulst.Resulst = "";
                resulst.Status = false;
            }
            return resulst;
        }

        /// <summary>
        /// 泛型接口请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> ExecuteHttpClient<T>(BaseRequest baseRequest)
        {
            ApiResponse<T> resulst = new ApiResponse<T>();
            try
            {
                string requstUrl = baseUrl + baseRequest.Route;
                var request = new RestRequest(requstUrl, baseRequest.Method);
                request.AddHeader("Content-Type", baseRequest.ContentType);
                if (baseRequest.Body != null)
                    request.AddStringBody(JsonConvert.SerializeObject(baseRequest.Body), DataFormat.Json);

                var response = await restClient.ExecuteAsync(request);
                resulst.Message = response.ErrorMessage;
                resulst.Resulst = JsonConvert.DeserializeObject<T>(response.Content);
                resulst.Status = response.IsSuccessful;
            }
            catch (Exception ex)
            {
                resulst.Message = ex.Message;
                resulst.Resulst = default;
                resulst.Status = false;
            }
            return resulst;
        }
    }
}
