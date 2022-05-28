using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyASP.NETCOREIdenity.DTO;

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
            WeatherForecasts = await httpclient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        }
    }
}
