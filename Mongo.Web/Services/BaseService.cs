using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text;
using static Mango.Web.SD;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponsDto responsDto { get; set; }
        public HttpClient _httpClient { get; set; }
        //public IHttpClientFactory _httpClient { get; set; }
        public BaseService(HttpClient httpClient)
        {
            this.responsDto = new ResponsDto();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                //var client = _httpClient.CreateClient("MangoAPI");
                var message = new HttpRequestMessage(apiRequest.ApiType,apiRequest.Url);
                message.Headers.Add("Accsept", "application/json");
                _httpClient.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Clear();


                if (apiRequest.Data != null)
                {
                    message.Content = JsonContent.Create(apiRequest.Data);
                }
                
                var apiResponse = await _httpClient.SendAsync(message);
                //var apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(apiContent)!;
            }
            catch (Exception ex)
            {
                var dto = new ResponsDto()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>() { ex.Message.ToString() },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                return JsonConvert.DeserializeObject<T>(res)!;
            }

        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
