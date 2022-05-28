using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyASP.NETCOREIdenity.DTO;

namespace UdemyASP.NETCOREIdenity.Pages
{
    public class CryptoCurrenciesModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<CryptoCurrencyLiveChangeDTO> CryptoCurrencyLiveChangeDTOs { get; set; } = null!;

        public CryptoCurrenciesModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var httpclient = httpClientFactory.CreateClient("WebAPI");
            CryptoCurrencyLiveChangeDTOs = await httpclient.GetFromJsonAsync<List<CryptoCurrencyLiveChangeDTO>>("CryptoCurrencyLiveChange");
        }
    }
}
