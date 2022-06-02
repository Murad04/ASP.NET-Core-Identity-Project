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
            var httpclient = httpClientFactory.CreateClient("WebAPI");
            var res = await httpclient.PostAsJsonAsync("Auth", new Credentials { Name = "Murad", Password = "password" });
            res.EnsureSuccessStatusCode();
            string strJwt = await res.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strJwt);
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            WeatherForecasts = await httpclient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        }
    }
}
