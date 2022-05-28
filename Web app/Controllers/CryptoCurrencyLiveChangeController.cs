using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CryptoCurrencyLiveChangeController : ControllerBase
    {
        private static readonly string[] CryptoCurrencies = new[]
        {
            "Bitcoin","Ethereum","Tether","DogeCoin","Binance USD"
        };

        private static readonly double[] Price = new[]
        {
            28899.58,1766.73,1.00,302.55,1.01
        };

        private static readonly double[] MarketCap = new[]
        {
            550.57,13.77,36.74,2.058,1.059
        };

        private static readonly double[] ChangeIn1Hour = new[]
        {
            0.14,0.18,0.94,0.01,0.13
        };

        private readonly ILogger<CryptoCurrencyLiveChangeController> _logger;

        public CryptoCurrencyLiveChangeController(ILogger<CryptoCurrencyLiveChangeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "CryptoCurrencyLiveRate")]
        public IEnumerable<CryptoCurrencyLiveChage> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new CryptoCurrencyLiveChage
            {
                Coin = CryptoCurrencies[Random.Shared.Next(CryptoCurrencies.Length)],
                Price = Price[Random.Shared.Next(Price.Length)],
                MarketCap = MarketCap[Random.Shared.Next(MarketCap.Length)],
                ChangeIn1Hour = ChangeIn1Hour[Random.Shared.Next(ChangeIn1Hour.Length)],
                DateTime=DateTime.Now
            }).ToArray();
        }
    }
}
