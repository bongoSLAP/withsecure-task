namespace CoinSight.Core.Interfaces;

public interface IMarketDominanceHandler
{
    Task<decimal> GetMarketDominance(string coinId, int topN);
}