namespace CoinSight.Core.Models.Requests;

public class TrendsRequest : RequestBase
{
    public int Days { get; set; } = 1;
}