namespace UdemyASP.NETCOREIdenity.DTO
{
    public class CryptoCurrencyLiveChangeDTO
    {
        public string Coin { get; set; } = null!;
        public double Price { get; set; }
        public double MarketCap { get; set; }
        public double ChangeIn1Hour { get; set; }
    }
}
