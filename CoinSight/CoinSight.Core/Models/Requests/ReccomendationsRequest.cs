namespace CoinSight.Core.Models.Requests;

public class ReccomendationsRequest : RequestBase
{
    public int Days { get; set; } = 1;
    public int TopN { get; set; } = 2;
}