namespace Inforce.UrlShortener.BLL.Validation
{
    public class MarketException : Exception
    {
        public MarketException()
        {
        }
        public MarketException(string message) : base(message)
        {
        }
        public MarketException(string message, Exception innerExeption) : base(message, innerExeption)
        {
        }
    }
}
