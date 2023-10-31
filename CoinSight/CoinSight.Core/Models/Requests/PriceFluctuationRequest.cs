namespace CoinSight.Core.Models.Requests;

public class PriceFluctuationRequest : RequestBase
{
    public int Days { get; set; } = 1;
}