using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text;
using static Mango.Web.SD;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Http.Headers;

namespace Mango.Web.Services
{
    public class WebApiCaller : IWebApiCaller
    {
        protected HttpClient _httpClient;
        public WebApiCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)  
        {
            try
            {
                var message = new HttpRequestMessage(apiRequest.ApiType,apiRequest.Url);
                message.Headers.Add("Accept", "application/json");

                if (apiRequest.Data != null)
                {
                    message.Content = JsonContent.Create(apiRequest.Data);
                }
                if (!String.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }
                var apiResponse = await _httpClient.SendAsync(message);
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

    }
}
