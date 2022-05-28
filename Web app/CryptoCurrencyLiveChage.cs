namespace Web_app
{
    public class CryptoCurrencyLiveChage
    {
        public string Coin { get; set; } = null!;
        public double Price { get; set; }
        public double MarketCap { get; set; }
        public double ChangeIn1Hour { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
    }
}
