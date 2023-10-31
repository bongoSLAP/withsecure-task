namespace CoinSight.Core.Models.Requests;

public class MarketDominanceRequest : RequestBase
{
    public int TopN { get; set; } = 0;
}