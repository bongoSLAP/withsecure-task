using System.Text.Json;
using CoinSight.Core.Interfaces;

namespace CoinSight.Handlers;

public class MarketDominanceHandler : IMarketDominanceHandler
{
    private readonly ICoinGeckoClient _client;
    
    public MarketDominanceHandler(ICoinGeckoClient client)
    {
        _client = client;
    }

    public async Task<decimal> GetMarketDominance(string coinId, int topN)
    {
        JsonDocument marketDataResponse = await _client.GetTopCoinsByMarketCap(topN);
        var marketData = marketDataResponse.RootElement.EnumerateArray().ToList();

        decimal coinMarketCap = 0;
        decimal totalMarketCap = 0;
        var coinFound = false;  

        foreach (var coin in marketData)
        {
            string? id = coin.GetProperty("id").GetString();
        
            if (id == null)
                throw new HttpRequestException();

            if (id == coinId)
            {
                coinMarketCap = coin.GetProperty("market_cap").GetDecimal();
                coinFound = true;
            }

            totalMarketCap += coin.GetProperty("market_cap").GetDecimal();
        }

        if (!coinFound)
            throw new ArgumentException($"Coin ID {coinId} not found in the top {topN} coins.");

        decimal dominance = (coinMarketCap / totalMarketCap) * 100;
        return dominance;
    }
}