using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UdemyASP.NETCOREIdenity.Authorization;
using UdemyASP.NETCOREIdenity.DTO;
using UdemyASP.NETCOREIdenity.Pages.Account;

namespace UdemyASP.NETCOREIdenity.Pages.HRManager
{
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> WeatherForecasts { get; set; } = null!;

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGetAsync()
        {
            WeatherForecasts = await InvokeEndpoint<List<WeatherForecastDTO>>("WebAPI", "WeatherForecast");
        }
        private async Task<JwtToken> Authenticate()
        {
            var httpclient = httpClientFactory.CreateClient("WebAPI");
            var res = await httpclient.PostAsJsonAsync("Auth", new Credentials { Name = "Murad", Password = "password" });
            res.EnsureSuccessStatusCode();
            string strJwt = await res.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("access_token",strJwt);
            return JsonConvert.DeserializeObject<JwtToken>(strJwt);
        }
        private async Task<T> InvokeEndpoint<T>(string clientName,string url)
        {
            //get token from session
            JwtToken? token = null;
            var strTokenObj = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrWhiteSpace(strTokenObj))
            {
                token = await Authenticate();
            }
            else
            {
                token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj);
            }
            if (token == null ||
                string.IsNullOrWhiteSpace(token.AccessToken) ||
                token.ExpiresAt <= DateTime.UtcNow)
            {
                token = await Authenticate();
            }
            var httpclient = httpClientFactory.CreateClient(clientName);
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
            return await httpclient.GetFromJsonAsync<T>(url);
        }
    }
}
